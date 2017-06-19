using Bytes2you.Validation;
using ProjectManager.Framework.Core.Commands.Contracts;
using ProjectManager.Framework.Core.Common.Exceptions;
using ProjectManager.Framework.Data;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager.Framework.Core.Commands.Abstracts
{
    public abstract class Command : ICommand
    {
        private readonly IDatabase database;

        public Command(IDatabase database)
        {
            Guard.WhenArgument(database, "dataBase").IsNull().Throw();

            this.database = database;
        }

        protected IDatabase Database
        {
            get
            {
                return this.database;
            }
        }

        public abstract int ParameterCount
        {
            get;
        }

        public abstract string Execute(IList<string> parameters);

        protected virtual void ValidateParameters(IList<string> parameters)
        {
            if (parameters.Count != this.ParameterCount)
            {
                throw new UserValidationException("Invalid command parameters count!");
            }

            if (parameters.Any(x => x == string.Empty))
            {
                throw new UserValidationException("Some of the passed parameters are empty!");
            }
        }
    }
}