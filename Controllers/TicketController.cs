using Contracts.RequestModels;
using Contracts.ResponseModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIForExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;
        // GET: api/<TicketController>
        [Route("api/v1/get-available-ticket")]
        [HttpGet]
        public async Task<ActionResult<GetTicketResponse>> Get([FromBody] GetTicketRequest request,
            [FromServices] IValidator<GetTicketRequest> validator, CancellationToken ct)
        {
            var data = new GetTicketRequest { CategoryName = request.CategoryName, EventDate = request.EventDate, TicketCode = request.TicketCode, TicketName = request.TicketName};
            var validation = await validator.ValidateAsync(data);
            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState);
                ValidationProblem(ModelState);
            }

            var response = await _mediator.Send(request, ct);
            return Ok(response);
        }

        // GET api/<TicketController>/5
        [Route("api/v1/get-booked-ticket/{TicketId}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTicketByIdResponse>> Get(string id,
            [FromServices] IValidator<GetTicketByIdRequest> validator, CancellationToken ct)
        {
            var request = new GetTicketByIdRequest { TicketId = id };
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState);
                ValidationProblem(ModelState);
            }

            var response = await _mediator.Send(request, ct);
            return Ok(response);
        }

        [Route("api/v1/book-ticket")]
        // POST api/<TicketController>
        [HttpPost]
        public async Task<ActionResult<BookTicketResponse>> Post([FromBody] BookTicketRequest request,
            [FromServices] IValidator<BookTicketRequest> validator, 
            CancellationToken ct)
        {
            var req = new BookTicketRequest
            {
                TicketCode = request.TicketCode,
                Quantity = request.Quantity,

            };

            var response = await _mediator.Send(req, ct);
            return Ok(response);
        }

        // PUT api/<TicketController>/5
        [Route("api/v1/edit-booked-ticket/{TicketId}")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateTicketResponse>> Put(string id, [FromBody] UpdateTicketRequest request,
            [FromServices] IValidator<UpdateTicketRequest> validator, CancellationToken ct)
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
            var response = await _mediator.Send(request, ct);
            return Ok(response);
        }

        // DELETE api/<TicketController>/5
        [Route("api/v1/revoke-ticket/{tickedCode}/{Qty}")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteTicketResponse>> Delete(string ticketCode, int quantity,
            [FromServices] IValidator<DeleteTicketRequest> validator, CancellationToken ct)
        {
            var request = new DeleteTicketRequest
            {
                Quantity = quantity,
                TicketId = ticketCode
            };

            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
            var response = await _mediator.Send(request, ct);
            return Ok(response);
        }
    }
}