using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Multiplay;
using System.Threading;
using System;

namespace Sever
public static class Server
{
    public static Dictionary<int, Room> Rooms;                  //游戏房间集合

    public static List<Player> Players;                         //玩家集合

    private static Socket _serverSocket;

    static void Start()
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint point = new IPEndPoint(IPAddress.Parse(ip), 8848);
        socket.Bind(point); //套接字绑定IP终端
        socket.Listen(0);
        Thread thread = new Thread(_Await) { IsBackground = true };
        thread.Start();

    }
    private static void _Await()
    {
        Socket client = null;

        while (true)
        {
            try
            {
                //同步等待,程序会阻塞在这里
                client = _serverSocket.Accept();

                //获取客户端唯一键
                string endPoint = client.RemoteEndPoint.ToString();

                //新增玩家
                Player player = new Player(client);
                Players.Add(player);

                Console.WriteLine($"{player.Socket.RemoteEndPoint}连接成功");

                //创建特定类型的方法
                ParameterizedThreadStart receiveMethod =
                   new ParameterizedThreadStart(_Receive);  //Receive方法在后面实现

                Thread listener = new Thread(receiveMethod) { IsBackground = true };

                listener.Start(player); //开启一个线程监听该客户端发送的消息
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
}
    
   
