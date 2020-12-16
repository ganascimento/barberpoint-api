using MediatR;

namespace GADev.BarberPoint.Application.Commands.Service
{
    public class CreateServiceCommand : IRequest<Application.Responses.ResponseService>
    {
        public string Name { get; set; }
        public short Duration { get; set; }
        public decimal Value { get; set; }
        public int BarberShopId { get; set; }
    }
}