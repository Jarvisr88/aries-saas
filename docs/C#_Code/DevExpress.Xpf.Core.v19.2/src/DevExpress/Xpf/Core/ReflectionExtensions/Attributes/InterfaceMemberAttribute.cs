namespace DevExpress.Xpf.Core.ReflectionExtensions.Attributes
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class InterfaceMemberAttribute : Attribute
    {
        public InterfaceMemberAttribute(string interfaceTypeName)
        {
            this.InterfaceName = interfaceTypeName;
        }

        public InterfaceMemberAttribute(Type interfacType) : this(interfacType.FullName)
        {
        }

        public string InterfaceName { get; private set; }
    }
}

