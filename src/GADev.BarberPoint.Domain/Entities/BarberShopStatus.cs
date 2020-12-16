using System;

namespace GADev.BarberPoint.Domain.Entities
{
    public class BarberShopStatus : Core.BaseEntity
    {
        public DateTime ExpirationDate { get; set; }
        public int PlanId { get; set; }
        
        public Plan Plan { get; set; }
        
        public BarberShopStatus()
        {
            
        }

        public BarberShopStatus(int id)
        {
            Id = id;
        }

        public BarberShopStatus(DateTime expirationDate)
        {
            ExpirationDate = expirationDate;
        }
    }
}