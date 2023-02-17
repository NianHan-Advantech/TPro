using Microsoft.AspNetCore.Mvc;

namespace TPro.Web.ApiControllers
{
    /// <summary>
    /// 对外开放的接口
    /// </summary>
    public class OpenController : ApiBaseController
    {
        [HttpGet]
        public IActionResult GetMsg()
        {
            return Ok("后端消息");
        }
    }
}