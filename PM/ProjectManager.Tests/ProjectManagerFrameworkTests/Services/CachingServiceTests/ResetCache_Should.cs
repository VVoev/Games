using Moq;
using NUnit.Framework;
using ProjectManager.Framework.Services.Providers;
using ProjectManager.Framework.Services.Providers.Contracts;
using ProjectManager.Tests.ProjectManagerFrameworkTests.Services.CachingServiceTests.Fakes;
using System;

namespace ProjectManager.Tests.ProjectManagerFrameworkTests.Services.CachingServiceTests
{
    [TestFixture]
    public class ResetCache_Should
    {
        [Test]
        public void ClearTheCacheDictionary_WhenCalled()
        {
            // arrange
            var duration = new TimeSpan();
            var timeProviderMock = new Mock<ITimeProvider>();
            timeProviderMock.Setup(x => x.Now()).Returns(new DateTime(2017, 12, 12));
            TimeProvider.CurrentTimeProvider = timeProviderMock.Object;

            var cachingService = new FakeCachingService(duration);
            cachingService.CacheTest.Add("someName", "someValue");

            // act
            cachingService.ResetCache();

            // assert
            Assert.AreEqual(cachingService.CacheTest.Count, 0);
        }

        [Test]
        public void SetTimeExpiringCorrectly_WhenCalled()
        {
            // arrange
            var duration = new TimeSpan(10);
            var timeProviderMock = new Mock<ITimeProvider>();
            timeProviderMock.Setup(x => x.Now()).Returns(new DateTime(2017, 12, 12));
            TimeProvider.CurrentTimeProvider = timeProviderMock.Object;

            var cachingService = new FakeCachingService(duration);
            cachingService.CacheTest.Add("someName", "someValue");

            var expectedTimeExpiring = TimeProvider.CurrentTimeProvider.Now() + duration;

            // act
            cachingService.ResetCache();

            // assert
            Assert.AreEqual(cachingService.TimeExpiringTest, expectedTimeExpiring);
        }
    }
}
