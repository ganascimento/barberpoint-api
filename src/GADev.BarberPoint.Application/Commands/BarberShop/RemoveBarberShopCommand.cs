using MediatR;

namespace GADev.BarberPoint.Application.Commands.BarberShop
{
    public class RemoveBarberShopCommand : IRequest<Application.Responses.ResponseService>
    {
        public int Id { get; set; }
    }
}