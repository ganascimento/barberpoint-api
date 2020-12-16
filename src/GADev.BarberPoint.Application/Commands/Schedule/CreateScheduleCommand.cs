using System;
using MediatR;

namespace GADev.BarberPoint.Application.Commands.Schedule
{
    public class CreateScheduleCommand : IRequest<Application.Responses.ResponseService>
    {
        public DateTime DateSchedule { get; set; }
        public DateTime TimeNotifier { get; set; }
        public int UserId { get; set; }
        public int BarberShopId { get; set; }
        public int BarberId { get; set; }
        public int ServiceId { get; set; }
    }
}