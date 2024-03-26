using Contracts.Request;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamNETWebAPI.Controllers
{
    [Route("api/v1/revoke-ticket")]
    [ApiController]
    public class DeleteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeleteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // DELETE api/<DeleteController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteTicketRequest>> Delete([FromBody] DeleteTicketRequest request, [FromServices] IValidator<DeleteTicketRequest> validator, CancellationToken cancellationToken)
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
    }
}
