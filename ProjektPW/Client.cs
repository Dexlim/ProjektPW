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

        public int isPaused;
        public Thread thread;
        public SemaphoreSlim pauseSemaphore = new SemaphoreSlim(0,1);

    public Client(int id, Label status, RadioButton sign, setButton form, List<Shelf> Shelves)
        {
            this.id = id;
            this.isPatient = true;
            this.waiting_time = -1;
            this.status = status;
            this.sign = sign;
            this.mainForm = form;
            this.Shelves = Shelves;
            this.isPaused = -1;
            this.thread = new Thread(MainLoop);
            thread.IsBackground = true;
            thread.Start();
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
            this.isPaused = -1;
            this.thread = new Thread(MainLoop);
            thread.IsBackground = true;
            thread.Start();
        }

        public void MainLoop()
        {
            while(!mainForm.form_closed)
            {
                if(isPaused == 1)
                {
                    isPaused = 2;
                    this.pauseSemaphore.Wait();
                }
                Random rnd = new Random();
                int random_shelf = rnd.Next(0, setButton.shelvesAmount);
                int random_product = rnd.Next(0, setButton.productsAmount);
                if (mainForm.form_closed) return;
                mainForm.Invoke(new Action(delegate ()
                {
                status.Text = "Status: Waiting for shelf" + Convert.ToString(random_shelf);
                }));
                Shelves[random_shelf].semaphore.Wait();
                    if (!isPatient)
                    {

                    }
                    Consume(random_shelf, random_product);
                if (mainForm.form_closed) return;
                mainForm.Invoke(new Action(delegate ()
                {
                    sign.Checked = false;
                }));
                Shelves[random_shelf].semaphore.Release();
            }
        }

        private void Consume(int shelfID,int productID)
        {
            mainForm.Invoke(new Action(delegate ()
            {
                status.Text = "Status: Buying " + Convert.ToChar(productID + 65) + " from shelf" + Convert.ToString(shelfID);
                sign.Checked = true;
            }));
            Thread.Sleep(1000);
            if (mainForm.form_closed) return;
            mainForm.Invoke(new Action(delegate ()
            {
                mainForm.logBox.Text += mainForm.clock1_Time() + "Client" + Convert.ToString(this.id) + " bought product " + Convert.ToChar(productID + 65) + " from shelf" + Convert.ToString(shelfID + 1) + "\n";
            }));
                if (productID == 0)
                { 
                    Shelves[shelfID].product1--;
                    if (Shelves[shelfID].product1 == 0)
                    {
                    mainForm.Invoke(new Action(delegate ()
                    {
                        mainForm.logBox.Text += mainForm.clock1_Time() + "Client" + Convert.ToString(this.id) + " finished product " + Convert.ToChar(productID + 65) + " on shelf" + Convert.ToString(shelfID + 1) + "\n";
                    }));
                        mainForm.storekeeper_semaphore.Wait();
                        Stock(1, shelfID);
                    mainForm.Invoke(new Action(delegate ()
                    {
                        mainForm.storekeeper.Text = "Status: ";
                    }));
                        mainForm.storekeeper_semaphore.Release();
                        return;
                    }
                }
                if (productID == 1)
                {
                    Shelves[shelfID].product2--;
                    if (Shelves[shelfID].product2 == 0)
                    {
                    mainForm.Invoke(new Action(delegate ()
                    {
                        mainForm.logBox.Text += mainForm.clock1_Time() + "Client" + Convert.ToString(this.id) + " finished product " + Convert.ToChar(productID + 65) + " on shelf" + Convert.ToString(shelfID + 1) + "\n";
                    }));
                        mainForm.storekeeper_semaphore.Wait();
                        Stock(2, shelfID);
                    mainForm.Invoke(new Action(delegate ()
                    {
                        mainForm.storekeeper.Text = "Status: ";
                    }));
                        mainForm.storekeeper_semaphore.Release();
                        return;
                    }
                }
                if (productID == 2)
                {
                    Shelves[shelfID].product3--;
                    if (Shelves[shelfID].product3 == 0)
                    {
                    mainForm.Invoke(new Action(delegate ()
                    {
                        mainForm.logBox.Text += mainForm.clock1_Time() + "Client" + Convert.ToString(this.id) + " finished product " + Convert.ToChar(productID + 65) + " on shelf" + Convert.ToString(shelfID + 1) + "\n";
                    }));
                        mainForm.storekeeper_semaphore.Wait();
                        Stock(3, shelfID);
                    mainForm.Invoke(new Action(delegate ()
                    {
                        mainForm.storekeeper.Text = "Status: ";
                    }));
                        mainForm.storekeeper_semaphore.Release();
                        return;
                    }
                }
                if (productID == 3)
                {
                    Shelves[shelfID].product4--;
                    if (Shelves[shelfID].product4 == 0)
                    {
                    mainForm.Invoke(new Action(delegate ()
                    {
                        mainForm.logBox.Text += mainForm.clock1_Time() + "Client" + Convert.ToString(this.id) + " finished product " + Convert.ToChar(productID + 65) + " on shelf" + Convert.ToString(shelfID + 1) + "\n";
                    }));
                        mainForm.storekeeper_semaphore.Wait();
                        Stock(4, shelfID);
                    mainForm.Invoke(new Action(delegate ()
                    {
                        mainForm.storekeeper.Text = "Status: ";
                    }));
                        mainForm.storekeeper_semaphore.Release();
                        return; 
                    }
                }
                Shelves[shelfID].Update();
                mainForm.sold_products++;
                mainForm.Invoke(new Action(delegate ()
                {
                    mainForm.sold_label.Text = "Sold products: " + Convert.ToString(mainForm.sold_products);
                }));
            }

        private void Stock(int product, int shelf_id)
        {   
            if (product == 1)
            {
                mainForm.Invoke(new Action(delegate ()
                {
                    mainForm.storekeeper.Text = "Status: Putting product A on shelf" + Convert.ToString(shelf_id + 1);
                    mainForm.sign11.Checked = true;
                }));
                Thread.Sleep(1000);
                Shelves[shelf_id].product1 = Shelves[shelf_id].max_product;
                mainForm.Invoke(new Action(delegate ()
                {
                    mainForm.logBox.Text += mainForm.clock1_Time() + "Storekeeper restocked product A on shelf" + Convert.ToString(shelf_id + 1) + "\n";
                    mainForm.sign11.Checked = false;
                }));
                Shelves[shelf_id].Update();
                return;
            }
            if (product == 2)
            {
                mainForm.Invoke(new Action(delegate ()
                {
                    mainForm.storekeeper.Text = "Status: Putting product B on shelf" + Convert.ToString(shelf_id + 1);
                    mainForm.sign11.Checked = true;
                }));
                Thread.Sleep(1000);
                Shelves[shelf_id].product2 = Shelves[shelf_id].max_product;
                mainForm.Invoke(new Action(delegate ()
                {
                    mainForm.logBox.Text += mainForm.clock1_Time() + "Storekeeper restocked product B on shelf" + Convert.ToString(shelf_id + 1) + "\n";
                    mainForm.sign11.Checked = false;
                    Shelves[shelf_id].Update();
                }));
                return;
            }
            if (product == 3)
            {
                mainForm.Invoke(new Action(delegate ()
                {
                    mainForm.storekeeper.Text = "Status: Putting product C on shelf" + Convert.ToString(shelf_id + 1);
                    mainForm.sign11.Checked = true;
                }));
                Thread.Sleep(1000);
                Shelves[shelf_id].product3 = Shelves[shelf_id].max_product;
                mainForm.Invoke(new Action(delegate ()
                {
                    mainForm.logBox.Text += mainForm.clock1_Time() + "Storekeeper restocked product C on shelf" + Convert.ToString(shelf_id + 1) + "\n";
                    mainForm.sign11.Checked = false;
                    Shelves[shelf_id].Update();
                }));
                return;
            }
            if (product == 4)
            {
                mainForm.Invoke(new Action(delegate ()
                {
                    mainForm.storekeeper.Text = "Status: Putting product D on shelf" + Convert.ToString(shelf_id + 1);
                    mainForm.sign11.Checked = true;
                }));
                Thread.Sleep(1000);
                Shelves[shelf_id].product4 = Shelves[shelf_id].max_product;
                mainForm.Invoke(new Action(delegate ()
                {
                    mainForm.logBox.Text += mainForm.clock1_Time() + "Storekeeper restocked product D on shelf" + Convert.ToString(shelf_id + 1) + "\n";
                    mainForm.sign11.Checked = false;
                    Shelves[shelf_id].Update();
                }));
                return;
            }
        }
    }
}
