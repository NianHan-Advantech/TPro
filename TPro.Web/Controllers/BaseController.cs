using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPro.Web.Filters;

namespace TPro.Web.Controllers
{
    [Route("[controller]/[action]")]
    [TypeFilter(typeof(CustomAuthorizeFilter))]
    public class BaseController : Controller
    {
        /// <summary>
        /// 用户无权限访问，401页面
        /// </summary>
        /// <returns></returns>
        [AllowFilter]
        [HttpGet]
        public IActionResult UnauthorizedPage()
        {
            return View("Unauthorized");
        }

        /// <summary>
        /// Web程序出现错误页面，500
        /// </summary>
        /// <returns></returns>
        [AllowFilter]
        [HttpGet]
        public IActionResult ErrorPage()
        {
            
            return View();
        }
    }
}