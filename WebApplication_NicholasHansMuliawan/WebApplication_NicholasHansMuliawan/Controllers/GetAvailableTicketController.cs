using Contracts.TicketData.RequestModels;
using Contracts.TicketData.ResponseModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication_NicholasHansMuliawan.Controllers
{
    [Route("api/v1/get-available-ticket")]
    [ApiController]
    public class GetAvailableTicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetAvailableTicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<TicketController>
        [HttpGet]
        public async Task<ActionResult<GetTicketDataListResponse>> Get([FromQuery] GetTicketDataListRequest request,CancellationToken cancellationToken,
            [FromServices] IValidator<GetTicketDataListRequest> validator)
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

        // POST api/<TicketController>
        [HttpPost]
        public async Task<ActionResult<PostTicketDataResponse>> Post([FromBody] PostTicketDataRequest request, CancellationToken cancellationToken)
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
