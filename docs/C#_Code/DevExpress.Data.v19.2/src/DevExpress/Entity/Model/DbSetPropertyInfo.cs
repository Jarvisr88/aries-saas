namespace DevExpress.Entity.Model
{
    using DevExpress.Entity.Model.Metadata;
    using System;
    using System.Collections.Generic;

    public class DbSetPropertyInfo : RuntimeWrapper, IEntitySetInfo
    {
        private string name;
        private readonly EFCoreContainerInfo entityContainerInfo;
        private Dictionary<string, object> attachedInfo;

        public DbSetPropertyInfo(object dbSetProperty, EFCoreContainerInfo entityContainerInfo) : base("Microsoft.Data.Entity.Internal.DbSetProperty", dbSetProperty)
        {
            this.entityContainerInfo = entityContainerInfo;
            this.attachedInfo = new Dictionary<string, object>();
        }

        public IEntityTypeInfo ElementType =>
            new EF7TypeInfo((Type) base.GetPropertyAccessor("ClrType").Value);

        public bool IsView =>
            base.GetPropertyAccessor("Setter").Value == null;

        public bool ReadOnly =>
            base.GetPropertyAccessor("Setter").Value == null;

        public string Name
        {
            get
            {
                string name = this.name;
                if (this.name == null)
                {
                    string local1 = this.name;
                    name = this.name = (string) base.GetPropertyAccessor("Name").Value;
                }
                return name;
            }
        }

        public IEntityContainerInfo EntityContainerInfo =>
            this.entityContainerInfo;

        Dictionary<string, object> IEntitySetInfo.AttachedInfo =>
            this.attachedInfo;
    }
}

