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
    public partial class IllegalEdit : Form
    {
        public int keyPpleValue;
        public int keyDriverValue;
        public int keyCarValue;
        public int code; 

        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public IllegalEdit(int code)
        {
            this.code = code;
            InitializeComponent();
            sqlcon.Open();
            //textBox1.Visible = false;
            //label1.Visible = false;
            MessageBox.Show(Convert.ToString(code));
        }

        private void IllegalEdit_Load(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(code);
            textBox1.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = $"insert into Нарушения values ({Convert.ToInt32(textBox1.Text)},{Convert.ToInt32(textBox2.Text)},{Convert.ToInt32(textBox3.Text)},{Convert.ToInt32(textBox4.Text)},'{(textBox5.Text)}','{textBox6.Text}')\n";
            query = $"update Нарушения set Нарушения.Код_вида = {Convert.ToInt32(textBox1.Text)}, Нарушения.Код_дежурства = {Convert.ToInt32(textBox2.Text)}, Нарушения.Код_авто = {Convert.ToInt32(textBox3.Text)}, Нарушения.Код_водителя = {Convert.ToInt32(textBox4.Text)}, Нарушения.Место = '{(textBox5.Text)}', Нарушения.Описание = '{textBox6.Text}' where Нарушения.Код = {code}";
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

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            sqlcon.Close();
            this.Hide();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            GridSelectItem objGridSelectItem = new GridSelectItem("ДТП");
            this.Hide();
            objGridSelectItem.ShowDialog();
            this.Show();
            int val = objGridSelectItem.ReturnValue;
            textBox1.Text = Convert.ToString(val);
            MessageBox.Show(Convert.ToString(val));
            textBox1.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox7_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void textBox2_MouseClick_1(object sender, MouseEventArgs e)
        {
            GridSelectItem objGridSelectItem = new GridSelectItem("Дежурство");
            this.Hide();
            objGridSelectItem.ShowDialog();
            this.Show();
            int val = objGridSelectItem.ReturnValue;
            textBox2.Text = Convert.ToString(val);
            MessageBox.Show(Convert.ToString(val));
            textBox2.Enabled = false;
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            GridSelectItem objGridSelectItem = new GridSelectItem("Автомобили");
            this.Hide();
            objGridSelectItem.ShowDialog();
            this.Show();
            int val = objGridSelectItem.ReturnValue;
            textBox3.Text = Convert.ToString(val);
            MessageBox.Show(Convert.ToString(val));
            textBox3.Enabled = false;
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            GridSelectItem objGridSelectItem = new GridSelectItem("Водители");
            this.Hide();
            objGridSelectItem.ShowDialog();
            this.Show();
            int val = objGridSelectItem.ReturnValue;
            textBox4.Text = Convert.ToString(val);
            MessageBox.Show(Convert.ToString(val));
            textBox4.Enabled = false;
        }
    }
}
