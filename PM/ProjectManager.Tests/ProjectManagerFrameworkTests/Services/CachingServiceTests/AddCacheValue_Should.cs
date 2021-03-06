﻿using Moq;
using NUnit.Framework;
using ProjectManager.Framework.Services.Providers;
using ProjectManager.Framework.Services.Providers.Contracts;
using ProjectManager.Tests.ProjectManagerFrameworkTests.Services.CachingServiceTests.Fakes;
using System;

namespace ProjectManager.Tests.ProjectManagerFrameworkTests.Services.CachingServiceTests
{
    public class AddCacheValue_Should
    {
        [Test]
        public void ReturntheCorrectValue_WhenCalledWithExistingKeyInThedicionary()
        {
            var duration = new TimeSpan(10);
            var timeProviderMock = new Mock<ITimeProvider>();
            timeProviderMock.Setup(x => x.Now()).
                Returns(new DateTime(2017, 12, 12));

            TimeProvider.CurrentTimeProvider = timeProviderMock.Object;

            var cachingService = new FakeCachingService(duration);
            string className = "someName";
            string methodName = "someName";
            object value = "SomeValue";

            // act
            cachingService.AddCacheValue(className, methodName, value);

            // assert
            Assert.AreSame(cachingService.CacheTest[$"{className}.{methodName}"], value);
        }
    }
}