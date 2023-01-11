using Microsoft.EntityFrameworkCore;
using System.Linq;
using TPro.EntityFramework.Entity.MyDbEntity;

namespace TPro.EntityFramework.Data.MyDbData
{

    public class TPUserData : Repository<TPUser>
    {
        public TPUser IsAccountExist(string account)
        {
            var number = GetFiltered(e => e.Account.ToLower() == account.ToLower())
                .Include(e => e.URs)
                .ThenInclude(u => u.Role)
                .FirstOrDefault();
            return number;
        }
    }
}