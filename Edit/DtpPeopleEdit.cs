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
    public partial class DtpPeopleEdit : Form
    {
        public int keyPpleValue;
        public int keyDriverValue;
        public int keyCarValue;
        public int code;

        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public DtpPeopleEdit(int code)
        {
            this.code = code;
            MessageBox.Show(Convert.ToString(code));
            InitializeComponent();
            sqlcon.Open();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //string query = $"insert into Участники_ДТП values ({Convert.ToInt32(textBox1.Text)},{Convert.ToInt32(textBox2.Text)},{Convert.ToInt32(textBox3.Text)},{textBox4.Text})\n";
            string query = $"update Участники_ДТП set  Участники_ДТП.Код_ДТП = {Convert.ToInt32(textBox1.Text)}, Участники_ДТП.Код_водителя = {Convert.ToInt32(textBox2.Text)}, Участники_ДТП.Код = {Convert.ToInt32(textBox3.Text)}, Участники_ДТП.Виновник = '{textBox4.Text}' where Участники_ДТП = {code}";
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

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            GridSelectItem objGridSelectItem = new GridSelectItem("Водители");
            this.Hide();
            objGridSelectItem.ShowDialog();
            this.Show();
            int val = objGridSelectItem.ReturnValue;
            textBox2.Text = Convert.ToString(val);
            MessageBox.Show(Convert.ToString(val));
            textBox2.Enabled = false;
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

        private void DtpPeopleEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
