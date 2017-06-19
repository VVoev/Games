using Bytes2you.Validation;
using ProjectManager.Framework.Core.Commands.Contracts;
using ProjectManager.Framework.Services;
using System;
using System.Collections.Generic;

namespace ProjectManager.Framework.Core.Commands.Decorators
{
    public class CacheableCommand : ICommand
    {
        private readonly ICommand command;
        private readonly ICachingService cachingService;

        public CacheableCommand(ICommand command, ICachingService cachingService)
        {
            Guard.WhenArgument(command, "command").IsNull().Throw();
            Guard.WhenArgument(cachingService, "cachingService").IsNull().Throw();

            this.cachingService = cachingService;
            this.command = command;
        }

        public int ParameterCount
        {
            get
            {
                return this.command.ParameterCount;
            }
        }

        public string Execute(IList<string> parameters)
        {
            string className = this.command.GetType().Name;
            string methodName = "Execute";
                
            if (!this.cachingService.IsExpired)
            {
                return (string)this.cachingService.GetCacheValue(className, methodName);
            }

            var listedProjects = this.command.Execute(parameters);

            this.cachingService.ResetCache();
            this.cachingService.AddCacheValue(className, methodName, listedProjects);
            return listedProjects;
        }
    }
}
