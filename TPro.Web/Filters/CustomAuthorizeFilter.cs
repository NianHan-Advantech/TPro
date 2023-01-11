using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;
using TPro.Business.IServiceProvider;
using TPro.EntityFramework.Entity.MyDbEntity;

namespace TPro.Web.Filters
{
    public class CustomAuthorizeFilter : Attribute, IAuthorizationFilter
    {
        private readonly IAuthService _authService;

        public CustomAuthorizeFilter(IAuthService authService)
        {
            _authService = authService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(it => it is IAllowAnonymousFilter))
            {
                return;
            }
            else
            {
                var http = context.HttpContext;
                var path = http.Request.Path.ToString().ToLower();
                var area = path.Split('/')[0];
                var req = context.HttpContext.Request;
                http.Request.RouteValues.TryGetValue("controller", out var controller);
                http.Request.RouteValues.TryGetValue("action", out var action);
                var route = new RoutePath
                {
                    Path = path,
                    AreaName = area == controller.ToString() ? "" : area,
                    ControllerName = controller?.ToString() ?? "",
                    ActionName = action?.ToString() ?? ""
                };
                var roleids = http.User.Claims
                    .Where(c => c.Type == ClaimTypes.GroupSid)
                    .Select(c => Convert.ToInt32(c.Value))
                    .ToList();
                var userid = Convert.ToInt32(context.HttpContext.User?.FindFirstValue(ClaimTypes.Sid));
                if (_authService.IsRoleAllowed(route, roleids))
                {
                    return;
                }
                else
                {
                    context.Result = new RedirectResult("/Base/UnauthorizedPage");
                }
            }
        }
    }
}