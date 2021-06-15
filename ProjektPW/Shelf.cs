using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ProjektPW
{
    class Shelf
    {
        public int id;
        public int product1 = -1;
        public int product2 = -1;
        public int product3 = -1;
        public int product4 = -1;
        public int max_product;
        public Button square;
        public ProgressBar bar;
        public setButton mainForm;
        public Shelf(int id, int product_amount,int product_quantity, Button square, ProgressBar bar, setButton mainForm)
        {
            this.id = id;
            if(product_amount > 0)
                this.product1 = product_quantity;
            if(product_amount > 1)
                this.product2 = product_quantity;
            if(product_amount > 2)
                this.product3 = product_quantity;
            if(product_amount > 3)
                this.product4 = product_quantity;
            this.square = square;
            this.bar = bar;
            this.max_product = product_quantity;
            this.mainForm = mainForm;
        }

        public void Update()
        {
            string max = "/" + Convert.ToString(max_product) + "\n";
            string result = "A: " + Convert.ToString(product1) + max;

            if (product2 == -1)
            {
                square.Text = result;
                bar.Value = (int)(((float)product1 / (float)max_product) * 100);
                return;
            }
            result += "B: " + Convert.ToString(product2) + max;
            if (product3 == -1)
            {
                square.Text = result;
                bar.Value = (int)(((float)Math.Min(product1,product2) / (float)max_product) * 100);
                return;
            }
            result += "C: " + Convert.ToString(product3) + max;
            if (product4 == -1) 
            {
                square.Text = result;
                bar.Value = (int)((float)(Math.Min(Math.Min(product1, product2),product3) / (float)max_product) * 100);
                return;
            }
            result += "D: " + Convert.ToString(product4) + max;
            square.Text = result;
            bar.Value = (int)(((float)Math.Min(Math.Min(Math.Min(product1, product2), product3),product4) / (float)max_product) * 100);
        }

        public void Stock(int product)
        {
            if(product==1)
            {
                //Semafor
                this.mainForm.storekeeper.Text = "Status: Putting product A on shelf" + Convert.ToString(id+1);
                this.mainForm.sign11.Checked = true;
                //Sleep
                this.product1 = max_product;
                mainForm.logBox.Text += mainForm.clock1_Time() + "Storekeeper restocked product A on shelf" + Convert.ToString(id + 1) + "\n";
                this.mainForm.storekeeper.Text = "Status: ";
                this.mainForm.sign11.Checked = false;
                this.Update();
                //Semafor
            }
            else if(product==2)
            {
                //Semafor
                this.mainForm.storekeeper.Text = "Status: Putting product B on shelf" + Convert.ToString(id + 1);
                this.mainForm.sign11.Checked = true;
                //Sleep
                this.product2 = max_product;
                mainForm.logBox.Text += mainForm.clock1_Time() + "Storekeeper restocked product B on shelf" + Convert.ToString(id + 1) + "\n";
                this.mainForm.storekeeper.Text = "Status: ";
                this.mainForm.sign11.Checked = false;
                this.Update();
                //Semafor
            }
            else if(product==3)
            {
                //Semafor
                this.mainForm.storekeeper.Text = "Status: Putting product C on shelf" + Convert.ToString(id + 1);
                this.mainForm.sign11.Checked = true;
                //Sleep
                this.product3 = max_product;
                mainForm.logBox.Text += mainForm.clock1_Time() + "Storekeeper restocked product C on shelf" + Convert.ToString(id + 1) + "\n";
                this.mainForm.storekeeper.Text = "Status: ";
                this.mainForm.sign11.Checked = false;
                this.Update();
                //Semafor
            }
            else if(product==4)
            {
                //Semafor
                this.mainForm.storekeeper.Text = "Status: Putting product D on shelf" + Convert.ToString(id + 1);
                this.mainForm.sign11.Checked = true;
                //Sleep
                this.product4 = max_product;
                mainForm.logBox.Text += mainForm.clock1_Time() + "Storekeeper restocked product D on shelf" + Convert.ToString(id + 1) + "\n";
                this.mainForm.storekeeper.Text = "Status: ";
                this.mainForm.sign11.Checked = false;
                this.Update();
                //Semafor
            }
        }
    }
}
