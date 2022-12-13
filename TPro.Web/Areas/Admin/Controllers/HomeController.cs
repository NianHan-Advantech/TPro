using Microsoft.AspNetCore.Mvc;
using TPro.Business.Admin.IServiceProvider;

namespace TPro.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly IDbService _dbService;
        private readonly IMenuService _menuService;

        public HomeController(IDbService dbService, IMenuService menuService)
        {
            _dbService = dbService;
            _menuService = menuService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Iscm()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Iscm1()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetMenuTree()
        {
            var res = _menuService.GetMenuTree();
            return ToJsonContent(res);
        }

        [HttpGet]
        public IActionResult GetAllTableNames()
        {
            var res = _dbService.GetAllTableNames();
            return Ok(res);
        }
    }
}