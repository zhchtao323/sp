using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
//using System.Data.SQLite;
using System.Data;
using System.Reflection;
using System.Timers;
using System.Data.OleDb;
using System.Xml;

namespace ZCT.Data.SqlBuilder
{
    public interface IDBFactory
    {
        IDbConnection CreateConnection(string connection_string);
        IDbDataAdapter CreateAdaptor(string query);
        string NetTypeToDBType(string netType);
        bool IsTableExist(string data);

    }
   

}
