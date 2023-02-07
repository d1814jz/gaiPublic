﻿using System;
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
    public partial class AccountingEdit : Form
    {
        public int keyPpleValue;
        public int keyDriverValue;
        public int keyCarValue;
        public int code; 

        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public AccountingEdit(int code)
        {
            this.code = code;
            MessageBox.Show(Convert.ToString(code));
            InitializeComponent();
            sqlcon.Open();
        }

        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //string query = $"update into Учет values ({Convert.ToInt32(textBox1.Text)},{Convert.ToInt32(textBox2.Text)},{Convert.ToInt32(textBox3.Text)},'{textBox4.Text}','{(textBox5.Text)}','{(textBox6.Text)}') where Учет.Код = {code}\n";
            string query = $"update Учет set Учет.Код_сотрудника = {Convert.ToInt32(textBox1.Text)}, Учет.Код_водителя = {Convert.ToInt32(textBox2.Text)} , Учет.Код_авто = {Convert.ToInt32(textBox3.Text)}, Учет.Номер = '{(textBox4.Text)}', Учет.Дата_постановки = '{(textBox5.Text)}', Учет.Дата_снятия = '{(textBox6.Text)}' where Учет.Код = {code}";
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
            textBox6.Text = null;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            sqlcon.Close();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            GridSelectItem objGridSelectItem = new GridSelectItem("Сотрудники");
            this.Hide();
            objGridSelectItem.ShowDialog();
            this.Show();
            int val = objGridSelectItem.ReturnValue;
            textBox1.Text = Convert.ToString(val);
            MessageBox.Show(Convert.ToString(val));
            textBox1.Enabled = false;
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
          
        }
    }
}
