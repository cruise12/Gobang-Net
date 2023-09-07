using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartMsg : BaseMsg
{
    public int color ;
    public override int Reading(byte[] bytes, int beginindex = 0)
    {
        int index = beginindex;
      color=ReadInt(bytes,ref index);
        return index - beginindex;
    }

    public override byte[] Writing()
    {
        int index = 0;
        byte[] bytes = new byte[GetBytesNum()];
        WriteInt(bytes, GetId(), ref index);
        WriteInt(bytes, GetLength(), ref index);
        WriteInt(bytes, 4, ref index);
        return bytes;
    }

    public override int GetBytesNum()
    {
        return 12;
    }

    public override int GetId()
    {
        return 1004;
    }
    public override int GetLength()
    {
        return 4;
    }
}
