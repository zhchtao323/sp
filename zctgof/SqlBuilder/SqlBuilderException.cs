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
    public class SqlBuilderException : ApplicationException
    {
        public SqlBuilderException(string message) : base(message) { }
    }

}
