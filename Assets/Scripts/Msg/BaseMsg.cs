using System.Collections;
using System.Collections.Generic;

public class BaseMsg : BaseData
{
    public override int GetBytesNum()
    {
        throw new System.NotImplementedException();
    }

    public override int Reading(byte[] bytes, int beginindex = 0)
    {
        throw new System.NotImplementedException();
    }

    public override byte[] Writing()
    {
        throw new System.NotImplementedException();
    }
    public virtual int GetId()
    {
        return 0;
    }
    public virtual int GetLength()
    {
        return 0;
    }
  
}
