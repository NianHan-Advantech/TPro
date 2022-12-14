using Microsoft.AspNetCore.Mvc;

namespace TPro.Web.Areas.Admin.Controllers
{
    public class SysManageController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JurisdictionManage()
        {
            return View();
        }

        public IActionResult UserManage()
        {
            return View();
        }

        public IActionResult RoleManage()
        {
            return View();
        }

        public IActionResult MemoryManage()
        {
            return View();
        }
    }
}