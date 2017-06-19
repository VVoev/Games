using Moq;
using NUnit.Framework;
using ProjectManager.Framework.Services;
using ProjectManager.Framework.Services.Providers;
using ProjectManager.Framework.Services.Providers.Contracts;
using System;

namespace ProjectManager.Tests.ProjectManagerFrameworkTests.Services.CachingServiceTests
{
    [TestFixture]
    public class IsExpired_Should
    {
        [Test]
        public void ReturnTrue_WhenCachingTimeHasExpired()
        {
            var duration = new TimeSpan(10);
            var timeProviderMock = new Mock<ITimeProvider>();
            timeProviderMock.SetupSequence(x => x.Now()).
                Returns(new DateTime(2017, 12, 12))
                .Returns(new DateTime(2017, 12, 22));
            TimeProvider.CurrentTimeProvider = timeProviderMock.Object;

            var cachingService = new CachingService(duration);

            // act
            bool isExpired = cachingService.IsExpired;

            // assert
            Assert.IsTrue(isExpired);
        }

        [Test]
        public void ReturnFales_WhenCachingTimeHasntExpired()
        {
            var duration = new TimeSpan(10);
            var timeProviderMock = new Mock<ITimeProvider>();
            timeProviderMock.Setup(x => x.Now()).
                Returns(new DateTime(2017, 12, 12));

            TimeProvider.CurrentTimeProvider = timeProviderMock.Object;

            var cachingService = new CachingService(duration);

            // act
            bool isExpired = cachingService.IsExpired;

            // assert
            Assert.IsFalse(isExpired);
        }
    }
}
