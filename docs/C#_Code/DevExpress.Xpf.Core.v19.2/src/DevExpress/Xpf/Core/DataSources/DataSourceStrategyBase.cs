namespace DevExpress.Xpf.Core.DataSources
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class DataSourceStrategyBase
    {
        protected readonly IDataSource owner;
        private bool canGetDesignData;
        private string path;

        public DataSourceStrategyBase(IDataSource owner)
        {
            this.owner = owner;
        }

        public virtual bool CanGetDesignData() => 
            this.canGetDesignData;

        public virtual bool CanUpdateData() => 
            this.CanGetDesignData();

        public virtual object CreateContextIstance() => 
            (this.ContextType != null) ? Activator.CreateInstance(this.ContextType) : null;

        public virtual object CreateData(object value) => 
            value;

        public MemberInfo GetDataMemberInfo() => 
            this.CanGetDesignData() ? this.GetDataMemberInfoCore() : null;

        protected virtual MemberInfo GetDataMemberInfoCore() => 
            this.ContextType.GetProperty(this.path);

        public virtual object GetDataMemberValue(object contextInstance) => 
            ((PropertyInfo) this.GetDataMemberInfo()).GetValue(contextInstance, null);

        public virtual Type GetDataObjectType() => 
            this.OwnerDataMemberType;

        public virtual List<DesignTimePropertyInfo> GetDesignTimeProperties() => 
            null;

        internal void Update(Type contextType, string path)
        {
            this.ContextType = contextType;
            this.path = path;
            this.canGetDesignData = (this.ContextType != null) && !string.IsNullOrEmpty(path);
        }

        protected virtual Type OwnerDataMemberType =>
            ((PropertyInfo) this.GetDataMemberInfo()).PropertyType;

        protected Type ContextType { get; private set; }
    }
}

