//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Data;
//using System.Data.SQLite;
////using System.Data.SqlClient;

//namespace ZCT.Data
//{  
//    /// <summary>
//    /// ms  sql 数据库操作类
//    /// </summary>
//    public  class SqliteData :IDisposable, IData //IData,
//    {
//        ///// <summary>
//        ///// Database connection.
//        ///// </summary>
//        //protected IDbConnection cn;
//        ///// <summary>
//        ///// Current transaction.
//        ///// </summary>
//        //protected IDbTransaction trans;

//        //SQLiteConnection
//        private  SQLiteConnection sqlcon;
//        private SQLiteTransaction sqlt;
//        public SqliteData(string conn)
//        {
//            sqlcon = new SQLiteConnection(conn);
//            sqlcon.Open();
//        }
//        ~SqliteData()
//        {
//            try
//            {
//                sqlcon.Close();
                
//            }
//            catch
//            { }
//            finally
//            {
//                if (sqlcon != null)
//                {sqlcon.Dispose(); }
//            }
//        }
//        #region IDbData 成员
//        /// <summary>
//        /// 执行SQLiteCommand，无返回结果，
//        /// </summary>
//        /// <param name="M_str_sqlstr">SQL语句</param>
//        public void ExecuteNonQuery(string M_str_sqlstr)
//        {            
//            SQLiteCommand sqlcom = new SQLiteCommand(M_str_sqlstr, sqlcon);

//           sqlcom.ExecuteNonQuery();
//            //sqlcom.Dispose();
//        }
//        public int  ExecuteNonQueryI(string M_str_sqlstr)
//        {
//            SQLiteCommand sqlcom = new SQLiteCommand(M_str_sqlstr, sqlcon);

//            return sqlcom.ExecuteNonQuery();
//            //sqlcom.Dispose();
//        }

//        public void ExecuteNonQuerys(List<String> sql)//没有在接口中
//        {
//            SQLiteTransaction SQLiteTransaction = this.sqlcon.BeginTransaction();
//            foreach (String Sql1 in sql)
//            {
//                SQLiteCommand SQLiteCommand = new SQLiteCommand(Sql1, this.sqlcon, SQLiteTransaction);
//                SQLiteCommand.ExecuteNonQuery();
//            }
//            SQLiteTransaction.Commit();
//        }

//        public void ExecuteNonQuery(string M_str_sqlstr, System.Data.IDbDataParameter[] pars)
//        {
//            SQLiteCommand sqlcom = new SQLiteCommand(M_str_sqlstr, sqlcon);
//            sqlcom.Parameters.AddRange(pars);
//            sqlcom.ExecuteNonQuery();
//            //sqlcom.Dispose();
//        }       

//        public object ExecuteScalar(string M_str_sqlstr)
//        {
//            SQLiteCommand sqlcom = new SQLiteCommand(M_str_sqlstr, sqlcon);          
//            object obj = sqlcom.ExecuteScalar();
//            //if (obj.ToString() != "")
//            //{ i = (Decimal)obj; }
//            //sqlcom.Dispose();
//            return obj;
//        }

//        public object ExecuteScalar(string M_str_sqlstr, System.Data.IDbDataParameter[] pars)
//        {
//            SQLiteCommand sqlcom = new SQLiteCommand(M_str_sqlstr, sqlcon);
//            sqlcom.Parameters.AddRange(pars);
//            object obj = sqlcom.ExecuteScalar();
//            //if (obj.ToString() != "")
//            //{ i = (Decimal)obj; }
//            //sqlcom.Dispose();
//            return obj;
//        }
//        /// <summary>
//        /// 支持更改和插入操作，不支持删除操作。 行（如果已经存在一行的话）或者添加行（如果尚且不存在行的话）时，LoadDataRow   最有用。如果找到了行的键值，LoadDataRow   将更新该行的值，而不是插入新行。
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="list"></param>
//        /// <param name="dt"></param>
//        /// <param name="tableName"></param>
//        public void UpdateList<T>(List<T> list,ref DataTable  dt,string tableName) where T : new()
//        {
//            //DataTable dtnew= TableList<T>.ToDataTable<T>(list);
//            ////比较2个表 产生一个结果
//            //foreach (DataRow dr in dtnew.Rows)
//            //{
//            //    dr.
//            //    if (dt.Rows.Contains(dr))
//            //    { 
                
//            //    }
//            //}
//            TableList<T>.ToDataTable<T>(list,ref dt);
//            string str = "select *  from "+tableName;
//            this.UpdateDs(dt, str);
//        }
        
