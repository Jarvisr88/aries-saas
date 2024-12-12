namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Entity.Model;
    using DevExpress.Entity.ProjectModel;
    using System;
    using System.Linq;

    public abstract class EF60DbDescendantBuilderProviderBase : DescendantBuilderProvider
    {
        protected EF60DbDescendantBuilderProviderBase()
        {
        }

        public override bool Available(Type dbContext, IDXTypeInfo dbDescendant, ISolutionTypesProvider typesProvider)
        {
            EdmxResource edmxResource = EdmxResource.GetEdmxResource(dbDescendant);
            bool flag = true;
            if (edmxResource != null)
            {
                flag = string.IsNullOrEmpty(this.ExpectedProviderName) || (edmxResource.GetProviderName() == this.ExpectedProviderName);
            }
            return (flag && EntityFrameworkModelBase.IsAtLeastEF6(dbContext));
        }

        protected IDXAssemblyInfo GetServicesAssembly(IDXTypeInfo dbDescendant, ISolutionTypesProvider typesProvider, string servicesAssemblyName)
        {
            IDXAssemblyInfo info = typesProvider.ActiveProjectTypes.Assemblies.FirstOrDefault<IDXAssemblyInfo>(x => x.Name == servicesAssemblyName);
            if (info != null)
            {
                return info;
            }
            if (!dbDescendant.IsSolutionType || dbDescendant.Assembly.IsProjectAssembly)
            {
                return null;
            }
            IProjectTypes projectTypes = typesProvider.GetProjectTypes(dbDescendant.Assembly.AssemblyFullName);
            projectTypes ??= typesProvider.ActiveProjectTypes;
            return projectTypes.Assemblies.FirstOrDefault<IDXAssemblyInfo>(x => (x.Name == servicesAssemblyName));
        }

        protected abstract string ExpectedProviderName { get; }
    }
}

