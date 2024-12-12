namespace DevExpress.Utils
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public interface IOfficeScroller : IDisposable
    {
        void Start(Control control);
        void Start(Control control, Point screenPoint);
    }
}

