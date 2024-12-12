namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;

    public class ItemInfo
    {
        public double Offset = double.NaN;

        public ItemInfo(double length, double minLength, double maxLength, ItemAlignment alignment)
        {
            this.MinLength = minLength;
            this.MaxLength = maxLength;
            this.Length = length;
            this.Alignment = alignment;
        }

        public ItemAlignment Alignment { get; private set; }

        public bool IsProcessed { get; set; }

        public double Length { get; set; }

        public double MinLength { get; set; }

        public double MaxLength { get; private set; }
    }
}

