namespace DevExpress.Office.Utils
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.IO;

    public class OfficeColorRecord
    {
        private const uint maskDefault = 0xff000000;
        private const uint maskColorScheme = 0x8000000;
        private const uint maskSystemColor = 0x10000000;
        private const uint maskSchemeIndex = 0xff;
        private const uint maskSystemIndex = 0xffff;
        private const uint maskColorUse = 0xff;
        private const uint maskTransform = 0xff00;
        private const uint maskTransformValue = 0xff0000;
        private uint data;

        public OfficeColorRecord()
        {
            this.data = uint.MaxValue;
        }

        public OfficeColorRecord(System.Drawing.Color color)
        {
            this.Color = color;
        }

        public OfficeColorRecord(int colorIndex)
        {
            this.ColorSchemeIndex = (byte) colorIndex;
        }

        public OfficeColorRecord(OfficeColorUse colorUse, OfficeColorTransform transform, byte transformValue)
        {
            if (colorUse == OfficeColorUse.None)
            {
                throw new ArgumentException("colorUse");
            }
            if (transform == OfficeColorTransform.None)
            {
                throw new ArgumentException("transform");
            }
            this.data = (uint) (((0x10000000 | (transformValue << 0x10)) | (((int) transform) << 8)) | colorUse);
        }

        public static OfficeColorRecord FromBytes(byte[] data, int offset)
        {
            OfficeColorRecord record = new OfficeColorRecord();
            record.ReadBytes(data, offset);
            return record;
        }

        public static OfficeColorRecord FromStream(BinaryReader reader)
        {
            OfficeColorRecord record = new OfficeColorRecord();
            record.Read(reader);
            return record;
        }

        public byte[] GetBytes() => 
            BitConverter.GetBytes(this.data);

        protected internal void Read(BinaryReader reader)
        {
            this.data = reader.ReadUInt32();
        }

        private void ReadBytes(byte[] data, int offset)
        {
            this.data = BitConverter.ToUInt32(data, offset);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.data);
        }

        public bool IsDefault =>
            (this.data & 0xff000000) == 0xff000000;

        public bool SystemColorUsed =>
            !this.IsDefault && ((this.data & 0x10000000) == 0x10000000);

        public bool ColorSchemeUsed =>
            !this.IsDefault && ((this.data & 0x8000000) == 0x8000000);

        public System.Drawing.Color Color
        {
            get => 
                DXColor.FromArgb(((int) this.data) & 0xff, (int) ((this.data & 0xff00) >> 8), (int) ((this.data & 0xff0000) >> 0x10));
            set => 
                this.data = (uint) ((value.R | (value.G << 8)) | (value.B << 0x10));
        }

        public byte ColorSchemeIndex
        {
            get => 
                (byte) (this.data & 0xff);
            set => 
                this.data = (uint) (0x8000000 | value);
        }

        public int SystemColorIndex
        {
            get => 
                ((int) this.data) & 0xffff;
            set => 
                this.data = (uint) (0x10000000L | (value & 0xffffL));
        }

        public OfficeColorUse ColorUse
        {
            get
            {
                if (!this.SystemColorUsed)
                {
                    return OfficeColorUse.None;
                }
                uint num = this.data & 0xff;
                return ((num >= 240) ? ((OfficeColorUse) num) : OfficeColorUse.None);
            }
        }

        public OfficeColorTransform Transform =>
            this.SystemColorUsed ? (((OfficeColorTransform) this.data) & ((OfficeColorTransform) 0xff00)) : OfficeColorTransform.None;

        public byte TransformValue =>
            this.SystemColorUsed ? ((byte) ((this.data & 0xff0000) >> 0x10)) : 0;
    }
}

