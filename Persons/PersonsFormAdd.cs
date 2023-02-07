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
    public partial class PersonsFormAdd : Form
    {
        public int keyPpleValue;
        public int keyDriverValue;
        public int keyCarValue;

        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public PersonsFormAdd()
        {
            InitializeComponent();
            sqlcon.Open();
        }

   

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = $"insert into Сотрудники values ('{(textBox1.Text)}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{(textBox5.Text)}')\n";
            SqlCommand add = new SqlCommand(query,sqlcon);
            SqlDataReader reader = add.ExecuteReader();
            reader.Close();
            DialogResult dialogResult = MessageBox.Show(query, "Результат", MessageBoxButtons.OK);
            textBox1.Text = null;
            textBox1.Enabled = true;
            textBox2.Text = null;
            textBox2.Enabled = true;
            textBox3.Text = null;
            textBox3.Enabled = true;
            textBox4.Text = null;
            textBox5.Text = null;
            PersonsFormAddHistory objPersonsFormAddHistory = new PersonsFormAddHistory();
            this.Hide();
            objPersonsFormAddHistory.Show();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            sqlcon.Close();
            this.Hide();
        }
    }
}
