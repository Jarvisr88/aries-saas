namespace DevExpress.Emf
{
    using System;

    public class EmfPlusTranslateWorldTransformRecord : EmfPlusModifyWorldTransform
    {
        private readonly float x;
        private readonly float y;

        public EmfPlusTranslateWorldTransformRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.x = reader.ReadSingle();
            this.y = reader.ReadSingle();
        }

        public EmfPlusTranslateWorldTransformRecord(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(this.x);
            writer.Write(this.y);
        }

        public override DXTransformationMatrix Matrix =>
            new DXTransformationMatrix(1f, 0f, 0f, 1f, this.x, this.y);

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusTranslateWorldTransform;

        protected override int DataSize =>
            8;
    }
}

