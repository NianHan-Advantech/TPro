using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TPro.EntityFramework.Entity.MyDbEntity;

namespace TPro.EntityFramework.Data.MyDbData
{
    public class MenuData : Repository<Menu>
    {
        public List<Menu> GetMenus()
        {
            var tree = GetFiltered(e => e.ParentId == 0 && e.Id > 0).Include(e => e.Children).ToList();
            return tree;
        }
    }
}
