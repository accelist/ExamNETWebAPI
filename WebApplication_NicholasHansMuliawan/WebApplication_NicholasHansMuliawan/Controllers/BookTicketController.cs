using System.ComponentModel.DataAnnotations;
using Contracts.BookTicketModel.RequestModel;
using Contracts.BookTicketModels.RequestModel;
using Contracts.BookTicketModels.ResponseModel;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication_NicholasHansMuliawan.Controllers
{
    [Route("api/book-ticket")]
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
        public async Task<ActionResult<GetBookTicketDataResponse>> Get(CancellationToken cancellationToken)
        {
            var request = new GetBookTicketDataRequest();
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        // POST api/<BookTicketController>
        [HttpPost]
        public async Task<ActionResult<PostBookTicketRequest>> Post([FromBody] PostBookTicketRequest request, CancellationToken cancellationToken,
            [FromServices] IValidator<PostBookTicketRequest> validator)
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
