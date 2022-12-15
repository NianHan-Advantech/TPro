using Microsoft.AspNetCore.Mvc;
using TPro.Business.Admin.IServiceProvider;

namespace TPro.Web.Areas.Admin.Controllers
{
    public class SysManageController : AdminBaseController
    {
        private readonly ISysManageService _sysManageService;

        public SysManageController(ISysManageService sysManageService)
        {
            _sysManageService = sysManageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region 权限管理

        public IActionResult JurisdictionManage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetRouteJurisd()
        {
            var res = _sysManageService.GetJurisdictions();
            return Json(res);
        }

        #endregion 权限管理

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