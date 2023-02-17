using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPro.EntityFramework.DbContexts;
using TPro.EntityFramework.DbProvider;

namespace TPro.EntityFramework.Data.PKGData
{
    public class PKGRepository<T> : Repository<T> where T : class, new()
    {
        public PKGRepository() 
        {
            _unitOfWork = new UnitOfWork(new PKGDbContext());
        }
    }
}
