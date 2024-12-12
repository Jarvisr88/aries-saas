namespace ActiproSoftware.Drawing
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public interface IDoubleBufferCanvas : IDisposable
    {
        void Copy(Rectangle sourceRect, Point destLocation);
        void Flush();
        void Invert(Rectangle bounds);
        void PrepareGraphics(PaintEventArgs e);
        void Reset();

        System.Drawing.Graphics Graphics { get; }
    }
}

