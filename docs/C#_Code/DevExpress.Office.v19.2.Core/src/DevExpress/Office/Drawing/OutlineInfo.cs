namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class OutlineInfo : ICloneable<OutlineInfo>, ISupportsCopyFrom<OutlineInfo>, ISupportsSizeOf, ISupportsBinaryReadWrite
    {
        private static readonly OutlineInfo defaultInfo = new OutlineInfo();
        private const uint MaskJoinStyle = 3;
        private const uint MaskDashing = 60;
        private const uint MaskHeadLength = 0xc0;
        private const uint MaskHeadWidth = 0x300;
        private const uint MaskTailLength = 0xc00;
        private const uint MaskTailWidth = 0x3000;
        private const uint MaskHeadType = 0x1c000;
        private const uint MaskTailType = 0xe0000;
        private const uint MaskStrokeAlignment = 0x300000;
        private const uint MaskEndCapStyle = 0xc00000;
        private const uint MaskCompoundType = 0x7000000;
        private const uint MaskHasLineJoinStyle = 0x8000000;
        private const uint MaskHasCompoundType = 0x10000000;
        private const uint MaskHasDashing = 0x20000000;
        private const uint MaskHasWidth = 0x40000000;
        private uint packedValues = 0x801542;
        private uint packedValues2;
        private const uint MaskHasHeadLength = 1;
        private const uint MaskHasHeadWidth = 2;
        private const uint MaskHasTailLength = 4;
        private const uint MaskHasTailWidth = 8;
        private const uint MaskHasHeadType = 0x10;
        private const uint MaskHasTailType = 0x20;
        private const uint MaskHasStrokeAlignment = 0x40;
        private const uint MaskHasEndCapStyle = 0x80;
        private const uint MaskHasMiterLimit = 0x100;
        private int width;
        private int miterLimit = 0xc3500;

        public OutlineInfo Clone()
        {
            OutlineInfo info = new OutlineInfo();
            info.CopyFrom(this);
            return info;
        }

        public void CopyFrom(OutlineInfo value)
        {
            this.packedValues = value.packedValues;
            this.packedValues2 = value.packedValues2;
            this.width = value.width;
            this.miterLimit = value.miterLimit;
        }

        void ISupportsBinaryReadWrite.Read(BinaryReader reader)
        {
            this.packedValues = reader.ReadUInt32();
            this.packedValues2 = reader.ReadUInt32();
            this.width = reader.ReadInt32();
            this.miterLimit = reader.ReadInt32();
        }

        void ISupportsBinaryReadWrite.Write(BinaryWriter writer)
        {
            writer.Write(this.packedValues);
            writer.Write(this.packedValues2);
            writer.Write(this.width);
            writer.Write(this.miterLimit);
        }

        public override bool Equals(object obj)
        {
            OutlineInfo info = obj as OutlineInfo;
            return ((info != null) ? ((this.packedValues == info.packedValues) && ((this.packedValues2 == info.packedValues2) && ((this.width == info.width) && (this.miterLimit == info.miterLimit)))) : false);
        }

        private bool GetBooleanValue(uint mask) => 
            (this.packedValues & mask) != 0;

        public override int GetHashCode() => 
            ((this.packedValues.GetHashCode() ^ this.packedValues2.GetHashCode()) ^ this.width) ^ this.miterLimit;

        private uint GetUIntValue(uint mask, int bits) => 
            (this.packedValues & mask) >> (bits & 0x1f);

        private void SetBooleanValue(uint mask, bool bitVal)
        {
            if (bitVal)
            {
                this.packedValues |= mask;
            }
            else
            {
                this.packedValues &= ~mask;
            }
        }

        private void SetUIntValue(uint mask, int bits, uint value)
        {
            this.packedValues &= ~mask;
            this.packedValues |= (value << (bits & 0x1f)) & mask;
        }

        public int SizeOf() => 
            ObjectSizeHelper.CalculateApproxObjectSize32(this, true);

        public static OutlineInfo DefaultInfo =>
            defaultInfo;

        public static OutlineHeadTailSize DefaultHeadTailSize =>
            OutlineHeadTailSize.Medium;

        public static OutlineHeadTailType DefaultHeadTailType =>
            OutlineHeadTailType.None;

        public LineJoinStyle JoinStyle
        {
            get => 
                (LineJoinStyle) this.GetUIntValue(3, 0);
            set => 
                this.SetUIntValue(3, 0, (uint) value);
        }

        public OutlineDashing Dashing
        {
            get => 
                (OutlineDashing) this.GetUIntValue(60, 2);
            set => 
                this.SetUIntValue(60, 2, (uint) value);
        }

        public OutlineHeadTailSize HeadLength
        {
            get => 
                (OutlineHeadTailSize) this.GetUIntValue(0xc0, 6);
            set => 
                this.SetUIntValue(0xc0, 6, (uint) value);
        }

        public OutlineHeadTailSize HeadWidth
        {
            get => 
                (OutlineHeadTailSize) this.GetUIntValue(0x300, 8);
            set => 
                this.SetUIntValue(0x300, 8, (uint) value);
        }

        public OutlineHeadTailSize TailLength
        {
            get => 
                (OutlineHeadTailSize) this.GetUIntValue(0xc00, 10);
            set => 
                this.SetUIntValue(0xc00, 10, (uint) value);
        }

        public OutlineHeadTailSize TailWidth
        {
            get => 
                (OutlineHeadTailSize) this.GetUIntValue(0x3000, 12);
            set => 
                this.SetUIntValue(0x3000, 12, (uint) value);
        }

        public OutlineHeadTailType HeadType
        {
            get => 
                (OutlineHeadTailType) this.GetUIntValue(0x1c000, 14);
            set => 
                this.SetUIntValue(0x1c000, 14, (uint) value);
        }

        public OutlineHeadTailType TailType
        {
            get => 
                (OutlineHeadTailType) this.GetUIntValue(0xe0000, 0x11);
            set => 
                this.SetUIntValue(0xe0000, 0x11, (uint) value);
        }

        public OutlineStrokeAlignment StrokeAlignment
        {
            get => 
                (OutlineStrokeAlignment) this.GetUIntValue(0x300000, 20);
            set => 
                this.SetUIntValue(0x300000, 20, (uint) value);
        }

        public OutlineEndCapStyle EndCapStyle
        {
            get => 
                (OutlineEndCapStyle) this.GetUIntValue(0xc00000, 0x16);
            set => 
                this.SetUIntValue(0xc00000, 0x16, (uint) value);
        }

        public OutlineCompoundType CompoundType
        {
            get => 
                (OutlineCompoundType) this.GetUIntValue(0x7000000, 0x18);
            set => 
                this.SetUIntValue(0x7000000, 0x18, (uint) value);
        }

        public bool HasLineJoinStyle
        {
            get => 
                this.GetBooleanValue(0x8000000);
            set => 
                this.SetBooleanValue(0x8000000, value);
        }

        public bool HasCompoundType
        {
            get => 
                this.GetBooleanValue(0x10000000);
            set => 
                this.SetBooleanValue(0x10000000, value);
        }

        public bool HasDashing
        {
            get => 
                this.GetBooleanValue(0x20000000);
            set => 
                this.SetBooleanValue(0x20000000, value);
        }

        public bool HasWidth
        {
            get => 
                this.GetBooleanValue(0x40000000);
            set => 
                this.SetBooleanValue(0x40000000, value);
        }

        public int Width
        {
            get => 
                this.width;
            set => 
                this.width = value;
        }

        public int MiterLimit
        {
            get => 
                this.miterLimit;
            set => 
                this.miterLimit = value;
        }

        public bool IsDefaultHeadEndStyle =>
            (this.HeadLength == DefaultInfo.HeadLength) && ((this.HeadType == DefaultInfo.HeadType) && (this.HeadWidth == DefaultInfo.HeadWidth));

        public bool IsDefaultTailEndStyle =>
            (this.TailLength == DefaultInfo.TailLength) && ((this.TailType == DefaultInfo.TailType) && (this.TailWidth == DefaultInfo.TailWidth));

        public bool HasHeadLength
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues2, 1);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues2, 1, value);
        }

        public bool HasHeadWidth
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues2, 2);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues2, 2, value);
        }

        public bool HasTailLength
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues2, 4);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues2, 4, value);
        }

        public bool HasTailWidth
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues2, 8);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues2, 8, value);
        }

        public bool HasHeadType
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues2, 0x10);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues2, 0x10, value);
        }

        public bool HasTailType
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues2, 0x20);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues2, 0x20, value);
        }

        public bool HasStrokeAlignment
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues2, 0x40);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues2, 0x40, value);
        }

        public bool HasEndCapStyle
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues2, 0x80);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues2, 0x80, value);
        }

        public bool HasMiterLimit
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues2, 0x100);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues2, 0x100, value);
        }
    }
}

