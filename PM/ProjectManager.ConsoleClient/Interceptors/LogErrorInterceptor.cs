using System;
using Ninject.Extensions.Interception;
using ProjectManager.Framework.Core.Common.Contracts;
using Bytes2you.Validation;
using ProjectManager.Framework.Core.Common.Exceptions;

namespace ProjectManager.ConsoleClient.Interceptors
{
    public class LogErrorInterceptor : IInterceptor
    {
        private readonly IWriter writer;
        private readonly ILogger logger;

        public LogErrorInterceptor(IWriter writer, ILogger logger)
        {
            Guard.WhenArgument(writer, "writer").IsNull().Throw();
            Guard.WhenArgument(logger, "logger").IsNull().Throw();

            this.writer = writer;
            this.logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (UserValidationException ex)
            {
                this.logger.Error(ex.Message);
                this.writer.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                this.writer.WriteLine("Opps, something happened. Check the log file :(");
                this.logger.Error(ex.Message);
            }
        }
    }
}
