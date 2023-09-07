using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace Sever
{
    public class Player
    {
        public Socket Socket; //�����׽���

        public string Name;   //�������

        public bool InRoom;   //�Ƿ��ڷ�����

        public int RoomId;    //�����������

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