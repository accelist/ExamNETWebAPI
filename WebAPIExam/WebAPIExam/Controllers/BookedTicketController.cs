using Contracts.RequestModels.BookedTicket;
using Contracts.ResponseModels.BookedTicket;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIExam.Controllers
{
    [Route("api/v1/booked")]
    [ApiController]
    public class BookedTicketController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookedTicketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<BookedTicketController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<BookedTicketController>/5
        [HttpGet("get-booked-ticket/{id}")]
        public async Task<ActionResult<BookedTicketDetailResponse>> Get(Guid id, CancellationToken cancellationToken)
        {
            var request = new BookedTicketDetailRequest { BookedId = id };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        // POST api/<BookedTicketController>
        [Route("book-ticket")]
        [HttpPost]
        public async Task<ActionResult<BookTicketResponse>> Post([FromBody] BookTicketRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        // PUT api/<BookedTicketController>/5
        [HttpPut("edit-booked-ticekt/{id}")]
        public async Task<ActionResult<EditBookedTicketResponse>>Put(Guid id,[FromBody] EditBookedTicketModel requestModel, CancellationToken cancellationToken)
        {
            var request = new EditBookedTicketRequest
            {
                BookedId = id,
                Quantity = requestModel.Quantity,
            };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        // DELETE api/<BookedTicketController>/5
        [HttpDelete("revoke-ticket/{id}/{code}/{qty}")]
        public async Task<ActionResult<DeleteBookedTicketsResponse>> Delete(Guid id, string code, int qty, CancellationToken cancellationToken)
        {
            var request = new DeleteBookedTicketsRequest
            {
                BookedId = id,
                TicketCode = code,
                Quantity = qty,
            };

            var result = await _mediator.Send(request, cancellationToken);

            return Ok(result);
        }
    }
}
