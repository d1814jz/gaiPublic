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
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;

namespace gai
{
    public partial class GridSelectItem : Form
    {
        private int _startRow = -1;
        private IEnumerable<DataGridViewCellCollection> _rowsToAdd
            = Enumerable.Empty<DataGridViewCellCollection>();
        public string TableName;
        public string queryLoad = string.Empty;

        public int ReturnValue { get; set; }
        public GridSelectItem(string TableName)
        {
            InitializeComponent();
            this.TableName = TableName;
            label1.Visible = false;
            textBox1.Visible = false;
            DateCheckBox.Visible = false; 
            DateCheckBox.Visible = false;
            bntGridView.Visible = false;
            sqlcon.Open();
        }

        private void GridSelectItem_Load(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
             btnGridView_Click(null, null);

            /*btnDelete.Visible = false;
            button1.Visible = false;
            button2.Visible = false;*/
        }
        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        // SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        private void btnBack_Click(object sender, EventArgs e)
        {
            if ((int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value == 0)
            {
                MessageBox.Show("Нужно выбрать данные!");
                return;

            }
            sqlcon.Close();
            this.Close();
        }

        private void comboBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            /*DataTable schemaTable = sqlcon.GetSchema("Tables");
            foreach (DataRow row in schemaTable.Rows)
            {
                comboBox1.Items.Add(row["TABLE_NAME"].ToString());
            }*/
            /*comboBox1.Items.Add("Автомобили");
            comboBox1.Items.Add("Водители");
            comboBox1.Items.Add("Звания");
            comboBox1.Items.Add("Должности");
            comboBox1.Items.Add("Районы");
            comboBox1.Items.Add("Виды_поощрений_и_взысканий");
            comboBox1.Items.Add("Виды_точек_дежурства");
            comboBox1.Items.Add("Виды_нарушений");
            comboBox1.Items.Add("Служебный_автомобиль");
            comboBox1.Items.Add("Улицы");*/
        }

        private void btnGridView_Click(object sender, EventArgs e)
        {

            if (TableName == null)
                return;
            if (TableName == "Виды_поощрений_и_взысканий" || TableName == "Виды_нарушений" || TableName == "Участники_ДТП")
            {
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            }

            queryLoad = "SELECT * FROM " + TableName;
            switch (TableName)
            {
                case "Улицы":
                    queryLoad = $"select Улицы.Код, Районы.Район, Улицы.Улица from Улицы, Районы where Районы.Код = Улицы.Код";
                    break;
                case "Дежуство":
                    queryLoad = $"select Дежурство.Код, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Служебный_автомобиль.Марка, Служебный_автомобиль.Модель, Служебный_автомобиль.Номер, Точки_дежурства.Начальаня_точка,Точки_дежурства.Конечная_точка,Дежурство.Дата, Дежурство.Место from Дежурство, Служебный_автомобиль, Точки_дежурства, Сотрудники where Дежурство.Код_служебного_автомобиля = Служебный_автомобиль.Код and Дежурство.Код_сотрудника = Сотрудники.Код and Дежурство.Код_точки = Точки_дежурства.Код";
                    break;
                case "Учет":
                    queryLoad = $"SELECT Автомобили.Код, Сотрудники.Фамилия as [Фамилия сотрудника], Сотрудники.Имя as [Имя сотрудника],Сотрудники.Отчество as [Отчество сотрудника], Водители.Фамилия as [Фамилия владельца], Водители.Имя as [Имя владельца], Водители.Отчество as [Отчество владельца], Автомобили.Марка, Автомобили.Модель, Учет.Номер, Учет.Дата_постановки, Учет.Дата_снятия FROM Учет, Сотрудники, Водители, Автомобили where Учет.Код_сотрудника = Сотрудники.Код and Учет.Код_водителя = Водители.Код and Учет.Код_авто = Автомобили.Код";
                    break;
                case "ДТП":
                    queryLoad = $"select Дтп.Код, Районы.Район ,Улицы.Улица, Сотрудники.Имя as [Имя сотрудника], Сотрудники.Фамилия as [Фамилия сотрудника], Сотрудники.Отчество as [Сотрудник отчество] ,Водители.Имя as [Имя водителя], Водители.Фамилия as [Фамилия водителя], Водители.Отчество as [Отчество водителя], ДТП.Описание, ДТП.Дата, Участники_ДТП.Виновник from ДТП, Улицы, Сотрудники, Участники_ДТП, Водители, Районы where Участники_ДТП.Код_ДТП = ДТП.Код and ДТП.Код_улицы = Улицы.Код and Улицы.Код_района = Районы.Код and Участники_ДТП.Код_водителя = Водители.Код and ДТП.Код_сотрудника = Сотрудники.Код";
                    break;
                case "Точки_дежурства":
                    queryLoad = $"select Точки_дежурства.Код, Виды_точек_дежурства.Вид_точки, Точки_дежурства.Начальаня_точка, Точки_дежурства.Конечная_точка from Точки_дежурства, Виды_точек_дежурства where Точки_дежурства.Код_вида = Виды_точек_дежурства.Код";
                    break;
                case "Участники_ДТП":
                    queryLoad = $"select Дтп.Код, Районы.Район ,Улицы.Улица, Сотрудники.Имя as [Имя сотрудника], Сотрудники.Фамилия as [Фамилия сотрудника], Сотрудники.Отчество as [Сотрудник отчество] ,Водители.Имя as [Имя водителя], Водители.Фамилия as [Фамилия водителя], Водители.Отчество as [Отчество водителя], ДТП.Описание, ДТП.Дата, Участники_ДТП.Виновник from ДТП, Улицы, Сотрудники, Участники_ДТП, Водители, Районы where Участники_ДТП.Код_ДТП = ДТП.Код and ДТП.Код_улицы = Улицы.Код and Улицы.Код_района = Районы.Код and Участники_ДТП.Код_водителя = Водители.Код and ДТП.Код_сотрудника = Сотрудники.Код";
                    break;
                case "Нарушения":
                    queryLoad = $"select Нарушения.Код, Виды_нарушений.Вид_нарушения as [Вид нарушения], Дежурство.Дата as [Дата нарушения], Автомобили.Марка, Автомобили.Модель, Водители.Фамилия, Водители.Имя, Водители.Отчество , Нарушения.Место, Нарушения.Описание from Нарушения, Водители, Автомобили, Виды_нарушений, Дежурство where Нарушения.Код_водителя = Водители.Код and Автомобили.Код = Нарушения.Код_авто and Нарушения.Код_вида = Виды_нарушений.Код and Нарушения.Код_дежурства = Дежурство.Код";
                    //queryLoad = $"select Виды_нарушений.Код, Виды_нарушений.Вид_нарушения as [Вид нарушения], Дежурство.Дата as [Дата нарушения], Автомобили.Марка, Автомобили.Модель, Водители.Фамилия, Водители.Имя, Водители.Отчество , Нарушения.Место, Нарушения.Описание from Нарушения, Водители, Автомобили, Виды_нарушений, Дежурство where Нарушения.Код_водителя = Водители.Код and Автомобили.Код = Нарушения.Код_авто and Нарушения.Код_вида = Виды_нарушений.Код and Нарушения.Код_дежурства = Дежурство.Код";

                    break;
                case "История":
                    queryLoad = $"select Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Звания.Звание, Должности.Должность from Сотрудники, Звания, Должности, История where История.Код_должности = Должности.Код and История.Код_звания = Звания.Код and История.Код_сотрудника = Сотрудники.Код";
                    break;
                case "Поощрения_и_взыскания":
                    queryLoad = $"select Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Виды_поощрений_и_взысканий.Вид, Поощрения_и_взыскания.Дата, Поощрения_и_взыскания.Описание from Поощрения_и_взыскания, Виды_поощрений_и_взысканий, Сотрудники where Сотрудники.Код = Поощрения_и_взыскания.Код_сотрудника and Виды_поощрений_и_взысканий.Код = Поощрения_и_взыскания.Код_вида";
                    break;


            }

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            
            var _dataTable = new DataTable();
            var _sqlDataAdapter = new SqlDataAdapter(queryLoad, sqlcon);
            _sqlDataAdapter.Fill(_dataTable);
            dataGridView1.DataSource = _dataTable;
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].HeaderText = dataGridView1.Columns[i].HeaderText.Replace("_", " ");
            //dataGridView1.Columns["Код"].ReadOnly = true;
            dataGridView1.Columns["Код"].Visible = false;

