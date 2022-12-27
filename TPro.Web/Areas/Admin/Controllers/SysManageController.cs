using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TPro.Business.Admin.IServiceProvider;
using TPro.Web.MemoryCatch;

namespace TPro.Web.Areas.Admin.Controllers
{
    public class SysManageController : AdminBaseController
    {
        private readonly ISysManageService _sysManageService;
        private readonly IMemoryCache _memoryCache;

        public SysManageController(ISysManageService sysManageService, IMemoryCache memoryCache)
        {
            _sysManageService = sysManageService;
            _memoryCache = memoryCache;
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
        #region 缓存管理
        public IActionResult MemoryManage()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetMemorios()
        {
            var list = CacheKeys.GetCacheKeys(_memoryCache);
            return Ok();
        }
        #endregion

    }
}