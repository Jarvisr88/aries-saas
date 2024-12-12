namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class SolutionTypesProviderConsole : SolutionTypesProviderBase
    {
        private List<Type> typeList;
        private string activeProjectAssemblyFullName;
        private IEnumerable<string> solutionAssemblyFullNames;

        public SolutionTypesProviderConsole()
        {
            this.typeList = new List<Type>();
        }

        public SolutionTypesProviderConsole(string activeProjectAssemblyFullName)
        {
            this.typeList = new List<Type>();
            this.activeProjectAssemblyFullName = activeProjectAssemblyFullName;
        }

        public SolutionTypesProviderConsole(string activeProjectAssemblyFullName, IEnumerable<string> solutionAssemblyFullNames) : this(activeProjectAssemblyFullName)
        {
            this.solutionAssemblyFullNames = solutionAssemblyFullNames;
        }

        public void Add(Type type)
        {
            if (!this.typeList.Contains(type))
            {
                this.typeList.Add(type);
            }
        }

        public void AddRange(IEnumerable<Type> types)
        {
            foreach (Type type in types)
            {
                this.Add(type);
            }
        }

        protected override string GetActiveProjectAssemblyFullName() => 
            this.activeProjectAssemblyFullName;

        protected override IEnumerable<Type> GetActiveProjectTypes() => 
            this.typeList;

        protected override IDXAssemblyInfo GetAssemblyCore(string assemblyName)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            return new DXAssemblyInfo(assembly, false, false, null, assembly.GetTypes());
        }

        protected override string GetOutputDir() => 
            null;

        protected override string[] GetProjectOutputs() => 
            null;

        protected override IEnumerable<string> GetSolutionAssemblyFullNames() => 
            this.solutionAssemblyFullNames;
    }
}

