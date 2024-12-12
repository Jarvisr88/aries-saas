namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class VmlCoordUnit : ISupportsCopyFrom<VmlCoordUnit>
    {
        public VmlCoordUnit()
        {
        }

        public VmlCoordUnit(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void CopyFrom(VmlCoordUnit source)
        {
            Guard.ArgumentNotNull(source, "source");
            this.X = source.X;
            this.Y = source.Y;
        }

        public int X { get; set; }

        public int Y { get; set; }
    }
}

