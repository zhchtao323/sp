using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace ZCT.Data
{
 /// <summary>
    /// 报表生成类
    /// </summary>   
    public class Report
    {
        /// <summary>
        /// 直接输出报表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="name"></param>
        public static void ReportXs(DataTable dt, string name)
        {
            try
            {
                List<reportRow> rs = reportRow.GetRows(Report.getString("" + name + ".csv", System.Text.Encoding.Default));
                string str = Report.getString(name + ".xml", System.Text.Encoding.UTF8);
                str = Report.EcelDeal(str);
                string table = Report.ExportTable(dt, rs);
                str = Report.EcelAppend(str, table);
                Report.Writefile(str, @"c:\" + name + ".xml");

                Process myProcess = new Process();
                //    // string myDocumentsPath =Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                myProcess.StartInfo.FileName = @"c:\" + name + ".xml";// myDocumentsPath + "\\MyFile.doc";
                //myProcess.StartInfo.Verb = "Print";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }
        public static void Writefile(string str, string filename)
        {
            ExportExcel xls = new ExportExcel();
            StringBuilder strb = new StringBuilder();
            strb.Append(str);
            xls.WriteFile(strb, filename);
        }
        /// <summary>
        ///  读取文件返回字符串
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string getString(string filename, System.Text.Encoding cod)
        {
            System.IO.StreamReader SR = new System.IO.StreamReader(filename, cod);
            string S;
            string strFileText = string.Empty;
            //SR = System.IO.File.OpenText(filename);
            S = SR.ReadLine();
            strFileText = S + "\n"; ;
            while (S != null)
            {
                S = SR.ReadLine();

                strFileText += S + "\n";
            }
            SR.Close();
            return strFileText;
        }
        /// <summary>
        /// 处理Excel文件中对行列的限制
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EcelDeal(string str)
        {
            // <Table ss:ExpandedColumnCount="3" ss:ExpandedRowCount="6" x:FullColumns="1"
            int begin = str.IndexOf("ss:ExpandedColumnCount=");
            int end = str.IndexOf("x:FullColumns=");
            int l = end - begin - 1;
            //string str2 = str.Substring(begin, l); 
            //string str2=str.Substring(begin, end - 1);
            str = str.Remove(begin, l);
            //string str3 = str.Substring(begin, begin-end - 1); 
            //str.r

            string str_sty = " <Style ss:ID=\"s666\">\n";
            str_sty = str_sty + "<NumberFormat ss:Format=\"&quot;￥&quot;#,##0.00;&quot;￥&quot;\\-#,##0.00\"/>\n";
            str_sty = str_sty + "</Style>\n";
            int ss = str.IndexOf("</Styles>");
            str = str.Insert(ss, str_sty);
            return str;
        }
        /// <summary>
        /// excel文件后添加行
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EcelAppend(string strSource, string strApp)
        {
            //
            int locate = strSource.IndexOf("</Table>");
            strSource = strSource.Insert(locate - 1, "\n" + strApp + "\n");
            return strSource;
        }
        public static List<reportRow> GetRows(string filename)
        {
            string str = getString(filename, System.Text.Encoding.Default);
            List<reportRow> dg = reportRow.GetRows(str);
            return dg;
        }


        /// <summary>
        /// 输出表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rr"></param>
        /// <returns></returns>
        public static string ExportTable(DataTable dt, List<reportRow> rr)
        {
            StringBuilder str = new StringBuilder();
            ExportExcel xls = new ExportExcel();
            List<string> dg = new List<string>();
            DataColumnCollection cols = dt.Columns;
            foreach (DataRow dr in dt.Rows)
            {
                str.Append(xls.RowBegin);
                foreach (reportRow r in rr)
                {
                    switch (r.Lx)
                    { // str.Append(xls.AddCell(gy1.Price.ToString(), lx.Money));//金额
                        case "bool":
                            if ((r.FormatString == "") | (dr[r.DataName] == DBNull.Value))
                            {
                                str.Append(xls.AddCell(dr[r.DataName].ToString(), lx.lx_String));
                            }
                            else
                            {
                                string[] sp = r.FormatString.Split('|');
                                if (sp.Length >= 2)
                                {
                                    bool b = (bool)dr[r.DataName];
                                    if (b)
                                    { str.Append(xls.AddCell(sp[0], lx.lx_String)); }
                                    else
                                    { str.Append(xls.AddCell(sp[1], lx.lx_String)); }
                                }
                                else
                                {
                                    if (sp.Length == 1)
                                    {
                                        bool b2 = (bool)dr[r.DataName];
                                        if (b2)
                                        { str.Append(xls.AddCell(sp[0], lx.lx_String)); }
                                        else
                                        { str.Append(xls.AddCell("", lx.lx_String)); }
                                    }
                                    else
                                    { str.Append(xls.AddCell(dr[r.DataName].ToString(), lx.lx_String)); }
                                }
                            }
                            break;
                        case "txt":
                            str.Append(xls.AddCell(dr[r.DataName].ToString(), lx.lx_String));
                            break;
                        case "gs"://公式
                            str.Append(xls.AddCell(r.DataName, lx.lx_gs));
                            break;
                        case "num":
                            if (r.FormatString == "")
                            {
                                str.Append(xls.AddCell(dr[r.DataName].ToString(), lx.lx_Number));
                            }
                            else
                            {//Money
                                switch (r.FormatString.ToUpper())
                                {
                                    case "MONEY":
                                        decimal d11 = 0;
                                        decimal.TryParse(dr[r.DataName].ToString(), out d11);
                                        str.Append(xls.AddCell(d11.ToString(), lx.Money));
                                        break;
                                    case "TEXT":
                                        decimal d12 = 0;
                                        decimal.TryParse(dr[r.DataName].ToString(), out d12);
                                        str.Append(xls.AddCell(d12.ToString("#.00"), lx.lx_String));
                                        break;
                                    default:
                                        decimal d1 = 0;
                                        decimal.TryParse(dr[r.DataName].ToString(), out d1);
                                        str.Append(xls.AddCell(d1.ToString(r.FormatString), lx.lx_Number));
                                        break;
                                }
                            }
                            break;
                        case "rq":
                            DateTime dt21;
                            if (dr[r.DataName].ToString() != "")
                            {
                                DateTime.TryParse(dr[r.DataName].ToString(), out dt21);
                                str.Append(xls.AddCell(dt21.ToString(r.FormatString), lx.lx_String));
                            }
                            else
                            { str.Append(xls.AddCell(dr[r.DataName].ToString(), lx.lx_String)); }
                            break;
                    }
                }
                str.Append(xls.RowEnd);
            }
            return str.ToString();
        }
    }
}
