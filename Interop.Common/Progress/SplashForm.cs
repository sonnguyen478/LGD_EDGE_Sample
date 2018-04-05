using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Interop.Common.Progress
{
    partial class SplashForm : Form
    {
        private int dot;    

        public SplashForm()
        {
            InitializeComponent();
            UpdateProgress(0,"");
            dot = 0;
        }
 
        public void UpdateProgress(int value, string _text)
        {
            try
            {
                label2.Text = _text;
              //  label1.Text = String.Format("Progress: {0}%", value);
                label1.Text = String.Format(" {0}% ", value);
                progressBar1.Value = value;
            }
            catch { }
        }
 
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string title = "loading....";
                Text = title.Substring(0, title.Length - 4 + (dot++ % 5));
            }
            catch { }
        }
    }
}
