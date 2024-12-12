namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class DXPropertyInfo : DXMemberInfo, IDXPropertyInfo, IDXMemberInfo
    {
        public DXPropertyInfo(PropertyInfo propertyInfo) : base(propertyInfo)
        {
            this.PropertyType = MetaDataServices.GetExistingOrCreateNew(propertyInfo.PropertyType);
        }

        public DXPropertyInfo(string name, IDXTypeInfo propertyType) : base(name)
        {
            this.PropertyType = propertyType;
        }

        public IDXTypeInfo PropertyType { get; private set; }
    }
}

