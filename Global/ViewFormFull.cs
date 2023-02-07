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
    public partial class ViewFormFull : Form
    {
        private int _startRow = -1;
        private IEnumerable<DataGridViewCellCollection> _rowsToAdd
            = Enumerable.Empty<DataGridViewCellCollection>();
        public string TableName;

        public ViewFormFull()
        {
            InitializeComponent();
            
            sqlcon.Open();
        }

        private void ViewFormFull_Load(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
            DateCheckBox.Visible = false;
        }
        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        private void btnBack_Click(object sender, EventArgs e)
        {
            sqlcon.Close();
            this.Close();
        }

        private void comboBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            DataTable schemaTable = sqlcon.GetSchema("Tables");
            foreach (DataRow row in schemaTable.Rows)
            {
                comboBox1.Items.Add(row["TABLE_NAME"].ToString().Replace("_", " "));
                
            }
            comboBox1.Items.Remove("Log");
            
        }

        private void btnGridView_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem == null)
                return;
            TableName = comboBox1.SelectedItem.ToString().Replace(" ","_");
            if (TableName == "Виды_поощрений_и_взысканий" || TableName == "Виды_нарушений" || TableName == "Участники_ДТП")
            {
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            }

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            string query = "SELECT * FROM " + TableName;
            var _dataTable = new DataTable();
            var _sqlDataAdapter = new SqlDataAdapter(query, sqlcon);
            _sqlDataAdapter.Fill(_dataTable);
            dataGridView1.DataSource = _dataTable;
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].HeaderText = dataGridView1.Columns[i].HeaderText.Replace("_", " ");
            dataGridView1.Columns["Код"].ReadOnly = true;

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int index = (int)dataGridView1.CurrentCell.Value;
                string query = $"Update {TableName} set";
                SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);

                SqlCommand command = new SqlCommand(query, sqlcon);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (TableName == null)
                return;

            Enabled = false;
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите удалить запись", "Delete", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
                return;

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int index = (int)dataGridView1.CurrentCell.Value;
                string query = $"Delete  from {TableName}  Where Код = {index}";
                SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);

                SqlCommand command = new SqlCommand(query, sqlcon);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
            
            Enabled = true;
            btnGridView_Click(null, null);
        }


        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (TableName != null && _rowsToAdd.Count() != 0)
                button1_Click(null, null);
            btnGridView_Click(null, null);
            
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _startRow = _startRow == -1 ? e.RowIndex : _startRow;
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            _rowsToAdd = _rowsToAdd.Append(e.Row.Cells);
        }

        private bool Check(DataGridViewCellCollection cells)
        {
            foreach (DataGridViewCell cell in cells)
                if (cell.Value != null)
                    return true;

            return false;
        }

        //// добавление
        private void button1_Click(object sender, EventArgs e)
        {
            Enabled = false;

            _rowsToAdd = _rowsToAdd.Append(dataGridView1.Rows[_rowsToAdd.First()[0].RowIndex -1].Cells);

            string query = $"insert into {TableName} values \n";

            foreach (var row in _rowsToAdd)
            {
                //скип поля "Код"
                if (!Check(row))
                    continue;
                query += "(";
                int i = 0;
                int numtype = 1;
                foreach (DataGridViewCell cell in row)
                {
                    if (i++ == 0)
                        continue;
                    if (cell.Value.GetType() == numtype.GetType())
                    {
                        query += $"{cell.Value ?? "null"}, ";
                        continue;
                    }
                    query += $"'{cell.Value ?? "null"}', ";
                }
                query = query.Remove(query.Length - 2, 2);
                query += "),\n";
            }

            query = query.Remove(query.Length - 2, 2);

            //MessageBox.Show(query);
            
            using (SqlCommand command = new SqlCommand(query, sqlcon) { CommandType = CommandType.Text })
            {

                using (var reader = command.ExecuteReader())
                    MessageBox.Show(reader.RecordsAffected.ToString());
            }

            Enabled = true;
            btnGridView_Click(null, null);

            _rowsToAdd = Enumerable.Empty<DataGridViewCellCollection>();

        }

        private void bntInf_Click(object sender, EventArgs e)
        {
            InformationForm objInformationForm = new InformationForm();
            this.Hide();
            objInformationForm.ShowDialog();
            this.Show();

        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
               button1_Click(sender, e);
                //button1_Click(null, null);
        }
        public string SearchTableName = "Имя";
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SearchTableName = dataGridView1.SelectedCells[0].OwningColumn.HeaderText;
            MessageBox.Show(SearchTableName);
            string query = $"Столбец: {SearchTableName}";
            label2.Text = query.Replace("_"," ");
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
        public string TableNameCase; 
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnBack_Click(null, null);            
            if (e.KeyCode == Keys.F1 && dataGridView1.SelectedCells[0].OwningColumn.HeaderText.Contains("Код"))
            {
                string Name = dataGridView1.SelectedCells[0].OwningColumn.HeaderText;

                switch (Name)
                {
                    case "Код сотрудника":
                        TableNameCase = "Сотрудники";
                        break;
                    case "Код служебного автомобиля":
                        TableNameCase = "Служебные_автомобили";
                        break;
                    case "Код точки":
                        TableNameCase = "Точки_дежурства";
                        break;
                    case "Код улицы":
                        TableNameCase = "Улицы";
                        break;
                    case "Код звания":
                        TableNameCase = "Звания";
                        break;

                    case "Код должности":
                        TableNameCase = "Должности";
                        break;
                    case "Код вида":
                        if (TableName == "Поощрения_и_взыскания")
                            TableNameCase = "Виды поощрений и взысканий";
                        if (TableName == "Нарушения")
                            TableNameCase = "Виды_нарушений";
                        break;
                    case "Код дежурства":
                        TableNameCase = "Дежурство";
                        break;
                    case "Код авто":
                        TableNameCase = "Автомобили";
                        break;
                    case "Код водителя":
                        TableNameCase = "Водители";
                        break;
                    case "Код района":
                        TableNameCase = "Районы";
                        break;
                    case "Код ДТП":
                        TableNameCase = "ДТП";
                        break;

                }


                //MessageBox.Show(Convert.ToString(dataGridView1.CurrentCell.Value));
                GridSelectItem objGridSelectItem = new GridSelectItem(TableNameCase);
                this.Hide();
                objGridSelectItem.ShowDialog();
                this.Show();
                if (objGridSelectItem.ReturnValue == 0)
                    return;
                dataGridView1.CurrentCell.Value = objGridSelectItem.ReturnValue;




            }

        }
    }
}
