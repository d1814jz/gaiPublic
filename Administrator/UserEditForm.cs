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
    public partial class UserEditForm : Form
    {
        public string TableName = "Пользователи";
        public UserEditForm()
        {
            InitializeComponent();
            btnGridView_Click(null, null);
            sqlcon.Open();
        }
        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        //SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=login;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGridView_Click(object sender, EventArgs e)
        {
            btnGridView.Columns.Clear();
            btnGridView.DataSource = null;
            string query = "SELECT * FROM Пользователи";
            var _dataTable = new DataTable();
            var _sqlDataAdapter = new SqlDataAdapter(query, sqlcon);
            _sqlDataAdapter.Fill(_dataTable);
            btnGridView.DataSource = _dataTable;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Enabled = false;
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите удалить запись", "Delete", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
                return;
            foreach (DataGridViewRow row in btnGridView.SelectedRows)
            {
                int index = (int)btnGridView.CurrentCell.Value;
                string query = $"Delete  from Users Where Код = {index}";

                SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);

                SqlCommand command = new SqlCommand(query, sqlcon)
                {
                    CommandType = CommandType.Text
                };
                var reader = command.ExecuteReader();
                btnGridView.Rows.Remove(row);
                reader.Dispose();
            }
            Enabled = true;
            btnGridView_Click(null, null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddUserForm objAddUserForm = new AddUserForm();
            this.Hide();
            objAddUserForm.ShowDialog();
            btnGridView_Click(null, null);
            this.Show();

        }
        public string SearchTableName = "Имя";
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SearchTableName = btnGridView.SelectedCells[0].OwningColumn.HeaderText;
            MessageBox.Show(SearchTableName);
            label2.Text = $"Столбец: {SearchTableName}";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Boolean gridSearch = false;
            string query;
            if (e.KeyCode == Keys.Enter)

            {
                gridSearch = true;
                if (textBox1.Text == string.Empty || textBox1.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Update");
                    btnGridView_Click(null, null);
                    return;
                }
                if (gridSearch == true)
                {
                    SearchTableName = SearchTableName.Replace(" ", "_");
                    btnGridView.Columns.Clear();
                    btnGridView.DataSource = null;

                    if (SearchTableName.Contains("Дата") || SearchTableName.Contains("дата"))
                        DateCheckBox.Checked = true;

                    if (DateCheckBox.Checked == true)
                    {
                        textBox1.Text = textBox1.Text.Replace('\r', ' ');
                        textBox1.Text = textBox1.Text.Replace('\n', ' ');
                        query = $"SELECT * FROM {TableName} where {TableName}.[{SearchTableName}] = '{textBox1.Text.Replace('.', '-')}'";
                        MessageBox.Show(query);
                    }
                    else
                    {
                        int numtype = 1;
                        if (btnGridView.SelectedCells.GetType() != numtype.GetType())
                        {
                            MessageBox.Show("String");
                            query = $"SELECT * FROM {TableName} where {TableName}.[{SearchTableName}]  like '%{textBox1.Text}%'";
                        }
                        else
                        {
                            MessageBox.Show("number");
                            query = $"SELECT * FROM {TableName} where {TableName}.[{SearchTableName}]  = {textBox1.Text}";
                        }
                    }
                    query = query.Replace("\r", "");
                    query = query.Replace("\n", "");
                    //textBox1.Text = textBox1.Text.Replace('\n', ' ');
                    var _dataTable = new DataTable();
                    var _sqlDataAdapter = new SqlDataAdapter(query, sqlcon);
                    _sqlDataAdapter.Fill(_dataTable);
                    btnGridView.DataSource = _dataTable;
                    for (int i = 1; i < btnGridView.Columns.Count; i++)
                        btnGridView.Columns[i].HeaderText = btnGridView.Columns[i].HeaderText.Replace("_", " ");
                    //dataGridView1.Columns["Код"].ReadOnly = true;
                    //if (Convert.ToString(dataGridView1.Rows[0].Cells[0]) == "")
                    // MessageBox.Show("empty");
                    btnGridView.Columns["Код"].Visible = false;
                    textBox1.Text = string.Empty;
                    query = "";


                }
            }
            else
                gridSearch = false;
        }

       
    }
}
