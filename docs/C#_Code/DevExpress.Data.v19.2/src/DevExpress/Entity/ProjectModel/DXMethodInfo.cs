namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class DXMethodInfo : DXMemberInfo, IDXMethodInfo, IDXMemberInfo
    {
        public DXMethodInfo(MethodInfo methodInfo) : base(methodInfo)
        {
            this.ReturnType = MetaDataServices.GetExistingOrCreateNew(methodInfo.ReturnType);
        }

        public DXMethodInfo(string name, IDXTypeInfo returnType) : base(name)
        {
            this.ReturnType = returnType;
        }

        public IDXTypeInfo ReturnType { get; private set; }
    }
}

