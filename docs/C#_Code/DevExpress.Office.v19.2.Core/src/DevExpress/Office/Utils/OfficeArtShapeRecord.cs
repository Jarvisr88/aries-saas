namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class OfficeArtShapeRecord : OfficeDrawingPartBase
    {
        private const int defaultFlags = 0xa00;
        private const int recordLength = 8;
        private int shapeTypeCode;
        private int shapeIdentifier;
        private int flags;
        private const int maskGroup = 1;
        private const int maskChild = 2;
        private const int maskPatriarch = 4;
        private const int maskDeleted = 8;
        private const int maskOleShape = 0x10;
        private const int maskHaveMaster = 0x20;
        private const int maskFlipH = 0x40;
        private const int maskFlipV = 0x80;
        private const int maskConnector = 0x100;
        private const int maskHaveAnchor = 0x200;
        private const int maskBackground = 0x400;
        private const int maskHaveShapeType = 0x800;

        private OfficeArtShapeRecord()
        {
        }

        public OfficeArtShapeRecord(int shapeTypeCode)
        {
            this.shapeTypeCode = shapeTypeCode;
            this.flags = 0xa00;
        }

        public static OfficeArtShapeRecord FromStream(BinaryReader reader)
        {
            OfficeArtShapeRecord record = new OfficeArtShapeRecord();
            record.Read(reader);
            return record;
        }

        public static OfficeArtShapeRecord FromStream(BinaryReader reader, OfficeArtRecordHeader header)
        {
            OfficeArtShapeRecord record = new OfficeArtShapeRecord(header.InstanceInfo);
            record.Read(reader);
            return record;
        }

        private bool GetBoolValue(int mask) => 
            (this.flags & mask) != 0;

        protected internal override int GetSize() => 
            8;

        protected internal void Read(BinaryReader reader)
        {
            this.shapeIdentifier = reader.ReadInt32();
            this.flags = reader.ReadInt32();
        }

        private void SetBoolValue(bool value, int mask)
        {
            if (value)
            {
                this.flags |= mask;
            }
            else
            {
                this.flags &= ~mask;
            }
        }

        protected internal override void WriteCore(BinaryWriter writer)
        {
            writer.Write(this.ShapeIdentifier);
            writer.Write(this.Flags);
        }

        public override int HeaderInstanceInfo =>
            this.shapeTypeCode;

        public override int HeaderTypeCode =>
            0xf00a;

        public override int HeaderVersion =>
            2;

        public override int Length =>
            8;

        public int ShapeIdentifier
        {
            get => 
                this.shapeIdentifier;
            set => 
                this.shapeIdentifier = value;
        }

        public int Flags
        {
            get => 
                this.flags;
            set => 
                this.flags = value;
        }

        public bool IsGroup
        {
            get => 
                this.GetBoolValue(1);
            set => 
                this.SetBoolValue(value, 1);
        }

        public bool IsChild
        {
            get => 
                this.GetBoolValue(2);
            set => 
                this.SetBoolValue(value, 2);
        }

        public bool IsPatriarch
        {
            get => 
                this.GetBoolValue(4);
            set => 
                this.SetBoolValue(value, 4);
        }

        public bool IsDeleted
        {
            get => 
                this.GetBoolValue(8);
            set => 
                this.SetBoolValue(value, 8);
        }

        public bool IsOleShape
        {
            get => 
                this.GetBoolValue(0x10);
            set => 
                this.SetBoolValue(value, 0x10);
        }

        public bool HaveMaster
        {
            get => 
                this.GetBoolValue(0x20);
            set => 
                this.SetBoolValue(value, 0x20);
        }

        public bool FlipH
        {
            get => 
                this.GetBoolValue(0x40);
            set => 
                this.SetBoolValue(value, 0x40);
        }

        public bool FlipV
        {
            get => 
                this.GetBoolValue(0x80);
            set => 
                this.SetBoolValue(value, 0x80);
        }

        public bool IsConnector
        {
            get => 
                this.GetBoolValue(0x100);
            set => 
                this.SetBoolValue(value, 0x100);
        }

        public bool HaveAnchor
        {
            get => 
                this.GetBoolValue(0x200);
            set => 
                this.SetBoolValue(value, 0x200);
        }

        public bool IsBackground
        {
            get => 
                this.GetBoolValue(0x400);
            set => 
                this.SetBoolValue(value, 0x400);
        }

        public bool HaveShapeType
        {
            get => 
                this.GetBoolValue(0x800);
            set => 
                this.SetBoolValue(value, 0x800);
        }
    }
}

