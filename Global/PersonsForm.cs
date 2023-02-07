using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace gai
{
    public partial class PersonsForm : Form
    {
        public string TableName;

        private void PersonsForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog; 
        }
        public PersonsForm(string TableName)
        {
            InitializeComponent();
            sqlcon.Open();
            btnGridView.Visible = false;
            this.TableName = TableName;
            btnGridView_Click(null,null);     
            DateCheckBox.Visible = false; 

        }
        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");



        private void btnGridView_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            MessageBox.Show(TableName);
            string query = $"SELECT * FROM {TableName}";
            //string query = "select Сотрудники.Код, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Звания.Звание, Должности.Должность from Сотрудники, Звания, Должности, История where История.Код_должности = Должности.Код and История.Код_звания = Звания.Код and История.Код_сотрудника = Сотрудники.Код";
            var _dataTable = new DataTable();
            var _sqlDataAdapter = new SqlDataAdapter(query, sqlcon);
            _sqlDataAdapter.Fill(_dataTable);
            dataGridView1.DataSource = _dataTable;
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].HeaderText = dataGridView1.Columns[i].HeaderText.Replace("_", " ");
            //dataGridView1.Columns["Код"].ReadOnly = true;
            dataGridView1.Columns["Код"].Visible = false;
        }

        private void History_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFullTable = new ViewFormFullTable("История", "PersonsForm");
            this.Hide();
            objViewFormFullTable.ShowDialog();
            this.Show();
        }

        private void addRank_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFullTable = new ViewFormFullTable("Звания", "PersonsForm");
            this.Hide();
            objViewFormFullTable.ShowDialog();
            this.Show();

        }

        private void addPost_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFullTable = new ViewFormFullTable("Должности", "PersonsForm");
            this.Hide();
            objViewFormFullTable.ShowDialog();
            this.Show();
        }

        public string SearchTableName = "Имя";
        public Type typeCell;
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SearchTableName = dataGridView1.SelectedCells[0].OwningColumn.HeaderText;
        }

        #region oldTextBox
        /*
        string query;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Update");
                btnGridView_Click(null, null);
                return;
            }
            if (gridSearch == true)
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = null;

                if (SearchTableName.Contains("Дата") || SearchTableName.Contains("дата"))
                    DateCheckBox.Checked = true;

                if (DateCheckBox.Checked == true)
                {
                    query = $"SELECT * FROM {TableName} where {TableName}.[{SearchTableName}]  = '{textBox1.Text.Replace('.', '-')}'";
                    MessageBox.Show(query);
                }
                else
                    query = $"SELECT * FROM {TableName} where {TableName}.[{SearchTableName}]  = {textBox1.Text}";
                var _dataTable = new DataTable();
                var _sqlDataAdapter = new SqlDataAdapter(query, sqlcon);
                _sqlDataAdapter.Fill(_dataTable);
                dataGridView1.DataSource = _dataTable;
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].HeaderText = dataGridView1.Columns[i].HeaderText.Replace("_", " ");
                //dataGridView1.Columns["Код"].ReadOnly = true;
                //if (Convert.ToString(dataGridView1.Rows[0].Cells[0]) == "")
                   // MessageBox.Show("empty");
                dataGridView1.Columns["Код"].Visible = false;
                /*
            }
        }
        

        Boolean gridSearch = false; 
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gridSearch = true;
            }
            else
                gridSearch = false;
        }
        */
        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = null;

                    if (SearchTableName.Contains("Дата") || SearchTableName.Contains("дата"))
                        DateCheckBox.Checked = true;

                    if (DateCheckBox.Checked == true)
                    {
                        textBox1.Text = textBox1.Text.Replace('\r', ' ');
                        textBox1.Text = textBox1.Text.Replace('\n', ' ');
                        query = $"SELECT * FROM {TableName} where {TableName}.[{SearchTableName}]  = '{textBox1.Text.Replace('.', '-')}'";
                        MessageBox.Show(query);
                    }
                    else
                    {
                        int numtype = 1;
                        if (dataGridView1.SelectedCells.GetType() != numtype.GetType())
                        {
                            MessageBox.Show("String");
                            query = $"SELECT * FROM {TableName} where {TableName}.[{SearchTableName}]  = '{textBox1.Text}'";
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
                    dataGridView1.DataSource = _dataTable;
                    for (int i = 1; i < dataGridView1.Columns.Count; i++)
                        dataGridView1.Columns[i].HeaderText = dataGridView1.Columns[i].HeaderText.Replace("_", " ");
                    //dataGridView1.Columns["Код"].ReadOnly = true;
                    //if (Convert.ToString(dataGridView1.Rows[0].Cells[0]) == "")
                    // MessageBox.Show("empty");
                    dataGridView1.Columns["Код"].Visible = false;
                    textBox1.Text = string.Empty;
                    query = "";
                    
                
                }
            }
            else
                gridSearch = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            sqlcon.Close();
            this.Close();
        }

        private void btnEditPersons_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFullTable = new ViewFormFullTable("Сотрудники", "PersonsForm");
            this.Hide();
            objViewFormFullTable.ShowDialog();
            this.Show();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnBack_Click(null, null);
        }

        
    }
}
