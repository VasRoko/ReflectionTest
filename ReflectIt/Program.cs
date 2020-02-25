using System;
using System.Collections.Generic;

namespace ReflectIt
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeList = CreateCollection(typeof(List<>), typeof(Employee));
            Console.WriteLine(employeeList.GetType().Name);
            var geneicArgs = employeeList.GetType().GenericTypeArguments;

            foreach(var arg in geneicArgs)
            {
                Console.Write("[{0}]", arg.Name);
            }

            Console.WriteLine();
        }

        private static object CreateCollection(Type collectionType, Type itemType)
        {
            return Activator.CreateInstance(collectionType);
        }

        public class Employee
        {
            public string Name { get; set; }
        }
    }
}