            #region rename
            /*if (TableName == "Автомобили")
            {
                List<string> Автомобили = new List<string> { "Марка", "Модель", "Год выпуска", "Vin номер" };
                for (int i = 0; i < Автомобили.Count; i++)
                    dataGridView1.Columns[i + 1].HeaderText = Автомобили[i];
            }
            if (TableName == "Водители")
            {
                List<string> Водители = new List<String> { "Фамилия", "Имя", "Отчество", "Адрес", "Номер ВУ" };
                for (int i = 0; i < Водители.Count; i++)
                    dataGridView1.Columns[i + 1].HeaderText = Водители[i];
            }
           
            if (TableName == "Виды_точек_дежурства")
            {
                List<string> Виды_точек_дежурства = new List<string> { "Вид точки" };
                for (int i = 0; i < Виды_точек_дежурства.Count; i++)
                    dataGridView1.Columns[i + 1].HeaderText = Виды_точек_дежурства[i];
            }

            if (TableName == "Виды_нарушений")
            {
                List<string> Виды_нарушений = new List<string> { "Вид нарушения", "Штраф", "Предупреждение", "Срок лишения" };
                for (int i = 0; i < Виды_нарушений.Count; i++)
                    dataGridView1.Columns[i + 1].HeaderText = Виды_нарушений[i];
            }
            */
            #endregion

            //Призапуске
            MessageBox.Show(TableName);
            if (TableName == "Автомобили")
                    dataGridView1.Columns["Код"].Visible = false;
        }

        private int _colId = 0;

        private void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                //int index = (int)dataGridView1.CurrentCell.Value;
                int index = (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value;
                MessageBox.Show(Convert.ToString(index));
                //int index = (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value
                var newVal = row.Cells[_colId].Value;
                string colName = dataGridView1.Columns[_colId].HeaderText.Replace(" ", "_");
                string query = $"Update {TableName} set {colName} = {(newVal is string ? $"\'{newVal}\'" : newVal)} where Код = {index}";
                MessageBox.Show(query);
                SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);

                SqlCommand command = new SqlCommand(query, sqlcon);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }
        public virtual string[] DataKeyNames { get; set; }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (TableName == null)
                return;

            Enabled = false;
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите удалить запись", "Delete", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
                return;
            //int index = (int)dataGridView1.CurrentCell.Value;
            

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {

                //int index = (int)dataGridView1.CurrentCell.Value;
                int index = (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value;
                MessageBox.Show(Convert.ToString(index));

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
            _colId = e.ColumnIndex;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
               button1_Click(sender, e);
                //button1_Click(null, null);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //_colId = e.ColumnIndex;
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            _colId = e.ColumnIndex;
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.ReturnValue = (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value;
           
            btnBack_Click(null, null);
        }

        private void GridSelectItem_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.F2):
                    btnBack_Click(null, null);
                    break;
                case (Keys.F1):
                    btnSelect_Click(null, null);
                    break;
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
