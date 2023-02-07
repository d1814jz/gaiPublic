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
    public partial class DtpFormNew : Form
    {
        public string TableName;
        public DtpFormNew(string TableName)
        {
            InitializeComponent();
            sqlcon.Open();
            btnGridView.Visible = false;
            this.TableName = TableName;
            btnGridView_Click(null,null);     
            DateCheckBox.Visible = false;
            label1.Visible = false;
            textBox1.Visible = false;
            label2.Visible = false;
            btnGridView.Visible = false;
        }
        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");



        private void btnGridView_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            MessageBox.Show(TableName);
      
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            //string query = $"select ДТП.Код as [Код ДТП], Районы.Район ,Улицы.Улица, Сотрудники.Имя as [Имя сотрудника], Сотрудники.Фамилия as [Фамилия сотрудника], Сотрудники.Отчество as [Сотрудник отчество] ,Водители.Имя as [Имя водителя], Водители.Фамилия as [Фамилия водителя], Водители.Отчество as [Отчество водителя], Участники_ДТП.Виновник from ДТП, Улицы, Сотрудники, Участники_ДТП, Водители, Районы where Участники_ДТП.Код_ДТП = ДТП.Код and ДТП.Код_улицы = Улицы.Код and Улицы.Код_района = Районы.Код and Участники_ДТП.Код_водителя = Водители.Код and ДТП.Код_сотрудника = Сотрудники.Код";
            //string query = $"select * from ДТП";
            string query = $"select Дтп.Код, Районы.Район ,Улицы.Улица, Сотрудники.Имя as [Имя сотрудника], Сотрудники.Фамилия as [Фамилия сотрудника], Сотрудники.Отчество as [Сотрудник отчество] ,Водители.Имя as [Имя водителя], Водители.Фамилия as [Фамилия водителя], Водители.Отчество as [Отчество водителя], ДТП.Описание, ДТП.Дата, Участники_ДТП.Виновник from ДТП, Улицы, Сотрудники, Участники_ДТП, Водители, Районы where Участники_ДТП.Код_ДТП = ДТП.Код and ДТП.Код_улицы = Улицы.Код and Улицы.Код_района = Районы.Код and Участники_ДТП.Код_водителя = Водители.Код and ДТП.Код_сотрудника = Сотрудники.Код";

            var _dataTable = new DataTable();
            var _sqlDataAdapter = new SqlDataAdapter(query, sqlcon);
            _sqlDataAdapter.Fill(_dataTable);
            dataGridView1.DataSource = _dataTable;
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].HeaderText = dataGridView1.Columns[i].HeaderText.Replace("_", " ");
            dataGridView1.Columns["Код"].Visible = false;
            dataGridView1.ReadOnly = false ;
        }

  


        public string SearchTableName = "Имя";
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SearchTableName = dataGridView1.SelectedCells[0].OwningColumn.HeaderText;
            label2.Text = $"Столбец: {SearchTableName}";
            int index = (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value;
            MessageBox.Show(Convert.ToString(index));

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDrivers_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFullTable = new ViewFormFullTable("Водители","DtpFormNew");
            this.Hide();
            objViewFormFullTable.ShowDialog();
            this.Show();
        }

        private void bntAuto_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFullTable = new ViewFormFullTable("Автомобили", "DtpFormNew");
            this.Hide();
            objViewFormFullTable.ShowDialog();
            this.Show();
        }

        private void bntDtp_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFullTable = new ViewFormFullTable("ДТП", "DtpFormNew");
            this.Hide();
            objViewFormFullTable.ShowDialog();
            this.Show();
        }

        private void btnDtpPersons_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFullTable = new ViewFormFullTable("Участники_ДТП", "DtpFormNew");
            this.Hide();
            objViewFormFullTable.ShowDialog();
            this.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DtpAdd objDtpAdd = new DtpAdd();
            this.Hide();
            objDtpAdd.ShowDialog();
            this.Show();
            btnGridView_Click(null, null);
        }

        private void bntDelete_Click(object sender, EventArgs e)
        {

            Enabled = false;
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите удалить запись", "Delete", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
                return;
            int index = (int)dataGridView1.CurrentCell.Value;
            MessageBox.Show(Convert.ToString(index));


            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {

                //int index = (int)dataGridView1.CurrentCell.Value;
                //int index = (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value;
                //MessageBox.Show(Convert.ToString(index));

                string query = $"Delete  from {TableName}  Where Код = {index}";
                SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);

                SqlCommand command = new SqlCommand(query, sqlcon);
                SqlDataReader reader = command.ExecuteReader();

                reader.Close();
            }

            Enabled = true;
            btnGridView_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DtpPeopleAdd objDtpAddPeople = new DtpPeopleAdd();
            this.Hide();
            objDtpAddPeople.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*DtpFormEdit objDtpFormEdit = new DtpFormEdit((int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
            this.Hide();
            objDtpFormEdit.ShowDialog();
            this.Show();*/
        }
    }
}
