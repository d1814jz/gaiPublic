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
using System.IO;
using System.Threading;

namespace gai
{
    public partial class SelectItemForReport : Form
    {
        private int _startRow = -1;
        private IEnumerable<DataGridViewCellCollection> _rowsToAdd
            = Enumerable.Empty<DataGridViewCellCollection>();
        public string TableName;
        public string path; 
        public string queryLoad = string.Empty;
        public string queryLoad2 = string.Empty;
        public string pathFrom = string.Empty;

        public int ReturnValue { get; set; }
        public SelectItemForReport(string TableName, String path)
        {
            InitializeComponent();
            this.TableName = TableName;
            label2.Visible = false;
            bntGridView.Visible = false;
            button1.Visible = false;
            dataGridView2.Visible = false;
            btnSelect2.Visible = false;
            
            sqlcon.Open();
        }

        private void SelectItemForReport_Load(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
            this.path = path;
            path = @"C:\User\Fafka\Desktop";
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
            if(TableName == "Дежурство")
                queryLoad = $"select Дежурство.Код, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество from Дежурство, Служебный_автомобиль, Точки_дежурства, Сотрудники where Дежурство.Код_служебного_автомобиля = Служебный_автомобиль.Код and Дежурство.Код_сотрудника = Сотрудники.Код and Дежурство.Код_точки = Точки_дежурства.Код";

            switch (TableName)
            {
                case "Учет":
                    //queryLoad = $"SELECT Автомобили.Код, Автомобили.Марка, Автомобили.Модель, Учет.Номер, Учет.Дата_постановки, Учет.Дата_снятия FROM Учет, Сотрудники, Водители, Автомобили where Учет.Код_сотрудника = Сотрудники.Код and Учет.Код_водителя = Водители.Код and Учет.Код_авто = Автомобили.Код";
                    queryLoad = $"SELECT Учет.Код, Автомобили.Марка, Автомобили.Модель, Автомобили.Vin_номер FROM Учет, Сотрудники, Водители, Автомобили where Учет.Код_сотрудника = Сотрудники.Код and Учет.Код_водителя = Водители.Код and Учет.Код_авто = Автомобили.Код";
                    break;
                case "Нарушения":
                    queryLoad = $"select Нарушения.Код, Виды_нарушений.Вид_нарушения as [Вид нарушения], Водители.Фамилия, Водители.Имя, Водители.Отчество from Нарушения, Водители, Автомобили, Виды_нарушений, Дежурство where Нарушения.Код_водителя = Водители.Код and Автомобили.Код = Нарушения.Код_авто and Нарушения.Код_вида = Виды_нарушений.Код and Нарушения.Код_дежурства = Дежурство.Код";
                    break;

                case "Дежуство":
                    queryLoad = $"select Дежурство.Код, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Точки_дежурства.Начальаня_точка,Точки_дежурства.Конечная_точка, Дежурство.Дата from Дежурство, Служебный_автомобиль, Точки_дежурства, Сотрудники where Дежурство.Код_служебного_автомобиля = Служебный_автомобиль.Код and Дежурство.Код_сотрудника = Сотрудники.Код and Дежурство.Код_точки = Точки_дежурства.Код";
                    break;
                case "ДТП":
                    queryLoad = $"select Дтп.Код, Районы.Район ,Улицы.Улица ,Водители.Имя as [Имя водителя], Водители.Фамилия as [Фамилия водителя], Водители.Отчество as [Отчество водителя] from ДТП, Улицы, Сотрудники, Участники_ДТП, Водители, Районы  where Участники_ДТП.Код_ДТП = ДТП.Код and ДТП.Код_улицы = Улицы.Код and Улицы.Код_района = Районы.Код  and Участники_ДТП.Код_водителя = Водители.Код and ДТП.Код_сотрудника = Сотрудники.Код";
                    //queryLoad = $"select Дтп.Код, Районы.Район ,Улицы.Улица, Сотрудники.Имя as [Имя сотрудника], Сотрудники.Фамилия as [Фамилия сотрудника], Сотрудники.Отчество as [Сотрудник отчество] ,Водители.Имя as [Имя водителя], Водители.Фамилия as [Фамилия водителя], Водители.Отчество as [Отчество водителя], Участники_ДТП.Виновник from ДТП, Улицы, Сотрудники, Участники_ДТП, Водители, Районы where Участники_ДТП.Код_ДТП = ДТП.Код and ДТП.Код_улицы = Улицы.Код and Улицы.Код_района = Районы.Код and Участники_ДТП.Код_водителя = Водители.Код and ДТП.Код_сотрудника = Сотрудники.Код";
                    //queryLoad = "select Дтп.Код, Районы.Район ,Улицы.Улица,Водители.Имя as [Имя водителя], Водители.Фамилия as [Фамилия водителя], Водители.Отчество as [Отчество водителя], ДТП.Дата from ДТП, Улицы, Сотрудники, Участники_ДТП, Водители, Районы, Автомобили, Звания, История where Участники_ДТП.Код_ДТП = ДТП.Код and ДТП.Код_улицы = Улицы.Код and Улицы.Код_района = Районы.Код and Участники_ДТП.Код_водителя = Водители.Код  and ДТП.Код_сотрудника = Сотрудники.Код and Участники_ДТП.Код_авто = Автомобили.Код and  История.Код_сотрудника = ДТП.Код_сотрудника and История.Код_звания = Звания.Код";
                    //queryLoad = $"select Сотрудники.Код, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество from Сотрудники, Звания, Должности, История where История.Код_должности = Должности.Код and История.Код_звания = Звания.Код and История.Код_сотрудника = Сотрудники.Код";
                    break;
            }


            if(TableName == "Дежурство")
                queryLoad2 = $"select Дежурство.Код, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Служебный_автомобиль.Марка, Служебный_автомобиль.Модель, Служебный_автомобиль.Номер, Точки_дежурства.Начальаня_точка,Точки_дежурства.Конечная_точка,Дежурство.Дата, Дежурство.Место from Дежурство, Служебный_автомобиль, Точки_дежурства, Сотрудники where Дежурство.Код_служебного_автомобиля = Служебный_автомобиль.Код and Дежурство.Код_сотрудника = Сотрудники.Код and Дежурство.Код_точки = Точки_дежурства.Код";
            #region laodgrid2
            /*
            switch (TableName)
            {
                case "Нарушения":
                    //queryLoad2 = $"select Нарушения.Код, Виды_нарушений.Вид_нарушения as [Вид нарушения], Дежурство.Дата as [Дата нарушения], Автомобили.Марка, Автомобили.Модель, Водители.Фамилия, Водители.Имя, Водители.Отчество , Нарушения.Место, Нарушения.Описание from Нарушения, Водители, Автомобили, Виды_нарушений, Дежурство where Нарушения.Код_водителя = Водители.Код and Автомобили.Код = Нарушения.Код_авто and Нарушения.Код_вида = Виды_нарушений.Код and Нарушения.Код_дежурства = Дежурство.Код and where Нарушения.Код = {dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value}";
                    queryLoad2 = $"select Нарушения.Код, Виды_нарушений.Вид_нарушения as [Вид нарушения], Дежурство.Дата as [Дата нарушения], Автомобили.Марка, Автомобили.Модель, Водители.Фамилия, Водители.Имя, Водители.Отчество , Нарушения.Место, Нарушения.Описание from Нарушения, Водители, Автомобили, Виды_нарушений, Дежурство where Нарушения.Код_водителя = Водители.Код and Автомобили.Код = Нарушения.Код_авто and Нарушения.Код_вида = Виды_нарушений.Код and Нарушения.Код_дежурства = Дежурство.Код";

                    break;
                case "Дежуство":
                    queryLoad2 = $"select Дежурство.Код, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Служебный_автомобиль.Марка, Служебный_автомобиль.Модель, Служебный_автомобиль.Номер, Точки_дежурства.Начальаня_точка,Точки_дежурства.Конечная_точка,Дежурство.Дата, Дежурство.Место from Дежурство, Служебный_автомобиль, Точки_дежурства, Сотрудники where Дежурство.Код_служебного_автомобиля = Служебный_автомобиль.Код and Дежурство.Код_сотрудника = Сотрудники.Код and Дежурство.Код_точки = Точки_дежурства.Код";
                    break;
                case "Учет":
                    queryLoad2 = $"SELECT Автомобили.Код, Сотрудники.Фамилия as [Фамилия сотрудника], Сотрудники.Имя as [Имя сотрудника],Сотрудники.Отчество as [Отчество сотрудника], Водители.Фамилия as [Фамилия владельца], Водители.Имя as [Имя владельца], Водители.Отчество as [Отчество владельца], Автомобили.Марка, Автомобили.Модель, Учет.Номер, Учет.Дата_постановки, Учет.Дата_снятия FROM Учет, Сотрудники, Водители, Автомобили where Учет.Код_сотрудника = Сотрудники.Код and Учет.Код_водителя = Водители.Код and Учет.Код_авто = Автомобили.Код";
                    break;
                case "Сотрудники":
                    queryLoad2 = $"select Сотрудники.Код, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Звания.Звание, Должности.Должность from Сотрудники, Звания, Должности, История where История.Код_должности = Должности.Код and История.Код_звания = Звания.Код and История.Код_сотрудника = Сотрудники.Код";
                    break;
            }
            */
            #endregion
            /*
             Select для word
            case "Дежуство":
                    queryLoad = $"select Дежурство.Код, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Служебный_автомобиль.Марка, Служебный_автомобиль.Модель, Служебный_автомобиль.Номер, Точки_дежурства.Начальаня_точка,Точки_дежурства.Конечная_точка,Дежурство.Дата, Дежурство.Место from Дежурство, Служебный_автомобиль, Точки_дежурства, Сотрудники where Дежурство.Код_служебного_автомобиля = Служебный_автомобиль.Код and Дежурство.Код_сотрудника = Сотрудники.Код and Дежурство.Код_точки = Точки_дежурства.Код";
                    break;
                case "Учет":
                    queryLoad = $"SELECT Автомобили.Код, Сотрудники.Фамилия as [Фамилия сотрудника], Сотрудники.Имя as [Имя сотрудника],Сотрудники.Отчество as [Отчество сотрудника], Водители.Фамилия as [Фамилия владельца], Водители.Имя as [Имя владельца], Водители.Отчество as [Отчество владельца], Автомобили.Марка, Автомобили.Модель, Учет.Номер, Учет.Дата_постановки, Учет.Дата_снятия FROM Учет, Сотрудники, Водители, Автомобили where Учет.Код_сотрудника = Сотрудники.Код and Учет.Код_водителя = Водители.Код and Учет.Код_авто = Автомобили.Код";
                    break;
                case "История":
                    queryLoad = $"select Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Звания.Звание, Должности.Должность from Сотрудники, Звания, Должности, История where История.Код_должности = Должности.Код and История.Код_звания = Звания.Код and История.Код_сотрудника = Сотрудники.Код";
                    break;
             */


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
            //MessageBox.Show(TableName);
            if (TableName == "Автомобили")
                    dataGridView1.Columns["Код"].Visible = false;
            //MessageBox.Show(queryLoad);
            

            
        }

