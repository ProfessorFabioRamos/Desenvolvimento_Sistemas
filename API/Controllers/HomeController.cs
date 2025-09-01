using Microsoft.AspNetCore.Mvc;

namespace MinhaAPI.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase{
        [HttpGet]
        public IActionResult Index(){
            return Ok(new {message = "Bem vindo!"});
        }
    }
}
