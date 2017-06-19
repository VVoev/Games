using Bytes2you.Validation;
using ProjectManager.Framework.Core.Common.Contracts;

namespace ProjectManager.Framework.Core
{
    public class Engine : IEngine
    {
        private readonly IProcessor processor;
        private readonly IReader reader;
        private readonly IWriter writer;
        
        public Engine(IProcessor processor, IReader reader, IWriter writer)
        {           
            Guard.WhenArgument(processor, "processor").IsNull().Throw();
            Guard.WhenArgument(reader, "reader").IsNull().Throw();          
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.processor = processor;
            this.reader = reader;
            this.writer = writer;
        }

        public void Start()
        {
            for (;;)
            {
                var commandLine = this.reader.ReadLine();

                if (commandLine.ToLower() == "exit")
                {
                    this.writer.WriteLine("Program terminated.");
                    break;
                }

                this.processor.ProcessCommand(commandLine);
            }
        }
    }
}
