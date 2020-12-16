using MediatR;

namespace GADev.BarberPoint.Application.Commands.Service
{
    public class ChangeServiceCommand : IRequest<Application.Responses.ResponseService>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Duration { get; set; }
        public decimal Value { get; set; }
        public int BarberShopId { get; set; }
    }
}