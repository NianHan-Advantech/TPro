using NUnit.Framework;
using System;
using System.Globalization;
using System.Net.Mail;
using TPro.Common.CustomSql.SystemSql;
using TPro.Common.EMail;
using TPro.Common.NianLog;
using TPro.Common.NSpider;
using TPro.Common.PinYin;
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
            var emailservice = new NEmailServer("1",1);
            emailservice.SendEmail(new MailMessage());
        }

        [Test]
        public void Test4()
        {
            var a = DateTime.Parse("2022-1");
            var d = Convert.ToDateTime("2022-1");
            var r = a.ToString("MMM", CultureInfo.CreateSpecificCulture("en-GB"));
        }

        [Test]
        public void Test5()
        {
            var res = PinYinHelper.ConvertToPinYin("ÖÐ¹ú");
        }
    }
}