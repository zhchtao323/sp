using System;
namespace ZCT.Data
{
  public   interface IData
    {
        void BeginTr();
        void ComitTR();
        void ExecuteNonQuery(string M_str_sqlstr);
        void ExecuteNonQuery(string M_str_sqlstr, System.Data.IDbDataParameter[] pars);
        int ExecuteNonQueryI(string M_str_sqlstr);
        void ExecuteNonQuerys(System.Collections.Generic.List<string> sql);
        object ExecuteScalar(string M_str_sqlstr);
        object ExecuteScalar(string M_str_sqlstr, System.Data.IDbDataParameter[] pars);
        System.Collections.Generic.List<T> FillCollection<T>(string cmd) where T : new();
        System.Collections.Generic.List<T> FillCollection<T>(System.Data.DataTable dt) where T : new();
        System.Collections.Generic.List<string> FillList(string cmd);
        System.Collections.Generic.List<T> FillListOne<T>(string cmd);
        T FillT<T>(string cmd) where T : new();
        System.Data.IDbConnection GetConn();
        System.Data.IDataReader GetDr(string M_str_sqlstr);
        System.Data.IDataReader GetDr(string M_str_sqlstr, System.Data.IDbDataParameter[] pars);
        System.Data.DataSet Getds(string M_str_sqlstr, string M_str_table);
        System.Data.DataSet Getds(string M_str_sqlstr, System.Data.IDbDataParameter[] pars, string M_str_table);
        void RollTr();
        void SetConn(string file);
        void UpdateDs(System.Data.DataTable dt, string selsql);
        void UpdateList<T>(System.Collections.Generic.List<T> list, ref System.Data.DataTable dt, string tableName) where T : new();
    }
}
