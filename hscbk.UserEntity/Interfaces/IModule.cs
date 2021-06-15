using System;
using hscbk.UserEntity.Modules;
namespace hscbk.UserEntity.Interfaces
{
    public interface IModule
    {
         int id { get; set; }
         string ModuleId { get; set; }
         string ParentId { get; set; }

    }
}
