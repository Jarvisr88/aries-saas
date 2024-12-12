namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class OfficeArtBlipStoreContainer : OfficeDrawingPartBase
    {
        private List<BlipBase> blips = new List<BlipBase>();

        public static OfficeArtBlipStoreContainer FromStream(BinaryReader reader, BinaryReader embeddedReader, OfficeArtRecordHeader header)
        {
            OfficeArtBlipStoreContainer container = new OfficeArtBlipStoreContainer();
            container.Read(reader, embeddedReader, header);
            return container;
        }

        protected internal override int GetSize()
        {
            int num = 0;
            int count = this.Blips.Count;
            for (int i = 0; i < count; i++)
            {
                num += this.Blips[i].GetSize();
            }
            return num;
        }

        protected internal void Read(BinaryReader reader, BinaryReader embeddedReader, OfficeArtRecordHeader header)
        {
            long endPosition = reader.BaseStream.Position + header.Length;
            this.blips = BlipFactory.ReadAllBlips(reader, embeddedReader, endPosition);
        }

        protected internal override bool ShouldWrite() => 
            this.Blips.Count > 0;

        public void Write(BinaryWriter writer, BinaryWriter embeddedWriter)
        {
            if (this.ShouldWrite())
            {
                base.WriteHeader(writer);
                this.WriteCore(writer, embeddedWriter);
            }
        }

        protected internal virtual void WriteCore(BinaryWriter writer, BinaryWriter embeddedWriter)
        {
            int count = this.Blips.Count;
            for (int i = 0; i < count; i++)
            {
                FileBlipStoreEntry entry = this.Blips[i] as FileBlipStoreEntry;
                if (entry != null)
                {
                    entry.Write(writer, embeddedWriter);
                }
                else
                {
                    this.Blips[i].Write(writer);
                }
            }
        }

        public override int HeaderInstanceInfo =>
            this.Blips.Count;

        public override int HeaderTypeCode =>
            0xf001;

        public override int HeaderVersion =>
            15;

        public List<BlipBase> Blips =>
            this.blips;
    }
}

