using Microsoft.AspNetCore.Mvc;
using Contracts.RequestModel.GetAvailableTicket;
using Contracts.ResponseModel.GetAvailableTicket;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Entity.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamWebAPI.Controllers
{
    [Route("api/v1/get-available-ticket")]
    [ApiController]
    public class AvailableTicketController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AvailableTicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<GetAvailableTicketController>
        [HttpGet]
        public async Task<ActionResult<GetAvailableTicketResponse>> Get([FromQuery] GetAvailableTicketRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        // POST api/<GetAvailableTicketController>
        [HttpPost]
        public async Task<ActionResult<CreateAvailableTicketResponse>> Post([FromBody] CreateAvailableTicketRequest request, [FromServices] IValidator<CreateAvailableTicketRequest> validator, CancellationToken cancellationToken)
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


        // DELETE api/<GetAvailableTicketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
