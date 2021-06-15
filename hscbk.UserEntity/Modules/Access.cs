using System;
using hscbk.UserEntity.Interfaces;
namespace hscbk.UserEntity.Modules
{
    public class Access:IAccessor
    {
        public Access()
        {
        }

        public Access(bool c, bool r, bool u, bool d)
        {
            this.Create = c;
            this.Read = r;
            this.Update = u;
            this.Delete = d;
        }

        public bool Create { get ; set ; }
        public bool Read { get ; set ; }
        public bool Update { get ; set ; }
        public bool Delete { get ; set ; }
        public string ModuleId { get; set; }
        public string UserRoleId { get; set; }
        public int id { get; set; }

        public override bool Equals(object obj)
        {
            var instance = obj as Access;
            return this.Create == instance.Create &&
                this.Delete == instance.Delete &&
                this.Read == instance.Read &&
                this.Update == instance.Update ?
                true : false;
        }
    }
}
