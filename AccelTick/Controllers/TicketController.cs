using Microsoft.AspNetCore.Mvc;
using Contracts.ResponseModels.Tickets;
using MediatR;
using Contracts.RequestModels.Tickets;
using Contracts.ResponseModels.BookedTickets;
using Contracts.RequestModels.BookedTickets;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Formats.Asn1;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccelTick.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<TicketController>
        [HttpGet("get-available-tickets")]
        public async Task<ActionResult<GetTicketDataListResponse>> Get([FromQuery]GetTicketDataListRequest query,CancellationToken cancellationToken)
        {
            
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        // GET api/<TicketController>/5
        [HttpGet("get-booked-ticket/{id}")]
        public async Task<ActionResult<GetBookedTicketDataResponse>>Get(Guid id , [FromServices] IValidator<GetBookedTicketDataRequest> validator, CancellationToken cancellationToken)
        {
            var request = new GetBookedTicketDataRequest
            {
                TicketId = id
            };

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        // POST api/<TicketController>
        [HttpPost("book-ticket")]
        public async Task<ActionResult<CreateBookedTicketResponse>> Post([FromBody]CreateBookedTicketRequest request, [FromServices] IValidator<CreateBookedTicketRequest> validator ,CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("revoke-ticket/{BookedTicketId}/{KodeTicket}/{Qty}")]
        public void Delete(int id)
        {
        }
    }
}