        private int _colId = 0;

       


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

      

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            btnSelect2_Click(null, null);
            CreateDocument();
            this.ReturnValue = (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value;

            
        }

        private void CreateDocument()
        {
            //MessageBox.Show(path);
            btnSelect2_Click(null, null);
            //MessageBox.Show(Convert.ToString(dataGridView2.Rows[0].Cells[2].Value));
            int countColums = dataGridView2.RowCount - 1;
            
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; //endofdoc is a predefined bookmark
            object oTemplate = "c:\\MyTemplate.dot";
            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            //oWord.Visible = true;
            oWord.Visible = false;
            String documentName = string.Empty;
            switch (TableName)
            {
                case "Учет":
                    documentName = $"Автомобиль {dataGridView2.Rows[0].Cells[7].Value} {dataGridView2.Rows[0].Cells[8].Value} {dataGridView2.Rows[0].Cells[9].Value} {dataGridView2.Rows[0].Cells[13].Value} {dataGridView2.Rows[0].Cells[12].Value}";                   
                        break;
                case "Нарушения":
                    documentName = $"Нарушение {dataGridView1.Rows[0].Cells[2].Value} {dataGridView1.Rows[0].Cells[3].Value} {dataGridView1.Rows[0].Cells[4].Value}";                 
                        break;
                case "Дежурство":
                    documentName = $"Дежурство {dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value} {dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value} {dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value}";

                    break;
                case "ДТП":
                    documentName = $"ДТП {dataGridView2.Rows[0].Cells[6].Value} {dataGridView2.Rows[0].Cells[8].Value} {dataGridView2.Rows[0].Cells[7].Value}";
                        break;
            }

            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            Word.Paragraph oPara1;
            object start = oDoc.Content.Start;
            object end = oDoc.Content.End;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);

