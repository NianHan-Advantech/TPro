using NUnit.Framework;
using System;

namespace TPro.UnitTest
{
    public class ExtentionTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var a = 9;
            var b = a >> 1;
        }
    }
}