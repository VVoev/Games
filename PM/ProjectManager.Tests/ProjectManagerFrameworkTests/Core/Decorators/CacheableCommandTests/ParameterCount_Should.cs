using Moq;
using NUnit.Framework;
using ProjectManager.Framework.Core.Commands.Contracts;
using ProjectManager.Framework.Core.Commands.Decorators;
using ProjectManager.Framework.Services;

namespace ProjectManager.Tests.ProjectManagerFrameworkTests.Core.Decorators.CacheableCommandTests
{
    [TestFixture]
    public class ParameterCount_Should
    {
        [Test]
        public void ReturnTheSameValueAsTheInnerCommandParametersCount_WhenCalled()
        {
            // arrange
            var command = new Mock<ICommand>();
            var cachingService = new Mock<ICachingService>();

            int innerCommandParametersCount = 5;
            command.Setup(x => x.ParameterCount).Returns(innerCommandParametersCount);

            var cacheableCommand = new CacheableCommand(command.Object, cachingService.Object);
           
            // act and assert
            Assert.AreEqual(cacheableCommand.ParameterCount, innerCommandParametersCount);
        }
    }
}
