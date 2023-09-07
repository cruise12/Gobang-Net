using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomMsg : BaseMsg
{
    public int id;
    public int RoomId;

   
    public override int GetId()
    {
        return 1009;
    }
    public override int GetLength()
    {
        return 8;
    }
    public override int GetBytesNum()
    {
        return 16;
    }
    public override byte[] Writing()
    {
        int index = 0;
        byte[] bytes = new byte[GetBytesNum()];
        WriteInt(bytes, GetId(), ref index);
        WriteInt(bytes, GetLength(), ref index);
        WriteInt(bytes, id, ref index);
        WriteInt(bytes, RoomId, ref index);
        return bytes;
    }
    public override int Reading(byte[] bytes, int beginIndex = 0)
    {
        int index = beginIndex;
        id=ReadInt(bytes, ref index);
        RoomId=ReadInt(bytes, ref index);
        return index-beginIndex;
    }
}
