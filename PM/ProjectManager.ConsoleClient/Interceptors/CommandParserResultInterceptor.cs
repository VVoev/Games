using Bytes2you.Validation;
using Ninject.Extensions.Interception;
using ProjectManager.Framework.Core.Common.Contracts;

namespace ProjectManager.ConsoleClient.Interceptors
{
    public class CommandParserResultInterceptor : IInterceptor
    {
        private readonly IWriter writer;

        public CommandParserResultInterceptor(IWriter writer)
        {
            Guard.WhenArgument(writer, "writer").IsNull().Throw();
            this.writer = writer;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
            if (invocation.Request.Method.Name == "ProcessCommand")
            {
                writer.WriteLine(invocation.ReturnValue);
            }
        }
    }
}
