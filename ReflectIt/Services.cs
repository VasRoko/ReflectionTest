using static ReflectIt.Program;

namespace ReflectIt
{
    public interface ILogger
    {

    }

    public interface IRepository<T>
    {

    }

    public class Repository<T> : IRepository<T>
    {
        public Repository(ILogger logger)
        {

        }
    }

    public class ServerLogger : ILogger 
    { 
        
    }

    public class InvoiceService
    {
        public InvoiceService(IRepository<Employee> repository, ILogger logger)
        {

        }
    }
}
