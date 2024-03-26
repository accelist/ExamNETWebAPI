using Contracts.RequestModels.Ticket;
using Contracts.RequestModels.TicketRequest;
using Contracts.ResponseModels.Ticket;
using Contracts.ResponseModels.TicketResponse;
using Entity.Entity;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Validator.Ticket;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiExam.Controllers
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
        [HttpGet("get-available-ticket")]
        public async Task<ActionResult<GetTicketDataResponse>> Get( CancellationToken cancellationToken)
        {
            var request = new GetTicketDataRequest();

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        [HttpGet("get-booked-ticket")]
        public async Task<ActionResult<GetBookListresponse>> Gets(CancellationToken cancellationToken)
        {
            var request = new GetBookListRequest();
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        // GET api/<TicketController>/5
        [HttpGet("get-available-ticket/{BookedTicketId}")]
        public async Task<ActionResult<GetBookTicketResponse>> Get(Guid BookedTicketId, [FromServices]GetBookTicketValidator validator, CancellationToken cancellationToken)
        {
            var request = new GetBookTicketRequest
            {
                BookedId = BookedTicketId
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
        [HttpPost("add-ticket")]
        public async Task<ActionResult<CreateTicketResponse>> Post([FromBody] CreateTicketRequest request, [FromServices] IValidator<CreateTicketRequest> validator, CancellationToken cancellationToken)
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

        [HttpPost("book-ticket")]
        public async Task<ActionResult<BookTicketRespponse>> Post(Guid id, int quota, [FromServices]IValidator<BookTicketRequest> validator, CancellationToken cancellationToken)
        {
            var request = new BookTicketRequest
            {
                TicketId = id,
                Quantity = quota
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

        // PUT api/<TicketController>/5
        [HttpPut("edit-booked-item/{BookedTicktetId}")]
        public async Task<ActionResult<UpdateBookDataResponse>> Put(Guid BookedTicktetId, [FromBody] UpdateBookDataModel model, [FromServices]IValidator<UpdateBookDataRequest> validator, CancellationToken cancellationToken)
        {
            var request = new UpdateBookDataRequest
            {
                BookId = BookedTicktetId,
                Quantity = model.Quantity,
                TicketId = model.TicketId
            };
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("revoke-ticket/{BookedTicktetId}/{KodeTicket}/{Qty}")]
        public async Task<ActionResult<DeleteBookTicketResponse>> Delete(Guid BookedTicktetId, Guid KodeTicket, int Qty,[FromServices]DeleteBookTicketValidator validator, CancellationToken cancellationToken)
        {
            var request = new DeleteBookTicketRequest
            {
                BookedId = BookedTicktetId,
                TicketId = KodeTicket,
                Quantity = Qty
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
    }
}
