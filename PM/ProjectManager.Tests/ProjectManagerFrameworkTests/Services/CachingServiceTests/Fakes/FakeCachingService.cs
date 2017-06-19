using System;
using ProjectManager.Framework.Services;
using System.Collections.Generic;

namespace ProjectManager.Tests.ProjectManagerFrameworkTests.Services.CachingServiceTests.Fakes
{
    public class FakeCachingService : CachingService
    {
        public FakeCachingService(TimeSpan duration) 
            : base(duration)
        {
        }

        public IDictionary<string, object> CacheTest
        {
            get
            {
                return this.Cache;
            }
        }

        public DateTime TimeExpiringTest
        {
            get
            {
                return this.TimeExpiring;
            }
        }

        public TimeSpan DurationTest
        {
            get
            {
                return this.Duration;
            }
        }
    }
}
