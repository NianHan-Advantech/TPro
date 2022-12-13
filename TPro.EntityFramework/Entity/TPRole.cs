using System.Collections.Generic;

namespace TPro.EntityFramework.Entity
{
    public class TPRole
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserRole> URs { get; set; }
        public virtual ICollection<Jurisdiction> Jurisdictions { get; set; }
    }
}