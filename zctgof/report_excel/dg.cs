using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ZCT.Data
{
    public class DgV
    {
        /// <summary>
        /// 设置Datagridview控件
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="dg1"></param>
        public static void SetDg(DataGridView dgv, List<reportRow> dg1,string conn)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            foreach (reportRow d1 in dg1)
            {
                switch (d1.Lx)
                {
                    case "com":
                        DataGridViewComboBoxColumn com = new DataGridViewComboBoxColumn();
                        if(d1.DataSql.Trim()!="")
                        {
                            MsData m=new MsData(conn);
                            List<string> strq= m.FillList(d1.DataSql);
                            strq.Add("");
                            com.DataSource = strq;
                        }
                        com.DataPropertyName = d1.DataName;
                        com.HeaderText = d1.HeadName;
                        dgv.Columns.Add(com);
                        break;
                    case "bool":
                        DataGridViewCheckBoxColumn ch = new DataGridViewCheckBoxColumn();
                        ch.DataPropertyName = d1.DataName;
                        ch.HeaderText = d1.HeadName;
                        dgv.Columns.Add(ch);
                        break;
                    default:
                        DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
                        c.DataPropertyName = d1.DataName;
                        if (d1.FormatString != "")
                        {
                            if (d1.FormatString == "Money")
                            {
                                DataGridViewCellStyle dc = new DataGridViewCellStyle();
                                dc.Format = "#.00";
                                c.DefaultCellStyle = dc;
                            }
                            else
                            {
                                DataGridViewCellStyle dc = new DataGridViewCellStyle();
                                dc.Format = d1.FormatString;
                                c.DefaultCellStyle = dc;
                            }
                        }
                        c.HeaderText = d1.HeadName;
                        dgv.Columns.Add(c);
                        break;
                
                }
            
            }
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        
        }
        /// <summary>
        /// 增加窗体控件、并且绑定数据
        /// </summary>
        /// <param name="dg1"></param>
        public void Adddli(System.Windows.Forms.Control.ControlCollection cont, List<reportRow> dg1)
        {
            //int x;
            //int y;
            foreach (reportRow d1 in dg1)
            {
                switch (d1.Lx)
                {
                    case "com":
                        ComboBox com = new ComboBox();                      
                  
                        
                        break;
                    case "ch":
                      

                        break;
                    default:
                      

                        break;

                }

            }
        
        }
        
    }
}
