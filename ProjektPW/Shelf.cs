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

        public SemaphoreSlim semaphore;

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
            semaphore = new SemaphoreSlim(1, 1);
        }

        public void Update()
        {
            string max = "/" + Convert.ToString(max_product) + "\n";
            string result = "A: " + Convert.ToString(product1) + max;
            mainForm.Invoke(new Action(delegate ()
            {
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
                    bar.Value = (int)(((float)Math.Min(product1, product2) / (float)max_product) * 100);
                    return;
                }
                result += "C: " + Convert.ToString(product3) + max;
                if (product4 == -1)
                {
                    square.Text = result;
                    bar.Value = (int)((float)(Math.Min(Math.Min(product1, product2), product3) / (float)max_product) * 100);
                    return;
                }
                result += "D: " + Convert.ToString(product4) + max;
                square.Text = result;
                bar.Value = (int)(((float)Math.Min(Math.Min(Math.Min(product1, product2), product3), product4) / (float)max_product) * 100);
            }));
        }


    }
}
