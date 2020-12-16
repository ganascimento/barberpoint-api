using MediatR;

namespace GADev.BarberPoint.Application.Commands.Schedule
{
    public class RemoveScheduleCommand : IRequest<Application.Responses.ResponseService>
    {
        public int Id { get; set; }
    }
}