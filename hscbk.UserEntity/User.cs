using System;
using hcsbk.DAL;
using hscbk.UserEntity.Modules;
using hscbk.UserEntity.Interfaces;
namespace hscbk.UserEntity
{
    public class User:IUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int id { get; set; }
        public string RoleId { get; set; } = "0";
        public override string ToString()
        {
            return string.Format("{0} --- {1} --- {2} --- {3}", id, Login, Password, RoleId);
        }
    }

}
