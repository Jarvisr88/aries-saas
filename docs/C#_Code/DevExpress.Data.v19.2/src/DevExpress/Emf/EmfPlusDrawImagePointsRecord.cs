namespace DevExpress.Emf
{
    using System;

    public class EmfPlusDrawImagePointsRecord : EmfPlusRecord
    {
        private readonly DXPointF[] points;
        private readonly DXRectangleF srcRectangle;

        public EmfPlusDrawImagePointsRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            reader.ReadInt32();
            reader.ReadInt32();
            this.srcRectangle = reader.ReadDXRectangleF(false);
            this.points = reader.ReadPoints(reader.ReadInt32(), flags);
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DXPointF[] Points =>
            this.points;

        public DXRectangleF SrcRectangle =>
            this.srcRectangle;
    }
}

