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
                Instance.Of<ISomeRandomInterface>();
            }
            catch (Exception e)
            {
                expectedException = e;
            }

            // assert
            Assert.IsNotNull(expectedException);
            Assert.IsInstanceOfType(expectedException, typeof(NotImplementedException));
            Assert.IsTrue(expectedException.Message.Contains("not supposed"));
        }

        interface ISomeRandomInterface
        {
        }

        [TestMethod]
        public void WhenCreatingAnInstanceOfClassWithoutPublicConstructor_ThrowExplanatoryNotImplementedException()
        {
            // arrange
            Exception expectedException = null;

            // act
            try
            {
                Instance.Of<ClassWithoutPublicConstructor>();
            }
            catch (Exception e)
            {
                expectedException = e;
            }

            // assert
            Assert.IsNotNull(expectedException);
            Assert.IsInstanceOfType(expectedException, typeof(NotImplementedException));
            Assert.IsTrue(expectedException.Message.Contains("don't know how to construct"));
        }

        class ClassWithoutPublicConstructor
        {
            private ClassWithoutPublicConstructor()
            {
            }
        }

        [TestMethod]
        public void WhenCreatingAnInstanceOfClassWithMultiplePublicConstructors_ThrowExplanatoryNotImplementedException()
        {
            // arrange
            Exception expectedException = null;

            // act
            try
            {
                Instance.Of<ClassWithMultiplePublicConstructors>();
            }
            catch (Exception e)
            {
                expectedException = e;
            }

            // assert
            Assert.IsNotNull(expectedException);
            Assert.IsInstanceOfType(expectedException, typeof(NotImplementedException));
            Assert.IsTrue(expectedException.Message.Contains("don't know how to construct"));
        }

        class ClassWithMultiplePublicConstructors
        {
            public ClassWithMultiplePublicConstructors() { }
            public ClassWithMultiplePublicConstructors(int i) { }
        }

        [TestMethod]
        public void WhenCreatingAnInstanceOfClassWithParameterlessConstructor_ReturnsInstance()
        {
            // act
            var result = Instance.Of<ClassWithSingleParameterlessConstructor>();

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClassWithSingleParameterlessConstructor));
        }

        class ClassWithSingleParameterlessConstructor
        {
            public ClassWithSingleParameterlessConstructor() { }
        }
    }
}
