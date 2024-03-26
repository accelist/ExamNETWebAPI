using Contracts.RequestModels.Category;
using Contracts.RequestModels.Ticket;
using Contracts.ResponseModels.Category;
using Contracts.ResponseModels.Ticket;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamNETWebAPI.Controllers
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
        public async Task<ActionResult<GetCategoryDataListResponse>> Get(CancellationToken ct)
        {
            GetCategoryDataListRequest request = new GetCategoryDataListRequest();
            GetCategoryDataListResponse response = await _mediator.Send(request, ct);

            return Ok(response);
        }


        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult<CreateCategoryDataResponse>> Post([FromBody] CreateCategoryDataRequest request, CancellationToken ct)
        {
            CreateCategoryDataResponse response = await _mediator.Send(request, ct);
            return Ok(response);
        }


        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
