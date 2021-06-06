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
    public partial class setButton : Form
    {
        private bool timer1_status = false;
        private bool pause_status = false;
        private int clock_minutes = 0;
        private int clock_seconds = 0;

        public setButton()
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
        private void trackBar1_MouseUp(object sender, EventArgs e)
        {
           logBox.Text+= clock1_Time() + "Changed timer delay to: " +label1.Text+"\n";
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (pause_status == false)
            {
                pauseButton.Text = "Resume";
                logBox.Text += clock1_Time() + "Simulation paused.\n";
                timer1.Enabled = false;
            }
            else
            {
                pauseButton.Text = "Pause";
                logBox.Text += clock1_Time() + "Simulation resumed.\n";
                timer1.Enabled = true;
            }
            pause_status = !pause_status;
            
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
                string message = "Are you sure you want to restart simulation?";
                string caption = "Restart";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                    Application.Restart();
            
        }

        private void clock1_Tick(object sender, EventArgs e)
        {
            clock_seconds++;
            if(clock_seconds==60)
            {
                clock_seconds = 0;
                clock_minutes++;
            }
        }
        private string clock1_Time()
        {
            string result = "[";
            if (clock_minutes < 10)
                result += "0";
            result += Convert.ToString(clock_minutes)+":";
            if (clock_seconds < 10)
                result += "0";
            result += Convert.ToString(clock_seconds) + "]: ";
            return result;
        }

        private void hurryInput_Click(object sender, EventArgs e)
        { 
            toolTip1.Show("Values: 0-10", hurryInput, 0, -30, 1000);
        }
        private void patientInput_Click(object sender, EventArgs e)
        {
            toolTip1.Show("Values: 0-10", patientInput, 0, -30, 1000);
        }

        private void waitInput_Click(object sender, EventArgs e)
        {
            toolTip1.Show("Values: 1-5", waitInput, 0, -30, 1000);
        }

        private void shelvesInput_Click(object sender, EventArgs e)
        {
            toolTip1.Show("Values: 1-15", shelvesInput, 0, -30, 1000);
        }

        private void amountInput_Click(object sender, EventArgs e)
        {
            toolTip1.Show("Values: 1-4", amountInput, 0, -30, 1000);
        }

        private void quantityInput_Click(object sender, EventArgs e)
        {
            toolTip1.Show("Values: 1-100", quantityInput, 0, -30, 1000);
        }
    }
}
