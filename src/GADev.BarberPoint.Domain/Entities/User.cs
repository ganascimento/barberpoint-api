using System.Collections.Generic;

namespace GADev.BarberPoint.Domain.Entities
{
    public class User : Core.BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Role> MyProperty { get; set; }        

        public User()
        {
            
        }

        public User(int id)
        {
            Id = id;
        }
    }
}