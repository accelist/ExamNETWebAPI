using Contracts.RequestModels.BookedTicket;
using Contracts.ResponseModels.BookedTicket;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebExamApi.Controllers
{
    [Route("api/v1/bookedticket")]
    [ApiController]
    public class BookTicketController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookTicketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<BookTicketController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BookTicketController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookTicketController>
        [HttpPost]
        public async Task<ActionResult<CreateBookedTicketResponse>> Post([FromBody] CreateBookedTicketRequest model, [FromServices] IValidator<CreateBookedTicketRequest> validator, CancellationToken ct)
        {
            var validate = await validator.ValidateAsync(model);
            if (!validate.IsValid)
            {
                validate.AddToModelState(ModelState);
                return ValidationProblem();
            }
            var response = await _mediator.Send(model, ct);
            return Ok(response);
        }

        // PUT api/<BookTicketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookTicketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
