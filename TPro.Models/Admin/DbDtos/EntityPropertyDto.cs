using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPro.Models.Admin.DbDtos
{
    public class EntityPropertyDto
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public string Type { get; set; }
        public bool IsKey { get; set; } = false;
        public bool IsForiginKey { get; set; } = false;
        public string ForiginEntityName { get; set; }
    }
}
