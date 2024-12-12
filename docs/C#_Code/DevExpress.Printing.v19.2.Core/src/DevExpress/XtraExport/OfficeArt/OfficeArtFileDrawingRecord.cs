namespace DevExpress.XtraExport.OfficeArt
{
    using System;
    using System.IO;

    internal class OfficeArtFileDrawingRecord : OfficeArtPartBase
    {
        private int drawingId;
        private int numberOfShapes;
        private int lastShapeId;

        public OfficeArtFileDrawingRecord(int drawingId, int numberOfShapes, int lastShapeId)
        {
            this.drawingId = drawingId;
            this.numberOfShapes = numberOfShapes;
            this.lastShapeId = lastShapeId;
        }

        protected internal override int GetSize() => 
            8;

        protected internal override void WriteCore(BinaryWriter writer)
        {
            writer.Write(this.numberOfShapes);
            writer.Write(this.lastShapeId);
        }

        public override int HeaderInstanceInfo =>
            this.drawingId;

        public override int HeaderTypeCode =>
            0xf008;

        public override int HeaderVersion =>
            0;
    }
}

