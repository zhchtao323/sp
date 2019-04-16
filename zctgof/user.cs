using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Text;
//using System.Management;
namespace ZCT.Data
{
    /// <summary>
    /// 用户类tst
    /// </summary>
    public class user
    {//
        private class Qx
        {
            public int xt = -1;
            public int qx=-1;
        }
        private static user instance;
        public static user Instance()
        {
            if (instance == null)
            {
                instance = new user();
            }
            return instance;
        }
        private bool yz=false;

        public bool Yz
        {
            get { return yz; }
            set { yz = value; }
        }
        private user()
        { }
        ArrayList qxs = new ArrayList();
        string name = "";

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string pwd = "";
        
        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }
        int dw = 0;
        /// <summary>
        /// 单位
        /// </summary>
        public int Dw
        {
            get { return dw; }
            set { dw = value; }
        }
        string  dwmc = "无";
        /// <summary>
        /// 单位
        /// </summary>
        public string  Dwmc
        {
            get { return dwmc; }
            set { dwmc = value; }
        }
        /// <summary>
        /// 取车间编码
        /// </summary>
        /// <returns></returns>
        public string GetDWbm()　　
        {
            //Char.
            int i = (int)'a';
            char c = (char)(i + dw - 1);
            return c.ToString();
        }

        /// <summary>
        /// 取系统权限
        /// </summary>
        /// <param name="xt"></param>
        /// <returns></returns>
        public int GetQx(int xt)  //取系统的权限
        {
            int jg = -1;
            if (xt == 1)  //xt1库存系统
            {
                if (dw == 9)
                {
                    jg = 0;  //全部
                }
                else
                {
                    if (dw != 0)
                    {
                        jg = 1;　　//仅车间
                    }
                }
            }
            return jg;
        }
        /// <summary>
        /// 取权限集合
        /// </summary>
        public void getQxs()
        {
            try
            {
                string str_con = ConnStr.Instance().Constr;//@"Data Source=169.85.170.18;Initial Catalog=LZK;User ID=lzk;pwd=lzk2009~";
                SqlConnection sqlc = new SqlConnection(str_con);
                string str_cm = "select global_qx.xtid,global_qx.qx from global_user,global_qx where global_user.userid=global_qx.userid   and global_use.name='" + name + "' and global_use.pwd='" + pwd + "'";
                SqlCommand sqlcomd = new SqlCommand(str_cm, sqlc);
                sqlc.Open();
                SqlDataReader dr = sqlcomd.ExecuteReader();
                while (dr.Read())
                {
                    Qx q1 = new Qx();
                    q1.xt = dr.GetInt16(0);
                    q1.qx = dr.GetInt16(1);
                    qxs.Add(q1);
   
                }
                 dr.Close();
                 sqlc.Close();
            }
            catch
            {     }        
        }
       
        /// <summary>
        /// 取当前用户、单位编码
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strMa"></param>
        /// <returns></returns>
        public bool GetUser(string strName, string strMa)
        {
            bool ret = false;
            try
            {

                //MsData m = new MsData(ConnStr.Instance().Constr);
                string str_con = ConnStr.Instance().Constr;// @"Data Source=169.85.170.18;Initial Catalog=gl;User ID=lzk;pwd=lzk2009~";// "pwd=sjfc_gks{;user id=gks;Initial Catalog=scgl;Data Source=169.85.170.18";
                SqlConnection sqlc = new SqlConnection(str_con);
                string str_cm = "select dwmc from global_user where name='" + strName + "' and pwd='" + strMa + "'";
                SqlCommand sqlcomd = new SqlCommand(str_cm, sqlc);
                sqlc.Open();
                object obj = sqlcomd.ExecuteScalar();
                if (obj != null)//存在用户
                {
                    this.dwmc = obj.ToString();
                    this.name = strName;
                    this.pwd = strMa;
                    yz = true;
                    ret = true;
                }
                else
                { yz = false; }
                sqlc.Close();
                return ret;
            }
            catch
            {
                return ret;
            }            
        }  
    }
}
