using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TPro.Common.CustomSql;
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
            string a = "";
            //int a = 0;
            char b = 'A';
            var res = Activator.CreateInstance(a.GetType());
        }
        [Test]
        public void Test2()
        {
            var user = new TPUser();
            var type = user.GetType();
            var properties = type.GetProperties();
        }
        [Test]
        public void Test3()
        {
            var user = new TPUser();
            var db = new UnitOfWork();
            var list = db.GetBySql(user.GetType(), "SELECT * FROM TPUser");
        }
        [Test]
        public void Test4()
        {
            var a = DateTime.Parse("2022-1");
            var d = Convert.ToDateTime("2022-1");
        }
    }
}