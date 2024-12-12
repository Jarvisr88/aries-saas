namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class OfficeArtDrawingContainer : CompositeOfficeDrawingPartBase
    {
        private OfficeArtFileDrawingGroupRecord fileDrawingBlock = new OfficeArtFileDrawingGroupRecord();
        private OfficeArtBlipStoreContainer blipContainer = new OfficeArtBlipStoreContainer();
        private OfficeArtSplitMenuColorContainer splitMenuColorContainer = new OfficeArtSplitMenuColorContainer();

        public OfficeArtDrawingContainer()
        {
            base.Items.Add(this.fileDrawingBlock);
            base.Items.Add(this.blipContainer);
            base.Items.Add(this.splitMenuColorContainer);
        }

        private void CheckHeader(OfficeArtRecordHeader header)
        {
            if ((header.Version != 15) || ((header.InstanceInfo != 0) || (header.TypeCode != 0xf000)))
            {
                OfficeArtExceptions.ThrowInvalidContent();
            }
        }

        public static OfficeArtDrawingContainer FromStream(BinaryReader reader, BinaryReader embeddedReader)
        {
            OfficeArtDrawingContainer container = new OfficeArtDrawingContainer();
            container.Read(reader, embeddedReader);
            return container;
        }

        private void ParseHeader(BinaryReader reader, BinaryReader embeddedReader)
        {
            OfficeArtRecordHeader header = OfficeArtRecordHeader.FromStream(reader);
            if (header.TypeCode == 0xf006)
            {
                this.fileDrawingBlock = OfficeArtFileDrawingGroupRecord.FromStream(reader, header);
            }
            else if (header.TypeCode == 0xf001)
            {
                this.blipContainer = OfficeArtBlipStoreContainer.FromStream(reader, embeddedReader, header);
            }
            else if (header.TypeCode == 0xf11e)
            {
                this.splitMenuColorContainer = OfficeArtSplitMenuColorContainer.FromStream(reader, header);
            }
            else
            {
                reader.BaseStream.Seek((long) header.Length, SeekOrigin.Current);
            }
        }

        protected internal void Read(BinaryReader reader, BinaryReader embeddedReader)
        {
            OfficeArtRecordHeader header = OfficeArtRecordHeader.FromStream(reader);
            this.CheckHeader(header);
            long num = reader.BaseStream.Position + header.Length;
            while (reader.BaseStream.Position < num)
            {
                this.ParseHeader(reader, embeddedReader);
            }
        }

        public void Write(BinaryWriter writer, BinaryWriter embeddedWriter)
        {
            base.WriteHeader(writer);
            this.FileDrawingBlock.Write(writer);
            this.BlipContainer.Write(writer, embeddedWriter);
            this.SplitMenuColorContainer.Write(writer);
        }

        public override int HeaderInstanceInfo =>
            0;

        public override int HeaderTypeCode =>
            0xf000;

        public override int HeaderVersion =>
            15;

        public OfficeArtFileDrawingGroupRecord FileDrawingBlock =>
            this.fileDrawingBlock;

        public OfficeArtBlipStoreContainer BlipContainer =>
            this.blipContainer;

        public OfficeArtSplitMenuColorContainer SplitMenuColorContainer =>
            this.splitMenuColorContainer;
    }
}

