using System;
namespace hscbk.UserEntity.Interfaces
{
    public interface IUser
    {
         string Login { get; set; }
         string Password { get; set; }
         int id { get; set; }

        
    }
}