            oPara1.LeftIndent = oWord.CentimetersToPoints(3);
            oPara1.RightIndent = oWord.CentimetersToPoints(1);
            oPara1.Format.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
            //oPara1.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
            oPara1.Range.Font.Name = "Times New Roman";
            oPara1.Range.Font.Size = 14;
            String queryFull = string.Empty;
            String query = String.Empty;
            String razdel = "===========================================================";
            for (int i = 0; i < countColums + 1; i++)
            {            
                switch (TableName)
                {
                    case "Нарушения":
                        query = $"Дата: {dataGridView2.Rows[0].Cells[2].Value}\nСотрудник: {dataGridView2.Rows[0].Cells[11].Value} {dataGridView2.Rows[0].Cells[12].Value} {dataGridView2.Rows[0].Cells[13].Value}\nДокладывает, что при управлении автомобилем {dataGridView2.Rows[0].Cells[3].Value} {dataGridView2.Rows[0].Cells[4].Value}, государственный номер автомобиля {dataGridView2.Rows[0].Cells[14].Value} был задержан гражданин {dataGridView2.Rows[0].Cells[5].Value} {dataGridView2.Rows[0].Cells[6].Value} {dataGridView2.Rows[0].Cells[7].Value} на ул. {dataGridView2.Rows[0].Cells[8].Value}.\nОписание нарушения: {dataGridView2.Rows[0].Cells[9].Value}\nИсходя из вида нарушения: {dataGridView2.Rows[0].Cells[1].Value}\nБыл наложен штраф в размере {dataGridView2.Rows[0].Cells[10].Value}  бв.\nРапорт сформирован при помощи автогенерации, дата и время на момент генерации: {DateTime.Now.ToString()}\n\n{razdel}\n\n";
                        break;
                    case "Дежурство":
                        query = $"{dataGridView2.Rows[i].Cells[9].Value}\nДокладываю, что сотрудник: { dataGridView2.Rows[i].Cells[1].Value} { dataGridView2.Rows[i].Cells[2].Value} {dataGridView2.Rows[i].Cells[3].Value}. Находился на дежурстве по ул. {dataGridView2.Rows[i].Cells[10].Value}.\nС использованием служебного автомобиля (Марка, модель, государственный номер автомобиля): { dataGridView2.Rows[i].Cells[4].Value} { dataGridView2.Rows[i].Cells[5].Value} { dataGridView2.Rows[i].Cells[6].Value}.\nНачальная точка дежурства: ул. { dataGridView2.Rows[i].Cells[7].Value}\nКонечная точка дежурства: ул. { dataGridView2.Rows[i].Cells[8].Value}\nРапорт сформирован при помощи автогенерации, дата и время на момент генерации: {DateTime.Now.ToString()}\n\n{razdel}\n\n";
                        break;
                    case "Учет":
                        query = $"Автомобиль {dataGridView2.Rows[0].Cells[7].Value} {dataGridView2.Rows[0].Cells[8].Value} {dataGridView2.Rows[0].Cells[9].Value} {dataGridView2.Rows[0].Cells[13].Value} {dataGridView2.Rows[0].Cells[12].Value}\nДата постановки автомобиля на учет в ГАИ: {dataGridView2.Rows[0].Cells[10].Value}\nСотрудник зарегистрировавший автомобиль: {dataGridView2.Rows[0].Cells[14].Value} {dataGridView2.Rows[0].Cells[1].Value} {dataGridView2.Rows[0].Cells[2].Value} {dataGridView2.Rows[0].Cells[3].Value}\nВладелец: {dataGridView2.Rows[0].Cells[4].Value} {dataGridView2.Rows[0].Cells[5].Value} {dataGridView2.Rows[0].Cells[6].Value}\nАвтомобилю был присвоен государственный номер: {dataGridView2.Rows[0].Cells[9].Value}\nДата снятия, если имеется: {dataGridView2.Rows[0].Cells[11].Value}\nРапорт сформирован при помощи автогенерации, дата и время на момент генерации: {DateTime.Now.ToString()}\n\n{razdel}\n\n";
                        break;
                    case "ДТП":
                        String tmp = $"{dataGridView2.Rows[0].Cells[11].Value}";
                        if (tmp.Contains("False"))
                            tmp = "Нет";
                        if (tmp.Contains("True"))
                            tmp = "Да";
                        //{dataGridView2.Rows[0].Cells[11].Value}
                        query = $"Дата: {dataGridView2.Rows[0].Cells[10].Value}\nСотрудник {dataGridView2.Rows[0].Cells[3].Value} {dataGridView2.Rows[0].Cells[4].Value} {dataGridView2.Rows[0].Cells[5].Value} докладывает, что на улице {dataGridView2.Rows[0].Cells[2].Value} {dataGridView2.Rows[0].Cells[1].Value} район\n {dataGridView2.Rows[0].Cells[6].Value} {dataGridView2.Rows[0].Cells[8].Value} {dataGridView2.Rows[0].Cells[7].Value} при управлении автомобилем далее {dataGridView2.Rows[0].Cells[12].Value} {dataGridView2.Rows[0].Cells[13].Value} {dataGridView2.Rows[0].Cells[14].Value} {dataGridView2.Rows[0].Cells[15].Value} \nПроизошло: {dataGridView2.Rows[0].Cells[9].Value} \nВиновник: {tmp} \nРапорт сформирован при помощи автогенерации, дата и время на момент генерации: {DateTime.Now.ToString()}\n\n{razdel}\n\n";    
                        break;
                }
               
                if (i != countColums)
                {
                    
                    queryFull += query;
                    
                }
            }

