namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class DrawingTextBodyInfo : ICloneable<DrawingTextBodyInfo>, ISupportsCopyFrom<DrawingTextBodyInfo>, ISupportsSizeOf, ISupportsBinaryReadWrite
    {
        private static readonly DrawingTextBodyInfo defaultInfo = new DrawingTextBodyInfo();
        private const uint maskAnchor = 7;
        private const uint maskHorizontalOverflow = 8;
        private const uint maskVerticalOverflow = 0x30;
        private const uint maskTextWrapping = 0x40;
        private const uint maskAnchorCenter = 0x80;
        private const uint maskCompatibleLineSpacing = 0x100;
        private const uint maskForceAntiAlias = 0x200;
        private const uint maskFromWordArt = 0x400;
        private const uint maskRightToLeftColumns = 0x800;
        private const uint maskParagraphSpacing = 0x1000;
        private const uint maskUprightText = 0x2000;
        private const uint maskVerticalText = 0x1c000;
        private const uint maskHasRotation = 1;
        private const uint maskHasParagraphSpacing = 2;
        private const uint maskHasVerticalOverflow = 4;
        private const uint maskHasHorizontalOverflow = 8;
        private const uint maskHasVerticalText = 0x10;
        private const uint maskHasTextWrapping = 0x20;
        private const uint maskHasNumberOfColumns = 0x40;
        private const uint maskHasSpaceBetweenColumns = 0x80;
        private const uint maskHasRightToLeftColumns = 0x100;
        private const uint maskHasFromWordArt = 0x200;
        private const uint maskHasAnchor = 0x400;
        private const uint maskHasAnchorCenter = 0x800;
        private const uint maskHasForceAntiAlias = 0x1000;
        private const uint maskHasCompatibleLineSpacing = 0x2000;
        private uint packedValues = 0x406d;
        private uint packedOptionsValues;
        private int numberOfColumns = 1;
        private int rotation;
        private float spaceBetweenColumns;

        public DrawingTextBodyInfo Clone()
        {
            DrawingTextBodyInfo info = new DrawingTextBodyInfo();
            info.CopyFrom(this);
            return info;
        }

        public void CopyFrom(DrawingTextBodyInfo value)
        {
            Guard.ArgumentNotNull(value, "value");
            this.packedValues = value.packedValues;
            this.packedOptionsValues = value.packedOptionsValues;
            this.numberOfColumns = value.numberOfColumns;
            this.rotation = value.rotation;
            this.spaceBetweenColumns = value.spaceBetweenColumns;
        }

        void ISupportsBinaryReadWrite.Read(BinaryReader reader)
        {
            this.packedValues = reader.ReadUInt32();
            this.packedOptionsValues = reader.ReadUInt32();
            this.numberOfColumns = reader.ReadInt32();
            this.rotation = reader.ReadInt32();
            this.spaceBetweenColumns = reader.ReadSingle();
        }

        void ISupportsBinaryReadWrite.Write(BinaryWriter writer)
        {
            writer.Write(this.packedValues);
            writer.Write(this.packedOptionsValues);
            writer.Write(this.numberOfColumns);
            writer.Write(this.rotation);
            writer.Write(this.spaceBetweenColumns);
        }

        public override bool Equals(object obj)
        {
            DrawingTextBodyInfo info = obj as DrawingTextBodyInfo;
            return ((info != null) ? ((this.packedValues == info.packedValues) && ((this.packedOptionsValues == info.packedOptionsValues) && ((this.numberOfColumns == info.numberOfColumns) && ((this.rotation == info.rotation) && (this.spaceBetweenColumns == info.spaceBetweenColumns))))) : false);
        }

        public override int GetHashCode() => 
            ((((int) (this.packedValues ^ this.packedOptionsValues)) ^ this.numberOfColumns) ^ this.rotation) ^ this.spaceBetweenColumns.GetHashCode();

        public int SizeOf() => 
            DXMarshal.SizeOf(base.GetType());

        public static DrawingTextBodyInfo DefaultInfo =>
            defaultInfo;

        public DrawingTextAnchoringType Anchor
        {
            get => 
                (DrawingTextAnchoringType) PackedValues.GetIntBitValue(this.packedValues, 7, 0);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 7, 0, (int) value);
        }

        public DrawingTextHorizontalOverflowType HorizontalOverflow
        {
            get => 
                (DrawingTextHorizontalOverflowType) PackedValues.GetIntBitValue(this.packedValues, 8, 3);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 8, 3, (int) value);
        }

        public DrawingTextVerticalOverflowType VerticalOverflow
        {
            get => 
                (DrawingTextVerticalOverflowType) PackedValues.GetIntBitValue(this.packedValues, 0x30, 4);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 0x30, 4, (int) value);
        }

        public DrawingTextWrappingType TextWrapping
        {
            get => 
                (DrawingTextWrappingType) PackedValues.GetIntBitValue(this.packedValues, 0x40, 6);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 0x40, 6, (int) value);
        }

        public bool AnchorCenter
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x80);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x80, value);
        }

        public bool CompatibleLineSpacing
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x100);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x100, value);
        }

        public bool ForceAntiAlias
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x200);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x200, value);
        }

        public bool FromWordArt
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x400);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x400, value);
        }

        public bool RightToLeftColumns
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x800);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x800, value);
        }

        public bool ParagraphSpacing
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x1000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x1000, value);
        }

        public bool UprightText
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x2000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x2000, value);
        }

        public DrawingTextVerticalTextType VerticalText
        {
            get => 
                (DrawingTextVerticalTextType) PackedValues.GetIntBitValue(this.packedValues, 0x1c000, 14);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 0x1c000, 14, (int) value);
        }

        public bool HasRotation
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 1);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 1, value);
        }

        public bool HasParagraphSpacing
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 2);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 2, value);
        }

        public bool HasVerticalOverflow
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 4);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 4, value);
        }

        public bool HasHorizontalOverflow
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 8);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 8, value);
        }

        public bool HasVerticalText
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x10);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x10, value);
        }

        public bool HasTextWrapping
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x20);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x20, value);
        }

        public bool HasNumberOfColumns
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x40);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x40, value);
        }

        public bool HasSpaceBetweenColumns
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x80);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x80, value);
        }

        public bool HasRightToLeftColumns
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x100);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x100, value);
        }

        public bool HasFromWordArt
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x200);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x200, value);
        }

        public bool HasAnchor
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x400);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x400, value);
        }

        public bool HasAnchorCenter
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x800);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x800, value);
        }

        public bool HasForceAntiAlias
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x1000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x1000, value);
        }

        public bool HasCompatibleLineSpacing
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x2000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x2000, value);
        }

        public int NumberOfColumns
        {
            get => 
                this.numberOfColumns;
            set
            {
                ValueChecker.CheckValue(value, 1, 0x10, "NumberOfColumns");
                this.numberOfColumns = value;
            }
        }

        public int Rotation
        {
            get => 
                this.rotation;
            set => 
                this.rotation = value;
        }

        public float SpaceBetweenColumns
        {
            get => 
                this.spaceBetweenColumns;
            set
            {
                DrawingValueChecker.CheckPositiveCoordinate32F(this.spaceBetweenColumns, "SpaceBetweenColumns");
                this.spaceBetweenColumns = value;
            }
        }
    }
}

