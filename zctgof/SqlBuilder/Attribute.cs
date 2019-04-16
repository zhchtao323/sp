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
    /// <summary>
    /// Attribute to mark a property as a db field
    /// </summary>
    public class dbAttribute : Attribute
    {
        private bool m_isKey;
        private bool m_autoInc;
        private bool m_notNull;
        private string m_Default;
        public dbAttribute(bool isKey, bool auto_inc, bool not_null, string default_val)
        {
            m_isKey = isKey;
            m_autoInc = auto_inc;
            m_notNull = not_null;
            m_Default = default_val;

        }
        public bool isKey
        {
            get { return m_isKey; }
        }
        public bool isAutoInc
        {
            get { return m_autoInc; }
        }
        public bool isNotNull
        {
            get { return m_notNull; }
        }
        public string defValue
        {
            get { return m_Default; }
        }

    }
    /// <summary>
    /// attribute to mark class as a dbTable
    /// </summary>
    public class dbTable : Attribute
    {
        private string m_tableName;
        public dbTable(string name)
        {
            m_tableName = name;
        }
        public string tableName
        {
            get { return m_tableName; }
        }

    }
    /// <summary>
    /// Not implemented yet
    /// </summary>
    public class dbList : Attribute
    {
    }
    

}
