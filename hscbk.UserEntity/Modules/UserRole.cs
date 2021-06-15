using System;
using hscbk.UserEntity.Interfaces;
namespace hscbk.UserEntity.Modules
{
    public class UserRole:IRole
    {

        public UserRole()
        {
            
        }
        public UserRole(string RoleId)
        {
            this.RoleId = RoleId;
        }
        public int id { get ; set ; }
        public string RoleId { get; set; }
        public override string ToString()
        {
            return string.Format("{0} --- {1}", RoleId, id);
        }

    }
}
