using System.Collections.Generic;
using System.Linq;
using TPro.Common.Extentions;

namespace TPro.Common.Entity
{
    public static class EntityHelper
    {
        public static List<EntityProperty> GetEntityFieldProperty<T>(T t = null) where T : class, new()
        {
            var rlist = new List<EntityProperty>();
            if (t == null)
            {
                t = new T();
            }
            var properties = t.GetFieldProperties();
            foreach (var item in properties)
            {
                var nep = new EntityProperty();
                nep.Name = item.Name;
                nep.Type = item.PropertyType.Name;
                nep.Value = item.GetValue(t);
                if (item.CustomAttributes.Any(e => e.AttributeType.Name.Contains("ForeignKeyAttribute")))
                {
                    nep.IsForiginKey = true;
                    var attr = item.CustomAttributes.FirstOrDefault(e => e.AttributeType.Name.Contains("ForeignKeyAttribute"));
                    nep.ForiginEntityName = attr.ConstructorArguments[0].Value.ToString();
                }
                else if (item.CustomAttributes.Any(e => e.AttributeType.Name.Contains("KeyAttribute")))
                {
                    nep.IsKey = true;
                }
                rlist.Add(nep);
            }
            return rlist;
        }
    }

    public class EntityProperty
    {
        public string Name { get; set; }
        public dynamic Value { get; set; }
        public string Type { get; set; }
        public bool IsKey { get; set; } = false;
        public bool IsForiginKey { get; set; } = false;
        public string ForiginEntityName { get; set; }
    }
}