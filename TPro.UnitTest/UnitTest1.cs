using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TPro.Common.Entity;
using TPro.Common.Extentions;
using TPro.Common.Utils;
using TPro.EntityFramework;
using TPro.EntityFramework.DbProvider;
using TPro.EntityFramework.Entity;

namespace TPro.UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var user = new UserRole()
            {

            };
            var m = new List<EntityProperty>();
            var x = user.GetType();
            using var db = new MyDbContext();
            var entitytype = db.Model.FindEntityType(x.FullName);
            var properties = entitytype.GetProperties();
            var rlist = properties
            .Select(e => new EntityProperty
            {
                Name = e.Name,
                ForiginEntityName = e.GetContainingForeignKeys().FirstOrDefault()?.DependentToPrincipal.ClrType.Name ?? "",
                IsForiginKey = e.IsForeignKey(),
                IsKey = e.IsKey(),
                Type = e.ClrType.Name,
            }).ToList();
        }
        [Test]
        public void Test2()
        {
            int a = 1;
            var b = a.GetType().Assembly.CreateInstance(a.GetType().FullName);
            string c = "xa";
            //var d = c.GetType().Assembly.CreateInstance(c.GetType().FullName);
            var e = DateTime.Now;
            var f = e.GetType().Assembly.CreateInstance(e.GetType().FullName);
            //var user = new TPUser()
            //{
            //    Account = "Nian.han",
            //    PassWord = "123456",
            //    Email = "Nian.han@Advantech.com.cn"
            //};
            //using var db = new MyDbContext();
            //var x = user.GetType();
            //var res = db.Model.FindEntityType(user.GetType().FullName);
            //var a = x.Assembly.CreateInstance(x.FullName);
            //db.SaveChanges();
            //var a = res.GetForeignKeys();
            //var b = res.GetKeys();
        }
        [Test]
        public void Test3()
        {
            var user = new TPUser();
            var db = new UnitOfWork();
            var list = db.GetBySql(user.GetType(), "SELECT * FROM TPUser");
        }
    }
}