using Moq;
using Ninject.Extensions.Interception;
using NUnit.Framework;
using ProjectManager.ConsoleClient.Interceptors;
using ProjectManager.Framework.Core.Common.Contracts;
using ProjectManager.Framework.Core.Common.Exceptions;
using System;

namespace ProjectManager.Tests.ProjectManagerConsoleClientTests.Interceptors.LogErrorInterceptorTests
{
    [TestFixture]
    public class Intercept_Should
    {
        [Test]
        public void CallInvocationsProceedMethodOnce_WhenMethodIsCalled()
        {
            // arrange
            var writer = new Mock<IWriter>();
            var logger = new Mock<ILogger>();

            var invocation = new Mock<IInvocation>();

            var logErrorInterceptor = new LogErrorInterceptor(writer.Object, logger.Object);

            // act
            logErrorInterceptor.Intercept(invocation.Object);

            // assert
            invocation.Verify(x => x.Proceed(), Times.Once);
        }

        [Test]
        public void CallWritersWriteMethodWithcorrectExceptionMessage_WhenUserValidatorExceptionIsThrown()
        {
            // arrange
            var writer = new Mock<IWriter>();
            var logger = new Mock<ILogger>();

            var invocation = new Mock<IInvocation>();
            string exceptionMessage = "someExceptionMessage";
       
            invocation.Setup(x => x.Proceed()).Throws(new UserValidationException(exceptionMessage));

            var logErrorInterceptor = new LogErrorInterceptor(writer.Object, logger.Object);

            // act
            logErrorInterceptor.Intercept(invocation.Object);

            // assert
            writer.Verify(x => x.WriteLine(It.Is<string>(y => y == exceptionMessage)), Times.Once);
        }

        [Test]
        public void CallLoggersLogMethodWithCorrectExceptionMessage_WhenUserValidatorExceptionIsThrown()
        {
            // arrange
            var writer = new Mock<IWriter>();
            var logger = new Mock<ILogger>();

            var invocation = new Mock<IInvocation>();
            string exceptionMessage = "someExceptionMessage";

            invocation.Setup(x => x.Proceed()).Throws(new UserValidationException(exceptionMessage));

            var logErrorInterceptor = new LogErrorInterceptor(writer.Object, logger.Object);

            // act
            logErrorInterceptor.Intercept(invocation.Object);

            // assert
            logger.Verify(x => x.Error(It.Is<string>(y => y == exceptionMessage)), Times.Once);
        }

        [Test]
        public void CallLoggersLogMethodWithCorrectExceptionMessage_WhenGenericExceptionIsThrown()
        {
            // arrange
            var writer = new Mock<IWriter>();
            var logger = new Mock<ILogger>();

            var invocation = new Mock<IInvocation>();
            string exceptionMessage = "someGenericExceptionMessage";

            invocation.Setup(x => x.Proceed()).Throws(new Exception(exceptionMessage));

            var logErrorInterceptor = new LogErrorInterceptor(writer.Object, logger.Object);

            // act
            logErrorInterceptor.Intercept(invocation.Object);

            // assert
            logger.Verify(x => x.Error(It.Is<string>(y => y == exceptionMessage)), Times.Once);
        }

        [Test]
        public void CallWritersWriteMethodWithCorrectUserInformingMessage_WhenGenericExceptionIsThrown()
        {
            // arrange
            var writer = new Mock<IWriter>();
            var logger = new Mock<ILogger>();

            var invocation = new Mock<IInvocation>();
            string exceptionMessage = "someGenericExceptionMessage";

            invocation.Setup(x => x.Proceed()).Throws(new Exception(exceptionMessage));

            var logErrorInterceptor = new LogErrorInterceptor(writer.Object, logger.Object);

            // act
            logErrorInterceptor.Intercept(invocation.Object);

            // assert
            writer.Verify(x => x.WriteLine(It.Is<string>(y => y == "Opps, something happened. Check the log file :(")), Times.Once);
        }
    }
}