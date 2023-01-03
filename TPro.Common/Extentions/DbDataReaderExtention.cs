using FastMember;
using System;
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
    }
}