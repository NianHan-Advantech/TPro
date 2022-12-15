using System.Linq;
using TPro.Business.Admin.IServiceProvider;
using TPro.EntityFramework.Entity;
using TPro.Models.ResponseDtos;

namespace TPro.Business.Admin.ServiceProvider
{
    public class SysManageService : BaseService, ISysManageService
    {
        #region 权限

        public ResponseModel GetJurisdictions()
        {
            var helper = new EntityFramework.Data.JurisdictionData();
            var rlist = helper.GetAll().ToList();
            return rlist.Any() ? Success(rlist) : Fail();
        }
        public ResponseModel SaveJurisdiction(Jurisdiction jurisdiction)
        {
            return Success();
        }

        #endregion 权限

        #region 路由

        public ResponseModel GetRoutePaths()
        {
            var helper = new EntityFramework.Data.RoutePathData();
            var rlist = helper.GetAll().ToList();
            return rlist.Any() ? Success(rlist) : Fail();
        }

        #endregion 路由

        #region 用户

        public ResponseModel GetUsers()
        {
            var helper = new EntityFramework.Data.TPUserData();
            var rlist = helper.GetAll().ToList();
            return rlist.Any() ? Success(rlist) : Fail();
        }

        #endregion 用户

        #region 角色

        public ResponseModel GetRoles()
        {
            var helper = new EntityFramework.Data.TPRoleData();
            var rlist = helper.GetAll().ToList();
            return rlist.Any() ? Success(rlist) : Fail();
        }

        #endregion 角色
    }
}