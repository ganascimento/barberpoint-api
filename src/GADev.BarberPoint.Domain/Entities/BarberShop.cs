using System;

namespace GADev.BarberPoint.Domain.Entities
{
    public class BarberShop : Core.BaseEntity
    {
        public string Name { get; set; }
        public string PathLogo { get; set; }
        public bool IsActive { get; set; }
        public int UserAdminId { get; set; }
        public int AddressId { get; set; }
        public int BarberShopStatusId { get; set; }
        
        public Address Address { get; set; }
        public BarberShopStatus BarberShopStatus { get; set; }
        public User UserAdmin { get; set; }

        public BarberShop()
        {
            
        }

        public BarberShop(int id)
        {
            Id = id;
        }
    }
}