using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPro.EntityFramework.DbProvider;
using TPro.EntityFramework.Entity.MyDbEntity;

namespace TPro.EntityFramework.Data.MyDbData
{
    public class JurisdictionData : Repository<Jurisdiction>
    {
        public bool IsRoleAllowed(string path, int roleid)
        {
            var member = GetFiltered(e => e.RoleId == roleid).Include(e => e.Route).ToList();
            return member.Any(e => e.Route.Path == path);
        }
    }
}
