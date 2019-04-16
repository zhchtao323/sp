using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Collections.Generic;
namespace sp
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
            string cmd = "select    GROUP_name  from sp_group order by  xh";
            List<string> cc = m.FillList(cmd);
            cc.Add("全部");
            cmb.DataSource = cc;
        }
        
        ZCT.Data.MsData m = new ZCT.Data.MsData( @"Data Source=169.85.170.18;Initial Catalog=gk;User ID=sa;pwd=6985mes{");
        DataSet ds = new DataSet();
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPwd.Text == "3972253")
            {
                try
                {
                    this.Invalidate();
                    //ds.AcceptChanges();
                    m.UpdateDs(ds.Tables["sp"], "select  *  from   sp");
                }
                catch (Exception ex1)
                { MessageBox.Show(ex1.Message.ToString()); }
            }
            else
             { MessageBox.Show("密码不对！"); } 
        }
        private void ll()
        { 
          try
            {
                string cmd = "";
                if (cmb.Text == "全部")
                {  cmd = "select *  from   sp   "; }
                else
                {
                   cmd = "select *  from   sp   where   group_name='" + cmb.Text.Trim() + "' order by  xh";
                }
            ds = m.Getds(cmd, "sp");
            dataGridView1.DataSource = ds.Tables["sp"];
             }
            catch (Exception ex1)
            { MessageBox.Show(ex1.Message.ToString()); }
        
        }
              

        private void btnLoad_Click(object sender, EventArgs e)
        {
            ll();
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPwd.Text == "sjfc3972253")
            {
                try
                {
                    //this.Invalidate();
                    //ds.AcceptChanges();
                    //m.UpdateDs(ds.Tables["sp"], "select  *  from   sp");
                    string cmd = "delete   sp   where   group_name='" + cmb.Text.Trim() + "'";
                    m.ExecuteNonQuery(cmd);
                }
                catch (Exception ex1)
                { MessageBox.Show(ex1.Message.ToString()); }
            }
            else
            { MessageBox.Show("密码不对！"); } 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtPwd.Text == "sjfc3972253")
            {
                try
                {
                    //this.Invalidate();
                    //ds.AcceptChanges();
                    //m.UpdateDs(ds.Tables["sp"], "select  *  from   sp");
                    string cmd = "update   sp  set group_name='"+txtmc.Text.Trim()+"'   where   group_name='" + cmb.Text.Trim() + "'";
                    m.ExecuteNonQuery(cmd);
                    cmd = "update   sp_group  set group_name='" + txtmc.Text.Trim() + "'   where   group_name='" + cmb.Text.Trim() + "'";
                    m.ExecuteNonQuery(cmd);
                }
                catch (Exception ex1)
                { MessageBox.Show(ex1.Message.ToString()); }
            }
            else
            { MessageBox.Show("密码不对！"); } 
        }
    }
}
