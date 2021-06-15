using System;
namespace hscbk.UserEntity.Interfaces
{

    public interface IAccessor
    {
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}
