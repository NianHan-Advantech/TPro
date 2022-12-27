using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TPro.Web.Filters;
using TPro.Web.MemoryCatch;

namespace TPro.Web.Controllers
{
    /// <summary>
    /// 对外开放的页面
    /// </summary>
    [AllowFilter]
    public class OpenController : BaseController
    {
        private readonly IMemoryCache _memoryCache;

        public OpenController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult getmsg()
        {
            _memoryCache.Set(CacheKeys.Entry, "hello");
            return Ok();
        }
    }
}