using System;
using hscbk.UserEntity.Services;
namespace hscbk.UserEntity.Services
{
    public class AdminMenu
    {

        public void adminUI()
        {
            int a = 0;
            string login, password, res;
            AdminService aService = new AdminService();
            Console.WriteLine("Create admin(1) or Login admin(2):");
            a = Int32.Parse(Console.ReadLine());
            switch (a)
            {
                case 1:
                    {
                        Console.WriteLine("login:");
                        login = Console.ReadLine();
                        Console.WriteLine("password:");
                        password = Console.ReadLine();
                        aService.CreateAdmin(login, password);
                        adminPanel(aService);
                    }
                    break;
                case 2:
                    Console.WriteLine("login:");
                    login = Console.ReadLine();
                    Console.WriteLine("password:");
                    password = Console.ReadLine();
                    aService.UserAuthentication(login, password, out res);
                    if(res=="Admin")
                        adminPanel(aService);
                    else
                        Console.WriteLine("There is no such admin user");
                    break;
                default:
                    break;
            }
        }
        public void userUI()
        {
            int a = 0;
            string login, password, res;
            AdminService aService = new AdminService();
            Console.WriteLine("Create user(1) or Login user(2):");
            a = Int32.Parse(Console.ReadLine());
            switch (a)
            {
                case 1:
                    {
                        Console.WriteLine("login:");
                        login = Console.ReadLine();
                        Console.WriteLine("password:");
                        password = Console.ReadLine();
                        aService.CreateUser(login, password);
                        adminPanel(aService);
                    }
                    break;
                case 2:
                    Console.WriteLine("login:");
                    login = Console.ReadLine();
                    Console.WriteLine("password:");
                    password = Console.ReadLine();
                    aService.UserAuthentication(login, password, out res);
                    if (res == "User")
                        userPanel(aService);
                    else
                        Console.WriteLine("There is no such user");
                    break;
                default:
                    break;
            }
        }

        public void adminPanel(AdminService service)
        {
            int a = 0;
            string tempstr1, tempstr2, tempstr3;
            Console.WriteLine("1.Show users\n2.Show roles\n3.Create role\n4.Grant role\n5.Create module\n6.Show modules");
            a = Int32.Parse(Console.ReadLine());
            switch (a)
            {
                case 1:
                    service.PrintUsers();
                    break;
                case 2:
                    service.PrintRoles();
                    break;
                case 3:
                    Console.WriteLine("RoleId:");
                     tempstr1 = Console.ReadLine();
                    Console.WriteLine("ModuleID:");
                     tempstr2 = Console.ReadLine();
                    Console.WriteLine("CRUD:\nEx.:1110,1010,0000,0100");
                     tempstr3 = Console.ReadLine();
                    bool[] crud = GetGools(tempstr3);
                    service.CreateOrUpdateRole(tempstr1, tempstr2, crud[0], crud[1], crud[2], crud[3]);
                    Console.WriteLine("success");
                    break;
                case 4:
                    Console.WriteLine("User login:");
                    tempstr1 = Console.ReadLine();
                    Console.WriteLine("User roleid:");
                    tempstr2 = Console.ReadLine();
                    service.GrantRoleToUser(service.userExists(-1, tempstr1),tempstr2);
                    break;
                case 5:
                    Console.WriteLine("Module id:");
                    tempstr1 = Console.ReadLine();
                    Console.WriteLine("Module parent id:");
                    tempstr2 = Console.ReadLine();
                    service.CreateModule(tempstr1, tempstr2);
                    break;
                case 6:
                    service.PrintModules();
                    break;
                default:
                    break;
            }
        }
        public void userPanel(AdminService service)
        {
            int a = 0;
            string tempstr1, tempstr2, tempstr3;
            Console.WriteLine("1.Show role\n2.Show modules");
            a = Int32.Parse(Console.ReadLine());
            switch (a)
            {
                case 1:
                    service.PrintRolesAndInfos();
                    break;
                case 2:
                    service.PrintRolesAndInfos();
                    break;
                
                default:
                    break;
            }
        }

        private bool[] GetGools(string s)
        {
            bool[] g = { Convert.ToBoolean(Int16.Parse(s[0] + "")), Convert.ToBoolean(Int16.Parse(s[1] + "")), Convert.ToBoolean(Int16.Parse(s[2] + "")), Convert.ToBoolean(Int16.Parse(s[3] + ""))};
            return g;
        }

        public AdminMenu()
        {

        }


        public void  Menu()
        {
            int a = 0;
            Console.WriteLine("Admin(1) or User(2):");
            a = Int32.Parse(Console.ReadLine());
            switch (a)
            {
                case 1:
                    adminUI();
                    break;
                case 2:
                    userUI();
                    break;
                default:
                    break;
            }
            Menu();
        }



        
    }
}
