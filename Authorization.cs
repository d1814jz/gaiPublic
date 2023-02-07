using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Data.SqlClient;


namespace gai
{
    public partial class Authorization : Form
    {
        
        public Authorization()
        {
            Boolean txtUserBox = false;
            InitializeComponent();
            sqlcon.Open();
            txtUsername.Text = "Логин";
            txtPassword.Text = "Пароль";

        }
        Boolean txtUserBox = true;
        Boolean txtPasswordBox = true;
        //SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=login;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void btnLogin_Click(object sender, EventArgs e)
        {
           
            string query = "Select * from Пользователи Where Логин = '" + txtUsername.Text.Trim() + "' and Пароль = '" + txtPassword.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            string UserName = Convert.ToString(txtUsername.Text);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            if (dtbl.Rows.Count == 1) {
                SqlCommand Type = new SqlCommand("Select Тип from Пользователи Users Where Логин = '" + txtUsername.Text.Trim() + "'", sqlcon);
                SqlDataReader dr = Type.ExecuteReader();
                dr.Read();
                var userID = dr["Тип"];
                dr.Close();
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                //string query2 = "Insert into Log values('"+txtUsername.Text+"','"+dateTime+"')";
                //MessageBox.Show(query2);
             
                SqlCommand addLog = new SqlCommand("Insert into Log values('" + txtUsername.Text + "','" + dateTime + "')", sqlcon);
                DataTable logTable = new DataTable();
                SqlDataReader logDataReader = addLog.ExecuteReader();
                Form objApp = new MenuFrom((int)userID, $"{(string)UserName}");        
                this.Hide();
                objApp.ShowDialog();
                txtUsername.Text = "";
                txtPassword.Text = "";
                //this.Show();
                dr.Close();



            }
            else{
                this.Enabled = false;
                MessageBox.Show("Проверьте логин или пароль!");
            }
           
            this.Enabled = true;
            
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите выйти", "Выйти", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
                return;
            sqlcon.Close();
            this.Close();

        }

        private void Authorization_Load(object sender, EventArgs e)
        {


            
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_Click(object sender, EventArgs e)
        {
            
            if (txtUserBox == true)
                txtUsername.Text = "";
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            if (txtPasswordBox == true)
            {
                txtPassword.Text = "";
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
       
}
