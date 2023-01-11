using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TPro.Business.IServiceProvider;
using TPro.EntityFramework.Entity.MyDbEntity;
using TPro.Models.AuthDtos;
using TPro.Models.ResponseDtos;

namespace TPro.Business.ServiceProvider
{
    public class AuthService : BaseService, IAuthService
    {
        public bool IsRoleAllowed(string route, string rolename)
        {
            return true;
        }

        public bool IsRoleAllowed(RoutePath routePath, IEnumerable<int> roleids)
        {
            
            using var routehelp = new EntityFramework.Data.MyDbData.RoutePathData();
            var exist = routehelp.IsPathExist(routePath.Path);
            if (exist)
            {
                return routehelp.IsRoleAllowed(routePath.Path, roleids);
            }
            else
            {
                var res = routehelp.AddThenCommit(routePath);
                if (!res) return false;
                using var jushelp = new EntityFramework.Data.MyDbData.JurisdictionData();
                var jus = new Jurisdiction()
                {
                    RouteId = routePath.Id,
                    RoleId = 1,
                    IsAllowed = true
                };
                res = jushelp.AddThenCommit(jus);
                if (!res) return false;
                return IsRoleAllowed(routePath, roleids);
            }
        }

        public ResponseModel UserLogin(AuthTPUser authTPUser)
        {
            using var userhelper = new EntityFramework.Data.MyDbData.TPUserData();
            var user = userhelper.IsAccountExist(authTPUser.Account);
            if (user == null) return Fail("用户不存在，请联系管理员");
            var right = authTPUser.PassWord == user.PassWord;
            return right ? Success(user) : Fail("密码错误");
        }

    }
}