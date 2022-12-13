using System;

namespace TPro.Common.Extentions
{
    public static class TypeExtention
    {
        public static dynamic GetDefaultValue(this Type type)
        {
            if (type.IsValueType)
            {
                return 0;
            }
            else if (type.Name.StartsWith("String"))
            {
                return "";
            }
            else
            {
                return type.Assembly.CreateInstance(type.FullName) ?? "";
            }
        }
    }
}