using System;
namespace hscbk.UserEntity.Services
{
    public class AdminEventHandler
    {
        public string ClassName { get; set; }
        public DateTime EventDate { get; set; }
        public string Message { get; set; }

        public AdminEventHandler(string Message, string ClassName, DateTime EventDate)
        {
            this.Message = Message;
            this.ClassName = ClassName;
            this.EventDate = EventDate;
        }
    }
}
