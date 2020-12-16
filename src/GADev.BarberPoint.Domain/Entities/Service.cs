using System.Collections.Generic;

namespace GADev.BarberPoint.Domain.Entities
{
    public class Service : Core.BaseEntity
    {
        public string Name { get; set; }
        public short Duration { get; set; }
        public decimal Value { get; set; }
        public int BarberShopId { get; set; }

        public BarberShop BarberShop { get; set; }        
        public ICollection<Barber> Barbers { get; set; }

        public Service()
        {
            
        }

        public Service(int id)
        {
            Id = id;
        }
    }
}