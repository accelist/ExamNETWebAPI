using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.RequestModels.BookedTicket;
using Contracts.ResponseModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIExam
{
    [Route("api/v1/book-ticket")]
    [ApiController]
    public class BookedTicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookedTicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<CreateBookedTicketResponse>> Post([FromBody] CreateBookedTicketRequest request,
            [FromServices] IValidator<CreateBookedTicketRequest> validator, CancellationToken cancellationToken)
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

