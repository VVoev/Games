using ProjectManager.Framework.Core.Commands.Abstracts;
using ProjectManager.Framework.Core.Commands.Contracts;
using ProjectManager.Framework.Core.Common.Exceptions;
using ProjectManager.Framework.Data;
using ProjectManager.Framework.Data.Factories;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager.Framework.Core.Commands.Creational
{
    public sealed class CreateProjectCommand : CreationalCommand, ICommand
    {
        private const int ParameterCountConstant = 4;

        public CreateProjectCommand(IModelsFactory factory, IDatabase database)
            : base(factory, database)
        {
        }

        public override int ParameterCount
        {
            get
            {
                return ParameterCountConstant;
            }
        }

        public override string Execute(IList<string> parameters)
        {
            if (this.Database.Projects.Any(x => x.Name == parameters[0]))
            {
                throw new UserValidationException("A project with that name already exists!");
            }

            var project = this.Factory.CreateProject(parameters[0], parameters[1], parameters[2], parameters[3]);
            this.Database.Projects.Add(project);

            return "Successfully created a new project!";
        }
    }
}