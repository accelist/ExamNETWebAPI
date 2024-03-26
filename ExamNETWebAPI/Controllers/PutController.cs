using Contracts.Request;
using Contracts.Response;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamNETWebAPI.Controllers
{
    [Route("api/v1/edit-booked-ticket")]
    [ApiController]
    public class PutController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PutController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // PUT api/<PutController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PutTicketResponse>> Put(Guid id, [FromBody] PutTicketRequest request, [FromServices] IValidator<PutTicketRequest> validator, CancellationToken cancellationToken)
        {
            request.BookedID = id;
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
