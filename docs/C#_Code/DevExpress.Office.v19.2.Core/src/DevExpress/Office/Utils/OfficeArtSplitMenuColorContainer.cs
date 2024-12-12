namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class OfficeArtSplitMenuColorContainer : OfficeDrawingPartBase
    {
        private const int recordLength = 0x10;
        private OfficeColorRecord fillColor = new OfficeColorRecord(DXColor.Empty);
        private OfficeColorRecord lineColor = new OfficeColorRecord(DXColor.Empty);
        private OfficeColorRecord shapeColor = new OfficeColorRecord(DXColor.Empty);
        private OfficeColorRecord color3D = new OfficeColorRecord(DXColor.Empty);

        public static OfficeArtSplitMenuColorContainer FromStream(BinaryReader reader, OfficeArtRecordHeader header)
        {
            OfficeArtSplitMenuColorContainer container = new OfficeArtSplitMenuColorContainer();
            container.Read(reader, header);
            return container;
        }

        protected internal override int GetSize() => 
            0x10;

        protected internal void Read(BinaryReader reader, OfficeArtRecordHeader header)
        {
            if (header.Length != 0x10)
            {
                OfficeArtExceptions.ThrowInvalidContent();
            }
            this.fillColor = OfficeColorRecord.FromStream(reader);
            this.lineColor = OfficeColorRecord.FromStream(reader);
            this.shapeColor = OfficeColorRecord.FromStream(reader);
            this.color3D = OfficeColorRecord.FromStream(reader);
        }

        protected internal override void WriteCore(BinaryWriter writer)
        {
            this.FillColor.Write(writer);
            this.LineColor.Write(writer);
            this.ShapeColor.Write(writer);
            this.Color3D.Write(writer);
        }

        public override int HeaderInstanceInfo =>
            4;

        public override int HeaderTypeCode =>
            0xf11e;

        public override int HeaderVersion =>
            0;

        public override int Length =>
            0x10;

        public OfficeColorRecord FillColor =>
            this.fillColor;

        public OfficeColorRecord LineColor =>
            this.lineColor;

        public OfficeColorRecord ShapeColor =>
            this.shapeColor;

        public OfficeColorRecord Color3D =>
            this.color3D;
    }
}

