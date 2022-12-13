using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TPro.EntityFramework.Data
{
    public class RoutePathData : Repository<Entity.RoutePath>
    {
        public bool IsRoleAllowed(string path, IEnumerable<int> roleids)
        {
            var member = base.GetFiltered(e => e.Path == path).Include(e => e.Jurisdictions).FirstOrDefault();
            if (member == null||!member.Jurisdictions.Any(e=>roleids.Any(r=>e.RoleId==r)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsPathExist(string path)
        {
            return base.GetFiltered(e => e.Path == path).Any();
        }
    }
}