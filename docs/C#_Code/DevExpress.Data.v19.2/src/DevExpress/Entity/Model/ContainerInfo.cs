namespace DevExpress.Entity.Model
{
    using DevExpress.Entity.ProjectModel;
    using System;
    using System.Runtime.CompilerServices;

    public class ContainerInfo : DXTypeInfo, IContainerInfo, IDXTypeInfo, IHasName
    {
        public ContainerInfo(IDXTypeInfo type, DbContainerType containerType) : this(type.ResolveType(), containerType)
        {
            base.Assembly = type.Assembly;
        }

        public ContainerInfo(Type type, DbContainerType containerType) : base(type)
        {
            this.ContainerType = containerType;
        }

        public DbContainerType ContainerType { get; private set; }
    }
}

