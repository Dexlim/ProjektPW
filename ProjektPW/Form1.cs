using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektPW
{
    public partial class Form1 : Form
    {
        private bool timer1_status = false;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1_status)
                radioButton1.Checked = true;
            else
                radioButton1.Checked = false;
            timer1_status = !timer1_status;
           
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = Convert.ToString(trackBar1.Value)+"[ms]";
            timer1.Interval = trackBar1.Value;
        }
    }
}
