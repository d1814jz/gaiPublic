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

namespace gai
{
    public partial class IllegalForm : Form
    {
        public string TableName;
        public IllegalForm(string TableName)
        {
            InitializeComponent();
            sqlcon.Open();
            btnGridView.Visible = false;
            this.TableName = TableName;
            btnGridView_Click(null,null);     
            DateCheckBox.Visible = false;
            dataGridView1.ReadOnly = true;
            label2.Visible = false;
            textBox1.Visible = false;
            label1.Visible = false;
            bntAuto.Visible = false;
            bntDrivers.Visible = false;

        }
        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");



        private void btnGridView_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            MessageBox.Show(TableName);
            //string query = $"SELECT * FROM {TableName}";
            //string query = $"select Нарушения.Код_вида, Нарушения.Код_дежурства, Автомобили.Марка, Автомобили.Модель, Водители.Фамилия, Водители.Имя, Водители.Отчество, Нарушения.Место, Нарушения.Описание from Нарушения, Водители, Автомобили where Нарушения.Код_водителя = Водители.Код and Автомобили.Код = Нарушения.Код_авто";
            //string query = $"select Нарушения.Код, Виды_нарушений.Вид_нарушения as [Вид нарушения], Дежурство.Дата as [Дата нарушения], Автомобили.Марка, Автомобили.Модель, Водители.Фамилия, Водители.Имя, Водители.Отчество , Нарушения.Место, Нарушения.Описание from Нарушения, Водители, Автомобили, Виды_нарушений, Дежурство where Нарушения.Код_водителя = Водители.Код and Автомобили.Код = Нарушения.Код_авто and Нарушения.Код_вида = Виды_нарушений.Код and Нарушения.Код_дежурства = Дежурство.Код";
            string query = $"select Нарушения.Код, Виды_нарушений.Вид_нарушения as [Вид нарушения], Дежурство.Дата as [Дата нарушения], Автомобили.Марка, Автомобили.Модель, Водители.Фамилия, Водители.Имя, Водители.Отчество , Нарушения.Место, Нарушения.Описание from Нарушения, Водители, Автомобили, Виды_нарушений, Дежурство where Нарушения.Код_водителя = Водители.Код and Автомобили.Код = Нарушения.Код_авто and Нарушения.Код_вида = Виды_нарушений.Код and Нарушения.Код_дежурства = Дежурство.Код";

            var _dataTable = new DataTable();
            var _sqlDataAdapter = new SqlDataAdapter(query, sqlcon);
            _sqlDataAdapter.Fill(_dataTable);
            dataGridView1.DataSource = _dataTable;
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].HeaderText = dataGridView1.Columns[i].HeaderText.Replace("_", " ");
            //dataGridView1.Columns["Код"].ReadOnly = true;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.Columns["Код"].Visible = false;
  
        }




        public string SearchTableName = "Имя";
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SearchTableName = dataGridView1.SelectedCells[0].OwningColumn.HeaderText;
            MessageBox.Show(SearchTableName);
            label2.Text = $"Столбец: {SearchTableName}";
        }
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
                    SearchTableName = SearchTableName.Replace(" ", "_");
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = null;

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
                        if (dataGridView1.SelectedCells.GetType() != numtype.GetType())
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

        private void bntAuto_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFullTable = new ViewFormFullTable("Автомобили", "IllegalForm");
            this.Hide();
            objViewFormFullTable.ShowDialog();
            this.Show();
        }

        private void bntDrivers_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFullTable = new ViewFormFullTable("Водители","IllegalForm");
            this.Hide();
            objViewFormFullTable.ShowDialog();
            this.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFullTable = new ViewFormFullTable("Нарушения","IllegalForm");
            this.Hide();
            objViewFormFullTable.ShowDialog();
            this.Show();
            btnGridView_Click(null, null);
            
        }

        private void bntViewIleg_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFullTable = new ViewFormFullTable("Виды_нарушений", "IllegalForm");
            this.Hide();
            objViewFormFullTable.ShowDialog();
            this.Show();
            btnGridView_Click(null, null);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnBack_Click(null, null);
        }
    }
}
