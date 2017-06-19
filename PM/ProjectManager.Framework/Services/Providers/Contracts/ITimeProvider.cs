using System;

namespace ProjectManager.Framework.Services.Providers.Contracts
{
    public interface ITimeProvider
    {
        DateTime Now();
    }
}