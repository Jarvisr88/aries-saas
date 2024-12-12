namespace DevExpress.Xpf.Docking
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutGroupSerializationInfo : BaseLayoutItemSerializationInfo
    {
        public LayoutGroupSerializationInfo(BaseLayoutItem owner) : base(owner)
        {
        }

        [XtraSerializableProperty]
        public bool IsUserDefined { get; set; }
    }
}

