using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ZCT.Data
{
    /// <summary>
    /// 行配置
    /// </summary>
    public class reportRow
    {
        /// <summary>
        /// 根据字符串生成行列表
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<reportRow> GetRows(string str)
        {            
            List<reportRow> dg = new List<reportRow>();
            try
            {
                string[] y1 = str.Split('\n');
                foreach (string y2 in y1)
                {
                    string[] t = y2.Split(',');
                    if (t.Length >= 3)
                    {
                        reportRow dg1 = new reportRow();
                        dg1.DataName = t[0];
                        dg1.HeadName = t[1];
                        dg1.Lx = t[2];
                        if (t.Length >= 4)
                        {
                            dg1.FormatString = t[3];
                        }
                        dg.Add(dg1);
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
            return dg;
        }

        private string dataName;
        /// <summary>
        /// 数据字段的名字
        /// </summary>
        public string DataName
        {
            get { return dataName; }
            set { dataName = value; }
        }
        private string headName;
        /// <summary>
        /// 显示的名字
        /// </summary>
        public string HeadName
        {
            get { return headName; }
            set { headName = value; }
        }
        private string lx;
        /// <summary>
        /// 类型
        /// </summary>
        public string Lx
        {
            get { return lx; }
            set { lx = value; }
        }
        private string formatString;
        /// <summary>
        /// 显示格式字符串
        /// </summary>
        public string FormatString
        {
            get { return formatString; }
            set { formatString = value; }
        }
        private string dataSql;
        /// <summary>
        /// 字段的数据源，sql语句
        /// </summary>
        public string DataSql
        {
            get { return dataSql; }
            set { dataSql = value; }
        }
    }
}
