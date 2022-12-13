using Microsoft.EntityFrameworkCore;
using System.Linq;
using TPro.Business.Admin.IServiceProvider;
using TPro.Common.Entity;
using TPro.Common.Extentions;
using TPro.EntityFramework;
using TPro.Models.Others;
using TPro.Models.ResponseDtos;

namespace TPro.Business.Admin.ServiceProvider
{
    public class DbService : BaseService, IDbService
    {
        public ResponseModel GetTableInfos()
        {
            using var helper = new EntityFramework.Data.JurisdictionData();
            var res = helper.GetAllTableNames().Select(e => new WebItem { key = e, value = e, label = e }).ToList();
            return res.Any() ? Success(res) : Fail();
        }

        public ResponseModel GetAllTableNames()
        {
            using var db = new MyDbContext();
            var rlist = db.Model.GetEntityTypes()
                .Select(e => new WebItem { label = e.GetTableName(), value = e.ClrType.FullName, key = e.Name })
                .ToList();
            return rlist.Any() ? Success(rlist) : Fail();
        }

        public ResponseModel GetTableInfo(string fullname)
        {
            if (string.IsNullOrEmpty(fullname)) return Fail("无效的fullname");
            using var db = new MyDbContext();
            var entitytype = db.Model.FindEntityType(fullname);
            var properties = entitytype.GetProperties();
            var rlist = properties
            .Select(e => new EntityProperty
            {
                Name = e.Name,
                ForiginEntityName = e.GetContainingForeignKeys().FirstOrDefault()?.DependentToPrincipal.ClrType.Name ?? "",
                IsForiginKey = e.IsForeignKey(),
                IsKey = e.IsKey(),
                Type = e.ClrType.Name,
                Value = e.ClrType.GetDefaultValue()
            }).ToList();
            return rlist.Any() ? Success(rlist) : Fail();
        }

        public ResponseModel GetEntityInfo(string entityname, int key)
        {
            using var db = new MyDbContext();
            db.Model.FindEntityType(entityname);
            return Success();
        }

        public ResponseModel SaveEntityInfo()
        {
            using var userhelper = new EntityFramework.Data.TPUserData();
            var res = userhelper.GetBy(e => e.Id > 0);
            var rl = res.GetFieldProperties();
            return Success(rl);
        }
    }
}