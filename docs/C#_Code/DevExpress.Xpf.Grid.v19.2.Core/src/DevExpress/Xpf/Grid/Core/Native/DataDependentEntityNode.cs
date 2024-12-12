namespace DevExpress.Xpf.Grid.Core.Native
{
    using DevExpress.Xpf.Data;
    using System;

    internal class DataDependentEntityNode
    {
        private readonly string propertyName;
        private readonly DataDependentEntity entity;
        private readonly DataDependentEntityNode[] children;

        public DataDependentEntityNode(string propertyName, DataDependentEntity entity) : this(propertyName, entity, new DataDependentEntityNode[0])
        {
        }

        public DataDependentEntityNode(string propertyName, DataDependentEntity entity, DataDependentEntityNode[] children)
        {
            this.propertyName = propertyName;
            this.entity = entity;
            this.children = children;
        }

        public string PropertyName =>
            this.propertyName;

        public DataDependentEntity Entity =>
            this.entity;

        public DataDependentEntityNode[] Children =>
            this.children;
    }
}

