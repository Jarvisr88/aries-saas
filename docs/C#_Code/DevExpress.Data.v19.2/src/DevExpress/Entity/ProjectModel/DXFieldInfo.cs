namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class DXFieldInfo : DXMemberInfo, IDXFieldInfo, IDXMemberInfo
    {
        public DXFieldInfo(FieldInfo fieldInfo) : base(fieldInfo)
        {
            this.FieldType = MetaDataServices.GetExistingOrCreateNew(fieldInfo.FieldType);
        }

        public DXFieldInfo(string name, IDXTypeInfo fieldType) : base(name)
        {
            this.FieldType = fieldType;
        }

        public IDXTypeInfo FieldType { get; private set; }
    }
}

