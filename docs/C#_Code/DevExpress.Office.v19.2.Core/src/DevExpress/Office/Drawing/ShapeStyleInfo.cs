namespace DevExpress.Office.Drawing
{
    using DevExpress.Export.Xl;
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class ShapeStyleInfo : ICloneable<ShapeStyleInfo>, ISupportsCopyFrom<ShapeStyleInfo>, ISupportsSizeOf, ISupportsBinaryReadWrite
    {
        private int effectReferenceIdx;
        private int fillReferenceIdx;
        private byte fontReferenceIdx;
        private int lineReferenceIdx;

        public ShapeStyleInfo Clone()
        {
            ShapeStyleInfo info = new ShapeStyleInfo();
            info.CopyFrom(this);
            return info;
        }

        public void CopyFrom(ShapeStyleInfo value)
        {
            Guard.ArgumentNotNull(value, "ShapeStyleInfo");
            this.effectReferenceIdx = value.effectReferenceIdx;
            this.fillReferenceIdx = value.fillReferenceIdx;
            this.fontReferenceIdx = value.fontReferenceIdx;
            this.lineReferenceIdx = value.lineReferenceIdx;
        }

        void ISupportsBinaryReadWrite.Read(BinaryReader reader)
        {
            this.effectReferenceIdx = reader.ReadInt32();
            this.fillReferenceIdx = reader.ReadInt32();
            this.fontReferenceIdx = reader.ReadByte();
            this.lineReferenceIdx = reader.ReadInt32();
        }

        void ISupportsBinaryReadWrite.Write(BinaryWriter writer)
        {
            writer.Write(this.effectReferenceIdx);
            writer.Write(this.fillReferenceIdx);
            writer.Write(this.fontReferenceIdx);
            writer.Write(this.lineReferenceIdx);
        }

        public override bool Equals(object obj)
        {
            ShapeStyleInfo info = obj as ShapeStyleInfo;
            return ((info != null) ? ((this.effectReferenceIdx == info.effectReferenceIdx) && ((this.fillReferenceIdx == info.fillReferenceIdx) && ((this.fontReferenceIdx == info.fontReferenceIdx) && (this.lineReferenceIdx == info.lineReferenceIdx)))) : false);
        }

        public override int GetHashCode() => 
            ((this.effectReferenceIdx ^ this.fillReferenceIdx) ^ this.fontReferenceIdx) ^ this.lineReferenceIdx;

        public int SizeOf() => 
            DXMarshal.SizeOf(base.GetType());

        public int EffectReferenceIdx
        {
            get => 
                this.effectReferenceIdx;
            set => 
                this.effectReferenceIdx = value;
        }

        public int FillReferenceIdx
        {
            get => 
                this.fillReferenceIdx;
            set => 
                this.fillReferenceIdx = value;
        }

        public XlFontSchemeStyles FontReferenceIdx
        {
            get => 
                (XlFontSchemeStyles) this.fontReferenceIdx;
            set => 
                this.fontReferenceIdx = (byte) value;
        }

        public int LineReferenceIdx
        {
            get => 
                this.lineReferenceIdx;
            set => 
                this.lineReferenceIdx = value;
        }
    }
}

