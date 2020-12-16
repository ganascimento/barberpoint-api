using System.Threading.Tasks;
using GADev.BarberPoint.Application.Commands.BarberShop;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GADev.BarberPoint.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class BarberShopController : Controller
    {
        private readonly IMediator _mediator;

        public BarberShopController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBarberShopCommand command) {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromBody] RemoveBarberShopCommand command) {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}