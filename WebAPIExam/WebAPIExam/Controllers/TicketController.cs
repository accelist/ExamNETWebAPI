using Contracts.RequestModels.Ticket;
using Contracts.ResponseModels.Ticket;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIExam.Controllers
{
    
    [Route("api/v1/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<TicketController>
        [Route("get-all")]
        [HttpGet]
        public async Task<ActionResult<GetAllTicketResponse>> Get(CancellationToken cancellationToken)
        {
            var request = new GetAllTicketRequest();
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        // POST api/<TicketController>
        [HttpPost]
        public async Task<ActionResult<CreateTicketResponse>> Post([FromBody] CreateTicketRequest request, CancellationToken cancellationToken)
        {
            //TODO validation
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [Route("get-available-ticket")]
        [HttpGet]
        public async Task<ActionResult<GetAvailableTicketResponse>> Get(
            [FromQuery] GetAvailableTicketRequest request,
            //[FromServices]IValidator _validator,
            CancellationToken cancellationToken)
        {
            //TODO validation
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

    }
}
