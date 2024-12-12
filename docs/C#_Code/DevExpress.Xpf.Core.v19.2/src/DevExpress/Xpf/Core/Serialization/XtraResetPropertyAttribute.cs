namespace DevExpress.Xpf.Core.Serialization
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class XtraResetPropertyAttribute : Attribute
    {
        internal static readonly XtraResetPropertyAttribute DefaultInstance = new XtraResetPropertyAttribute();

        public XtraResetPropertyAttribute() : this(ResetPropertyMode.Auto)
        {
        }

        public XtraResetPropertyAttribute(ResetPropertyMode propertyResetType)
        {
            this.PropertyResetType = propertyResetType;
        }

        public ResetPropertyMode PropertyResetType { get; private set; }
    }
}

