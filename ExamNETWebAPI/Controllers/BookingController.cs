using Contracts.RequestModels;
using Contracts.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamNETWebAPI.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-available-ticket")] //TODO ganti route Booking GET
        public async Task<ActionResult<GetAvailableTicketsResponse>> Get([FromQuery] GetAvailableTicketsRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        [HttpGet("get-booked-ticket/{id}")]
        public async Task<ActionResult<GetBookingDetailResponse>> Get(Guid id, CancellationToken cancellationToken)
        {
            var request = new GetBookingDetailRequest
            {
                BookedTicketId = id
            };

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        [HttpPost("book-ticket")]
        public async Task<ActionResult<BookTicketsResponse>> Post([FromBody] BookTicketsRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        [HttpPut("edit-booked-ticket/{id}")]
        public async Task<ActionResult<UpdateBookingResponse>> Put(Guid id, [FromBody] UpdateBookingRequestDataListModel model, CancellationToken cancellationToken)
        {
            var request = new UpdateBookingRequest
            {
                BookedTicketId = id,
                Tickets = model.Tickets,
            };

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        [HttpDelete("revoke-ticket/{id}/{ticketCode}/{qty}")]
        public async Task<ActionResult<DeleteTicketsResponse>> Delete(Guid id, string ticketCode, int qty, CancellationToken cancellationToken)
        {
            var request = new DeleteTicketsRequest
            {
                BookedTickedId = id,
                TicketCode = ticketCode,
                Quantity = qty,
            };

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }
    }
}
