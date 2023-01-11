using NUnit.Framework;
using System;
using System.Globalization;
using TPro.Common.CustomSql.SystemSql;
using TPro.Common.NianLog;
using TPro.Common.NSpider;
using TPro.EntityFramework.Entity.MyDbEntity;

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
            var sqlhelper = new SQLiteHelper("Data Source=E:\\MvcTest\\templatedb.db;");
            var entity = new NianLogEntity();
            var res = sqlhelper.GetColumns(entity);
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
            var t = DateTime.Now;
            var r = t.ToString("MMM", CultureInfo.CreateSpecificCulture("en-GB"));
        }

        [Test]
        public void Test4()
        {
            var a = DateTime.Parse("2022-1");
            var d = Convert.ToDateTime("2022-1");
        }

        [Test]
        public void Test5()
        {
            var option = new NSpiderOption();
            option.TimeInterval = 999;
        }
    }
}