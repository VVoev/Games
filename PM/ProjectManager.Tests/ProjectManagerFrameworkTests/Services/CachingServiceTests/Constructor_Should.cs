using Moq;
using NUnit.Framework;
using ProjectManager.Framework.Services;
using ProjectManager.Framework.Services.Providers;
using ProjectManager.Framework.Services.Providers.Contracts;
using ProjectManager.Tests.ProjectManagerFrameworkTests.Services.CachingServiceTests.Fakes;
using System;

namespace ProjectManager.Tests.ProjectManagerFrameworkTests.Services.CachingServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentOutOfRange_WhenPassedDurationIsLessThanZero()
        {
            // arrange
            var duration = new TimeSpan(-10);

            // act and assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new CachingService(duration));
        }

        [Test]
        public void NotThrow_WhenPassedDurationIsValid()
        {
            // arrange
            var duration = new TimeSpan(10);
            var timeProviderMock = new Mock<ITimeProvider>();
            timeProviderMock.Setup(x => x.Now()).
                Returns(new DateTime(2017, 12, 12));

            TimeProvider.CurrentTimeProvider = timeProviderMock.Object;

            // act and assert
            Assert.DoesNotThrow(() => new CachingService(duration));
        }

        [Test]
        public void SetTimeExpiringToTheCurrentTime_WhenPassedDurationIsValid()
        {

            // arrange
            var duration = new TimeSpan(10);
            var timeProviderMock = new Mock<ITimeProvider>();

            var date = new DateTime(2017, 12, 12);
            timeProviderMock.Setup(x => x.Now()).
                Returns(date);

            TimeProvider.CurrentTimeProvider = timeProviderMock.Object;

            // act
            var cachingService =  new FakeCachingService(duration);

            // assert
            Assert.AreEqual(date, cachingService.TimeExpiringTest);
        }

        [Test]
        public void InitializeCacheDictionaryCorrectly_WhenPassedDurationIsValid()
        {

            // arrange
            var duration = new TimeSpan(10);
            var timeProviderMock = new Mock<ITimeProvider>();

            var date = new DateTime(2017, 12, 12);
            timeProviderMock.Setup(x => x.Now()).
                Returns(date);

            TimeProvider.CurrentTimeProvider = timeProviderMock.Object;

            // act
            var cachingService = new FakeCachingService(duration);

            // assert
            Assert.IsNotNull(cachingService.CacheTest);
            Assert.AreEqual(0, cachingService.CacheTest.Count);
        }

        [Test]
        public void SetDurationCorrectly_WhenPassedDurationIsValid()
        {

            // arrange
            var duration = new TimeSpan(10);
            var timeProviderMock = new Mock<ITimeProvider>();

            var date = new DateTime(2017, 12, 12);
            timeProviderMock.Setup(x => x.Now()).
                Returns(date);

            TimeProvider.CurrentTimeProvider = timeProviderMock.Object;

            // act
            var cachingService = new FakeCachingService(duration);

            // assert
            Assert.AreEqual(duration, cachingService.DurationTest);
        }
    }
}