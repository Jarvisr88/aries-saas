namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class DrawingTextSpacingInfo : ICloneable<DrawingTextSpacingInfo>, ISupportsCopyFrom<DrawingTextSpacingInfo>, ISupportsSizeOf, ISupportsBinaryReadWrite
    {
        private DrawingTextSpacingValueType type;
        private int value;

        public DrawingTextSpacingInfo Clone()
        {
            DrawingTextSpacingInfo info = new DrawingTextSpacingInfo();
            info.CopyFrom(this);
            return info;
        }

        public void CopyFrom(DrawingTextSpacingInfo value)
        {
            this.type = value.type;
            this.value = value.value;
        }

        void ISupportsBinaryReadWrite.Read(BinaryReader reader)
        {
            this.type = (DrawingTextSpacingValueType) reader.ReadInt32();
            this.value = reader.ReadInt32();
        }

        void ISupportsBinaryReadWrite.Write(BinaryWriter writer)
        {
            writer.Write((int) this.type);
            writer.Write(this.value);
        }

        public override bool Equals(object obj)
        {
            DrawingTextSpacingInfo info = obj as DrawingTextSpacingInfo;
            return ((info != null) ? ((this.type == info.type) && (this.value == info.value)) : false);
        }

        public override int GetHashCode() => 
            ((int) this.type) ^ this.value;

        public int SizeOf() => 
            DXMarshal.SizeOf(base.GetType());

        public DrawingTextSpacingValueType Type
        {
            get => 
                this.type;
            set
            {
                if (value == DrawingTextSpacingValueType.Automatic)
                {
                    this.value = 0;
                }
                this.type = value;
            }
        }

        public int Value
        {
            get => 
                this.value;
            set => 
                this.value = value;
        }
    }
}

