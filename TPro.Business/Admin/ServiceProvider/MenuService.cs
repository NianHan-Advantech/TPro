using System.Linq;
using TPro.Business.Admin.IServiceProvider;
using TPro.EntityFramework.Entity.MyDbEntity;
using TPro.Models.ResponseDtos;

namespace TPro.Business.Admin.ServiceProvider
{
    public class MenuService : BaseService, IMenuService
    {
        public ResponseModel GetMenuTree()
        {
            using var menuhelper = new EntityFramework.Data.MyDbData.MenuData();
            var tree = menuhelper.GetMenus();
            return tree.Any() ? Success(tree) : Fail();
        }
        public ResponseModel SaveMenuNode(Menu menu)
        {
            using var menuhelper = new EntityFramework.Data.MyDbData.MenuData();
            var res = menu.Id > 0 ? menuhelper.ModifyThenCommit(menu) : menuhelper.AddThenCommit(menu);
            return res ? Success() : Fail();
        }
        public ResponseModel RemoveMenuNode(int mid)
        {
            using var menuhelper = new EntityFramework.Data.MyDbData.MenuData();
            var menu = menuhelper.GetBy(e => e.Id == mid);
            if (menu == null) { return Fail("无效的Id"); }
            var res = menuhelper.RemoveThenCommit(menu);
            return res ? Success() : Fail();
        }

    }
}