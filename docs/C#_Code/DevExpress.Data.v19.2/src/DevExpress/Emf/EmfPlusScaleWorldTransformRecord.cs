namespace DevExpress.Emf
{
    using System;

    public class EmfPlusScaleWorldTransformRecord : EmfPlusModifyWorldTransform
    {
        private readonly float sx;
        private readonly float sy;

        public EmfPlusScaleWorldTransformRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.sx = reader.ReadSingle();
            this.sy = reader.ReadSingle();
        }

        public EmfPlusScaleWorldTransformRecord(float sx, float sy)
        {
            this.sx = sx;
            this.sy = sy;
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(this.sx);
            writer.Write(this.sy);
        }

        public override DXTransformationMatrix Matrix =>
            new DXTransformationMatrix(this.sx, 0f, 0f, this.sy, 0f, 0f);

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusScaleWorldTransform;

        protected override int DataSize =>
            8;
    }
}

