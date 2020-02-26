using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReflectIt
{
    public class Container
    {
        Dictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public ContainerBuilder For<TSouce>()
        {
            return For(typeof(TSouce));
        }

        public ContainerBuilder For(Type sourceType)
        {
            return new ContainerBuilder(this, sourceType);
        }

        public TSource Resolve<TSource>()
        {
            return (TSource)Resolve(typeof(TSource));
        }

        public object Resolve(Type sourceType)
        {
            if (_map.ContainsKey(sourceType))
            {
                var destinationType = _map[sourceType];
                return CreateInstance(destinationType);
            }
            else if (sourceType.IsGenericType && _map.ContainsKey(sourceType.GetGenericTypeDefinition()))
            {
                var destination = _map[sourceType.GetGenericTypeDefinition()];
                var closedDestination = destination.MakeGenericType(sourceType.GetGenericArguments());

                return CreateInstance(closedDestination);
            }
            else if (!sourceType.IsAbstract)
            {
                return CreateInstance(sourceType);
            }

            throw new InvalidOperationException($"Can not resolve {nameof(sourceType)} type");
        }

        private object CreateInstance(Type destinationType)
        {
            var paramaters = destinationType.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Count())
                .First()
                .GetParameters()
                .Select(p => Resolve(p.ParameterType))
                .ToArray();

            return Activator.CreateInstance(destinationType, paramaters);
        }

        public class ContainerBuilder
        {
            private readonly Container _container;
            private readonly Type _sourceType;

            public ContainerBuilder(Container container, Type sourceType)
            {
                _container = container;
                _sourceType = sourceType;
            }

            public ContainerBuilder Use<TDestination>()
            {
                return Use(typeof(TDestination));
            }

            public ContainerBuilder Use(Type destinationType)
            {
                _container._map.Add(_sourceType, destinationType);
                return this;
            }
        }
    }
}
