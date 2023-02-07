using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace gai
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
            sqlcon.Open();
        }
        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        // SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=login;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == ""|| textBox3.Text == "")
            {
                MessageBox.Show("Не заполнены все поля", "Error!", MessageBoxButtons.OK);
                return;
            }
                
            int number = Convert.ToInt32(textBox3.Text);
            if (number > 3 || number < 0)
                MessageBox.Show("Тип не может быть больше 3 или меньше 0", "Error!", MessageBoxButtons.OK);

                
            string query = $"Insert into Пользователи values('{textBox1.Text}','{textBox2.Text}',{Convert.ToInt32(textBox3.Text)})";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            SqlCommand command = new SqlCommand(query, sqlcon);
            SqlDataReader sdr = command.ExecuteReader();
            sqlcon.Close();
            this.Close();
     
        

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            sqlcon.Close();
            this.Hide();
        }
    }
}
