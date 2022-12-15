using TPro.Models.ResponseDtos;

namespace TPro.Business.Admin.IServiceProvider
{
    public interface ISysManageService
    {
        ResponseModel GetJurisdictions();

        ResponseModel GetRoutePaths();

        ResponseModel GetUsers();

        ResponseModel GetRoles();
    }
}