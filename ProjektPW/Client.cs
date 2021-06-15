using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ProjektPW
{
    class Client
    {
        public int id;
        public int waiting_time;
        public bool isPatient;
        public Label status;
        public RadioButton sign;
        public setButton mainForm;
        private List<Shelf> Shelves;

    public Client(int id, Label status, RadioButton sign, setButton form, List<Shelf> Shelves)
        {
            this.id = id;
            this.isPatient = true;
            this.waiting_time = -1;
            this.status = status;
            this.sign = sign;
            this.mainForm = form;
            this.Shelves = Shelves;
        }
        public Client(int id, int waiting_time, Label status, RadioButton sign, setButton form,List<Shelf> Shelves)
        {
            this.id = id;
            this.isPatient = false;
            this.waiting_time = waiting_time;
            this.status = status;
            this.sign = sign;
            this.mainForm = form;
            this.Shelves = Shelves;
        }

        public void MainLoop()
        {
            //while(true)
            //{
                Random rnd = new Random();
                int random_shelf = rnd.Next(0, setButton.shelvesAmount);
                int random_product = rnd.Next(0, setButton.productsAmount);

                status.Text = "Status: waiting for shelf" + Convert.ToString(random_shelf);
                sign.Checked = true;
                // Semafory tutaj
                     Consume(random_shelf, random_product);
                // Semafory tutaj
                sign.Checked = false;
                status.Text = "Status: ";
            //}
        }

        private void Consume(int shelfID,int productID)
        {
            status.Text = "Status: Buying " + Convert.ToChar(productID + 65) + " from shelf" + Convert.ToString(shelfID);
            // SLEEP
            mainForm.logBox.Text += mainForm.clock1_Time() + "Client" + Convert.ToString(this.id) + " bought product " + Convert.ToChar(productID+65) + " from shelf" + Convert.ToString(shelfID+1) + "\n";
            if (productID == 0)
            { 
                Shelves[shelfID].product1--;
                if (Shelves[shelfID].product1 == 0)
                {
                    mainForm.logBox.Text += mainForm.clock1_Time() + "Client" + Convert.ToString(this.id) + " finished product " + Convert.ToChar(productID + 65) + " on shelf" + Convert.ToString(shelfID + 1) + "\n";
                    Shelves[shelfID].Stock(1); return;
                }
            }
            if (productID == 1)
            {
                Shelves[shelfID].product2--;
                if (Shelves[shelfID].product2 == 0)
                {
                    mainForm.logBox.Text += mainForm.clock1_Time() + "Client" + Convert.ToString(this.id) + " finished product " + Convert.ToChar(productID + 65) + " on shelf" + Convert.ToString(shelfID + 1) + "\n";
                    Shelves[shelfID].Stock(2); return;
                }
            }
            if (productID == 2)
            {
                Shelves[shelfID].product3--;
                if (Shelves[shelfID].product3 == 0)
                {
                    mainForm.logBox.Text += mainForm.clock1_Time() + "Client" + Convert.ToString(this.id) + " finished product " + Convert.ToChar(productID + 65) + " on shelf" + Convert.ToString(shelfID + 1) + "\n";
                    Shelves[shelfID].Stock(3); return;
                }
            }
            if (productID == 3)
            {
                Shelves[shelfID].product4--;
                if (Shelves[shelfID].product4 == 0)
                {
                    mainForm.logBox.Text += mainForm.clock1_Time() + "Client" + Convert.ToString(this.id) + " finished product " + Convert.ToChar(productID + 65) + " on shelf" + Convert.ToString(shelfID + 1) + "\n";
                    Shelves[shelfID].Stock(4); return; 
                }
            }
            Shelves[shelfID].Update();           
        }
    }
}