            //oPara1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            oPara1.Range.Text = queryFull;
            oPara1.Range.InsertParagraphAfter();
            path = $@"{path}\{documentName}.docx";

            pathFrom = System.IO.Path.Combine(@"C:\Users\Fafka\Documents\" + $"{documentName}" + ".docx");
            //String pathTo = System.IO.Path.Combine($@"{path}"+ $"{documentName}"+".docx");
            String pathTo = System.IO.Path.Combine($@"{path}");
            object fileName = $@"{path}";
            //oDoc.SaveAs2(ref fileName, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            //oDoc.SaveAs2(pathTo);
            //from = System.IO.Path.Combine(@"E:\vid\", "(" + i.ToString() + ").PNG")
            //$@"C:\Users\Fafka\Documents\{documentName}.docx"; 
            oDoc.SaveAs(documentName);
            oDoc.Close();
            oWord.Quit();
            Thread.Sleep(900);
            button1.Visible = true;
            
            // Move the file.  
            //File.Move(pathFrom, pathTo);  
          
            //oDoc.SaveAs2(documentName);
            
            //@"" + @"\Report.doc"
            //MessageBox.Show($@"{path}" + $@"\{documentName}.doc" );
            //oDoc.SaveAs2($@"{path}" + $@"\{documentName}.doc", ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            //++oDoc.SaveAs2($@"{ path}\{documentName}.doc", ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
;           //oDoc.Close();
            
            //File.Move($@"C:\Users\Fafka\Documents\{documentName}.docx", path);
        }

