using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TPro.EntityFramework.Data
{

    public class TPUserData : Repository<Entity.TPUser>
    {
        public Entity.TPUser IsAccountExist(string account)
        {
            var number = base.GetFiltered(e => e.Account.ToLower() == account.ToLower())
                .Include(e => e.URs)
                .ThenInclude(u => u.Role)
                .FirstOrDefault();
            return number;
        }
    }
}