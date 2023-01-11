using System.Collections.Generic;

namespace TPro.EntityFramework.Entity.MyDbEntity
{
    public class Menu
    {
        public long Id { get; set; }
        public long ParentId { get; set; } = 0;
        public string MenuName { get; set; }
        public string RoutePath { get; set; }
        public string IconPath { get; set; }
        public string ActiveIconPath { get; set; }
        public virtual Menu Parent { get; set; }
        public virtual ICollection<Menu> Children { get; set; }
    }
}