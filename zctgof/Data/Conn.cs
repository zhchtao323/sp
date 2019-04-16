﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
namespace ZCT.Data
{
    /// <summary>
    /// 保存数据库连接字符串
    /// </summary>
    public  class ConnStr
    {
        #region
        private string constr = "";
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string Constr
        {
            get { return constr; }
            set { constr = value; }
        }
        private string lx = "sql";
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string Lx
        {
            get { return lx; }
            set { lx = value; }
        }

        private static ConnStr instance;
        /// <summary>
        /// 单件实例化
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static ConnStr InstanceFrmConfig()
        {
            if (instance == null)
            {
                string conn = "";
                string str_lx = "";
                try
                {
                    conn = ConfigurationSettings.AppSettings["local_conn"].Trim();
                    str_lx =ConfigurationSettings.AppSettings["local_lx"].Trim();
                    if ((conn != "") & (str_lx != ""))
                    {
                        instance = new ConnStr(conn);
                        instance.lx = str_lx;
                    }
                }
                catch
                {}
            }
            return instance;
        }
   
        public static ConnStr Instance(string str)
        {
            if (instance == null)
            {
                instance = new ConnStr(str);
            }
            return instance;
        }
        public static ConnStr Instance()
        {
            if (instance == null)
            {
                instance = new ConnStr("");
            }
            return instance;
        }
        /// <summary>
        /// 虚构函数
        /// </summary>
        /// <param name="conn"></param>
        private  ConnStr(string conn)
        {
            constr = conn;
        }
        /// <summary>
        /// 取数据执行对象
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="lx"></param>
        /// <returns></returns>
        public  IData GetIData()
        {
            return GetIData(constr, lx);
        }
        #endregion

        public static IData GetIData(string constr, string lx)
        {
            switch (lx)
            {
                case "sql":
                   return  new MsData(constr);
                   //break;
                //case "sqllite":
                //   return new SqliteData(constr);
                   //break;
                case "ole":
                   return new OLeDbData(constr);
                    //break;
                //case "mysql":
                //   return  new MySqlData(constr);
                //break;
                default:
                   return null;
                    //break;
            }
        }        
    }
}
