using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gai
{
    public partial class MenuFrom : Form
    {
        
        public int userID;
        public string UserName;

        public MenuFrom(int userID, string UserName)
        {
            
            InitializeComponent();
            this.userID = userID;
            this.UserName = UserName;
        }
        private void редактрованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите выйти", "Выйти", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
                return;
            Application.Exit();
        }
        private void базыДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
             * Form objApp;
            objApp = new ViewFormFullAllTables();
            */
            ViewFormAllTables objViewFormAllTables = new ViewFormAllTables();
            this.Hide();
            objViewFormAllTables.ShowDialog();
            this.Show();
        }

        private void пользователейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserEditForm objUserEditForm = new UserEditForm();
            //ViewFormFullTable objViewFormTable = new ViewFormFullTable("Пользователи");
            this.Hide();
            //objViewFormTable.ShowDialog();
            objUserEditForm.ShowDialog();
            this.Show();
        }
        
        private void просмотрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewForm objViewForm = new ViewForm();
            this.Hide();
            objViewForm.ShowDialog();
            this.Show();
        }
        
        private void MenuFrom_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            List<string> UserTypeName = new List<string>() { "Администратор", "Оператор", "Начальник" };
            label1.Text = $"Роль: {UserTypeName[userID - 1]}";
            label2.Text = $"Логин: {UserName}";
            timer1.Start();
            label3.Text = DateTime.Now.ToString();
            
            //справкаToolStripMenuItem.Visible = false;
            кадрыToolStripMenuItem.Visible = true;
            if (userID == 2)
            {
                пользователейToolStripMenuItem.Visible = false;
                отчетToolStripMenuItem.Visible = false;
                инструментыToolStripMenuItem.Visible = false;
                кадрыToolStripMenuItem.Visible = false;
                дежурствоToolStripMenuItem.Visible = true;
                отчетToolStripMenuItem1.Visible = false; 



            }
            if (userID == 3)
            {
                редактрованиеToolStripMenuItem.Visible = false;
                кадрыToolStripMenuItem.Visible = true;
                нарушенияToolStripMenuItem.Visible = true;
                дТПToolStripMenuItem.Visible = true;
                учётToolStripMenuItem.Visible = true;
                инструментыToolStripMenuItem.Visible = false;
                дежурствоToolStripMenuItem.Visible = true;
                отчетToolStripMenuItem1.Visible = true;


            }
        }

        private void отчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportForm objReportForm = new ReportForm();
            this.Hide();
            objReportForm.ShowDialog();
            this.Show();

        }

        private void справочникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*ViewFormFull objViewFormFull = new ViewFormFull();
            this.Hide();
            objViewFormFull.ShowDialog();
            this.Show();*/
        }

        private void автомобилиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFull = new ViewFormFullTable("Автомобили", "MenuForm");
            this.Hide();
            objViewFormFull.ShowDialog();
            this.Show();
        }

        private void водителиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFull = new ViewFormFullTable("Водители", "MenuForm");
            this.Hide();
            objViewFormFull.ShowDialog();
            this.Show();
        }

        private void званияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFull = new ViewFormFullTable("Звания", "MenuForm");
            this.Hide();
            objViewFormFull.ShowDialog();
            this.Show();
        }

        private void должностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFull = new ViewFormFullTable("Должности", "MenuForm");
            this.Hide();
            objViewFormFull.ShowDialog();
            this.Show();
        }

        private void районыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFull = new ViewFormFullTable("Районы", "MenuForm");
            this.Hide();
            objViewFormFull.ShowDialog();
            this.Show();
        }

        private void видыПоощренийИВзысканийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFull = new ViewFormFullTable("Виды_поощрений_и_взысканий", "MenuForm");
            this.Hide();
            objViewFormFull.ShowDialog();
            this.Show();
        }

        private void видыТочекДежурстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFull = new ViewFormFullTable("Виды_точек_дежурства", "MenuForm");
            this.Hide();
            objViewFormFull.ShowDialog();
            this.Show();
        }

        private void видыНарушенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFull = new ViewFormFullTable("Виды_нарушений", "MenuForm");
            this.Hide();
            objViewFormFull.ShowDialog();
            this.Show();
        }

        private void служебныйАвтомобильToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFull = new ViewFormFullTable("Служебный_автомобиль", "MenuForm");
            this.Hide();
            objViewFormFull.ShowDialog();
            this.Show();
        }

        private void кадрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonsForm objPersonsForm  = new PersonsForm("Сотрудники");
            this.Hide();
            objPersonsForm.ShowDialog();
            this.Show();
        }

        private void дТПToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DtpForm objDtpForm = new DtpForm("ДТП");
            this.Hide();
            objDtpForm.ShowDialog();
            this.Show();
        }

        private void инструментыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1;
            
            timer1.Enabled = true;
            label3.Text = DateTime.Now.ToString();
            
        }

        private void историяПодключенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormFullTable objViewFormFull = new ViewFormFullTable("Log", "MenuForm");
            this.Hide();
            objViewFormFull.ShowDialog();
            this.Show();

        }

        private void учётToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountingForm objAccountingForm = new AccountingForm("Учет");
            this.Hide();
            objAccountingForm.ShowDialog();
            this.Show();
        }

        private void нарушенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IllegalForm objIllegalForm = new IllegalForm("Нарушения");
            this.Hide();
            objIllegalForm.ShowDialog();
            this.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void дежурствоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DutyForm objDutyForm = new DutyForm("Дежурство");
            this.Hide();
            objDutyForm.ShowDialog();
            this.Show();
        }

        private void отчетToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReportForm objReportForm = new ReportForm();
            this.Hide();
            objReportForm.ShowDialog();
            this.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void историяToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
