namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class MapItem
    {
        public MapItem(RectangleF bounds, int index1, int index2);

        public RectangleF Bounds { get; private set; }

        public int Index1 { get; private set; }

        public int Index2 { get; private set; }
    }
}

