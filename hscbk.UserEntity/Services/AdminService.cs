using System;
using System.Collections.Generic;
using hcsbk.DAL;
using hscbk.UserEntity.Modules;
using System.Linq;
using hscbk.UserEntity.Interfaces;
namespace hscbk.UserEntity.Services
{
    public class AdminService
    {
        public event AdminPanelHandler DataEvent;
        LiteDbEntity db = new LiteDbEntity(@"myLiteDb");
        IUser user;
        string ExcetionMsg;

        public bool CreateAdmin(string login, string password)
        {
            Admin user = new Admin();
            List<Admin> usersList = db?.getCollection<Admin>();
            user.Password = password;
            user.Login = login;
            if (!usersList.Any(u => u.Login == login))
            { 
                try
                {
                    db.CreateRecord(user, out ExcetionMsg);
                    //DataEvent(this, new AdminEventHandler("Admin tried to add new user", user.GetType().Name, DateTime.Now));
                }
                catch (Exception ex)
                {
                    //DataEvent(this, new AdminEventHandler(ex.Message, ex.GetType().Name, DateTime.Now));
                    return false;
                }
            }
            else
            {
                Console.WriteLine("User with such a login alrredy exists");
            }
            
            return true;
        }

        public bool CreateUser(string login, string password)
        {
            User user = new User();
            user.Password = password;
            user.Login = login;
            List<User> usersList = db?.getCollection<User>();
            try
            {
                db.CreateRecord(user, out ExcetionMsg);
                //DataEvent(this, new AdminEventHandler("Admin tried to add new user", user.GetType().Name, DateTime.Now));
            }
            catch(Exception ex)
            {
                //DataEvent(this, new AdminEventHandler(ex.Message, ex.GetType().Name, DateTime.Now));
                return false;
            }
            
            return true;
        }
        public bool UserAuthentication(string login, string password, out string authRes)
        {

            if (db.getCollection<User>()
                .Any(u => u.Login == login &&
                u.Password == password))
            {
                authRes = "User";
                user = db.getCollection<User>().Where(u => u.Login == login).First();
                return true;

            }
            else if (db.getCollection<Admin>()
                .Any(u => u.Login == login && u.Password==password))
            {
                authRes = "Admin";
                user = db.getCollection<Admin>().Where(u => u.Login == login).First();
                return true;
            }
            else
            {
                Console.WriteLine("Wrong password or login");
                authRes = "Fail";
                return false;
            }
            
        }
        public User userExists(int userId=-1, string userLogin="")
        {
            return db.getCollection<User>().Where(r => r.id == userId || r.Login==userLogin).FirstOrDefault() ;
        }

