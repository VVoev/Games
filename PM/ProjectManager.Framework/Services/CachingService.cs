using Bytes2you.Validation;
using ProjectManager.Framework.Services.Providers;
using System;
using System.Collections.Generic;

namespace ProjectManager.Framework.Services
{
    public class CachingService : ICachingService
    {
        private readonly TimeSpan duration;
        private DateTime timeExpiring;

        private IDictionary<string, object> cache;

        public CachingService(TimeSpan duration)
        {
            Guard.WhenArgument(duration, "duration").IsLessThan(TimeSpan.Zero).Throw();

            this.duration = duration;
            this.timeExpiring = TimeProvider.CurrentTimeProvider.Now();
            this.cache = new Dictionary<string, object>();
        }
        
        // seams for testing purposes
        protected IDictionary<string, object> Cache
        {
            get
            {
                return this.cache;
            }
        }

        protected DateTime TimeExpiring
        {
            get
            {
                return this.timeExpiring;
            }
        }

        protected TimeSpan Duration
        {
            get
            {
                return this.duration;
            }
        }

        public void ResetCache()
        {
            this.cache = new Dictionary<string, object>();
            this.timeExpiring = TimeProvider.CurrentTimeProvider.Now() + this.duration;
        }

        public bool IsExpired
        {
            get
            {
                if (this.timeExpiring < TimeProvider.CurrentTimeProvider.Now())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public object GetCacheValue(string className, string methodName)
        {
            return this.cache[$"{className}.{methodName}"];
        }

        public void AddCacheValue(string className, string methodName, object value)
        {
            this.cache.Add($"{className}.{methodName}", value);
        }
    }
}
