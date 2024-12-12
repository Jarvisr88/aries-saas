namespace ActiproSoftware.WinUICore
{
    using ActiproSoftware.ComponentModel;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public interface IUIControl : IDisposable, IUIElement, ILogicalTreeNode
    {
        bool AddPendingGraphicsInversion(Rectangle bounds);
        bool AddPendingScrollOperation(Rectangle bounds, int xAmount, int yAmount);
        bool AddPendingScrollOperation(Rectangle bounds, Orientation orientation, int amount);
        void AddToInvalidatedRegion(Rectangle rect);
        void ResetDoubleBufferCanvas(bool recurse);

        bool Capture { get; set; }

        bool IsPaintValid { get; }

        IUIElement MouseCaptureElement { get; set; }
    }
}

