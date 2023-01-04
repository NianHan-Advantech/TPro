using SqlDataReaderMapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace TPro.Common.Extentions
{
    public static class IDataReaderExtention
    {
        public static TEntity MapToTEntity<TEntity>(this IDataReader reader) where TEntity : class, new()
        {
            return new SqlDataReaderMapper<TEntity>(reader).Build();
        }

        public static object ToObject(this IDataReader reader, Type type)
        {
            var properties = type.GetProperties();
            var obj = Activator.CreateInstance(type);
            foreach (var property in properties)
            {
                var value = reader[property.Name];
                if (value != null)
                    property.SetValue(obj, Convert.ChangeType(value.ToString(), property.PropertyType));
            }
            return obj;
        }

        public static List<TEntity> MapToTEntities<TEntity>(this IDataReader reader) where TEntity : class, new()
        {
            var list = new List<TEntity>();
            while (reader.Read())
            {
                var t = new SqlDataReaderMapper<TEntity>(reader).Build();
                list.Add(t);
            }
            return list;
        }

        public static List<object> ToObjects(this IDataReader reader, Type type)
        {
            var list = new List<object>();
            var properties = type.GetProperties();
            while (reader.Read())
            {
                var obj = Activator.CreateInstance(type);
                foreach (var property in properties)
                {
                    try
                    {
                        var value = reader[property.Name];
                        if (value != null)
                            property.SetValue(obj, Convert.ChangeType(value.ToString(), property.PropertyType));
                    }
                    catch { }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}