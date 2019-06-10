namespace Ninstance
{
    using System;

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

            throw new NotImplementedException();
        }
    }
}
