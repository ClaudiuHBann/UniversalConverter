using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertInfoController : Controller
    {
        private readonly ILogger<ConvertInfoController> _logger;

        public ConvertInfoController(ILogger<ConvertInfoController> logger)
        {
            _logger = logger;
        }

        [Route("[action]")]
        [HttpPost]
        public JsonResult Post([FromBody] ConvertInfo ci)
        {
            return Json(ci);
        }
    }
}