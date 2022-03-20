using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Newtonsoft.Json;

namespace Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ConvertInfoController : Controller {
        [Route("[action]")]
        [HttpPost]
        public IActionResult Post([FromBody] ConvertInfo ci) {
            if (ci.Category < 0 || ci.Category > UConverter.UConverter.subcategoriesCount.Count) {
                return BadRequest("JSON.category does not exist!");
            }

            if (ci.Items is null) {
                return BadRequest("JSON.items are not existing!");
            }

            if (ci.From == ci.To) {
                return Ok(ci.Items);
            }

            if (ci.From < 0 && ci.From > UConverter.UConverter.subcategoriesCount[ci.Category] - 1) {
                return BadRequest("JSON.from are invalid!");
            }

            if (ci.To < 0 && ci.To > UConverter.UConverter.subcategoriesCount[ci.Category] - 1) {
                return BadRequest("JSON.to are invalid!");
            }

            if (UConverter.UConverter.uConverter[ci.Category].IsFormatted(ci.Items) is false) {
                return BadRequest("JSON.items are not correctly formatted!");
            }

            List<string>? jsonResult = new();

#if DEBUG
            Console.WriteLine(JsonConvert.SerializeObject(ci, Formatting.Indented) + '\n');
#endif

            ci.Items.ForEach((item) => jsonResult.Add(UConverter.UConverter.uConverter[ci.Category].Convert(item, ci.From, ci.To)));

#if DEBUG
            Console.WriteLine(JsonConvert.SerializeObject(jsonResult, Formatting.Indented) + "\n\n\n");
#endif

            return Ok(jsonResult);
        }
    }
}