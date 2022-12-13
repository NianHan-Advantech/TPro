using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TPro.Common.Extentions
{
    public static class ObjectExtention
    {
        public static List<PropertyInfo> GetFieldProperties(this object obj)
        {
            return obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
        }

        public static Dictionary<string, string> GetFieldProperties1(this object obj)
        {
            var fps = new Dictionary<string, string>();
            if (obj == null)
            {
                return fps;
            }
            var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return fps;
            }
            foreach (var item in properties)
            {
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    fps.Add(item.Name, item.PropertyType.Name);
                }
            }
            return fps;
        }
    }
}