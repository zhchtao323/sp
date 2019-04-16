using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Devices
{
    [Serializable]
    public class TranSend : MarshalByRefObject, ISerializable
    {
        public TranSend()
        { }

        //lisy<int, object> data = new Dictionary<int, object>();
        Dictionary<int, object> data = new Dictionary<int, object>();

        public Dictionary<int, object> GetData(int[] ids)
        {
            throw new System.NotImplementedException();

        }
        public Dictionary<int, object> GetData(string group)
        {
            data.Add(1, "asdad");
            data.Add(2, 121);
            data.Add(3, -12.456);
            data.Add(4, DateTime.Now);
            for(int i=5;i<=2000;i++)
            {
                data.Add(i, false);
            }
            
            
            //return throw new System.NotImplementedException();
            return data;
        }//要发送方的数据
        public Dictionary<int, object> GetDataChange(string group)
        {
            throw new System.NotImplementedException();
        }//要发送方的数据

        #region ISerializable 成员

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //info.AddValue("_", AdoNetHelper.SerializeDataSet(this));

            throw new NotImplementedException();
        }

        #endregion
    }

   
}
