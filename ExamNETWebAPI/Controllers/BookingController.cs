using Contracts.RequestModels.Booking;
using Contracts.ResponseModels.Booking;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Validators.Booking;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamNETWebAPI.Controllers
{
    [Route("api/v1/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<BookingController>
        [HttpGet]
        public async Task<ActionResult<GetBookingDataListResponse>> Get(CancellationToken ct)
        {
            GetBookingDataListRequest request = new GetBookingDataListRequest();
            GetBookingDataListResponse response = await _mediator.Send(request, ct);

            return Ok(response);
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookingController>
        [HttpPost]
        public async Task<ActionResult<CreateBookingDataResponse>> Post([FromBody] CreateBookingDataRequest request, [FromServices] 
        IValidator<CreateBookingDataRequest> createBookingValidator, CancellationToken ct)
        {
            var validationResult = await createBookingValidator.ValidateAsync(request, ct);

            if(!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }

            var response = await _mediator.Send(request, ct);

            return Ok(response);
        }

        // PUT api/<BookingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
