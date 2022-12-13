using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace TPro.Web.Filters
{
    public class BasicAuthorizeFilter :Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(it => it is IAllowAnonymous))
            {
                return;
            }
            else
            {
                var http = context.HttpContext;
                var a = context.ActionDescriptor;
                //context.Result = new ViewResult { StatusCode = 500 };
                var auth = context.HttpContext.Request.Headers["Authorization"];
                //if (auth == null)
                //{
                //    context.Result =new UnauthorizedResult();
                //}
                //var authparts=auth.
            }
        }

        private bool ValidateBasic()
        {
            return true;
        }
    }
}