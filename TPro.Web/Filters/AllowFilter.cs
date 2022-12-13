using Microsoft.AspNetCore.Mvc.Authorization;
using System;

namespace TPro.Web.Filters
{
    public class AllowFilter : Attribute, IAllowAnonymousFilter
    {
    }
}