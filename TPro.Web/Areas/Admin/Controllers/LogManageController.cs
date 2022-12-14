using Microsoft.AspNetCore.Mvc;

namespace TPro.Web.Areas.Admin.Controllers
{
    public class LogManageController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RequestLog()
        {
            return View();
        }

        public IActionResult ExceptionLog()
        {
            return View();
        }
    }
}