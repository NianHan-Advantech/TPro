using Microsoft.AspNetCore.Mvc;
using TPro.Business.Admin.IServiceProvider;
using TPro.EntityFramework.Entity.MyDbEntity;

namespace TPro.Web.Areas.Admin.Controllers
{
    public class SetController : AdminBaseController
    {
        private readonly IMenuService _menuService;

        public SetController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MenuTree()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveMenuNode([FromBody]Menu menu)
        {
            var res = _menuService.SaveMenuNode(menu);
            return Json(res);
        }
        [HttpGet]
        public IActionResult RemoveMenuNode(int mid)
        {
            var res = _menuService.RemoveMenuNode(mid);
            return Json(res);
        }
    }
}