using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TPro.Common.Extentions;
using TPro.Common.Utils;
using TPro.EntityFramework;
using TPro.EntityFramework.Entity;

namespace TPro.Web.Filters
{
    public class RequestLogFilter : Attribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var req = context.HttpContext.Request;
            var res = context.HttpContext.Response;
            var uemail = context.HttpContext.User?.FindFirstValue(ClaimTypes.Email) ?? "未登录";
            var reqstr = "";
            if (req.Method == "POST")
            {
                reqstr = req.Body.ReadToString();
            }
            else if (req.Method == "GET")
            {
                reqstr = Utils.Serialize(req.Query);
            }
            var rlog = new ReqLog()
            {
                HandleTime= DateTime.Now,
                ReqStr=reqstr,
                UserInfo=uemail
            };
            var db = new MyDbContext();
            db.ReqLog.Add(rlog);
            db.SaveChanges();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            return;
        }
    }
}
