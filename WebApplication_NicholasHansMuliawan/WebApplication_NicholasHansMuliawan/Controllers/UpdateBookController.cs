using Contracts.BookTicketModels.RequestModel;
using Contracts.BookTicketModels.ResponseModel;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication_NicholasHansMuliawan.Controllers
{
    [Route("api/edit-booked-ticket")]
    [ApiController]
    public class UpdateBookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdateBookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // PUT api/<UpdateBookController>/5
        [HttpPut]
        public async Task<ActionResult<DeleteUpdateBookTicketResponse>> Put([FromQuery] UpdateBookTicketRequest request, CancellationToken cancellationToken,
            [FromServices] IValidator<DeleteBookTicketRequest> validator)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
