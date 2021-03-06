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
    /// Wrappper of PropertyInfo object. Hides database attributes (dbAttribute) from developer
    /// </summary>
    public class dbPropertyInfo
    {
        private object m_Object;
        private PropertyInfo m_PropertyInfo;
        private dbAttribute m_dbAttribute;
        private IDBFactory m_dbFactory;
        public dbPropertyInfo(PropertyInfo p, object o, IDBFactory db_factory)
        {
            m_dbFactory = db_factory;
            m_Object = o;
            m_PropertyInfo = p;
            m_dbAttribute = null;
            parse();
        }
        private void parse()
        {
            object[] attrs = m_PropertyInfo.GetCustomAttributes(typeof(dbAttribute), false);
            if (attrs.Length > 0)
                m_dbAttribute = (dbAttribute)attrs[0];

        }
        public string defaultValue
        {
            get { return m_dbAttribute.defValue; }
        }
        public bool isKey
        {
            get { return m_dbAttribute.isKey; }
        }
        public bool isAutoInc
        {
            get { return m_dbAttribute.isAutoInc; }
        }
        public bool isDbProperty
        {
            get { return m_dbAttribute != null; }
        }
        public string fieldName
        {
            get { return m_PropertyInfo.Name; }
        }
        public string fieldType
        {
            get { return m_dbFactory.NetTypeToDBType(m_PropertyInfo.PropertyType.Name.ToLower()); }
        }
        public string propertyName
        {
            get { return m_PropertyInfo.Name; }
        }
        public string getValue()
        {
            switch (m_PropertyInfo.PropertyType.Name.ToLower())
            {
                case "string":
                    return string.Format("'{0}'", Convert.ToString(m_PropertyInfo.GetValue(m_Object, null)));
                case "double":
                    return Convert.ToString(Convert.ToDouble(m_PropertyInfo.GetValue(m_Object, null)));
                case "int16":
                    return Convert.ToString(Convert.ToInt16(m_PropertyInfo.GetValue(m_Object, null)));
                case "int32":
                    return Convert.ToString(Convert.ToInt32(m_PropertyInfo.GetValue(m_Object, null)));
                case "int64":
                    return Convert.ToString(Convert.ToInt64(m_PropertyInfo.GetValue(m_Object, null)));
                case "uint16":
                    return Convert.ToString(Convert.ToUInt16(m_PropertyInfo.GetValue(m_Object, null)));
                case "uint32":
                    return Convert.ToString(Convert.ToUInt32(m_PropertyInfo.GetValue(m_Object, null)));
                case "uint64":
                    return Convert.ToString(Convert.ToUInt64(m_PropertyInfo.GetValue(m_Object, null)));
                case "boolean":
                    return Convert.ToString(Convert.ToInt32(m_PropertyInfo.GetValue(m_Object, null)));
                case "datetime":
                    return string.Format("'{0}'", Convert.ToDateTime(m_PropertyInfo.GetValue(m_Object, null)).ToString("yyyy-MM-dd HH:mm:ss"));
                default:
                    return Convert.ToString(Convert.ToInt32(m_PropertyInfo.GetValue(m_Object, null)));
            }
        }
        /*  public string convertToSqliteType(string type)
          { 
              switch(type)
              {
                  case "string":
                      return "text";
                  case "double":
                  case "float":
                  case "single":
                      return "float";
                  case "int16":
                  case "int32":
                  case "int64":
                  case "uint16":
                  case "uint32":
                  case "uint64":
                  case "boolean":
                  case "byte":
                      return "integer";
                  case "datetime":
                      return "datetime";
                  default:
                      return "integer";
              }
          }
         * */
        public void setValue(object value)
        {
            switch (m_PropertyInfo.PropertyType.Name.ToLower())
            {
                case "string":
                    m_PropertyInfo.SetValue(m_Object, Convert.ToString(value), null);
                    break;
                case "byte":
                    m_PropertyInfo.SetValue(m_Object, Convert.ToByte(value), null);
                    break;
                case "int16":
                    m_PropertyInfo.SetValue(m_Object, Convert.ToInt16(value), null);
                    break;
                case "int32":
                    m_PropertyInfo.SetValue(m_Object, Convert.ToInt32(value), null);
                    break;
                case "int64":
                    m_PropertyInfo.SetValue(m_Object, Convert.ToInt64(value), null);
                    break;
                case "uint16":
                    m_PropertyInfo.SetValue(m_Object, Convert.ToUInt16(value), null);
                    break;
                case "uint32":
                    m_PropertyInfo.SetValue(m_Object, Convert.ToUInt32(value), null);
                    break;
                case "uint64":
                    m_PropertyInfo.SetValue(m_Object, Convert.ToUInt64(value), null);
                    break;
                case "double":
                    m_PropertyInfo.SetValue(m_Object, Convert.ToDouble(value), null);
                    break;
                case "float":
                    m_PropertyInfo.SetValue(m_Object, Convert.ToSingle(value), null);
                    break;
                case "boolean":
                    m_PropertyInfo.SetValue(m_Object, Convert.ToBoolean(value), null);
                    break;
                case "datetime":
                    m_PropertyInfo.SetValue(m_Object, Convert.ToDateTime(value), null);
                    break;
                default:
                    m_PropertyInfo.SetValue(m_Object, Convert.ToInt32(value), null);
                    break;

            }
        }
    }
   
}
