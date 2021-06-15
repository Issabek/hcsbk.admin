using System;
using hscbk.UserEntity.Services;
using hscbk.UserEntity.Interfaces;

namespace hscbk.UserEntity
{
    public delegate void AdminPanelHandler(object sender, AdminEventHandler e);
    public class Admin:AdminTemplate
    {
        public Admin()
        {

        }
    }
}
        
    
    
