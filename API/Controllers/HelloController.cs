using Microsoft.AspNetCore.Mvc;

namespace MinhaAPI.Controllers
{
    [ApiController]
    [Route("hello")]
    public class HelloController : ControllerBase{
        [HttpGet]
        public IActionResult Get(){
            return Ok(new {message = "Hello World!"});
        }
    }
}
