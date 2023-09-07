using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public static class NetworkUtils 
{
    public static byte[] Serialize(object obj)
    {
        //������뱻���ΪSerializable
        if (obj == null || !obj.GetType().IsSerializable)
            return null;
        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream())
        {
            formatter.Serialize(stream, obj);
            byte[] data = stream.ToArray();
            return data;
        }
    }

    //�����л���byte[] -> obj
    public static T Deserialize<T>(byte[] data) where T : class
    {
        //T�����ǿ����л�������
        if (data == null || !typeof(T).IsSerializable)
            return null;
        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream(data))
        {
            object obj = formatter.Deserialize(stream);
            return obj as T;
        }
    }
}
