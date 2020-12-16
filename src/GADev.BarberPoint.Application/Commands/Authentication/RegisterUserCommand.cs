using MediatR;

namespace GADev.BarberPoint.Application.Commands.Authentication
{
    public class RegisterUserCommand : IRequest<Application.Responses.ResponseService>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}