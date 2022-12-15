using TPro.EntityFramework.Entity;
using TPro.Models.ResponseDtos;

namespace TPro.Business.Admin.IServiceProvider
{

    public interface IMenuService
    {
        ResponseModel GetMenuTree();

        ResponseModel SaveMenuNode(Menu menu);

        ResponseModel RemoveMenuNode(int mid);
    }
}