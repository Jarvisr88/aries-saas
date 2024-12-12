namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class DXAssemblyInfo : HasTypesCacheBase, IDXAssemblyInfo, IHasTypesCache, IHasName
    {
        private string assemblyFullName;
        private DevExpress.Entity.ProjectModel.EdmxResources edmxResources;
        private DevExpress.Entity.ProjectModel.ResourceOptions resourceOptions;
        private string name;

        public DXAssemblyInfo(IDXAssemblyInfo dxAssemblyInfo)
        {
            if (dxAssemblyInfo != null)
            {
                this.resourceOptions = (dxAssemblyInfo as DXAssemblyInfo).resourceOptions;
                this.assemblyFullName = dxAssemblyInfo.AssemblyFullName;
                this.name = dxAssemblyInfo.Name;
                DXAssemblyInfo info = dxAssemblyInfo as DXAssemblyInfo;
                if (info != null)
                {
                    this.Assembly = info.Assembly;
                }
                this.IsProjectAssembly = dxAssemblyInfo.IsProjectAssembly;
                this.IsSolutionAssembly = dxAssemblyInfo.IsProjectAssembly || dxAssemblyInfo.IsSolutionAssembly;
            }
        }

        public DXAssemblyInfo(string assemblyFullName, bool isProjectAssembly, bool isSolutionAssembly, IResourceOptions resourceOptions)
        {
            this.ResourceOptions = resourceOptions;
            this.assemblyFullName = assemblyFullName;
            this.IsProjectAssembly = isProjectAssembly;
            this.IsSolutionAssembly = isSolutionAssembly | isProjectAssembly;
        }

        public DXAssemblyInfo(System.Reflection.Assembly assembly, bool isProjectAssembly, bool isSolutionAssembly, IResourceOptions resourceOptions, params Type[] type)
        {
            this.ResourceOptions = resourceOptions;
            this.Assembly = assembly;
            this.IsProjectAssembly = isProjectAssembly;
            this.IsSolutionAssembly = isSolutionAssembly | isProjectAssembly;
            this.AddTypes(type);
        }

        public override void Add(IDXTypeInfo typeInfo)
        {
            if (typeInfo is DXTypeInfo)
            {
                ((DXTypeInfo) typeInfo).Assembly = this;
            }
            base.Add(typeInfo);
        }

        private void AddTypes(IEnumerable<Type> types)
        {
            if (types != null)
            {
                foreach (Type type in types)
                {
                    this.Add(new DXTypeInfo(type));
                }
            }
        }

        public EdmxResource GetEdmxResource(IDXTypeInfo typeInfo) => 
            (typeInfo != null) ? this.EdmxResources.GetEdmxResource(typeInfo.Name) : null;

        private DevExpress.Entity.ProjectModel.EdmxResources EdmxResources
        {
            get
            {
                this.edmxResources ??= new DevExpress.Entity.ProjectModel.EdmxResources(this.Assembly, this.resourceOptions);
                return this.edmxResources;
            }
        }

        public System.Reflection.Assembly Assembly { get; private set; }

        public string AssemblyFullName
        {
            get
            {
                if (string.IsNullOrEmpty(this.assemblyFullName) && (this.Assembly != null))
                {
                    this.assemblyFullName = this.Assembly.FullName;
                }
                return this.assemblyFullName;
            }
        }

        public bool IsProjectAssembly { get; private set; }

        public IResourceOptions ResourceOptions { get; private set; }

        public bool IsSolutionAssembly { get; private set; }

        public string Name =>
            (this.Assembly == null) ? this.name : this.Assembly.GetName().Name;
    }
}

