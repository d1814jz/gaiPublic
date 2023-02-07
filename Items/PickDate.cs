using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gai
{
    public partial class PickDate : Form
    {
        public PickDate()
        {
            InitializeComponent();
            dateTimePicker1.Visible = true; 
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
