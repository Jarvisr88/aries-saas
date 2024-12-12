namespace DevExpress.XtraExport.OfficeArt
{
    using DevExpress.Utils;
    using System;
    using System.IO;

    internal class OfficeArtBlipStoreFileBlock : OfficeArtPartBase
    {
        private readonly OfficeArtBlipBase blip;

        public OfficeArtBlipStoreFileBlock(OfficeArtBlipBase blip)
        {
            Guard.ArgumentNotNull(blip, "blip");
            this.blip = blip;
        }

        protected internal override int GetSize() => 
            (0x24 + this.blip.GetSize()) + 8;

        protected internal override void WriteCore(BinaryWriter writer)
        {
            byte blipType = this.blip.BlipType;
            writer.Write(blipType);
            writer.Write(blipType);
            writer.Write(this.blip.Digest);
            writer.Write((ushort) 0xff);
            writer.Write((int) (this.blip.GetSize() + 8));
            writer.Write(this.blip.NumberOfReferences);
            writer.Write(0);
            writer.Write(0);
            this.blip.Write(writer);
        }

        public override int HeaderInstanceInfo =>
            this.blip.BlipType;

        public override int HeaderTypeCode =>
            0xf007;

        public override int HeaderVersion =>
            2;

        protected internal OfficeArtBlipBase Blip =>
            this.blip;
    }
}

