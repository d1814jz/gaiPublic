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
    public partial class ViewForm : Form
    {
        public string TableName;
        public ViewForm()
        {
            InitializeComponent();
            sqlcon.Open();
        }
        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void Back_Click(object sender, EventArgs e)
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
                comboBox1.Items.Add(row["TABLE_NAME"].ToString().Replace("_"," "));
                
            }
            comboBox1.Items.Remove("Log");
            btnGridView_Click(null, null);

        }
        private void ViewForm_Load(object sender, EventArgs e)
        {
            
           
        }


        private void btnGridView_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            if (comboBox1.SelectedItem == null)
                return;
            TableName = comboBox1.SelectedItem.ToString().Replace(" ", "_");
            MessageBox.Show(TableName);
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
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

        }
    }

}
    
