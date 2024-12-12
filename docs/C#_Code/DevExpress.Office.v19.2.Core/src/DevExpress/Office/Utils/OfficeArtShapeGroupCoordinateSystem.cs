namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class OfficeArtShapeGroupCoordinateSystem : OfficeDrawingPartBase
    {
        private const int recordLength = 0x10;
        private int left;
        private int top;
        private int right;
        private int bottom;

        public static OfficeArtShapeGroupCoordinateSystem FromStream(BinaryReader reader)
        {
            OfficeArtShapeGroupCoordinateSystem system = new OfficeArtShapeGroupCoordinateSystem();
            system.Read(reader);
            return system;
        }

        protected internal override int GetSize() => 
            0x10;

        protected internal void Read(BinaryReader reader)
        {
            this.Left = reader.ReadInt32();
            this.Top = reader.ReadInt32();
            this.Right = reader.ReadInt32();
            this.Bottom = reader.ReadInt32();
        }

        protected internal override void WriteCore(BinaryWriter writer)
        {
            writer.Write(this.Left);
            writer.Write(this.Top);
            writer.Write(this.Right);
            writer.Write(this.Bottom);
        }

        public override int HeaderInstanceInfo =>
            0;

        public override int HeaderTypeCode =>
            0xf009;

        public override int HeaderVersion =>
            1;

        public int Left
        {
            get => 
                this.left;
            set => 
                this.left = value;
        }

        public int Top
        {
            get => 
                this.top;
            set => 
                this.top = value;
        }

        public int Right
        {
            get => 
                this.right;
            set => 
                this.right = value;
        }

        public int Bottom
        {
            get => 
                this.bottom;
            set => 
                this.bottom = value;
        }
    }
}

