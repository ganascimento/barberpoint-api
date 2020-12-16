using MediatR;

namespace GADev.BarberPoint.Application.Commands.Authentication
{
    public class LoginUserCommand : IRequest<Application.Responses.ResponseService>
    {        
        public string Email { get; set; }
        public string Password { get; set; }
    }
}