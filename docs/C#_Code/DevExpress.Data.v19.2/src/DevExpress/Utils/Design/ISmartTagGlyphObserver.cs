namespace DevExpress.Utils.Design
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public interface ISmartTagGlyphObserver
    {
        void OnComponentSmartTagChanged(Control owner, Rectangle glyphBounds);
    }
}

