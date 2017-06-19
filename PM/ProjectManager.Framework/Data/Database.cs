using ProjectManager.Framework.Data;
using ProjectManager.Framework.Data.Models;
using System.Collections.Generic;

namespace ProjectManager.Data
{
    public class Database : IDatabase
    {
        private readonly IList<Project> projects;

        public Database()
        {
            this.projects = new List<Project>();
        }

        public IList<Project> Projects
        {
            get
            {
                return this.projects;
            }
        }
    }
}
