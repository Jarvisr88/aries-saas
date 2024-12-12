namespace DevExpress.Emf
{
    using System;

    public class EmfPlusFillPathRecord : EmfPlusFillBase
    {
        public EmfPlusFillPathRecord(byte pathId, ARGBColor color) : base(color, pathId)
        {
        }

        public EmfPlusFillPathRecord(byte pathId, byte brushId) : base(brushId, pathId)
        {
        }

        public EmfPlusFillPathRecord(short flags, EmfPlusReader reader) : base(flags, reader)
        {
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusFillPath;

        protected override int DataSize =>
            4;
    }
}

