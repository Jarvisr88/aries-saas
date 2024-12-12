namespace DevExpress.XtraExport.OfficeArt
{
    using DevExpress.XtraExport.Implementation;
    using System;
    using System.IO;

    internal class OfficeArtPictureClientData : OfficeArtClientData
    {
        private XlsPictureObject pictureObject;

        public OfficeArtPictureClientData(XlsPictureObject pictureObject)
        {
            this.pictureObject = pictureObject;
        }

        protected override void WriteObjData(BinaryWriter writer)
        {
            writer.Write((ushort) 0x15);
            writer.Write((ushort) 0x12);
            writer.Write((ushort) 8);
            writer.Write((ushort) this.PictureObject.PictureId);
            writer.Write((ushort) 0x6011);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write((ushort) 7);
            writer.Write((ushort) 2);
            writer.Write((ushort) 0xffff);
            writer.Write((ushort) 8);
            writer.Write((ushort) 2);
            writer.Write((ushort) 1);
            writer.Write(0);
        }

        public XlsPictureObject PictureObject =>
            this.pictureObject;
    }
}

