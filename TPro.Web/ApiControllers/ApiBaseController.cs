using Microsoft.AspNetCore.Mvc;
using TPro.Common.Utils;
using TPro.Web.Filters;

namespace TPro.Web.ApiControllers
{
    /// <summary>
    ///
    /// </summary>
    [Route("Api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName ="API")]
    [BasicAuthorizeFilter]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        //[ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IActionResult Success()
        {
            return NoContent();
        }

        [HttpPost]
        public IActionResult ToJsonContent(object obj)
        {
            var str = Utils.Serialize(obj);
            return Content(str);
        }
    }
}