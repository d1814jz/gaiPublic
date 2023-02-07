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
    public partial class RewardsEdit : Form
    {
        public int keyPpleValue;
        public int keyDriverValue;
        public int keyCarValue;
        public int code;

        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public RewardsEdit(int code)
        {
            this.code = code; 
            InitializeComponent();
            sqlcon.Open();
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = $"insert into Поощрения_и_взыскания values ({Convert.ToInt32(textBox1.Text)},{Convert.ToInt32(textBox2.Text)},'{(textBox3.Text)}','{textBox4.Text}')\n";
            query = $" update Поощрения_и_взыскания set Поощрения_и_взыскания.Код_сотрудника = {Convert.ToInt32(textBox1.Text)} , Поощрения_и_взыскания.Код_вида = {Convert.ToInt32(textBox2.Text)}, Поощрения_и_взыскания.Дата = '{(textBox3.Text)}', Поощрения_и_взыскания.Описание = '{textBox4.Text}' where Поощрения_и_взыскания.Код = {code}";
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
            GridSelectItem objGridSelectItem = new GridSelectItem("Виды_нарушений");
            this.Hide();
            objGridSelectItem.ShowDialog();
            this.Show();
            int val = objGridSelectItem.ReturnValue;
            textBox2.Text = Convert.ToString(val);
            MessageBox.Show(Convert.ToString(val));
            textBox2.Enabled = false;
        }
    }
}
