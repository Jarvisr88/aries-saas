namespace DevExpress.XtraExport.OfficeArt
{
    using DevExpress.XtraExport.Implementation;
    using System;
    using System.IO;

    internal class OfficeArtShapeClientData : OfficeArtClientData
    {
        private XlShape shape;

        public OfficeArtShapeClientData(XlShape shape)
        {
            this.shape = shape;
        }

        private ushort GetObjType() => 
            (this.shape.GeometryPreset != XlGeometryPreset.Rect) ? 1 : 2;

        protected override void WriteObjData(BinaryWriter writer)
        {
            writer.Write((ushort) 0x15);
            writer.Write((ushort) 0x12);
            writer.Write(this.GetObjType());
            writer.Write((ushort) this.shape.Id);
            writer.Write((ushort) 0x6011);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
        }

        public XlShape Shape =>
            this.shape;
    }
}

