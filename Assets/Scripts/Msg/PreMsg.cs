using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreMsg : BaseMsg
{

    public override int GetId()
    {
        return 1003;
    }
    public override int GetBytesNum()
    {
        return 8;
    }
    public override int Reading(byte[] bytes, int index = 0)
    {
        return 0;
    }
    public override int GetLength()
    {
        return 0;
    }

    public override byte[] Writing()
    {
        int index = 0;
        byte[] bytes = new byte[GetBytesNum()];
        WriteInt(bytes, GetId(), ref index);
        WriteInt(bytes, GetLength(), ref index);
        return bytes;
    }
}
