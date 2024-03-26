using Contracts.Request.Ticket;
using Contracts.Response.Ticket;
using Entity.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NETWebAPIExam.Controllers
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
        [HttpGet]
        [Route("get-available-ticket")]
        public async Task<ActionResult<GetTicketResponse>> Get([FromQuery] GetTicketRequest request,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        // GET api/<TicketController>/5
        /*[HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<TicketController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            /*var ticket = new Ticket
            {
                TicketId = Guid.NewGuid(),
                EventDate = DateTime.ParseExact(s: "22-11-2024 11:00", format: "dd-MM-yyyy HH:mm", provider: CultureInfo.InvariantCulture),
                Quota = 56,
                TicketCode = "H001",
                TicketName = "Ibis Hotel",
                CategoryName = "Hotel",
                Price = 450000
            };
            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync();*/

        }
    }
}
