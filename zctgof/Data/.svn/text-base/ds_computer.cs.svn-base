using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZCT.Data
{
    /// <summary>
    /// 保存数据库连接字符串
    /// </summary>
    public  class Ds_Compter
    {
        public static DataRow Ds_sum(  DataTable   dt)
        {
            DataRow dr = dt.NewRow();
             DataColumnCollection cols = dt.Columns;
            //cols[0].ColumnName = "";
            //cols[0].DataType == "";
            foreach (DataRow dr1 in dt.Rows)
            {                 
                foreach (DataColumn col in cols)
                {
                    if (col.DataType == Type.GetType("System.Decimal"))
                    {
                        System.Decimal dec = 0;
                        System.Decimal dec1 = 0;
                        Decimal.TryParse(dr[col.ColumnName].ToString(),out  dec);
                        Decimal.TryParse(dr1[col.ColumnName].ToString(), out  dec1);
                        dr[col.ColumnName] = dec + dec1;
                    }
                    if (col.DataType == Type.GetType("System.Int32"))
                    {
                        int i1 = 0;
                        int i2 = 0;
                        int.TryParse(dr[col.ColumnName].ToString(), out  i1);
                        int.TryParse(dr1[col.ColumnName].ToString(), out  i2);
                        dr[col.ColumnName] = i1+i2;
                    }
                   // dr
                }            
            }
            return dr;        
        }
    }
}
