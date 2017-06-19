using Moq;
using NUnit.Framework;
using ProjectManager.Framework.Core.Commands.Contracts;
using ProjectManager.Framework.Core.Commands.Decorators;
using ProjectManager.Framework.Services;
using System;

namespace ProjectManager.Tests.ProjectManagerFrameworkTests.Core.Decorators.CacheableCommandTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedCommmandIsNull()
        {
            // arrange
            var cacheService = new Mock<ICachingService>();

            Assert.Throws<ArgumentNullException>(() => new CacheableCommand(null, cacheService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenCachingServiceIsNull()
        {
            // arrange
            var command = new Mock<ICommand>();
          
            Assert.Throws<ArgumentNullException>(() => new CacheableCommand(command.Object, null));
        }

        [Test]
        public void NotThrow_WhenPassedArgumentAreValid()
        {
            // arrange
            var command = new Mock<ICommand>();
            var cacheService = new Mock<ICachingService>();

            Assert.DoesNotThrow(() => new CacheableCommand(command.Object, cacheService.Object));
        }
    }
}
