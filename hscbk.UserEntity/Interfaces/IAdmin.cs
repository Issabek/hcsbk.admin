using System;
using hscbk.UserEntity.Interfaces;
namespace hscbk.UserEntity.Interfaces
{
    public abstract class AdminTemplate:IUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int id { get; set; }
    }
}
