using Contracts.RequestModels.Ticket;
using Contracts.ResponseModels.Ticket;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamNETWebAPI.Controllers
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
        [HttpGet]
        public async Task<ActionResult<GetTicketDataListResponse>> Get(CancellationToken ct)
        {
            GetTicketDataListRequest request = new GetTicketDataListRequest();
            GetTicketDataListResponse response = await _mediator.Send(request, ct);

            return Ok(response);
        }

        // GET api/<TicketController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TicketController>
        [HttpPost]
        public async Task<ActionResult<CreateTicketDataResponse>> Post([FromBody] CreateTicketDataRequest request, CancellationToken ct)
        {
            CreateTicketDataResponse response = await _mediator.Send(request, ct);
            return Ok(response);
        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{code}")]
        public async Task<ActionResult<DeleteTicketDataResponse>> Delete(string code, [FromBody] DeleteTicketDataRequest request, CancellationToken ct)
        {
            //var request = new DeleteTicketDataRequest
            //{
            //    TicketCode = code
            //};

            var response = await _mediator.Send(request, ct);

            return Ok(response);
        }
    }
}
