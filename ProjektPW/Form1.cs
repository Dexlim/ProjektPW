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
        public int resigned_clients = 0;
        public int sold_products = 0;
        // Clock
        private int clock_minutes = 0;
        private int clock_seconds = 0;
        // Clients
        public int hurryClients = 0;
        public int patientClients = 0;
        public int waitingTime = 0;
        // Shelves
        public static int shelvesAmount = 0;
        public static int productsAmount = 0;
        public int productsQuantity = 0;

        List<Client> Clients = new List<Client>();
        List<Shelf> Shelves = new List<Shelf>();

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
                clock1.Enabled = false;
            }
            else
            {
                pauseButton.Text = "Pause";
                logBox.Text += clock1_Time() + "Simulation resumed.\n";
                timer1.Enabled = true;
                clock1.Enabled = true;
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
        public string clock1_Time()
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

        private string getShelfText(Shelf shelf)
        {
            string max = "/"+ Convert.ToString(shelf.max_product) +"\n";
            string result= "A: " + Convert.ToString(shelf.product1) + max;
            if (shelf.product2 == -1) return result;
            result += "B: " + Convert.ToString(shelf.product2) + max;
            if (shelf.product3 == -1) return result;
            result += "C: " + Convert.ToString(shelf.product3) + max;
            if (shelf.product4 == -1) return result;
            result += "D: " + Convert.ToString(shelf.product4) + max;
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int hurry;
            int patient;
            int wait;
            int shelves;
            int amount;
            int quantity;

            Button[] shelves_id = new Button[15] { shelf1, shelf2, shelf3, shelf4, shelf5, shelf6, shelf7, shelf8, shelf9, shelf10, shelf11, shelf12, shelf13, shelf14, shelf15 };
            ProgressBar[] progress_bars = new ProgressBar[15] { progressBar1, progressBar2, progressBar3, progressBar4, progressBar5, progressBar6, progressBar7, progressBar8, progressBar9, progressBar10, progressBar11, progressBar12, progressBar13, progressBar14, progressBar15 };

            GroupBox[] client_labels = new GroupBox[10] {groupBox5, groupBox6,groupBox7,groupBox8,groupBox9,groupBox10,groupBox11,groupBox12,groupBox13,groupBox14};
            Label[] client_statuses = new Label[10] { client1, client2, client3, client4, client5, client6, client7, client8, client9, client10 };
            RadioButton[] client_sign = new RadioButton[10] { sign1, sign2, sign3, sign4, sign5, sign6, sign7, sign8, sign9, sign10 };
            
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

            for (int i = 0; i < shelves; i++)
            {
                shelves_id[i].Visible = true;
                progress_bars[i].Visible = true;
                Shelves.Add(new Shelf(i, amount, quantity, shelves_id[i], progress_bars[i],this));
                Shelves[i].square.Text = getShelfText(Shelves[i]);
                Shelves[i].bar.Value = 100;
            }

            for (int i = 0; i < patient; i++)
            {
                client_labels[i].Visible = true;
                client_statuses[i].Visible = true;
                Clients.Add(new Client(i, client_statuses[i],client_sign[i],this,Shelves));
                Clients[i].MainLoop();
            }

            for(int i = 0; i < hurry; i++)
            {
                client_labels[i+5].Visible = true;
                client_statuses[i+5].Visible = true;
                Clients.Add(new Client(i+5, client_statuses[i+5], client_sign[i+5],this,Shelves));
                Clients[i+patient].MainLoop();
            }

            label12.Visible = true;
            label11.Visible = true;
            label10.Visible = true;
            label7.Visible = true;
            groupBox2.Visible = true;
            groupBox16.Visible = true;


        }
    }
}
