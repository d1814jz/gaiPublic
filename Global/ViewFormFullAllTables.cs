using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace gai
{

    public partial class ViewFormAllTables : Form
    {
        private int _startRow = -1;
        private IEnumerable<DataGridViewCellCollection> _rowsToAdd
            = Enumerable.Empty<DataGridViewCellCollection>();
        public string TableName;
        public string FormName;
        public string querySw = string.Empty;
        public List<string> queryList = new List<string>();
        public List<string> namesSpr = new List<string>() { "Автомобили", "Водители", "Звания", "Должности", "Районы", "Виды_поощрений_и_взысканий", "Виды_точек_дежурства", "Виды_нарущений", "Служебный_автомобиль" };
        public string queryLoad = string.Empty;
        public Form obj = null; 


        public ViewFormAllTables()
        {
            InitializeComponent();
            DateCheckBox.Visible = false;
            
            sqlcon.Open();
        }

        private void ViewFormAllTables_Load(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
           
            // btnGridView_Click(null, null);
        }
        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        // SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


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
            this.Enabled = true;
            if (TableName == null)
                return;
            if (TableName == "Виды_поощрений_и_взысканий" || TableName == "Виды_нарушений" || TableName == "Участники_ДТП")
            {
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            }
            string queryLoad = "SELECT * FROM " + TableName;
            switch (TableName)
            {
                case "Улицы":
                    queryLoad = $"select Улицы.Код, Районы.Район, Улицы.Улица from Улицы, Районы where Районы.Код = Улицы.Код";
                    break;
                case "Дежурство":
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
                    queryLoad = $"select Виды_нарушений.Код, Виды_нарушений.Вид_нарушения as [Вид нарушения], Дежурство.Дата as [Дата нарушения], Автомобили.Марка, Автомобили.Модель, Водители.Фамилия, Водители.Имя, Водители.Отчество , Нарушения.Место, Нарушения.Описание from Нарушения, Водители, Автомобили, Виды_нарушений, Дежурство where Нарушения.Код_водителя = Водители.Код and Автомобили.Код = Нарушения.Код_авто and Нарушения.Код_вида = Виды_нарушений.Код and Нарушения.Код_дежурства = Дежурство.Код";
                    break;
                case "История":
                    queryLoad = $"select История.Код, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Звания.Звание, Должности.Должность from Сотрудники, Звания, Должности, История where История.Код_должности = Должности.Код and История.Код_звания = Звания.Код and История.Код_сотрудника = Сотрудники.Код";
                    break;
                case "Поощрения_и_взыскания":
                    queryLoad = $"select Поощрения_и_взыскания.Код, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Виды_поощрений_и_взысканий.Вид, Поощрения_и_взыскания.Дата, Поощрения_и_взыскания.Описание from Поощрения_и_взыскания, Виды_поощрений_и_взысканий, Сотрудники where Сотрудники.Код = Поощрения_и_взыскания.Код_сотрудника and Виды_поощрений_и_взысканий.Код = Поощрения_и_взыскания.Код_вида";
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

            //Призапуске
            //--MessageBox.Show(TableName);
        }

        private int _colId = 0;

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int index = (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value;
            switch (TableName)
            {
                case "Дежуство":
                    obj = new DutyEdit(index);
                    break;
                case "Учет":
                    obj = new AccountingEdit(index);
                    break;
                case "ДТП":
                    obj = new DtpEdit(index);
                    break;
                case "Точки_дежурства":
                    obj = new DutyDotsEdit(index);
                    break;
                case "Участники_ДТП":
                    obj = new DtpPeopleEdit(index);
                    break;
                case "Нарушения":
                    obj = new IllegalEdit(index);
                    break;
                case "История":
                    obj = new HistoryEdit(index);
                    break;
                case "Поощрения_и_взыскания":
                    obj = new RewardsEdit(index);
                    break;


            }
            if (obj != null)
            {
                MessageBox.Show(Convert.ToString(obj));
                obj.Show();
                return;
            }
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                //int index = (int)dataGridView1.CurrentCell.Value;
                index = (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value;
                //--MessageBox.Show(Convert.ToString(index));
                //int index = (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value
                var newVal = row.Cells[_colId].Value;
                string colName = dataGridView1.Columns[_colId].HeaderText.Replace(" ", "_");
                string query = $"Update {TableName} set {colName} = {(newVal is string ? $"\'{newVal}\'" : newVal)} where Код = {index}";
                //--MessageBox.Show(query);
                SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);

                SqlCommand command = new SqlCommand(query, sqlcon);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
            MessageBox.Show("update");
        }
        public virtual string[] DataKeyNames { get; set; }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (TableName == null)
                return;

            Enabled = false;
            string question = "Вы действительно хотите удалить запись?";
            if (namesSpr.Contains(TableName))
                question = "Вы действительно хотите удалить запись. Запись также удалится из дочерних таблиц!";
            DialogResult dialogResult = MessageBox.Show(question, "Delete", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
                return;
            int index = (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value;
            //int index = (int)dataGridView1.CurrentCell.Value;
            MessageBox.Show(Convert.ToString(index));

            #region switch
            /*switch (TableName)
            {
                case "Районы": //удалить улицы
                    querySw = $"Delete from Улицы Where Код_района = {index}";
                    break;
                case "Сотрудники": {}//удалаить историю, учет, поощрение, дтп, дежурство
                    queryList = new List<string>();
                    queryList.Add($"Delete  from История  Where Код_сотрудника = {index}");
                    queryList.Add($"Delete  from Учет Where Код_сотрудника = {index}");
                    queryList.Add($"Delete  from Поощрения_и_взыскания  Where Код_сотрудника = {index}");
                    queryList.Add($"Delete  from ДТП  Where Код_сотрудника = {index}");
                    queryList.Add($"Delete  from Дежурство  Where Код_сотрудника = {index}");
                    break;
                case "Водители": //учет, нарушения, участники дтп
                    queryList = null;
                    queryList.Add($"Delete  from Учет Where Код_водителя = {index}");
                    queryList.Add($"Delete  from Нарушения Where Код_водителя = {index}");
                    queryList.Add($"Delete  from Участники_ДТП Where Код_водителя = {index}");
                    break;
                case "Виды нарушений": //Нарушения, 
                    querySw = $"Delete  from Нарушения Where Код_вида = {index}";
                    break;
                case "Виды точек дежурства": //Точки дежурства
                    querySw = $"Delete  from Точки_дежурства Where Код_точки = {index}";
                    break;
                case "Автомобили": //Учет, нарушения, участники дтп
                    queryList = null;
                    queryList.Add($"Delete from Учет Where Код_автомобиля= {index}");
                    queryList.Add($"Delete from Нарушения Where Код_автомобиля= {index}");
                    queryList.Add($"Delete from Участники_ДТП Where Код_автомобиля= {index}");
                    break;
                case "Звания": //историю
                    querySw = $"Delete from История Where Код_звания= {index}";
                    break;
                case "Должности": //История
                    querySw = $"Delete from Должности Where Код_должности= {index}";
                    break;
                
            }
            if(querySw != string.Empty)
            {
                MessageBox.Show(querySw);
                var _SqlDataAdapter = new SqlDataAdapter(querySw, sqlcon);
                var _SqlCommand = new SqlCommand(querySw, sqlcon);      
                var _SqlDataReader = _SqlCommand.ExecuteReader();
                _SqlDataReader.Close();
                MessageBox.Show("Удалено из дочерних");
            }
            else
            {            
                for (int i = 0; i<queryList.Count; i++)
                {
                    SqlDataAdapter sda = new SqlDataAdapter(queryList[i], sqlcon);
                    SqlCommand command = new SqlCommand(queryList[i], sqlcon);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                }
                MessageBox.Show("Удалено из дочерних");
            }
             */
            #endregion

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {

                //int index = (int)dataGridView1.CurrentCell.Value;
                //int index = (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value; перенес вышеь 
                //--MessageBox.Show(Convert.ToString(index));

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
            if (!namesSpr.Contains(TableName))
            {
                switch (TableName)
                {
                    case "Дежуство":
                        obj = new DutyAdd();
                        break;
                    case "Учет":
                        obj = new AccountingAdd();
                        break;
                    case "ДТП":
                        obj = new DtpAdd();
                        break;
                    case "Точки_дежурства":
                        obj = new DutyDotsAdd();
                        break;
                    case "Участники_ДТП":
                        obj = new DtpPeopleAdd();
                        break;
                    case "Нарушения":
                        obj = new IllegalAdd();
                        break;
                    case "История":
                        obj = new HistoryAdd();
                        break;
                    case "Поощрения_и_взыскания":
                        obj = new RewardsAdd();
                        break;


                }
                this.Hide();
                obj.ShowDialog();
                this.Show();
                btnGridView_Click(null, null);
                this.enabled = true; 
            }
            else
            {
                _rowsToAdd = _rowsToAdd.Append(dataGridView1.Rows[_rowsToAdd.First()[0].RowIndex - 1].Cells);
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
            TableName = comboBox1.SelectedItem.ToString().Replace(" ","_");
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
            //--MessageBox.Show(SearchTableName);
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
                    //--MessageBox.Show("Update");
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
                        //--MessageBox.Show(query);
                    }
                    else
                    {
                        int numtype = 1;
                        if (dataGridView1.SelectedCells.GetType() != numtype.GetType())
                        {
                            //--MessageBox.Show("String");
                            query = $"SELECT * FROM {TableName} where {TableName}.[{SearchTableName}]  like '%{textBox1.Text}%'";
                        }
                        else
                        {
                            //--MessageBox.Show("number");
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
                    dataGridView1.Columns["Код"].Visible = true;
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
        public string TableNameCase;
        private bool enabled;

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnBack_Click(null, null);
            if (e.KeyCode == Keys.F1 && dataGridView1.SelectedCells[0].OwningColumn.HeaderText.Contains("Код"))
            {
                string Name = dataGridView1.SelectedCells[0].OwningColumn.HeaderText;
                TableName = comboBox1.SelectedItem.ToString().Replace(" ","_");
                //--MessageBox.Show(TableName);
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
                            TableNameCase = "Виды_поощрений_и_взысканий";
                        if (TableName == "Нарушения")
                            TableNameCase = "Виды_нарушений";
                        if (TableName == "Точки_дежурства")
                            TableNameCase = "Виды_точек_дежурства";
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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
