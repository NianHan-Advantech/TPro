using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TPro.EntityFramework.Data
{
    public class MenuData : Repository<Entity.Menu>
    {
        public List<Entity.Menu> GetMenus()
        {
            var tree = base.GetFiltered(e => e.ParentId == 0 && e.Id > 0).Include(e => e.Children).ToList();
            return tree;
        }
    }
}
