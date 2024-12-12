namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class DXMemberInfo : IDXMemberInfo
    {
        public DXMemberInfo(MemberInfo memberInfo) : this(memberInfo.Name)
        {
        }

        public DXMemberInfo(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}

