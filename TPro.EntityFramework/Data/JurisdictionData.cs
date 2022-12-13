using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPro.EntityFramework.DbProvider;

namespace TPro.EntityFramework.Data
{
    public class JurisdictionData:Repository<Entity.Jurisdiction>
    {
        public bool IsRoleAllowed(string path, int roleid)
        {
            var member = base.GetFiltered(e => e.RoleId == roleid).Include(e => e.Route).ToList();
            return member.Any(e => e.Route.Path == path) ;
        }
    }
}
