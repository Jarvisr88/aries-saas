namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Drawing;

    public class GdiHashtable : IDisposable
    {
        private Hashtable brushHT;
        private Hashtable penHT;
        private Hashtable fontHT;

        public GdiHashtable();
        public virtual void Dispose();
        public SolidBrush GetBrush(Color color);
        public Font GetFont(Font baseFont, string fontName, float size, FontStyle style);
        public Pen GetPen(Color color, float width);
    }
}

