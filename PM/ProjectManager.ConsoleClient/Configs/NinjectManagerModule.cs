using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;
using Ninject.Parameters;
using ProjectManager.ConsoleClient.Configs;
using ProjectManager.ConsoleClient.Interceptors;
using ProjectManager.Data;
using ProjectManager.Framework.Core;
using ProjectManager.Framework.Core.Commands.Contracts;
using ProjectManager.Framework.Core.Commands.Creational;
using ProjectManager.Framework.Core.Commands.Decorators;
using ProjectManager.Framework.Core.Commands.Listing;
using ProjectManager.Framework.Core.Common.Contracts;
using ProjectManager.Framework.Core.Common.Providers;
using ProjectManager.Framework.Data;
using ProjectManager.Framework.Data.Factories;
using ProjectManager.Framework.Services;
using System.Linq;

namespace ProjectManager.Configs
{
    public class NinjectManagerModule : NinjectModule
    {
        private const string CreateUserCommandName = "createuser";
        private const string CreateTaskCommandName = "createtask";
        private const string CreateProjectCommandName = "createproject";
        private const string ListProjectDetailsCommandName = "listprojectdetails";
        private const string ListProjectsCommandName = "listprojects";

        // TODO: TODODODODODOD when I have more time
        public override void Load()
        {
            //Kernel.Bind(x =>
            //{
            //    x.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            //    .SelectAllClasses()
            //    .Where(type => type != typeof(Engine))
            //    .BindDefaultInterface();
            //});

            this.Bind<IConfigurationProvider>().To<ConfigurationProvider>();   
            IConfigurationProvider configurationProvider = Kernel.Get<IConfigurationProvider>();

            // providers
            this.Bind<ILogger>().To<FileLogger>().InSingletonScope().WithConstructorArgument(configurationProvider.LogFilePath);
            this.Bind<IReader>().To<ConsoleReader>().InSingletonScope();
            this.Bind<IWriter>().To<ConsoleWriter>().InSingletonScope();
            var commandProcessorBinding = this.Bind<IProcessor>().To<CommandProcessor>().InSingletonScope();
            commandProcessorBinding.Intercept().With<CommandParserResultInterceptor>();
            commandProcessorBinding.Intercept().With<LogErrorInterceptor>();

            this.Bind<IValidator>().To<Validator>().InSingletonScope();
            this.Bind<IModelsFactory>().To<ModelsFactory>().InSingletonScope();

            this.Bind<IEngine>().To<Engine>().InSingletonScope();

            // data
            this.Bind<IDatabase>().To<Database>().InSingletonScope();

            // services
            this.Bind<ICachingService>().To<CachingService>().WithConstructorArgument("duration", configurationProvider.CacheDurationInSeconds);

            // commands
            this.Bind<ICommand>().To<ValidatableCommand>().InSingletonScope().Named(CreateUserCommandName).WithConstructorArgument("command", this.Kernel.Get<CreateUserCommand>());
            this.Bind<ICommand>().To<ValidatableCommand>().InSingletonScope().Named(CreateTaskCommandName).WithConstructorArgument("command", this.Kernel.Get<CreateTaskCommand>());
            this.Bind<ICommand>().To<ValidatableCommand>().InSingletonScope().Named(CreateProjectCommandName).WithConstructorArgument("command", this.Kernel.Get<CreateProjectCommand>());
            this.Bind<ICommand>().To<ValidatableCommand>().InSingletonScope().Named(ListProjectDetailsCommandName).WithConstructorArgument("command", this.Kernel.Get<ListProjectDetailsCommand>());

            // with caching :)
            this.Bind<CacheableCommand>().ToSelf().WithConstructorArgument("command", this.Kernel.Get<ListProjectsCommand>());
            this.Bind<ICommand>().To<ValidatableCommand>().InSingletonScope().Named(ListProjectsCommandName).
                WithConstructorArgument("command", this.Kernel.Get<CacheableCommand>());

            // without caching ;)
            //this.Bind<ICommand>().To<ValidatableCommand>().InSingletonScope().Named(ListProjectsCommandName).WithConstructorArgument("command", this.Kernel.Get<ListProjectsCommand>());

            // factories 
            this.Bind<ICommandsFactory>().ToFactory().InSingletonScope();
            this.Bind<ICommand>().ToMethod(context =>
            {
                var commandName = (string)context.Parameters.First().GetValue(context, null);
                commandName = commandName.ToLower();
                var command = context.Kernel.Get<ICommand>(commandName);

                return command;
            }).NamedLikeFactoryMethod((ICommandsFactory factory) => factory.GetCommandFromString(null));        
        }
    }
}