        private void SelectItemForReport_KeyDown(object sender, KeyEventArgs e)
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

        //button
        private void btnSelect2_Click(object sender, EventArgs e)
        {
            if (TableName == null)
                return;
            if (TableName == "Виды_поощрений_и_взысканий" || TableName == "Виды_нарушений" || TableName == "Участники_ДТП")
            {
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            }

            queryLoad2 = String.Empty;
            //MessageBox.Show(TableName);
            if (TableName == "Дежурство")
                queryLoad2 = $"select Дежурство.Код, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Служебный_автомобиль.Марка, Служебный_автомобиль.Модель, Служебный_автомобиль.Номер, Точки_дежурства.Начальаня_точка,Точки_дежурства.Конечная_точка,Дежурство.Дата, Дежурство.Место from Дежурство, Служебный_автомобиль, Точки_дежурства, Сотрудники where Дежурство.Код_служебного_автомобиля = Служебный_автомобиль.Код and Дежурство.Код_сотрудника = Сотрудники.Код and Дежурство.Код_точки = Точки_дежурства.Код and Сотрудники.Фамилия = '{dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value}' and Сотрудники.Имя = '{dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value}' and Сотрудники.Отчество = '{dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value}' order by Дежурство.Дата desc";

            
            switch (TableName)
            {
                case "Нарушения":
                    //queryLoad2 = $"select Нарушения.Код, Виды_нарушений.Вид_нарушения as [Вид нарушения], Дежурство.Дата as [Дата нарушения], Автомобили.Марка, Автомобили.Модель, Водители.Фамилия, Водители.Имя, Водители.Отчество , Нарушения.Место, Нарушения.Описание, Виды_нарушений.Штраф from Нарушения, Водители, Автомобили, Виды_нарушений, Дежурство where Нарушения.Код_водителя = Водители.Код and Автомобили.Код = Нарушения.Код_авто and Нарушения.Код_вида = Виды_нарушений.Код and Нарушения.Код_дежурства = Дежурство.Код and Водители.Фамилия = '{dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value}' and Водители.Имя = '{dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value}' and Водители.Отчество = '{dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value}'";                   
                    queryLoad2 = $"select Нарушения.Код, Виды_нарушений.Вид_нарушения as [Вид нарушения], Дежурство.Дата as [Дата нарушения], Автомобили.Марка, Автомобили.Модель, Водители.Фамилия, Водители.Имя, Водители.Отчество , Нарушения.Место, Нарушения.Описание, Виды_нарушений.Штраф, Сотрудники.Фамилия as [СФ], Сотрудники.Имя, Сотрудники.Отчество, Учет.Номер from Нарушения, Водители, Автомобили, Виды_нарушений, Дежурство, Сотрудники, Учет where Нарушения.Код_водителя = Водители.Код and Автомобили.Код = Нарушения.Код_авто and Нарушения.Код_вида = Виды_нарушений.Код and Нарушения.Код_дежурства = Дежурство.Код and Дежурство.Код_сотрудника = Сотрудники.Код and Автомобили.Код = Учет.Код_авто and Водители.Фамилия = '{dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value}' and Водители.Имя = '{dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value}' and Водители.Отчество = '{dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value}' order by Дежурство.Дата desc";
                    break;
                case "Дежуство":
                    queryLoad2 = $"select Дежурство.Код, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Служебный_автомобиль.Марка, Служебный_автомобиль.Модель, Служебный_автомобиль.Номер, Точки_дежурства.Начальаня_точка,Точки_дежурства.Конечная_точка,Дежурство.Дата, Дежурство.Место from Дежурство, Служебный_автомобиль, Точки_дежурства, Сотрудники where Дежурство.Код_служебного_автомобиля = Служебный_автомобиль.Код and Дежурство.Код_сотрудника = Сотрудники.Код and Дежурство.Код_точки = Точки_дежурства.Код and Дежурство.Код = {dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value}";
                    break;
                case "Учет":
                    queryLoad2 = $"SELECT Автомобили.Код, Сотрудники.Фамилия as [Фамилия сотрудника], Сотрудники.Имя as [Имя сотрудника],Сотрудники.Отчество as [Отчество сотрудника], Водители.Фамилия as [Фамилия владельца], Водители.Имя as [Имя владельца], Водители.Отчество as [Отчество владельца], Автомобили.Марка, Автомобили.Модель, Учет.Номер, Учет.Дата_постановки, Учет.Дата_снятия, Автомобили.Vin_номер, Автомобили.Год_выпуска, Звания.Звание FROM Звания, История, Учет, Сотрудники, Водители, Автомобили where Учет.Код_сотрудника = Сотрудники.Код and Учет.Код_водителя = Водители.Код and Учет.Код_авто = Автомобили.Код and История.Код_сотрудника = Учет.Код_сотрудника and История.Код_звания = Звания.Код and Учет.Код = {dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value} order by Учет.Дата_постановки desc";
                    break;
                case "ДТП":
                    queryLoad2 = $"select Дтп.Код, Районы.Район ,Улицы.Улица, Сотрудники.Имя as [Имя сотрудника], Сотрудники.Фамилия as [Фамилия сотрудника], Сотрудники.Отчество as [Сотрудник отчество] ,Водители.Имя as [Имя водителя], Водители.Фамилия as [Фамилия водителя], Водители.Отчество as [Отчество водителя], ДТП.Описание, ДТП.Дата, Участники_ДТП.Виновник,  Автомобили.Марка, Автомобили.Модель, Автомобили.Год_выпуска, Учет.Номер, ДТП.Описание from ДТП, Улицы, Сотрудники, Участники_ДТП, Водители, Районы, Автомобили, Учет where Участники_ДТП.Код_ДТП = ДТП.Код and ДТП.Код_улицы = Улицы.Код and Улицы.Код_района = Районы.Код and Участники_ДТП.Код_водителя = Водители.Код  and ДТП.Код_сотрудника = Сотрудники.Код and Автомобили.Код = Учет.Код_авто and Участники_ДТП.Код_авто = Автомобили.Код and ДТП.Код = {dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value}";
                    //queryLoad2 = $"select Сотрудники.Код, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Звания.Звание, Должности.Должность from Сотрудники, Звания, Должности, История where История.Код_должности = Должности.Код and История.Код_звания = Звания.Код and История.Код_сотрудника = Сотрудники.Код and Сотрудники.Код = {dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value}";
                    break;

            }
            dataGridView2.Columns.Clear();
            dataGridView2.DataSource = null;
            

            var _dataTable2 = new DataTable();
            var _sqlDataAdapter2 = new SqlDataAdapter(queryLoad2, sqlcon);
            _sqlDataAdapter2.Fill(_dataTable2);
            dataGridView2.DataSource = _dataTable2;
            for (int i = 1; i < dataGridView2.Columns.Count; i++)
                dataGridView2.Columns[i].HeaderText = dataGridView2.Columns[i].HeaderText.Replace("_", " ");
            //dataGridView1.Columns["Код"].ReadOnly = true;
            dataGridView2.Columns["Код"].Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", $@"{pathFrom}");
        }
    }
}
