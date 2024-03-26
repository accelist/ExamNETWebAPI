using Contracts.Request;
using Contracts.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamNETWebAPI.Controllers
{
    [Route("api/v1/get-available-ticket")]
    [ApiController]
    public class GetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<GetController>
        [HttpGet]
        public async Task<ActionResult<GetTicketResponse>> Get([FromQuery] GetTicketRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }
    }
}
