namespace Ninstance
{
    using System;
    using System.Linq;
    using System.Reflection;

    using NSubstitute;

    public static class Instance
    {
        public static T Of<T>(params object[] implementations) where T : class
        {
            var constructor = GetUseableConstructorOf<T>();
            var arguments = CreateArgumentsFor(constructor, implementations);

            return (T)constructor.Invoke(arguments);
        }

        private static ConstructorInfo GetUseableConstructorOf<T>() where T : class
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
            return constructor;
        }

        private static object[] CreateArgumentsFor(ConstructorInfo constructor, object[] implementations)
            => constructor
                  .GetParameters()
                  .Select(p => CreateArgumentFor(p, implementations))
                  .ToArray();

        private static object CreateArgumentFor(ParameterInfo parameter, object[] implementations)
            => FindDependencyFor(parameter, implementations)
                ?? CreateSubstituteFor(parameter);

        private static object FindDependencyFor(ParameterInfo parameter, object[] implementations)
            => implementations
                .Where(d => parameter.ParameterType.IsAssignableFrom(d.GetType()))
                .FirstOrDefault();

        private static object CreateSubstituteFor(ParameterInfo parameter)
            => typeof(Substitute)
                .GetMethods()
                .Where(m => m.Name == "For")
                .Where(m => m.ReturnType.Name == "T")
                .Single()
                .MakeGenericMethod(parameter.ParameterType)
                .Invoke(null, new object[1] { new object[0] });
    }
}