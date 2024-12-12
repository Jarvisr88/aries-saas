namespace DevExpress.Emf
{
    using System;

    public class EmfPlusRotateWorldTransformRecord : EmfPlusRecord
    {
        private readonly float angle;

        public EmfPlusRotateWorldTransformRecord(float angle) : base(0)
        {
            this.angle = angle;
        }

        public EmfPlusRotateWorldTransformRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.angle = reader.ReadSingle();
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(this.angle);
        }

        public float Angle =>
            this.angle;

        public bool IsPostMultiplied =>
            (base.Flags & 0x2000) != 0;

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusRotateWorldTransform;

        protected override int DataSize =>
            4;
    }
}

