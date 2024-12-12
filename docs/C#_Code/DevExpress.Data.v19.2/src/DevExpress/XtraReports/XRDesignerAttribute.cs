namespace DevExpress.XtraReports
{
    using System;

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple=true, Inherited=true)]
    public sealed class XRDesignerAttribute : Attribute
    {
        private string designerTypeName;
        private Type designerBaseType;
        private string typeId;

        public XRDesignerAttribute(string designerTypeName);
        public XRDesignerAttribute(string designerTypeName, Type designerBaseType);
        public override bool Equals(object obj);
        public override int GetHashCode();

        public string DesignerBaseTypeName { get; }

        public string DesignerTypeName { get; }

        public override object TypeId { get; }
    }
}

