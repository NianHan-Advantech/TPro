using Microsoft.AspNetCore.Mvc;
using TPro.Common.Utils;
using TPro.Web.Filters;

namespace TPro.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [BasicAuthorizeFilter]
    [Route("Admin/[controller]/[action]")]
    public class AdminBaseController : Controller
    {
        [HttpGet]
        public IActionResult Success()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult ToJsonContent(object obj)
        {
            var str = Utils.Serialize(obj);
            return Content(str);
        }
    }
}