namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class DrawingGradientFillInfo : ICloneable<DrawingGradientFillInfo>, ISupportsCopyFrom<DrawingGradientFillInfo>, ISupportsSizeOf, ISupportsBinaryReadWrite
    {
        private const int offsetGradientFlip = 2;
        private const uint maskGradientType = 3;
        private const uint maskGradientFlip = 12;
        private const uint maskRotateWithShape = 0x10;
        private const uint maskScaled = 0x20;
        private const uint maskUseGradientType = 0x40;
        private const uint maskUseGradientFlip = 0x80;
        private const uint maskUseAngle = 0x100;
        private const uint maskUseRotateWithShape = 0x200;
        private const uint maskUseScaled = 0x400;
        private uint packedValues = 0x10;
        private int angle;

        public DrawingGradientFillInfo Clone()
        {
            DrawingGradientFillInfo info = new DrawingGradientFillInfo();
            info.CopyFrom(this);
            return info;
        }

        public void CopyFrom(DrawingGradientFillInfo value)
        {
            Guard.ArgumentNotNull(value, "value");
            this.packedValues = value.packedValues;
            this.angle = value.angle;
        }

        internal static DrawingGradientFillInfo Create(DevExpress.Office.Drawing.GradientType type) => 
            new DrawingGradientFillInfo { 
                UseGradientType = true,
                GradientType = type
            };

        void ISupportsBinaryReadWrite.Read(BinaryReader reader)
        {
            this.packedValues = reader.ReadUInt32();
            this.angle = reader.ReadInt32();
        }

        void ISupportsBinaryReadWrite.Write(BinaryWriter writer)
        {
            writer.Write(this.packedValues);
            writer.Write(this.angle);
        }

        public override bool Equals(object obj)
        {
            DrawingGradientFillInfo info = obj as DrawingGradientFillInfo;
            return ((info != null) ? ((this.packedValues == info.packedValues) && (this.angle == info.angle)) : false);
        }

        public override int GetHashCode() => 
            ((int) this.packedValues) ^ this.angle;

        public int SizeOf() => 
            DXMarshal.SizeOf(base.GetType());

        public DevExpress.Office.Drawing.GradientType GradientType
        {
            get => 
                (DevExpress.Office.Drawing.GradientType) PackedValues.GetIntBitValue(this.packedValues, 3);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 3, (int) value);
        }

        public TileFlipType Flip
        {
            get => 
                (TileFlipType) PackedValues.GetIntBitValue(this.packedValues, 12, 2);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 12, 2, (int) value);
        }

        public bool RotateWithShape
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x10);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x10, value);
        }

        public bool Scaled
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x20);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x20, value);
        }

        public int Angle
        {
            get => 
                this.angle;
            set
            {
                ValueChecker.CheckValue(value, 0, 0x1499700, "Angle");
                this.angle = value;
            }
        }

        public bool UseGradientType
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x40);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x40, value);
        }

        public bool UseAngle
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x100);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x100, value);
        }

        public bool UseFlip
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x80);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x80, value);
        }

        public bool UseRotateWithShape
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x200);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x200, value);
        }

        public bool UseScaled
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x400);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x400, value);
        }
    }
}

