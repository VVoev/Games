using Moq;
using NUnit.Framework;
using ProjectManager.ConsoleClient.Interceptors;
using ProjectManager.Framework.Core.Common.Contracts;
using System;

namespace ProjectManager.Tests.ProjectManagerConsoleClientTests.Interceptors.LogErrorInterceptorTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedWriterIsNull()
        {
            // arrange
            IWriter writer = null;
            var logger = new Mock<ILogger>();

            // act and assert
            Assert.Throws<ArgumentNullException>(() => new LogErrorInterceptor(writer, logger.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedLoggerIsNull()
        {
            // arrange
            var writer = new Mock<IWriter>();
            ILogger logger = null;

            // act and assert
            Assert.Throws<ArgumentNullException>(() => new LogErrorInterceptor(writer.Object,logger));
        }

        [Test]
        public void NotThrow_WhenPassedArgumentAreValid()
        {
            // arrange
            var writer = new Mock<IWriter>();
            var logger = new Mock<ILogger>();

            // act and assert
            Assert.DoesNotThrow(() => new LogErrorInterceptor(writer.Object, logger.Object));
        }
    }
}
