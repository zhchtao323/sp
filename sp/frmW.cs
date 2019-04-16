using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mjpeg
{
    public partial class frmW : Form
    {
        public frmW()
        {
            InitializeComponent();
        }

        string ygl = @"http://169.85.99.107";
        //string ygl = @"http://169.85.170.16";
        private void button1_Click(object sender, EventArgs e)
        {

            w1.Navigate(ygl);
            w1.Refresh();
        }
    }
}
