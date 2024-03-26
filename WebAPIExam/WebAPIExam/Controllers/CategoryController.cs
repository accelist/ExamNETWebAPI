using Contracts.RequestModels.Category;
using Contracts.ResponseModels.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIExam.Controllers
{
    [Route("api/v1/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<GetCategoryResponse>> Get(CancellationToken cancellationToken)
        {
            var request = new GetCategoryRequest();
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult<CreateCategoryResponse>> Post([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
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
