using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZCT.Data;
using System.Data.SqlClient;


namespace ZCT.Data
{
    /// <summary>
    /// 通用的数据编辑、导出窗体
    /// </summary>
    public partial class frmDl : Form
    {
        public frmDl()
        {
            InitializeComponent();
        }
        MsData m ;
        DataSet ds = new DataSet();
        string conn = "";
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Conn
        {
            get { return conn; }
            set { conn = value; }
        }
        string tableName = "";
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        List<reportRow> dgs = new List<reportRow>();
        /// <summary>
        /// 列表
        /// </summary>
        public List<reportRow> Dgs
        {
            get { return dgs; }
            set { dgs = value; }
        }
        private void dg_set()
        {
            dg.AutoGenerateColumns = false;
            DgV.SetDg(dg, dgs, conn);
            
        }
        private void GetDs(string tableName)
        {
            if (tableName != "")
            {
                string cmd = "select  *  from  " + tableName;
                ds = m.Getds(cmd, tableName);
                bd.DataSource = ds.Tables[tableName];
            }
        }
        private void frmDl_Load(object sender, EventArgs e)
        {
            m = new MsData(conn);
            dg_set();
            GetDs(tableName);
            dg.DataSource = bd;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
             {
                this.Validate();                
                bd.EndEdit();
                if (MessageBox.Show("请确认数据正确无误！保存吗？", "保存数据", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (ds.Tables[tableName].GetChanges() != null)
                    {
                        m.UpdateDs(ds.Tables[tableName].GetChanges(), "select *  from    "+tableName);
                        GetDs(tableName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存不成功！错误：" + ex.ToString());
            }
        }

        private void frmDl_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{ m.dis}
        }
    }
}
