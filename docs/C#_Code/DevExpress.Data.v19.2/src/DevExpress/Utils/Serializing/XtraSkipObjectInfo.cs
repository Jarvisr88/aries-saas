namespace DevExpress.Utils.Serializing
{
    using System;

    public class XtraSkipObjectInfo : XtraObjectInfo
    {
        public static readonly XtraObjectInfo SkipObjectInfoInstance = new XtraSkipObjectInfo();

        private XtraSkipObjectInfo() : base(null, null)
        {
        }

        public override bool Skip =>
            true;
    }
}

