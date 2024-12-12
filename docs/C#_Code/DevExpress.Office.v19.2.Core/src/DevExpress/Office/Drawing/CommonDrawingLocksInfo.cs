namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class CommonDrawingLocksInfo : ICloneable<CommonDrawingLocksInfo>, ISupportsCopyFrom<CommonDrawingLocksInfo>, ISupportsSizeOf, IEquatable<CommonDrawingLocksInfo>, ISupportsBinaryReadWrite
    {
        private const uint MaskNoGroup = 1;
        private const uint MaskNoSelect = 2;
        private const uint MaskNoRotate = 4;
        private const uint MaskNoChangeAspect = 8;
        private const uint MaskNoMove = 0x10;
        private const uint MaskNoResize = 0x20;
        private const uint MaskNoEditPoints = 0x40;
        private const uint MaskNoAdjustHandles = 0x80;
        private const uint MaskNoChangeArrowheads = 0x100;
        private const uint MaskNoChangeShapeType = 0x200;
        private const uint MaskNoTextEdit = 0x400;
        private const uint MaskNoCrop = 0x800;
        private const uint MaskNoDrillDown = 0x1000;
        private const uint MaskNoUngroup = 0x2000;
        private uint packedValues;

        public CommonDrawingLocksInfo Clone()
        {
            CommonDrawingLocksInfo info = new CommonDrawingLocksInfo();
            info.CopyFrom(this);
            return info;
        }

        public void CopyFrom(CommonDrawingLocksInfo value)
        {
            this.packedValues = value.packedValues;
        }

        void ISupportsBinaryReadWrite.Read(BinaryReader reader)
        {
            this.packedValues = reader.ReadUInt32();
        }

        void ISupportsBinaryReadWrite.Write(BinaryWriter writer)
        {
            writer.Write(this.packedValues);
        }

        public bool Equals(CommonDrawingLocksInfo other) => 
            (other != null) && (this.packedValues == other.packedValues);

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? (!(obj.GetType() != base.GetType()) ? this.Equals((CommonDrawingLocksInfo) obj) : false) : true) : false;

        public override int GetHashCode() => 
            (int) this.packedValues;

        public bool IsEmpty() => 
            this.packedValues == 0;

        public int SizeOf() => 
            DXMarshal.SizeOf(base.GetType());

        public bool NoGroup
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 1);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 1, value);
        }

        public bool NoSelect
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 2);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 2, value);
        }

        public bool NoRotate
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 4);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 4, value);
        }

        public bool NoChangeAspect
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 8);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 8, value);
        }

        public bool NoMove
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x10);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x10, value);
        }

        public bool NoResize
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x20);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x20, value);
        }

        public bool NoEditPoints
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x40);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x40, value);
        }

        public bool NoAdjustHandles
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x80);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x80, value);
        }

        public bool NoChangeArrowheads
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x100);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x100, value);
        }

        public bool NoChangeShapeType
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x200);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x200, value);
        }

        public bool NoTextEdit
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x400);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x400, value);
        }

        public bool NoCrop
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x800);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x800, value);
        }

        public bool NoDrillDown
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x1000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x1000, value);
        }

        public bool NoUngroup
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x2000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x2000, value);
        }
    }
}

