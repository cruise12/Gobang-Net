using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class PlayChessMsg : BaseMsg
{
    public int x;
    public int y;
    public int color;
    public override int Reading(byte[] bytes, int beginIndex = 0)
    {
        int index = beginIndex;
        x = ReadInt(bytes, ref index);
        y = ReadInt(bytes, ref index);
        color = ReadInt(bytes, ref index);
        return index - beginIndex;
    }

    public override byte[] Writing()
    {
        int index = 0;
        byte[] bytes = new byte[GetBytesNum()];
        WriteInt(bytes, GetId(), ref index);
        WriteInt(bytes, GetLength(), ref index);
        WriteInt(bytes, x, ref index);
        WriteInt(bytes, y, ref index);
        WriteInt(bytes, color, ref index);
        return bytes;
    }

    public override int GetBytesNum()
    {
        return 20;
    }

    public override int GetId()
    {
        return 1005;
    }
    public override int GetLength()
    {
        return 12;
    }

}
