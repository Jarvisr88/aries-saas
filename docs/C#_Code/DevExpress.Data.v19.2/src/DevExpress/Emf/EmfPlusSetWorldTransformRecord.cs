namespace DevExpress.Emf
{
    using System;

    public class EmfPlusSetWorldTransformRecord : EmfPlusRecord
    {
        private readonly DXTransformationMatrix matrix;

        public EmfPlusSetWorldTransformRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.matrix = reader.ReadTransformMatrix();
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DXTransformationMatrix Matrix =>
            this.matrix;
    }
}

