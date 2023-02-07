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
    public partial class ReportForm : Form
    {
        
        String oDate;
        String iDate;
        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-9R3H4TU\SQLEXPRESS;Initial Catalog=gaidatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public string path;
        public ReportForm()
        {
            InitializeComponent();
            sqlcon.Open();
            
            
        }

        private void Back_Click(object sender, EventArgs e)
        {
            sqlcon.Close();
            this.Close();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            button5.Visible = false;
            setpathButton.Visible = false;
        }

        private void personsButton_Click(object sender, EventArgs e)
        {
            SelectItemForReport objSelectItemForReport = new SelectItemForReport("ДТП",path);
            this.Hide();
            objSelectItemForReport.ShowDialog();
            this.Show();

        }

        private void illegalButton_Click(object sender, EventArgs e)
        {
            SelectItemForReport objSelectItemForReport = new SelectItemForReport("Нарушения", path);
            this.Hide();
            objSelectItemForReport.ShowDialog();
            this.Show();

        }

        private void setpathButton_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    // вот выбранный путь, его можно потом засунуть в ваш textbox
                    path = fbd.SelectedPath;
                    MessageBox.Show(path);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //диалог день, интервал
            PickDate objPickDate = new PickDate();
            this.Hide();
            objPickDate.ShowDialog();
            this.Show();
        }

        private void dutyButton_Click(object sender, EventArgs e)
        {
            SelectItemForReport objSelectItemForReport = new SelectItemForReport("Дежурство", path);
            this.Hide();
            objSelectItemForReport.ShowDialog();
            this.Show();

        }

        private void accountingButton_Click(object sender, EventArgs e)
        {
            SelectItemForReport objSelectItemForReport = new SelectItemForReport("Учет", path);
            this.Hide();
            objSelectItemForReport.ShowDialog();
            this.Show();

        }

    }
}
