using System;
using hscbk.UserEntity.Services;
using hcsbk.DAL;
namespace hcsbk.admin
{
    class Program
    {
       
        static void Main(string[] args)
        {
            AdminMenu menu = new AdminMenu();
            menu.Menu();
        }

        private static void DisplayMessage(object sender, AdminEventHandler ev)
        {
            string tempStr;
            LiteDbEntity db = new LiteDbEntity(@"EventsLog.db");
            db.CreateRecord(ev, out tempStr) ;
            Console.WriteLine(ev.Message);
        }
    }
}
