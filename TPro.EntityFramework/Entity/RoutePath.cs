using System.Collections.Generic;

namespace TPro.EntityFramework.Entity
{
    public class RoutePath
    {
        public long Id { get; set; }
        public string Path { get; set; }
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public virtual ICollection<Jurisdiction> Jurisdictions { get; set; }
    }
}