using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


public abstract class BaseData
{
    public abstract int GetBytesNum();
    public abstract Byte[] Writing();

    public abstract int Reading(byte[] bytes, int index=0 );
    public void WriteInt(byte[] bytes,int value,ref int index)
    {
        BitConverter.GetBytes(value).CopyTo(bytes, index);
        index += sizeof(int);
    }
    public void WriteFloat(byte[] bytes, float value,ref int index)
    {
        BitConverter.GetBytes(value).CopyTo(bytes, index);
        index += sizeof(float);
    }
    public void WriteBool(byte[] bytes, bool value,ref int index)
    {
        BitConverter.GetBytes(value).CopyTo(bytes, index);
        index += sizeof(bool);
    }
    public void WriteString(byte[] bytes, string value,ref int index)
    {
        byte[] strBytes=  Encoding.UTF8.GetBytes(value);
        WriteInt(bytes, strBytes.Length,ref index);
       
        strBytes.CopyTo(bytes, index);
        index += strBytes.Length;
    }
    public void WriteData(byte[] bytes, BaseData value, ref int index)
    {
        value.Writing().CopyTo(bytes, index);
        index += value.GetBytesNum();
    }

    protected int ReadInt(byte[] bytes,ref int index)
    {
        int value = BitConverter.ToInt32(bytes, index);
        index += sizeof(int);
        return value;
    }
    protected float ReadFloat(byte[] bytes,ref int index)
    {
        float value = BitConverter.ToSingle(bytes, index);
        index += sizeof(float);
        return value;
    }
    protected bool ReadBool(byte[] bytes,ref int index)
    {
        bool value = BitConverter.ToBoolean(bytes, index);
        index += sizeof(bool);
        return value;
    }
    protected string ReadString(byte[] bytes,ref int index)
    {
        int length=ReadInt(bytes,ref index);     
        string value = Encoding.UTF8.GetString(bytes, index, length);
        index += length;
        return value;
    }
    protected T ReadData<T>(byte[] bytes,ref int index) where T:BaseData,new()
    {
        T t = new T();
        int length= t.Reading(bytes, index);
        index += length;
        return  t;
    }
}
