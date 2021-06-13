using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public Shelf(int id, int product_amount,int product_quantity, Button square, ProgressBar bar)
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
        }
    }
}
