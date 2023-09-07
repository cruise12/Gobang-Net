using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetMgr : MonoBehaviour
{
    public static NetMgr Instance;
    private Socket socket;
    private Queue<BaseMsg> sendMsgQueue=new Queue<BaseMsg>();
    private Queue<BaseMsg> receiveMsgQueue = new Queue<BaseMsg>();

    private byte[] bytes = new byte[1024 * 1024];
    private int receiveNum;
    private bool isConnected=false;

    private byte[] cacheBtyes = new byte[1024 * 1024];
    private int cacheNum=0;

    private int HEART_TIME = 2;
    private HeartMsg heartMsg = new HeartMsg();

    private long forntTime;
    private Player Player;
    //private BaseMsg baseMsg;
    private GameStartMsg GameStartMsg;
    private void Awake()
    {
        Instance = this;
       InvokeRepeating("SendHeartMsg", 0, HEART_TIME);
    }

   void  SendHeartMsg()
    {
        if (isConnected)
        {
            HeartMsg chessMsg = new HeartMsg();
            byte[] bytes= chessMsg.Writing();
            socket.Send(bytes);
        }
           
    
      }
    public void Connect(string ip,int port)
    {
        if (!isConnected)
        {
            if (socket == null)
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                try
                {
                    socket.Connect(iPEndPoint);
                    isConnected = true;
                    ThreadPool.QueueUserWorkItem(sendMsg);
                    ThreadPool.QueueUserWorkItem(receiveMsg);

                }
                catch (SocketException e)
                {
                    print("连接失败");
                }
            }


          
        }
    }
    public void Send(BaseMsg msg)
    {
        if(socket!=null)
        {
            sendMsgQueue.Enqueue(msg);

        }
    }
    public void sendMsg(object obj)//开启线程循环查看是否需要发送
    {
        while(isConnected)
        {
            if(sendMsgQueue.Count>0)
            {
                socket.Send(sendMsgQueue.Dequeue().Writing());
            }

        }
    }

    public void receiveMsg(object obj)
    {
       while(isConnected)
        {
            if(socket.Available>0)
            {
                receiveNum=socket.Receive(bytes);
                
                HandleReceiveMsg(bytes, receiveNum);
            }

        }

    }

    private void HandleReceiveMsg(byte[] bytes, int receiveNum)
    {
        int nowIndex = 0;
        int msgNum = 0;
        int msgLength = 0;

        bytes.CopyTo(cacheBtyes, cacheNum);//单独解决了分包问题
        cacheNum += receiveNum;
        while (true)//单独解决了粘包问题
        {
            msgLength = -1;
            if (cacheNum - nowIndex >= 8)
            {
                msgNum = BitConverter.ToInt32(cacheBtyes, nowIndex);//读出id
                nowIndex += 4;
                msgLength = BitConverter.ToInt32(cacheBtyes, nowIndex);
                nowIndex += 4;
            }
            if (cacheNum - nowIndex >= msgLength && msgLength != -1)
            {

                BaseMsg baseMsg = null;
                switch (msgNum)
                {
                    case 1001:
                       // baseMsg = new ChessMsg();
                        baseMsg.Reading(cacheBtyes, nowIndex);
                                         
                        break;
                    case 999:
                        baseMsg = new HeartMsg();
                        break;
                    case 1009://进入房间
                        baseMsg = new EnterRoomMsg();
                        baseMsg.Reading(cacheBtyes, nowIndex);
                        break;
                    case 1004:
                        baseMsg = new GameStartMsg();
                        baseMsg.Reading(cacheBtyes, nowIndex);
                        break;
                    case 1010:
                        baseMsg = new ResultMsg();
                        break;

                  

                }
                if(baseMsg!=null)
                {
                    receiveMsgQueue.Enqueue(baseMsg);
                }
                nowIndex += msgLength;
                if (nowIndex == cacheNum)
                {
                    cacheNum = 0;
                    break;
                }
            }
            else
            {
                if (msgLength != -1)
                {
                    nowIndex -= 8;
                    Array.Copy(bytes, nowIndex, cacheBtyes, 0, (cacheNum - nowIndex));
                    cacheNum -= nowIndex;
                }
                break;
            }
        }
    }

    
    public void close()
    {
        if(socket!=null)
        {
            isConnected = false;
            socket.Shutdown(SocketShutdown.Both);
            socket.Disconnect(false);
            socket.Close();
            print("客户端结束连接");
        }

    }
    void Update()
    {
        if(receiveMsgQueue.Count>0)
        {
            BaseMsg baseMsg = receiveMsgQueue.Dequeue();
            if (baseMsg is EnterRoomMsg)
            {
                EnterRoomMsg enterRoomMsg = baseMsg as EnterRoomMsg;
                print(enterRoomMsg.id);
            }
            if (baseMsg is GameStartMsg)
            {
                GameStartMsg = baseMsg as GameStartMsg;
                GameStart(GameStartMsg);
               
            }
            if(baseMsg is ResultMsg)
            {
                ResultMsg result = baseMsg as ResultMsg;
                Console.WriteLine("收到棋子信息:{0}", result.x, result.y);
                if(result.ifWin==true)
                {
                    print("游戏结束");
                }
                else
                {
                    GameManager.Instance.CreateChess(new Vector2(result.x, result.y),(EPlayerTurn)result.color);
                }
            }
        }
    }

    private void GameStart(GameStartMsg msg)
    {
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single).completed += OnSceneLoaded;
        SceneManager.UnloadSceneAsync("Start");
    }
    private void OnSceneLoaded(AsyncOperation asyncOperation)
    {
        // 场景加载完成后，在这里查找物体
        GameObject gameObject = GameObject.Find("Player");
        if (gameObject != null)
        {
            Player player = gameObject.GetComponent<Player>();
            Player = player;
            GameManager.Instance.cureentTurn = EPlayerTurn.Black;
            if (GameStartMsg.color == 1)
                Player.MyTurn = EPlayerTurn.Black;
            else
                Player.MyTurn = EPlayerTurn.White;
        }
        else
        {
            print("没找到物体");
        }
    }


    private void OnDestroy()
    {
        close();
    }
}

