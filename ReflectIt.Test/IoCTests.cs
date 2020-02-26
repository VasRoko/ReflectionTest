using NUnit.Framework;
using static ReflectIt.Program;

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

        [Test]
        public void Can_Resolve_Types_Without_Default_Ctor()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<ServerLogger>();
            ioc.For<IRepository<Employee>>().Use<Repository<Employee>>();

            var repository = ioc.Resolve<IRepository<Employee>>();

            Assert.AreEqual(typeof(Repository<Employee>), repository.GetType());
        }

        [Test]
        public void Can_Resolve_Concrete_Type()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<ServerLogger>();
            ioc.For(typeof(IRepository<>)).Use(typeof(Repository<>));

            var service = ioc.Resolve<InvoiceService>();
            Assert.IsNotNull(service);
        }
    }
}