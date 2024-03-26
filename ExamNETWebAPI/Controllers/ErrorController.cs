using Microsoft.AspNetCore.Mvc;

namespace ExamNETWebAPI.Controllers
{
    
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public IActionResult HandleError() => Problem();
    }
}
