using NUnit.Framework;

namespace ReflectIt.Test
{
    public class Tests
    {
        [Test]
        public void Can_Resolve_Types()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<ServerLogger>();

            var logger = ioc.Resolve<ILogger>();

            Assert.AreEqual(typeof(ServerLogger), logger.GetType());
        }
    }
}