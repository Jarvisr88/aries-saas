namespace DevExpress.Emf
{
    using System;

    public class EmfPlusSetPageTransformRecord : EmfPlusRecord
    {
        private const short pageUnitMask = 0xff;
        private readonly float scaleFactor;
        private readonly DXGraphicsUnit unit;

        public EmfPlusSetPageTransformRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.scaleFactor = reader.ReadSingle();
            this.unit = ((DXGraphicsUnit) base.Flags) & ((DXGraphicsUnit) 0xff);
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public float ScaleFactor =>
            this.scaleFactor;

        public DXGraphicsUnit Unit =>
            this.unit;
    }
}

