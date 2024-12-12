namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class DrawingBlipFillInfo : ICloneable<DrawingBlipFillInfo>, ISupportsCopyFrom<DrawingBlipFillInfo>, ISupportsSizeOf, ISupportsBinaryReadWrite
    {
        private const uint maskDpi = 0xffff;
        private const uint maskRotateWithShape = 0x10000;
        private const uint maskStretch = 0x20000;
        private uint packedValues = 0x10000;

        public DrawingBlipFillInfo Clone()
        {
            DrawingBlipFillInfo info = new DrawingBlipFillInfo();
            info.CopyFrom(this);
            return info;
        }

        public void CopyFrom(DrawingBlipFillInfo value)
        {
            Guard.ArgumentNotNull(value, "value");
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

        public override bool Equals(object obj)
        {
            DrawingBlipFillInfo info = obj as DrawingBlipFillInfo;
            return ((info != null) ? (this.packedValues == info.packedValues) : false);
        }

        public override int GetHashCode() => 
            (int) this.packedValues;

        public int SizeOf() => 
            DXMarshal.SizeOf(base.GetType());

        public int Dpi
        {
            get => 
                PackedValues.GetIntBitValue(this.packedValues, 0xffff);
            set
            {
                ValueChecker.CheckValue(value, 0, 0xffff, "Dpi");
                PackedValues.SetIntBitValue(ref this.packedValues, 0xffff, value);
            }
        }

        public bool RotateWithShape
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x10000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x10000, value);
        }

        public bool Stretch
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x20000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x20000, value);
        }
    }
}

