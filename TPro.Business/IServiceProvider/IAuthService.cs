using System.Collections.Generic;
using TPro.EntityFramework.Entity;
using TPro.Models.AuthDtos;
using TPro.Models.ResponseDtos;

namespace TPro.Business.IServiceProvider
{
    public interface IAuthService
    {
        bool IsRoleAllowed(RoutePath routePath, IEnumerable<int> roleids);

        ResponseModel UserLogin(AuthTPUser authTPUser);

    }
}