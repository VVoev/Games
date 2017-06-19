using Moq;
using NUnit.Framework;
using ProjectManager.Framework.Core.Commands.Contracts;
using ProjectManager.Framework.Core.Commands.Decorators;
using ProjectManager.Framework.Services;
using System.Collections.Generic;

namespace ProjectManager.Tests.ProjectManagerFrameworkTests.Core.Decorators.CacheableCommandTests
{
    [TestFixture]
    public class Execute_Should
    {
        [Test]
        public void CallCachingServiceGetValueWithCorrectClassName_WhenCachedHasNotAlreadyExpired()
        {
            // arrange
            var command = new Mock<ICommand>();
            var cacheService = new Mock<ICachingService>();
            cacheService.Setup(x => x.IsExpired).Returns(false);

            string expectedClassName = command.Object.GetType().Name;
            string expectedMethodName = "Execute";

            var cachingCommand = new CacheableCommand(command.Object, cacheService.Object);

            var parameters = new List<string>();

            // act
            cachingCommand.Execute(parameters);

            // assert
            cacheService.Verify(x => x.GetCacheValue(expectedClassName, expectedMethodName), Times.Once);
        }

        [Test]
        public void ReturnTheCorrectCachedValue_WhenCacheHasNotAlreadyExpired()
        {
            // arrange
            var command = new Mock<ICommand>();
            var cacheService = new Mock<ICachingService>();
            cacheService.Setup(x => x.IsExpired).Returns(false);

            string expectedClassName = command.Object.GetType().Name;
            string expectedMethodName = "Execute";

            string cachedValue = "someValue";
            cacheService
                .Setup(x => x.GetCacheValue(expectedClassName, expectedMethodName))
                .Returns(cachedValue);

            var cachingCommand = new CacheableCommand(command.Object, cacheService.Object);

            var parameters = new List<string>();

            // act
            string actualValue = cachingCommand.Execute(parameters);

            // assert
            Assert.AreEqual(actualValue, cachedValue);
        }


        [Test]
        public void CallTheInnerCommandsExecuteValueWithCorrectParames_WhenCacheHasExpired()
        {
            // arrange
            var command = new Mock<ICommand>();
            var cacheService = new Mock<ICachingService>();
            cacheService.Setup(x => x.IsExpired).Returns(true);

            string expectedClassName = command.Object.GetType().Name;

            var cachingCommand = new CacheableCommand(command.Object, cacheService.Object);

            var parameters = new List<string>();

            // act
            string actualValue = cachingCommand.Execute(parameters);

            // assert
            command.Verify(x => x.Execute(It.Is<IList<string>>(y => y == parameters)), Times.Once);
        }

        [Test]
        public void CallCacheServiceResetMethod_WhenCacheHasExpired()
        {
            // arrange
            var command = new Mock<ICommand>();
            var cacheService = new Mock<ICachingService>();
            cacheService.Setup(x => x.IsExpired).Returns(true);

            string expectedClassName = command.Object.GetType().Name;

            var cachingCommand = new CacheableCommand(command.Object, cacheService.Object);

            var parameters = new List<string>();

            // act
            string actualValue = cachingCommand.Execute(parameters);

            // assert
            cacheService.Verify(x => x.ResetCache(), Times.Once);
        }


        [Test]
        public void CallCacheServiceAddCacheValueWithCorrectParameters_WhenCacheHasExpired()
        {
            // arrange
            var command = new Mock<ICommand>();
            var cacheService = new Mock<ICachingService>();
            cacheService.Setup(x => x.IsExpired).Returns(true);

            string expectedClassName = command.Object.GetType().Name;
            string expectedMethodName = "Execute";

            var cachingCommand = new CacheableCommand(command.Object, cacheService.Object);

            var parameters = new List<string>();
            string innerCommandReturnedValue = "someAddedValue";

            command.Setup(x => x.Execute(It.IsAny<IList<string>>())).Returns(innerCommandReturnedValue);

            // act
            string actualValue = cachingCommand.Execute(parameters);

            // assert
            cacheService.Verify(x => x.AddCacheValue(expectedClassName, expectedMethodName, innerCommandReturnedValue),Times.Once);
        }

        [Test]
        public void ReturnTheResultOfTheInnerCommandExecution_WhenCacheHasExpired()
        {
            // arrange
            var command = new Mock<ICommand>();
            var cacheService = new Mock<ICachingService>();
            cacheService.Setup(x => x.IsExpired).Returns(true);

            string expectedClassName = command.Object.GetType().Name;
         
            var cachingCommand = new CacheableCommand(command.Object, cacheService.Object);

            var parameters = new List<string>();
            string innerCommandReturnValue = "someAddedValue";

            command.Setup(x => x.Execute(It.IsAny<IList<string>>())).Returns(innerCommandReturnValue);

            // act
            string actualValue = cachingCommand.Execute(parameters);

            // assert
            Assert.AreEqual(actualValue, innerCommandReturnValue);
        }
    }
}
