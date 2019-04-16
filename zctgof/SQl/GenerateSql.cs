using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZCT.Data
{
    /// <summary>
    /// 产生sql语句
    /// </summary>
    public class GenerateSql
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="tj"></param>
        /// <param name="sz"></param>
        /// <returns></returns>
        public static string Getsql(string field, string tj,bool yh)
        {
            string str = " ";
            if (yh)
            {
                if ((tj != "") & (tj != "全部"))
                {
                    str = str + "   and  "+field+" ='" + tj + "' ";
                }
            }
            else
            {
                if ((tj != "") & (tj != "全部"))
                {
                    str = str + "   and " + field +"=" +tj + " ";
                }
            }
            return str;
        }
        public static string Getsql(string field, string tj, string fh)
        {
            string str = " ";
          
                if ((tj != "") & (tj != "全部"))
                {
                    str = str + "   and " + field  +fh + tj + " ";
                }
         
            return str;
        }
    }
}
