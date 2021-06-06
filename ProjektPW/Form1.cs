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
    {   // System status
        private bool timer1_status = false;
        private bool pause_status = false;
        // Clock
        private int clock_minutes = 0;
        private int clock_seconds = 0;
        // Clients
        public int hurryClients = 0;
        public int patientClients = 0;
        public int waitingTime = 0;
        // Shelves
        public int shelvesAmount = 0;
        public int productsAmount = 0;
        public int productsQuantity = 0;

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
            label1.Text = Convert.ToString(trackBar1.Value) + "[ms]";
            timer1.Interval = trackBar1.Value;
        }
        private void trackBar1_MouseUp(object sender, EventArgs e)
        {
            logBox.Text += clock1_Time() + "Changed timer delay to: " + label1.Text + "\n";
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
            if (clock_seconds == 60)
            {
                clock_seconds = 0;
                clock_minutes++;
            }
            timeLabel.Text = clock1_Time().Trim(':', ' ');
        }
        private string clock1_Time()
        {
            string result = "[";
            if (clock_minutes < 10)
                result += "0";
            result += Convert.ToString(clock_minutes) + ":";
            if (clock_seconds < 10)
                result += "0";
            result += Convert.ToString(clock_seconds) + "]: ";
            return result;
        }

        private void hurryInput_Click(object sender, EventArgs e)
        {
            toolTip1.Show("Values: 0-5", hurryInput, 0, -30, 500);
        }
        private void patientInput_Click(object sender, EventArgs e)
        {
            toolTip1.Show("Values: 0-5", patientInput, 0, -30, 500);
        }

        private void waitInput_Click(object sender, EventArgs e)
        {
            toolTip1.Show("Values: 1-5", waitInput, 0, -30, 500);
        }

        private void shelvesInput_Click(object sender, EventArgs e)
        {
            toolTip1.Show("Values: 1-15", shelvesInput, 0, -30, 500);
        }

        private void amountInput_Click(object sender, EventArgs e)
        {
            toolTip1.Show("Values: 1-4", amountInput, 0, -30, 500);
        }

        private void quantityInput_Click(object sender, EventArgs e)
        {
            toolTip1.Show("Values: 1-100", quantityInput, 0, -30, 500);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int hurry;
            int patient;
            int wait;
            int shelves;
            int amount;
            int quantity;

            if (int.TryParse(hurryInput.Text, out hurry))
            {
                if (hurry < 0 || hurry > 5)
                {
                    toolTip1.Show("Value out of range", hurryInput, 0, -30, 1000);
                    return;
                }
            }
            else
            {
                toolTip1.Show("You can only add numeric characters (0-9) into this field.", hurryInput, 0, -30, 1000);
                return;
            }

            if (int.TryParse(patientInput.Text, out patient))
            {
                if (patient < 0 || patient > 5)
                {
                    toolTip1.Show("Value out of range", hurryInput, 0, -30, 1000);
                    return;
                }
            }
            else
            {
                toolTip1.Show("You can only add numeric characters (0-9) into this field.", patientInput, 0, -30, 1000);
                return;
            }

            if (hurry + patient < 1)
            {
                toolTip1.Show("Clients amount cant be lower than 1", button1, 0, -30, 1000);
                return;
            }

            if (int.TryParse(waitInput.Text, out wait))
            {
                if (wait < 1 || wait > 5)
                {
                    toolTip1.Show("Value out of range", waitInput, 0, -30, 1000);
                    return;
                }
            }
            else
            {
                toolTip1.Show("You can only add numeric characters (0-9) into this field.", waitInput, 0, -30, 1000);
                return;
            }

            if (int.TryParse(shelvesInput.Text, out shelves))
            {
                if (shelves < 1 || shelves > 15)
                {
                    toolTip1.Show("Value out of range", shelvesInput, 0, -30, 1000);
                    return;
                }
            }
            else
            {
                toolTip1.Show("You can only add numeric characters (0-9) into this field.", shelvesInput, 0, -30, 1000);
                return;
            }

            if (int.TryParse(amountInput.Text, out amount))
            {
                if (amount < 1 || amount > 4)
                {
                    toolTip1.Show("Value out of range", amountInput, 0, -30, 1000);
                    return;
                }
            }
            else
            {
                toolTip1.Show("You can only add numeric characters (0-9) into this field.", amountInput, 0, -30, 1000);
                return;
            }

            if (int.TryParse(quantityInput.Text, out quantity))
            {
                if (quantity < 1 || shelves > 100)
                {
                    toolTip1.Show("Value out of range", quantityInput, 0, -30, 1000);
                    return;
                }
            }
            else
            {
                toolTip1.Show("You can only add numeric characters (0-9) into this field.", quantityInput, 0, -30, 1000);
                return;
            }

            logBox.Text += "Starting simulation with given arguments: \n";
            hurryClients = hurry;
            logBox.Text += "Clients in a hurry amount: \t" + Convert.ToString(hurry) + "\n";
            patientClients = patient;
            logBox.Text += "Patient cleints amount: \t" + Convert.ToString(patient) + "\n";
            waitingTime = wait;
            logBox.Text += "Waiting time to: \t\t" + Convert.ToString(wait) + "\n";
            shelvesAmount = shelves;
            logBox.Text += "Shelves amount: \t\t" + Convert.ToString(shelves) + "\n";
            productsAmount = amount;
            logBox.Text += "Amount of products: \t" + Convert.ToString(amount) + "\n";
            productsQuantity = quantity;
            logBox.Text += "Product's quantity to: \t" + Convert.ToString(quantity) + "\n";
            logBox.Text += "-----------------------------------------------------------------\n";

            groupBox15.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            hurryInput.Enabled = false;
            patientInput.Enabled = false;
            waitInput.Enabled = false;
            shelvesInput.Enabled = false;
            amountInput.Enabled = false;
            quantityInput.Enabled = false;
            button1.Enabled = false;
            label3.Enabled = false;
            label4.Enabled = false;
            label5.Enabled = false;
            label6.Enabled = false;
            label8.Enabled = false;
            label9.Enabled = false;
            timer1.Enabled = true;
            clock1.Enabled = true;
            pauseButton.Enabled = true;
            restartButton.Enabled = true;
            logBox.Enabled = true;


            if (patient > 0) { groupBox5.Visible = true; client1.Visible = true; }
            if (patient > 1) { groupBox6.Visible = true; client2.Visible = true; }
            if (patient > 2) { groupBox7.Visible = true; client3.Visible = true; }
            if (patient > 3) { groupBox8.Visible = true; client4.Visible = true; }
            if (patient > 4) { groupBox9.Visible = true; client5.Visible = true; }
            if (hurry > 0)   { groupBox10.Visible = true; client6.Visible = true; }
            if (hurry > 1)   { groupBox11.Visible = true; client7.Visible = true; }
            if (hurry > 2)   { groupBox12.Visible = true; client8.Visible = true; }
            if (hurry > 3)   { groupBox13.Visible = true; client9.Visible = true; }
            if (hurry > 4)   { groupBox14.Visible = true; client10.Visible = true; }

            if (shelves > 0) { shelf1.Visible = true; progressBar1.Visible = true; }
            if (shelves > 1) { shelf2.Visible = true; progressBar2.Visible = true; }
            if (shelves > 2) { shelf3.Visible = true; progressBar3.Visible = true; }
            if (shelves > 3) { shelf4.Visible = true; progressBar4.Visible = true; }
            if (shelves > 4) { shelf5.Visible = true; progressBar5.Visible = true; }
            if (shelves > 5) { shelf6.Visible = true; progressBar6.Visible = true; }
            if (shelves > 6) { shelf7.Visible = true; progressBar7.Visible = true; }
            if (shelves > 7) { shelf8.Visible = true; progressBar8.Visible = true; }
            if (shelves > 8) { shelf9.Visible = true; progressBar9.Visible = true; }
            if (shelves > 9) { shelf10.Visible = true; progressBar10.Visible = true; }
            if (shelves > 10) { shelf11.Visible = true; progressBar11.Visible = true; }
            if (shelves > 11) { shelf12.Visible = true; progressBar12.Visible = true; }
            if (shelves > 12) { shelf13.Visible = true; progressBar13.Visible = true; }
            if (shelves > 13) { shelf14.Visible = true; progressBar14.Visible = true; }
            if (shelves > 14) { shelf15.Visible = true; progressBar15.Visible = true; }

            label12.Visible = true;
            label11.Visible = true;
            label10.Visible = true;
            label7.Visible = true;
            groupBox2.Visible = true;
        }
    }
}
