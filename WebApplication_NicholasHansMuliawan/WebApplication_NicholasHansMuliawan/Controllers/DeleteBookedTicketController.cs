using Contracts.BookTicketModel.RequestModel;
using Contracts.BookTicketModels.RequestModel;
using Contracts.BookTicketModels.ResponseModel;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication_NicholasHansMuliawan.Controllers
{
    [Route("api/v1/revoke-ticket")]
    [ApiController]
    public class DeleteBookedTicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeleteBookedTicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // DELETE api/<DeleteBookedTicketController>/5
        [HttpDelete]
        public async Task<ActionResult<DeleteUpdateBookTicketResponse>> Delete([FromQuery] DeleteBookTicketRequest request , CancellationToken cancellationToken,
            [FromServices] IValidator<DeleteBookTicketRequest> validator)
        {

            //var validationResult = await validator.ValidateAsync(request);
            //if (!validationResult.IsValid)
            //{
            //    validationResult.AddToModelState(ModelState);
            //    return ValidationProblem(ModelState);
            //}

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
