using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TPro.Business.IServiceProvider;
using TPro.EntityFramework.Entity;
using TPro.Models.AuthDtos;
using TPro.Web.Filters;

namespace TPro.Web.Controllers
{

    public class TPUserController : BaseController
    {
        private readonly IAuthService _authService;

        public TPUserController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [AllowFilter]
        [Route("/TPro/Login")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [AllowFilter]
        [HttpPost]
        public async Task<IActionResult> UserLogin([FromBody]AuthTPUser authTPUser)
        {
            var res = _authService.UserLogin(authTPUser);
            if (res.Code != 200) return Json(res);
            var user = res.Data as TPUser;
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid,user.Id.ToString()),//userid
                new Claim(ClaimTypes.Name,user.Account),//username
                new Claim(ClaimTypes.Email,user.Email),//useremail
            };
            foreach (var item in user.URs)
            {
                claims.Add(new Claim(ClaimTypes.GroupSid, item.RoleId.ToString()));//roleid
                claims.Add(new Claim(ClaimTypes.Role, item.Role.Name));//rolename
            }
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Ok() ;
        }

        [HttpGet]
        public async Task<IActionResult> UserLogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
    }
}