using Contracts.RequestModels;
using Contracts.RequestModels.Unbooked;
using Contracts.ResponseModels;
using Contracts.ResponseModels.Unbooked;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamNETWebAPI.Controllers
{
    [Route("api/v1/TicketingService")]
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
        public async Task<ActionResult<GetTicketResponse>> GetAvailableTicket(CancellationToken cancellationToken)
        {
            var request = new GetTicketRequest();
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        [HttpGet("get-booked-ticket")]
        public async Task<ActionResult<GetBookedTicketResponse>> GetBookedTicket(CancellationToken cancellationToken)
        {
            var request = new GetBookedTicketRequest();
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        // GET api/<TicketController>/5
        [HttpGet("GetBookedTicketById{id}")]
        public async Task<ActionResult<DetailBookedResponse>> Get(Guid id, CancellationToken cancellationToken)
        {
            var request = new DetailBookedRequest()
            {
                BookId = id
            };

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        // POST api/<TicketController>
        [HttpPost("create-ticket")]
        public async Task<ActionResult<CreateTicketResponse>> Post([FromBody] CreateTicketRequest request, [FromServices] IValidator<CreateTicketRequest> validator, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }
        
        [HttpPost("book-ticket")]
        public async Task<ActionResult<BookTicketResponse>> Post([FromBody] BookTicketRequest request, [FromServices] IValidator<BookTicketRequest> validator, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);

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
        public async Task<ActionResult<UpdateBookedTicketRequest>> Put(Guid id, [FromBody] UpdateTicketModel model, [FromServices] IValidator<UpdateBookedTicketRequest> validator, CancellationToken cancellationToken)
        {
            var request = new UpdateBookedTicketRequest
            {
                BookId = id,
                UpdateTickets = new List<UpdateTicketModel>
                {
                    new UpdateTicketModel
                    {
                        TicketCode = model.TicketCode,
                        BuyQuantity = model.BuyQuantity
                    }
                }
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
        [HttpDelete("revoke-ticket/{id}/{code}/{qty}.")]
        public async Task<ActionResult<DeleteBookedTicketRequest>> Delete(Guid id, Guid code, int qty, [FromServices] IValidator<DeleteBookedTicketRequest> validator, CancellationToken cancellationToken)
        {
            var request = new DeleteBookedTicketRequest()
            {
                BookId = id,
                TicketCode = code,
                BuyQuantity = qty
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
    }
}
