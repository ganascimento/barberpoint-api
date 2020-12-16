using MediatR;

namespace GADev.BarberPoint.Application.Notifications.BarberShop
{
    public class CreateBarberShopNotification : INotification
    {
        public Domain.Entities.BarberShop BarberShop { get; set; }

        public CreateBarberShopNotification(Domain.Entities.BarberShop barberShop)
        {
            BarberShop = barberShop;
        }
    }
}