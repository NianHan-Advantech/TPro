using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TPro.EntityFramework.Entity.MyDbEntity;

namespace TPro.EntityFramework.Data.MyDbData
{
    public class RoutePathData : Repository<RoutePath>
    {
        public bool IsRoleAllowed(string path, IEnumerable<int> roleids)
        {
            var member = GetFiltered(e => e.Path == path).Include(e => e.Jurisdictions).FirstOrDefault();
            if (member == null || !member.Jurisdictions.Any(e => roleids.Any(r => e.RoleId == r)))
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
            return GetFiltered(e => e.Path == path).Any();
        }
    }
}