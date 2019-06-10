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

            throw new NotImplementedException();
        }
    }
}
