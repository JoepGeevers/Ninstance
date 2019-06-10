namespace Ninstance.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NinstanceTests
    {
        [TestMethod]
        public void WhenCreatingAnInstanceOfAnInterface_ThrowExplanatoryNotImplementedException()
        {
            // arrange
            Exception expectedException = null;

            // act
            try
            {
                Instance.Of<IDisposable>();
            }
            catch(Exception e)
            {
                expectedException = e;
            }

            // assert
            Assert.IsNotNull(expectedException);
            Assert.IsInstanceOfType(expectedException, typeof(NotImplementedException));
            Assert.IsTrue(expectedException.Message.Contains("not supposed"));
        }
    }
}