//        /// <summary>
//        /// sql语句得到集合
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="cmd"></param>
//        /// <returns></returns>
//        public List<T>  FillCollection<T>(string cmd) where T : new()
//        {
//            List<T> list = new List<T>();
//            DataSet ds = this.Getds(cmd, "temp");
//            list = TableList<T>.TableToList(ds.Tables["temp"], false);
//            return list;
//        }
//        /// <summary>
//        /// 由表得到集合
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public List<T> FillCollection<T>(DataTable  dt) where T : new()
//        {
//            List<T> list = new List<T>();            
//            list = TableList<T>.TableToList(dt, false);
//            return list;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="cmd"></param>
//        /// <returns></returns>
//        public T FillT<T>(string cmd) where T : new()
//        {
//            T list = new T();
//            DataSet ds = this.Getds(cmd, "temp");
//            if (ds.Tables["temp"].Rows.Count > 0)
//            {
//                list = TableList<T>.ConvertModel(ds.Tables["temp"].Rows[0]);
//            }
//            return list;
//        }
//        public static T GetObject<T>(DataRow dr) where T : new()
//        {
//            T  list= new T();
//            list = TableList<T>.ConvertModel(dr);
//            return list;
//        }
//        public List<string> FillList(string cmd)
//        {
//            List<string> r = new List<string>();
//            IDataReader sq = this.GetDr(cmd);
//            while (sq.Read())
//            {
//                r.Add(sq.GetValue(0).ToString());
//            }
//            sq.Close();
//            return r;
//        }

//        public List<T> FillListOne<T>(string cmd)
//        {
//            List<T> r = new List<T>();
//            IDataReader sq = this.GetDr(cmd);
//            while (sq.Read())
//            {
//                r.Add((T)sq.GetValue(0));
//            }
//            sq.Close();
//            return r;
//        }

//        public System.Data.IDbConnection GetConn()
//        {
//            return sqlcon;
//        }
//        public void SetConn(string file)
//        {
//             sqlcon=new SQLiteConnection(file);
//        }

//        public System.Data.IDataReader GetDr(string M_str_sqlstr, System.Data.IDbDataParameter[] pars)
//        {
//            SQLiteCommand sqlcom = new SQLiteCommand(M_str_sqlstr, sqlcon);
//            sqlcom.Parameters.AddRange(pars);
//            //sqlcon.Open();
//            IDataReader sqlread = sqlcom.ExecuteReader();//CommandBehavior.CloseConnection
//            return sqlread;
//        }

//        public System.Data.IDataReader GetDr(string M_str_sqlstr)
//        {
//            SQLiteCommand sqlcom = new SQLiteCommand(M_str_sqlstr, sqlcon);
//            //sqlcon.Open();
//            IDataReader sqlread =  sqlcom.ExecuteReader();
//            return sqlread;
//        }

//        public System.Data.DataSet Getds(string M_str_sqlstr, string M_str_table)
//        {            
//            SQLiteDataAdapter sqlda = new SQLiteDataAdapter(M_str_sqlstr, sqlcon);
//            DataSet myds = new DataSet();
//            sqlda.Fill(myds, M_str_table);
//            sqlda.Dispose();
//            return myds;
//        }

//        public System.Data.DataSet Getds(string M_str_sqlstr, System.Data.IDbDataParameter[] pars, string M_str_table)
//        {
//            SQLiteDataAdapter sqlda = new SQLiteDataAdapter(M_str_sqlstr, sqlcon);
//            sqlda.SelectCommand.Parameters.AddRange(pars);
//            DataSet myds = new DataSet();
//            sqlda.Fill(myds, M_str_table);
//            sqlda.Dispose();
//            return myds;
//        }

//        public void UpdateDs(System.Data.DataTable dt, string selsql)
//        {

//            SQLiteDataAdapter sqld = new SQLiteDataAdapter(selsql,sqlcon);
//            SQLiteCommandBuilder sqlb_zb = new SQLiteCommandBuilder(sqld);
//            sqld.MissingSchemaAction = MissingSchemaAction.AddWithKey;//主键约束
//           // 
//            sqld.UpdateCommand = sqlb_zb.GetUpdateCommand();
//            sqld.DeleteCommand = sqlb_zb.GetDeleteCommand();
//            sqld.Update(dt);
//            dt.AcceptChanges();
//            sqld.Dispose();
//        }
    
//        public void BeginTr()
//        {
//            sqlt=sqlcon.BeginTransaction();            
//        }

//        public void RollTr()
//        {
//            sqlt.Rollback();            
//        }

//        public void ComitTR()
//        {
//            sqlt.Commit();
//        }
//        #endregion

//        #region IDisposable 成员

//        void IDisposable.Dispose()
//        {
//            //sqlcon.Close();
//            //try
//            //{
//            //    sqlcon.Close();
//            //}
//            //catch
//            //{ }
//        }

//        #endregion
//    }
//}
