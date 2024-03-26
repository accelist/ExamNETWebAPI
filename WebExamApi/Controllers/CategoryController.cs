using Contracts.RequestModels.Category;
using Contracts.ResponseModels.Category;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebExamApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<CategoryController>
        [HttpGet("Get Category")]
        public async Task<ActionResult<GetCategoryDataResponse>> Get(CancellationToken ct)
        {
            var request = new GetCategoryDataRequest();
            var response = await _mediator.Send(request, ct);
            return Ok(response);    
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoryController>
        [HttpPost("Category")]
        public async Task <ActionResult<CreateCategoryDataResponse>>  Post([FromBody] CreateCategoryDataRequest model, [FromServices] IValidator <CreateCategoryDataRequest> validator,CancellationToken ct )
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

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
