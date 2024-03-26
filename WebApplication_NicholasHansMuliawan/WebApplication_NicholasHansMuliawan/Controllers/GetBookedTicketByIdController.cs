using System.ComponentModel.DataAnnotations;
using Contracts.BookTicketModel.RequestModel;
using Contracts.BookTicketModel.ResponseModel;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication_NicholasHansMuliawan.Controllers
{
    [Route("api/v1/get-booked-ticket")]
    [ApiController]
    public class GetBookedTicketByIdController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetBookedTicketByIdController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/<GetBookedTicketByIdController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBookedTicketByIdResponse>> Get(Guid id, CancellationToken cancellationToken,
            [FromServices] IValidator<GetBookedTicketByIdRequest> validator)
        {
            var request = new GetBookedTicketByIdRequest
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
    }
}
