namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class DrawingBlipTileInfo : ICloneable<DrawingBlipTileInfo>, ISupportsCopyFrom<DrawingBlipTileInfo>, ISupportsSizeOf, ISupportsBinaryReadWrite
    {
        private const int offsetFlip = 4;
        private const uint maskAlign = 15;
        private const uint maskFlip = 0x30;
        private uint packedValues;
        private int scaleX;
        private int scaleY;
        private long offsetX;
        private long offsetY;

        public DrawingBlipTileInfo Clone()
        {
            DrawingBlipTileInfo info = new DrawingBlipTileInfo();
            info.CopyFrom(this);
            return info;
        }

        public void CopyFrom(DrawingBlipTileInfo value)
        {
            Guard.ArgumentNotNull(value, "value");
            this.packedValues = value.packedValues;
            this.scaleX = value.scaleX;
            this.scaleY = value.scaleY;
            this.offsetX = value.offsetX;
            this.offsetY = value.offsetY;
        }

        void ISupportsBinaryReadWrite.Read(BinaryReader reader)
        {
            this.packedValues = reader.ReadUInt32();
            this.scaleX = reader.ReadInt32();
            this.scaleY = reader.ReadInt32();
            this.offsetX = reader.ReadInt64();
            this.offsetY = reader.ReadInt64();
        }

        void ISupportsBinaryReadWrite.Write(BinaryWriter writer)
        {
            writer.Write(this.packedValues);
            writer.Write(this.scaleX);
            writer.Write(this.scaleY);
            writer.Write(this.offsetX);
            writer.Write(this.offsetY);
        }

        public override bool Equals(object obj)
        {
            DrawingBlipTileInfo info = obj as DrawingBlipTileInfo;
            return ((info != null) ? ((this.packedValues == info.packedValues) && ((this.scaleX == info.scaleX) && ((this.scaleY == info.scaleY) && ((this.offsetX == info.offsetX) && (this.offsetY == info.offsetY))))) : false);
        }

        public override int GetHashCode() => 
            (((((int) this.packedValues) ^ this.scaleX) ^ this.scaleY) ^ this.offsetX.GetHashCode()) ^ this.offsetY.GetHashCode();

        public int SizeOf() => 
            DXMarshal.SizeOf(base.GetType());

        public RectangleAlignType TileAlign
        {
            get => 
                (RectangleAlignType) PackedValues.GetIntBitValue(this.packedValues, 15);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 15, (int) value);
        }

        public TileFlipType TileFlip
        {
            get => 
                (TileFlipType) PackedValues.GetIntBitValue(this.packedValues, 0x30, 4);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 0x30, 4, (int) value);
        }

        public int ScaleX
        {
            get => 
                this.scaleX;
            set
            {
                ValueChecker.CheckValue(value, 0, 0x186a0, "ScaleX");
                this.scaleX = value;
            }
        }

        public int ScaleY
        {
            get => 
                this.scaleY;
            set
            {
                ValueChecker.CheckValue(value, 0, 0x186a0, "ScaleY");
                this.scaleY = value;
            }
        }

        public long OffsetX
        {
            get => 
                this.offsetX;
            set => 
                this.offsetX = value;
        }

        public long OffsetY
        {
            get => 
                this.offsetY;
            set => 
                this.offsetY = value;
        }
    }
}

