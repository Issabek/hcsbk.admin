using System;
using hscbk.UserEntity.Interfaces;
namespace hscbk.UserEntity.Modules
{
    public class Module:IModule
    {
        public int id { get; set; }
        public string ModuleId { get; set; }
        public string ParentId { get; set; }

        public Module()
        {
        }
        public Module(string module, string parent)
        {
            this.ModuleId = module;
            this.ParentId = parent;
        }
        public override string ToString()
        {
            return string.Format("Module id:{0} --- Parent id: {1}", ModuleId, ParentId);
        }
    }
}
