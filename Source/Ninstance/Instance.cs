namespace Ninstance
{
    using System;

    public static class Instance
    {
        public static T Of<T>(params object[] dependencies) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
