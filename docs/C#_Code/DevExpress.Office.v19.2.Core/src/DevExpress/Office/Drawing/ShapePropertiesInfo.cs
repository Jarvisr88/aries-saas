namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class ShapePropertiesInfo : ICloneable<ShapePropertiesInfo>, ISupportsCopyFrom<ShapePropertiesInfo>, ISupportsSizeOf, ISupportsBinaryReadWrite
    {
        private const uint maskBlackWhiteMode = 15;
        private const uint maskShapeType = 0xff0;
        private const int offsetShapeType = 4;
        private uint packedValues;

        public ShapePropertiesInfo Clone()
        {
            ShapePropertiesInfo info = new ShapePropertiesInfo();
            info.CopyFrom(this);
            return info;
        }

        public void CopyFrom(ShapePropertiesInfo value)
        {
            Guard.ArgumentNotNull(value, "ShapePropertiesInfo");
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
            ShapePropertiesInfo info = obj as ShapePropertiesInfo;
            return ((info != null) ? (this.packedValues == info.packedValues) : false);
        }

        public override int GetHashCode() => 
            (int) this.packedValues;

        public int SizeOf() => 
            DXMarshal.SizeOf(base.GetType());

        public OpenXmlBlackWhiteMode BlackAndWhiteMode
        {
            get => 
                (OpenXmlBlackWhiteMode) PackedValues.GetIntBitValue(this.packedValues, 15);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 15, (int) value);
        }

        public ShapePreset ShapeType
        {
            get => 
                (ShapePreset) PackedValues.GetIntBitValue(this.packedValues, 0xff0, 4);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 0xff0, 4, (int) value);
        }
    }
}

