using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektPW
{
    class Client
    {
        public int id;
        public int waiting_time;
        public bool isPatient;
        public Label status;
        public RadioButton sign;

        public Client(int id, Label status, RadioButton sign)
        {
            this.id = id;
            this.isPatient = true;
            this.waiting_time = -1;
            this.status = status;
            this.sign = sign;
        }
        public Client(int id, int waiting_time, Label status, RadioButton sign)
        {
            this.id = id;
            this.isPatient = false;
            this.waiting_time = waiting_time;
            this.status = status;
            this.sign = sign;
        }
    }
}
