using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gai
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
        
            /*
                Убрать видимость лога!
             
             */

            /*
             Добавление сразу в все поля таблицы. 
             
             */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new Authorization());
            //Application.Run(new ViewFormFullTable("Учет", "MenuForm"));
            //Application.Run(new AccountingFormNew("ДТП"));
            //Application.Run(new ViewFormAllTables());
            //Application.Run(new DtpForm("Учет"));
            //Application.Run(new AccountingFormNew("Учет"));

            //Application.Run(new SelectItemForReport("ДТП", "dwadwa"));
            //Application.Run(new ReportForm());
            //Application.Run(new MenuFrom(1, "Admin"));
            //Application.Run(new ReportForm());
            //Application.Run(new PickDate());

            //Application.Run(new PickDate());
            Application.Run(new Authorization());
            //Application.Run(new MenuFrom(3, "user3"));
            //Application.Run(new DutyDotsEdit(1));
            //Application.Run(new ViewFormAllTables());
            //Application.Run(new DtpFormEdit(1));
            //Application.Run(new ViewFormAllTables());
            //Application.Run(new PersonsFormNew("Сотрудники"));
            //Application.Run(new IllegalForm("Нарушения"));
            //Application.Run(new PersonsForm("Сотрудники"));
            //Application.Run(new ViewForm());
            //Application.Run(new ViewFormFull());
            //Application.Run(new DtpFormNew("ДТП"));
           /* Application.Run(new DtpAdd());
            Application.Run(new DtpAddPeople());
            Application.Run(new DutyAdd());
            Application.Run(new DutyDotsAdd());
            Application.Run(new HistoryAdd());
            Application.Run(new IllegalAdd());
            Application.Run(new MemberDtpAdd());
            Application.Run(new RewardsAdd());*/
            //Application.Run(new());
        }
    }
}
