using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZCT.Data
{
    /// <summary>
    /// ms  sql ���ݿ������
    /// </summary>
    public  class MsSqlData
    {
        private static string conn1 = "";
        /// <summary>
        /// �����ַ���
        /// </summary>
        public static string Conn
        {
            get { return conn1; }
            set { conn1 = value; }
        }
        #region  �������ݿ�����
        /// <summary>
        /// �������ݿ�����.�������Ӷ���
        /// </summary>
        /// <returns>����SqlConnection����</returns>
        public static SqlConnection GetConn()
        {
            string M_str_sqlcon = conn1;
            SqlConnection myCon = new SqlConnection(M_str_sqlcon);
            return myCon;
        }
        #endregion

        /// <summary>
        /// sql���õ�����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <returns></returns>
        //public List<T> FillCollection<T>(string cmd) where T : new()
        //{
        //    List<T> list = new List<T>();
        //    DataSet ds = this.Getds(cmd, "temp");
        //    list = TableList<T>.TableToList(ds.Tables["temp"], false);
        //    return list;
        //}
        /// <summary>
        /// �ɱ�õ�����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        //public List<T> FillCollection<T>(DataTable dt) where T : new()
        //{
        //    List<T> list = new List<T>();
        //    list = TableList<T>.TableToList(dt, false);
        //    return list;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <returns></returns>
        //public T FillT<T>(string cmd) where T : new()
        //{
        //    T list = new T();
        //    DataSet ds = this.Getds(cmd, "temp");
        //    if (ds.Tables["temp"].Rows.Count > 0)
        //    {
        //        list = TableList<T>.ConvertModel(ds.Tables["temp"].Rows[0]);
        //    }
        //    return list;
        //}
        public static T GetObject<T>(DataRow dr) where T : new()
        {
            T list = new T();
            list = TableList<T>.ConvertModel(dr);
            return list;
        }
        //public List<string> FillList(string cmd)
        //{
        //    List<string> r = new List<string>();
        //    IDataReader sq = this.GetDr(cmd);
        //    while (sq.Read())
        //    {
        //        r.Add(sq.GetValue(0).ToString());
        //    }
        //    sq.Close();
        //    return r;
        //}

        //public List<T> FillListOne<T>(string cmd)
        //{
        //    List<T> r = new List<T>();
        //    IDataReader sq = this.GetDr(cmd);
        //    while (sq.Read())
        //    {
        //        r.Add((T)sq.GetValue(0));
        //    }
        //    sq.Close();
        //    return r;
        //}
        #region �������ݱ�
        /// <summary>
        /// �������ݱ�
        /// </summary>
        /// <param name="dt">Dataset ���ݼ�</param>
        /// <param name="name">����</param>
        /// <param name="selsql">sql��ѯ���</param>
        public static void   UpdateDs(DataSet  ds,string name,string selsql)
        {
            SqlConnection sqconn = MsSqlData.GetConn(); 
            SqlDataAdapter sqld = new SqlDataAdapter(selsql, sqconn);
            SqlCommandBuilder sqlb_zb = new SqlCommandBuilder(sqld);
            sqld.MissingSchemaAction = MissingSchemaAction.AddWithKey;//����Լ��
            sqld.Update(ds, name);
            sqld.Dispose();
            sqconn.Close();
        }
        /// <summary>
        /// �������ݱ�
        /// </summary>
        /// <param name="dt">����</param>
        /// <param name="selsql">sql��ѯ���</param>
        public static void UpdateDs(DataTable  dt, string selsql)
        {
            SqlConnection sqconn = MsSqlData.GetConn();
            SqlDataAdapter sqld = new SqlDataAdapter(selsql, sqconn);
            SqlCommandBuilder sqlb_zb = new SqlCommandBuilder(sqld);
            sqld.MissingSchemaAction = MissingSchemaAction.AddWithKey;//����Լ��
            sqld.Update(dt);
            sqld.Dispose();
            sqconn.Close();
        }       
        #endregion

      

        #region  ִ��SqlCommand����
        /// <summary>
        /// ִ��SqlCommand���޷��ؽ����
        /// </summary>
        /// <param name="M_str_sqlstr">SQL���</param>
        public static void ExecuteNonQuery(string M_str_sqlstr)
        {
            SqlConnection sqlcon = MsSqlData.GetConn();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon);
            sqlcom.ExecuteNonQuery();
            sqlcom.Dispose();
            sqlcon.Close();
            sqlcon.Dispose();
        }
        /// <summary>
        /// ִ��SqlCommand���޷��ؽ����
        /// </summary>
        /// <param name="M_str_sqlstr">SQL���</param>
        /// <param name="pars">������</param>
        public static void ExecuteNonQuery(string M_str_sqlstr,SqlParameter[] pars)
        {
            SqlConnection sqlcon = MsSqlData.GetConn();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon);
            sqlcom.Parameters.AddRange(pars);
            sqlcom.ExecuteNonQuery();
            sqlcom.Dispose();
            sqlcon.Close();
            sqlcon.Dispose();
        }    
        /// <summary>
        /// ������ִ��SqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL���</param>
        public static void ExecuteNonQueryTr(string M_str_sqlstr)
        {
            SqlConnection sqlcon = MsSqlData.GetConn();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon);
            SqlTransaction tr = sqlcon.BeginTransaction();
            sqlcom.Transaction = tr;            
            try
            {
                sqlcom.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                throw ex;
            }            
            sqlcom.Dispose();
            sqlcon.Close();
            sqlcon.Dispose();
        }
        /// <summary>
        /// ������ִ��SqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL���</param>
        /// <param name="pars">������</param>
        public static void ExecuteNonQueryTr(string M_str_sqlstr,SqlParameter[] pars)
        {
            SqlConnection sqlcon = MsSqlData.GetConn();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon);
            sqlcom.Parameters.AddRange(pars);
            SqlTransaction tr = sqlcon.BeginTransaction();
            sqlcom.Transaction = tr;
            try
            {
                sqlcom.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                throw ex;
            }
            sqlcom.Dispose();
            sqlcon.Close();
            sqlcon.Dispose();
        }
        /// <summary>
        /// ִ��SqlCommand�����ض���
        /// </summary>
        /// <param name="M_str_sqlstr">SQL���</param>
        public static object ExecuteScalar(string M_str_sqlstr)
        {
            SqlConnection sqlcon = MsSqlData.GetConn();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon);
            object obj = sqlcom.ExecuteScalar();
            //if (obj.ToString() != "")
            //{ i = (Decimal)obj; }
            sqlcom.Dispose();
            sqlcon.Close();
            sqlcon.Dispose();
            return obj;
        }
        /// <summary>
        /// ִ��SqlCommand�����ض���
        /// </summary>
        /// <param name="M_str_sqlstr">SQL���</param>
        /// <param name="pars">������</param>
        public static object ExecuteScalar(string M_str_sqlstr,SqlParameter[] pars)
        {
            SqlConnection sqlcon = MsSqlData.GetConn();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon);
            sqlcom.Parameters.AddRange(pars);
            object obj = sqlcom.ExecuteScalar();
            //if (obj.ToString() != "")
            //{ i = (Decimal)obj; }
            sqlcom.Dispose();
            sqlcon.Close();
            sqlcon.Dispose();
            return obj;
        }      
        #endregion

        #region  ����DataSet����
        /// <summary>
        /// ����һ��DataSet����
        /// </summary>
        /// <param name="M_str_sqlstr">SQL���</param>
        /// <param name="M_str_table">����</param>
        /// <returns>����DataSet����</returns>
        public static DataSet Getds(string M_str_sqlstr, string M_str_table)
        {
            SqlConnection sqlcon = MsSqlData.GetConn();
            SqlDataAdapter sqlda = new SqlDataAdapter(M_str_sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlda.Fill(myds, M_str_table);
            return myds;
        }
        /// <summary>
        /// ����һ��DataSet����
        /// </summary>
        /// <param name="M_str_sqlstr">SQL���</param>
        /// <param name="pars">������</param>
        /// <param name="M_str_table">����</param>
        /// <returns>����DataSet����</returns>
        public static DataSet Getds(string M_str_sqlstr,SqlParameter[] pars, string M_str_table)
        {
            SqlConnection sqlcon = MsSqlData.GetConn();
            SqlDataAdapter sqlda = new SqlDataAdapter(M_str_sqlstr, sqlcon);            
            sqlda.SelectCommand.Parameters.AddRange(pars);
            DataSet myds = new DataSet();
            sqlda.Fill(myds, M_str_table);
            return myds;
        }
        #endregion

        #region  ����SqlDataReader����
        /// <summary>
        /// ����һ��SqlDataReader����
        /// </summary>
        /// <param name="M_str_sqlstr">SQL���</param>
        /// <returns>����SqlDataReader����</returns>
        public static SqlDataReader GetDr(string M_str_sqlstr)
        {
            SqlConnection sqlcon = MsSqlData.GetConn();
            SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon);
            sqlcon.Open();
            SqlDataReader sqlread = sqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return sqlread;
        }
        /// <summary>
        /// ����һ��SqlDataReader����
        /// </summary>
        /// <param name="M_str_sqlstr">SQL���</param>
        /// /// <param name="pars">������</param>
        /// <returns>����SqlDataReader����</returns>       
        public static SqlDataReader GetDr(string M_str_sqlstr,SqlParameter[] pars)
        {
            SqlConnection sqlcon = MsSqlData.GetConn();
            SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon);
            sqlcom.Parameters.AddRange(pars);
            sqlcon.Open();
            SqlDataReader sqlread = sqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return sqlread;
        }
        #endregion

        #region ȡLIST����
        /// <summary>
        /// ȡ�ַ����б�
        /// </summary>
        /// <param name="cmd">sql���</param>
        /// <returns>�����ַ����б�</returns>
        //public static List<string> FillList(string cmd)
        //{
        //    List<string> r = new List<string>();
        //    SqlDataReader sq = MsSqlData.GetDr(cmd);
        //    while (sq.Read())
        //    {
        //        r.Add(sq.GetValue(0).ToString());
        //    }
        //    return r;
        //}
        /// <summary>
        /// ��ȡ�����ݼ��ϣ���ת�����ݵ�һ���ֶ�
        /// </summary>
        /// <typeparam name="T">Ҫ���ص���������</typeparam>
        /// <param name="cmd">sql���</param>
        /// <returns>�����б�</returns>
        //public static List<T> FillListOne<T>(string cmd)
        //{
        //    List<T> r = new List<T>();
        //    SqlDataReader sq = MsSqlData.GetDr(cmd);
        //    while (sq.Read())
        //    {
        //        r.Add((T)sq.GetValue(0));
        //    }
        //    return r;
        //}
        /// <summary>
        /// δ��ɣ������ݿ���ֶ�ת��Ϊ���������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        //public static List<T> FillCollection<T>(string cmd)
        //{
        //    List<T> list = new List<T>();
        //    SqlDataReader dr = MsSqlData.GetDr(cmd);
        //    while (dr.Read())
        //    {
        //        //T obj = Helper.CreateObject<T>(dr);
        //        //list.Add(obj);
        //    }
        //    //       DataTable table = dr.GetSchemaTable();
        //     //           StringBuilder builder = new StringBuilder();

        //     //           foreach (DataColumn c in table.Columns)
        //     //           {
        //     //               builder.Append(c.ColumnName);
        //     //           }

        //     //           builder.Append(type.FullName);
        //            //return builder.ToString();
        //    dr.Close();
        //    return list;
        //} 
        #endregion 
    }
}
