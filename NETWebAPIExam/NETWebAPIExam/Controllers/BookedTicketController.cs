using Contracts.Request.BookedTicket;
using Contracts.Request.Ticket;
using Contracts.Response.BookedTicket;
using Contracts.Response.Ticket;
using Entity.Entity;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NETWebAPIExam.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class BookedTicketController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DBContext _db;

        public BookedTicketController(IMediator mediator,DBContext db)
        {
            _mediator = mediator;
            _db = db;
        }
        // GET: api/<BookedTicketController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            /*var tdaw = (from t in _db.Tickets
                             join bt in _db.BookedTickets on t.TicketId equals bt.TicketId
                             select new BookedTicket
                             {
                                 BookedTicketId = bt.BookedTicketId
                             }).AsNoTracking().ToList();*/

            return new string[] { "value1", "value2" };
        }

        // GET api/<BookedTicketController>/5
        [HttpGet("get-booked-ticket/{id}")]
        //[Route("Get-Booked-Ticket/{BookedTicketId}")]
        public async Task<ActionResult<GetBookedResponse>> Get(Guid id, [FromServices] IValidator<GetBookedRequest> validator, CancellationToken cancellationToken)
        {
            var request = new GetBookedRequest
            {
                BookedId = id,
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

        // POST api/<BookedTicketController>
        [HttpPost]
        [Route("book-ticket")]
        public async Task<ActionResult<CreateBookedTicketResponse>> Post([FromBody] CreateBookedTicketRequest request, [FromServices] IValidator<CreateBookedTicketRequest> validator, CancellationToken cancellationToken)
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

        // PUT api/<BookedTicketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookedTicketController>/5
        [HttpDelete("/{BookedTicketId}/{KodeTicket}/{Quantity}")]
        public async Task<ActionResult<DeleteBookedResponse>> Delete(Guid BookedTicketId,string KodeTicket,int Quantity, [FromServices] IValidator<DeleteBookRequesr> validator, CancellationToken cancellationToken)
        {
            var request = new DeleteBookRequesr
            {
                Quantity = Quantity,
                BookedId = BookedTicketId,
                TicketCode = KodeTicket
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
