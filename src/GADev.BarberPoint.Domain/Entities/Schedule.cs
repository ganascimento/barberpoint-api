using System;

namespace GADev.BarberPoint.Domain.Entities
{
    public class Schedule : Core.BaseEntity
    {
        public DateTime DateSchedule { get; set; }
        public DateTime TimeNotifier { get; set; }
        public int UserId { get; set; }
        public int BarberShopId { get; set; }
        public int BarberId { get; set; }
        public int ServiceId { get; set; }
        
        public User User { get; set; }
        public BarberShop BarberShop { get; set; }
        public Barber Barber { get; set; }
        public Service Service { get; set; }
    }
}