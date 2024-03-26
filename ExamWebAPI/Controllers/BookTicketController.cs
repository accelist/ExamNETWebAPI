using Contracts.RequestModel.BookTicket;
using Contracts.ResponseModel.BookTicket;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamWebAPI.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class BookTicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookTicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/<BookTicketController>/5
        [HttpGet("get-booked-ticket/{id}")]
        public async Task<ActionResult<GetBookTicketResponse>> Get(string id, [FromServices] IValidator<GetBookTicketRequest> validator, CancellationToken cancellationToken)
        {
            var request = new GetBookTicketRequest
            {
                BookId = id
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

        // POST api/<BookTicketController>
        [HttpPost("book-ticket")]
        public async Task<ActionResult<CreateBookTicketResponse>> Post([FromBody] CreateBookTicketRequest request, [FromServices] IValidator<CreateBookTicketRequest> validator, CancellationToken cancellationToken)
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

        // PUT api/<BookTicketController>/5
        [HttpPut("{bookcode}")]
        public async Task<ActionResult<UpdateBookTicketResponse>> Put(string bookcode, [FromBody] UpdateBookTicketModel model,
            [FromServices] IValidator<UpdateBookTicketRequest> validator, CancellationToken cancellationToken)
        {
            var request = new UpdateBookTicketRequest { BookCode = bookcode, Quantity = model.Quantity };
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        // DELETE api/<BookTicketController>/5
        [HttpDelete("{bookcode}/{tickedcode}/{qty}")]
        public async Task<ActionResult<DeleteBookTicketResponse>> Delete(string bookcode, string tickedcode, string qty, [FromServices] IValidator<DeleteBookTicketRequest> validator, CancellationToken cancellationToken)
        {
            int quantity;
            if (!int.TryParse(qty, out quantity) || quantity <= 0)
            {
                ModelState.AddModelError(nameof(qty), "Quantity must be a positive integer.");
                return BadRequest(ModelState);
            }

            var request = new DeleteBookTicketRequest
            {
                BookCode = bookcode,
                TicketCode = tickedcode,
                qty = quantity
            };

            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }

            var response = await _mediator.Send(request, cancellationToken);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                ModelState.AddModelError("", "Failed to delete booked ticket.");
                return BadRequest(ModelState);
            }
        }
    }
}
