namespace DevExpress.Emf
{
    using System;

    public class EmfPlusDrawPathRecord : EmfPlusRecord
    {
        private readonly int penId;

        public EmfPlusDrawPathRecord(byte pathId, byte penId) : base(pathId)
        {
            this.penId = penId;
        }

        public EmfPlusDrawPathRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.penId = reader.ReadInt32();
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(this.penId);
        }

        public int PenId =>
            this.penId;

        protected override int DataSize =>
            4;

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusDrawPath;
    }
}

