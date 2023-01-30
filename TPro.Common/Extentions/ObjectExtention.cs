using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace TPro.Common.Extentions
{
    public static class ObjectExtention
    {
        public static List<PropertyInfo> GetFieldProperties(this object obj)
        {
            return obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
        }

        /// <summary>
        /// 通过属性名获取对象值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static dynamic GetValue(this object obj, string name)
        {
            var property = obj.GetType().GetProperty(name);
            return Convert.ChangeType(property.GetValue(obj), property.PropertyType);
        }

        public static dynamic JavascriptTypeToValue(this object obj)
        {
            return 1;
        }
    }
    public static class JsonElementExtention
    {

        public static dynamic ToValue(this JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Undefined:
                    break;
                case JsonValueKind.Object:
                    break;
                case JsonValueKind.Array:
                    break;
                case JsonValueKind.String:
                    return element.GetString();
                case JsonValueKind.Number:
                    return element.GetInt32();
                case JsonValueKind.True:
                case JsonValueKind.False:
                    return element.GetBoolean();
                case JsonValueKind.Null:
                    return null;
                default:
                    break;
            }
            return null;
        }
        public static dynamic ToValue(this JsonElement element, Type propertytype)
        {
            return Convert.ChangeType(element.ToValue(), propertytype);
        }
    }
}