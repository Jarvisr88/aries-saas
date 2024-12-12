namespace DevExpress.Emf
{
    using System;

    public class EmfPlusSetClipRegionRecord : EmfPlusSetClipRecord
    {
        public EmfPlusSetClipRegionRecord(short flags) : base(flags)
        {
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

