using System.Threading.Tasks;
using GADev.BarberPoint.Application.Commands.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GADev.BarberPoint.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authentication([FromBody] LoginUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}