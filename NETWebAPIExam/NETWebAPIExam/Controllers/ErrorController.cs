using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NETWebAPIExam.Controllers
{
    [ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public IActionResult HandleError() => Problem();
    }
}
