using System;
using System.Collections.Generic;
using MediatR;

namespace GADev.BarberPoint.Application.Commands.Barber
{
    public class ChangeBarberCommand : IRequest<Application.Responses.ResponseService>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeStartWork { get; set; }
        public DateTime TimeFinishWork { get; set; }
        public int BarberShopId { get; set; }
        public int UserId { get; set; }
        public List<int> ServicesId { get; set; }
    }
}