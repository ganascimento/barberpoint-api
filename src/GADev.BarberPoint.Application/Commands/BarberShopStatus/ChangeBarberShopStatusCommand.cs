using GADev.BarberPoint.Application.Responses;
using MediatR;

namespace GADev.BarberPoint.Application.Commands.BarberShopStatus
{
    public class ChangeBarberShopStatusCommand : IRequest<ResponseService>
    {
        public int PlanId { get; set; }
        public int BarberShopId { get; set; }
    }
}