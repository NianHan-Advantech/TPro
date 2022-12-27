using Microsoft.AspNetCore.Mvc;
using TPro.Business.Admin.IServiceProvider;

namespace TPro.Web.Areas.Admin.Controllers
{
    public class DbManageController : AdminBaseController
    {
        private readonly IDbService _dbService;

        public DbManageController(IDbService dbService)
        {
            _dbService = dbService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetTableInfo(string fullname)
        {
            var res = _dbService.GetTableInfo(fullname);
            return Json(res);
        }
        [HttpGet]
        public IActionResult GetTableDatas(string fullname, string tablename)
        {
            var res = _dbService.GetTableDatasByType(fullname, tablename);
            return Json(res);
        }
    }
}