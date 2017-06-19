using ProjectManager.Framework.Services.Providers.Contracts;
using System;

namespace ProjectManager.Framework.Services.Providers
{
    // ambient context DI pattern
    public class TimeProvider : ITimeProvider
    {
        public static ITimeProvider CurrentTimeProvider { get; set; }

        static TimeProvider()
        {
            CurrentTimeProvider = new TimeProvider();
        }

        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
