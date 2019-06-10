namespace Ninstance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using NSubstitute;

    public static class Instance
    {
        public static T Of<T>(params object[] dependencies) where T : class
        {
            var type = typeof(T);

            if (type.IsInterface)
            {
                throw new NotImplementedException("I'm not supposed to create instances for interfaces. Please use NSubstitute directly");
            }

            var constructors = type.GetConstructors();

            if (constructors.Length == 0)
            {
                throw new NotImplementedException($"I currently don't know how to construct your {type.Name} because it has no public constructors");
            }

            if (constructors.Length > 1)
            {
                throw new NotImplementedException($"I currently don't know how to construct your {type.Name} because it has multiple constructors");
            }

            var constructor = constructors.Single();
            var arguments = CreateArgumentsFor(constructor, dependencies);

            return (T)constructor.Invoke(arguments.ToArray());
        }

        private static IEnumerable<object> CreateArgumentsFor(ConstructorInfo constructor, object[] dependencies)
        {
            foreach (var constructorParameter in constructor.GetParameters())
            {
                yield return
                    dependencies
                        .Where(d => constructorParameter.ParameterType.IsAssignableFrom(d.GetType()))
                        .FirstOrDefault()
                    ??
                    typeof(Substitute)
                        .GetMethods()
                        .Where(m => m.Name == "For")
                        .Where(m => m.ReturnType.Name == "T")
                        .Single()
                        .MakeGenericMethod(constructorParameter.ParameterType)
                        .Invoke(null, new object[1] { new object[0] });
            }
        }
    }
}
