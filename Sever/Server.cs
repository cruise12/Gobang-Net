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
    public static Dictionary<int, Room> Rooms;                  //��Ϸ���伯��

    public static List<Player> Players;                         //��Ҽ���

    private static Socket _serverSocket;

    static void Start()
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint point = new IPEndPoint(IPAddress.Parse(ip), 8848);
        socket.Bind(point); //�׽��ְ�IP�ն�
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
                //ͬ���ȴ�,���������������
                client = _serverSocket.Accept();

                //��ȡ�ͻ���Ψһ��
                string endPoint = client.RemoteEndPoint.ToString();

                //�������
                Player player = new Player(client);
                Players.Add(player);

                Console.WriteLine($"{player.Socket.RemoteEndPoint}���ӳɹ�");

                //�����ض����͵ķ���
                ParameterizedThreadStart receiveMethod =
                   new ParameterizedThreadStart(_Receive);  //Receive�����ں���ʵ��

                Thread listener = new Thread(receiveMethod) { IsBackground = true };

                listener.Start(player); //����һ���̼߳����ÿͻ��˷��͵���Ϣ
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
}
    
   
