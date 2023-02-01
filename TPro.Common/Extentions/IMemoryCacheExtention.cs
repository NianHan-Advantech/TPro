using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace TPro.Common.Extentions
{
    public static class IMemoryCacheExtention
    {
        public static bool HasKey(this IMemoryCache cache, string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return cache.TryGetValue(key, out _);
        }

        public static bool HasKey<T>(this IMemoryCache cache, string key, out T t) where T : class, new()
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            cache.TryGetValue(key, out t);
            return true;
        }

        public static bool Set(this IMemoryCache cache, string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            cache.Set(key, value,
                new MemoryCacheEntryOptions().SetSlidingExpiration(expiresSliding)
                    .SetAbsoluteExpiration(expiressAbsoulte));
            return cache.HasKey(key);
        }
        /// <summary>
        /// 获取所有缓存键
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCacheKeys(this IMemoryCache cache)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var entries = cache.GetType().GetField("_entries", flags).GetValue(cache);
            var cacheItems = entries as IDictionary;
            var keys = new List<string>();
            if (cacheItems == null) return keys;
            foreach (DictionaryEntry cacheItem in cacheItems)
            {
                keys.Add(cacheItem.Key.ToString());
            }
            return keys;
        }
    }
}