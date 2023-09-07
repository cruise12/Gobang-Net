using System;
using System.Collections.Generic;
using System.Text;


 class ResultMsg:BaseMsg
  {
    public bool ifWin;
    public int x;
    public int y;
    public int color;
    public override int GetId()
    {
        return 1010;
    }
    public override int GetLength()
    {
        return 13;
    }
    public override int GetBytesNum()
    {
        return 21;
    }
    public override byte[] Writing()
    {
        int index = 0;
        byte[] bytes = new byte[GetBytesNum()];
        WriteInt(bytes, GetId(), ref index);
        WriteInt(bytes, GetLength(), ref index);
        WriteBool(bytes, ifWin, ref index);
        WriteInt(bytes, x, ref index);
        WriteInt(bytes, y, ref index);
        return bytes;
    }
    public override int Reading(byte[] bytes, int beginIndex = 0)
    {
        int index = beginIndex;
        ifWin=  ReadBool(bytes, ref index);
        x=  ReadInt(bytes, ref index);
        y = ReadInt(bytes, ref index);
        color = ReadInt(bytes, ref index);
        return index-beginIndex;
    }

    }

