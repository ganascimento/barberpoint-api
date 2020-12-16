using MediatR;

namespace GADev.BarberPoint.Application.Commands.Barber
{
    public class RemoveBarberCommand : IRequest<Application.Responses.ResponseService>
    {
        public int Id { get; set; }
    }
}