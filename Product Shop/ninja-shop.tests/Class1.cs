using System;
using NSubstitute;
using NUnit.Framework;

namespace ninja_shop.tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void ThisIsaTest()
        {
            var j = NSubstitute.Substitute.For<Junk>();
            j.num.Returns(3);
            Assert.AreEqual(3,j.num);
        }
    }

    public interface Junk
    {
        int num { get; }
    }
}