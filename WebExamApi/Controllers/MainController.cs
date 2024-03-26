﻿/*using Contracts.RequestModels.Category;
using Contracts.RequestModels.Ticket;
using Contracts.ResponseModels.Ticket;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebExamApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class MainController : ControllerBase
    {

        private readonly IMediator _mediator;
        public MainController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<MainController>
        [HttpGet("get-available-ticket")]
        public async Task<ActionResult<GetTicketDataResponse>> Get(CancellationToken ct)
        {
            var request = new GetTicketDataRequest();
            var response = await _mediator.Send(request,ct);
            return (response);
        }

        // GET api/<MainController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MainController>
        [HttpPost]
        public async Task<ActionResult<CreateTicketResponse>> Post([FromBody] CreateTicketRequest model, [FromServices] IValidator <CreateTicketRequest> validator, CancellationToken ct)
        {
            var validate = await validator.ValidateAsync(model);
            if (!validate.IsValid)
            {
                validate.AddToModelState(ModelState);
                return ValidationProblem();
            }
            var response = await _mediator.Send(model, ct);
            return Ok(response);
        }

        // PUT api/<MainController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MainController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
*/