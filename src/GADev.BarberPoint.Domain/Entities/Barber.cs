using System;
using System.Collections.Generic;

namespace GADev.BarberPoint.Domain.Entities
{
    public class Barber : Core.BaseEntity
    {
        public string Name { get; set; }
        public DateTime TimeStartWork { get; set; }
        public DateTime TimeFinishWork { get; set; }
        public bool IsConfirmed { get; set; }
        public int BarberShopId { get; set; }
        public int UserId { get; set; }

        public BarberShop BarberShop { get; set; }
        public User User { get; set; }
        public ICollection<Service> Services { get; set; }

        public Barber()
        {
            
        }

        public Barber(int id)
        {
            Id = id;
        }
    }
}