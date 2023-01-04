using FastMember;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace TPro.Common.Extentions
{
    public static class DbDataReaderExtention
    {
        public static T MapDataToObj<T>(this DbDataReader dataReader) where T : class, new()
        {
            var type = typeof(T);
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            var t = new T();
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                if (!dataReader.IsDBNull(i))
                {
                    var fidldname = dataReader.GetName(i);
                    if (members.Any(m => string.Equals(m.Name, fidldname)))
                    {
                        accessor[t, fidldname] = dataReader.GetValue(i);
                    }
                }
            }
            return t;
        }

        public static object MapDataToObj(this DbDataReader dataReader, Type type)
        {
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            var t = type.Assembly.CreateInstance(type.FullName);
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                if (!dataReader.IsDBNull(i))
                {
                    var fidldname = dataReader.GetName(i);
                    if (members.Any(m => string.Equals(m.Name, fidldname)))
                    {
                        accessor[t, fidldname] = dataReader.GetValue(i);
                    }
                }
            }
            return t;
        }

        public static List<T> MapDataToList<T>(this DbDataReader dataReader) where T : class, new()
        {
            var list = new List<T>();
            var type = typeof(T);
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            while (dataReader.Read())
            {
                var t = new T();
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (!dataReader.IsDBNull(i))
                    {
                        var fidldname = dataReader.GetName(i);
                        if (members.Any(m => string.Equals(m.Name, fidldname)))
                        {
                            accessor[t, fidldname] = dataReader.GetValue(i);
                        }
                    }
                }
                list.Add(t);
            }
            return list;
        }

        public static List<object> MapDataToList(this DbDataReader dataReader, Type type)
        {
            var list = new List<object>();
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            while (dataReader.Read())
            {
                var t = type.Assembly.CreateInstance(type.FullName);
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (!dataReader.IsDBNull(i))
                    {
                        var fidldname = dataReader.GetName(i);
                        if (members.Any(m => string.Equals(m.Name, fidldname)))
                        {
                            accessor[t, fidldname] = dataReader.GetValue(i);
                        }
                    }
                }
                list.Add(t);
            }
            return list;
        }

        public static List<object> ToObjects(this DbDataReader dataReader, Type type)
        {
            var list = new List<object>();
            var properties = type.GetProperties();
            while (dataReader.Read())
            {
                var obj = Activator.CreateInstance(type);
                foreach (var property in properties)
                {
                    try
                    {
                        var value = dataReader[property.Name];
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