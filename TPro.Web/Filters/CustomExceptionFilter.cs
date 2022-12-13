using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPro.Web.Controllers;

namespace TPro.Web.Filters
{
    public class CustomExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var http = context.HttpContext;
            context.Result = new ViewResult() { StatusCode = 500, ViewName = "" };
        }
    }
}