        public bool GrantRoleToUser(User user, string roleId)
        {
            try
            {
                user.RoleId = roleId;
                db.UpdateRecord(user, out ExcetionMsg);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool roleExists(string roleId)
        {
            return db.getCollection<UserRole>().Any(r => r.RoleId == roleId);
        }

        public bool CreateOrUpdateRole(string roleId, string moduleId, bool c, bool r, bool u, bool d)
        {
            List<UserRole> userRoles = db?.getCollection<UserRole>();
            UserRole role;
            string tempstr;
            if (userRoles.Any(r => r.RoleId == roleId))
            {
                role = userRoles.Where(r => r.RoleId == roleId).First();
                if (GetAccesses(roleId).Where(a => a.ModuleId == moduleId).FirstOrDefault()==null)
                {
                    Access tempAccess = new Access(c, r, u, d);
                    tempAccess.ModuleId = moduleId;
                    tempAccess.UserRoleId = roleId;
                    db.CreateRecord(tempAccess,out tempstr);
                    //DataEvent(this, new AdminEventHandler(string.Format("Admin has created new Access to role {0}",roleId), tempAccess.GetType().Name, DateTime.Now));
                    return true;
                }
                Access access = GetAccesses(roleId).Where(a => a.ModuleId == moduleId).First();
                access.Create = c;
                access.Read = r;
                access.Update = u;
                access.Delete = d;
                db.UpdateRecord<Access>(access, out tempstr);
                //DataEvent(this, new AdminEventHandler(string.Format("Admin has updated new Access to role {0}", roleId), access.GetType().Name, DateTime.Now));
                return true;
            }
            else
            {
                role = new UserRole(roleId);
                role.RoleId = roleId;
                Access tempAccess = new Access(c, r, u, d);
                tempAccess.ModuleId = moduleId;
                tempAccess.UserRoleId = roleId;
                db.CreateRecord(tempAccess, out tempstr);
                db.CreateRecord(role, out tempstr);
                //DataEvent(this, new AdminEventHandler(string.Format("Admin has created new role and access {0}", roleId), role.GetType().Name, DateTime.Now));
                return true;
            }

        }

        public List<Access> GetAccesses(string roleId)
        {
            return db.getCollection<Access>().Where(a => a.UserRoleId == roleId).ToList();
        }
        public bool CreateModule(string moduleId, string parentId="0")
        {
            if (db.getCollection<Module>().Any(m => m.ModuleId == moduleId && m.ParentId == parentId))
            {
                //DataEvent(this, new AdminEventHandler(string.Format("Admin tried to add module, but module already exists: ModuleId {0}", moduleId), "Module", DateTime.Now));
                return true;
            }
            else
            {
                Module module = new Module(moduleId, parentId);
                db.CreateRecord(module, out ExcetionMsg);
                //DataEvent(this, new AdminEventHandler(string.Format("Admin has added new module: ModuleId {0}", moduleId), "Module", DateTime.Now));
                return true;
            }
        }

        public bool HasRole(string RoleId)
        {
            return db.getCollection<User>().Any(u => u.RoleId == RoleId && u == user);
        }

        public List<UserRole> UserRoles(User user)
        {
            return  db.getCollection<UserRole>().Where(u => u.RoleId == user.RoleId).ToList();
        }

        public Access GetSpecificAccessOfUser(User user, string ModuleId, string roleId)
        {
            return db.getCollection<Access>().Where(a => a.UserRoleId == roleId && a.ModuleId == ModuleId).FirstOrDefault();
        }

        public void PrintRolesAndInfos()//не отображает подмодули
        {
            var roles = UserRoles(this.user as User);
            foreach (UserRole role in roles)
            {
                var AccessorsList = db.getCollection<Access>().Where(a => a.UserRoleId == role.RoleId).ToList();
                Console.WriteLine("{0} --- {1}", user.Login, role.RoleId);
                foreach(Access access in AccessorsList)
                {
                    Console.WriteLine("\tModule:{0} | Create: {1} | Read: {2} | Update: {3} | Delete: {4}", access.ModuleId, access.Create, access.Read, access.Update, access.Delete); ;
                }
            }
        }


        public void GetAccessedModules()
        {
            List<Module> modules = db.getCollection<Module>().Where()
            foreach()
        }


        public void PrintModules()
        {
            List<Module> modules = db.getCollection<Module>();
            List<Module> res = GetAllModules("0", modules).OrderBy(p => p.ParentId).ToList();
            foreach(var item in res)
            {
                Console.WriteLine(item.ToString());
            }
        }
        public void PrintRoles()
        {
            foreach (UserRole role in db.getCollection<UserRole>())
            {
                Console.WriteLine(role.ToString());
            }
        }
        //private List<Module> GetModule(List<Access> access)
        //{
        //    var list = db.getCollection<Module>().Where(m => access.FindAll(a => a.ModuleId == m.ModuleId)).ToList();
        //}
        //public void PrintModules(User user)
        //{
            
        //}
        public void PrintUsers()
        {
            foreach(User user in db.getCollection<User>())
            {
                Console.WriteLine(user.ToString());
            }
        }

        private List<Module> GetAllModules(string roleParentId,List<Module> SearchFrom)
        {
            List<Module>ResModules = new List<Module>();
            List<Module> ResModules2 = new List<Module>();
            string tempModule = null;
            foreach(Module module in SearchFrom)
            {
                if (module.ParentId == roleParentId)
                {
                    ResModules.Add(module);

                }
                else if (module.ParentId == tempModule)
                    ResModules.AddRange(GetAllModules(tempModule, SearchFrom));
                tempModule = module.ModuleId;
            }
            return ResModules;
        }












        public AdminService(string a, string b, out string c)
        {
            UserAuthentication(a, b, out c);
        }
        public AdminService()
        {

        }
        
    }

}