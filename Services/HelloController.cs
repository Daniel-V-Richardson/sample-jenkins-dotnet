using Microsoft.AspNetCore.Mvc;

namespace ABPFramworkProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        private readonly HelloService _helloService;

        public HelloController(HelloService helloService)
        {
            _helloService = helloService;
        }

        [HttpGet("SayHi")]
        public IActionResult SayHi()
        {
            var result = _helloService.SayHi();
            return Ok(result);
        }

        [HttpGet("FetchFromChecklist")]
        public async Task<IActionResult> FetchFromChecklist()
        {
            var result = await _helloService.FetchFromChecklist();
            return Ok(result);
        }
    }
}
