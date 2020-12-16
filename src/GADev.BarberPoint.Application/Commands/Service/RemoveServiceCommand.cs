using MediatR;

namespace GADev.BarberPoint.Application.Commands.Service
{
    public class RemoveServiceCommand : IRequest<Application.Responses.ResponseService>
    {
        public int Id { get; set; }
    }
}