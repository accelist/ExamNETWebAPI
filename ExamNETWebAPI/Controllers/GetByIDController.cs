using Contracts.Request;
using Contracts.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamNETWebAPI.Controllers
{
    [Route("api/v1/get-booked-ticket")]
    [ApiController]
    public class GetByIDController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetByIDController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/<GetByIDController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketData>> Get(Guid id, CancellationToken cancellationToken)
        {
            var request = new GetTicketRequest();
            var response = await _mediator.Send(request, cancellationToken);

            var ticketData = response.TicketDatas.FirstOrDefault(data => data.TicketID == id);

            if (ticketData == null)
            {
                return NotFound();
            }

            return Ok(ticketData);
        }
    }
}
