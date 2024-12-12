namespace DevExpress.Emf
{
    using System;

    public class EmfPlusMultiplyWorldTransformRecord : EmfPlusModifyWorldTransform
    {
        private readonly DXTransformationMatrix matrix;

        public EmfPlusMultiplyWorldTransformRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.matrix = reader.ReadTransformMatrix();
        }

        public override DXTransformationMatrix Matrix =>
            this.matrix;
    }
}

