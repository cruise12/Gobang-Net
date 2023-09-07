using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace Sever
{
    public class Player
    {
        public Socket Socket; //网络套接字

        public string Name;   //玩家名字

        public bool InRoom;   //是否在房间中

        public int RoomId;    //所处房间号码

        public Player(Socket socket)
        {
            Socket = socket;
            Name = "Player Unknown";
            InRoom = false;
            RoomId = 0;
        }

        public void EnterRoom(int roomId)
        {
            InRoom = true;
            RoomId = roomId;
        }

        public void ExitRoom()
        {
            InRoom = false;
        }
    }
}