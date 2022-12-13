using Newtonsoft.Json;

namespace TPro.Common.Utils
{
    public static class Utils
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="TObj"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<TObj>(TObj obj)
        {
            var setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            };
            return JsonConvert.SerializeObject(obj, setting);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="TObj"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static TObj DSerialize<TObj>(string json) where TObj : class, new()
        {
            return JsonConvert.DeserializeObject<TObj>(json);
        }
    }
}