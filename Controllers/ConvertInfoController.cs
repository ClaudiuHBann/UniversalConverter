using Microsoft.AspNetCore.Mvc;
using Server.Models;

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
            if (ci.Items is null)
            {
                return Json(null);
            }

            List<string>? jsonResult = new();

            foreach (var item in ci.Items)
            {
                jsonResult.Add(UConverter.UConverter.methods[ci.Category](item, ci.From, ci.To));
            }

            return Json(jsonResult);
        }
    }
}