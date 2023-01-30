using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TPro.Business.Admin.IServiceProvider;
using TPro.Common.Extentions;
using TPro.EntityFramework.DbContexts;
using TPro.Models.Admin.DbDtos;
using TPro.Models.Others;
using TPro.Models.ResponseDtos;

namespace TPro.Business.Admin.ServiceProvider
{
    public class DbService : BaseService, IDbService
    {
        public ResponseModel GetTableInfos()
        {
            return Success();
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

        public ResponseModel GetTableDatasByType(string entityname, string tablename)
        {
            using var db = new MyDbContext();
            var entitype = db.Model.FindEntityType(entityname).ClrType;
            var list = db.GetBySql(entitype, $"SELECT * FROM {tablename}");
            return list.Any() ? Success(list) : Fail();
        }

        public ResponseModel SaveEntityInfo(string entityname, List<EntityPropertyDto> entityProperties)
        {
            using var db = new MyDbContext();
            var entitytype = db.Model.FindEntityType(entityname).ClrType;
            var entity = entitytype.GetDefaultValue();
            var properties = entitytype.GetProperties();
            foreach (var property in properties)
            {
                var pro = entityProperties.FirstOrDefault(p => p.Name == property.Name);
                if (pro == null) continue;
                var value =(JsonElement)pro.Value;
                var nvalue = value.ToValue(property.PropertyType);
                if (nvalue != null)
                {
                    property.SetValue(entity, nvalue, null);
                }
            }
            db.Add(entity);
            var res = db.SaveChanges();
            return res > 0 ? Success() : Fail();
        }
    }
}