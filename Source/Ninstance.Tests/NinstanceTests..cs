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
                Instance.Of<ICarService>();
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

        [TestMethod]
        public void WhenCreatingAnInstanceOfClassWithConstructorWithTwoParameters_ProvidingOneImplementationAndOneUnusedParameter_ReturnsInstanceWithImplementationAndSubstitute()
        {
            // act
            var carService = new CarService(42);
            var houseService = new HouseService();

            var result = Instance.Of<ClassWithConstructorWithTwoParameters>(houseService, carService);

            // assert
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.carService);
            Assert.AreEqual(42, result.carService.CheckSum);

            Assert.IsNotNull(result.userService);
            Assert.IsInstanceOfType(result.userService, typeof(IUserService));
        }

        interface ICarService
        {
            int CheckSum { get; }
        }

        public class CarService : ICarService
        {
            public CarService(int checksum)
            {
                this.CheckSum = checksum;
            }

            public int CheckSum { get; private set; }
        }

        public interface IUserService
        {
        }

        public class HouseService
        {
        }

        class ClassWithConstructorWithTwoParameters
        {
            public ICarService carService { get; }
            public IUserService userService { get; }

            public ClassWithConstructorWithTwoParameters(ICarService carService, IUserService userService)
            {
                this.carService = carService;
                this.userService = userService;
            }
        }
    }
}
