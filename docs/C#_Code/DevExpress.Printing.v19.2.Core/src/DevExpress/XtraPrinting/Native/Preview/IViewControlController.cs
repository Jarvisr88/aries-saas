namespace DevExpress.XtraPrinting.Native.Preview
{
    using DevExpress.DocumentView;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public interface IViewControlController
    {
        event Action<Graphics> ViewControlPaint;

        IPage FindPage(RectangleF rectDoc);
        RectangleF GetPageRect(IPage page);
        void InvalidatePrintControl();
        void InvalidateViewControlRect(RectangleF rectangle);
        Point PointToClient(Point point);
        Rectangle RectangleToClient(Rectangle rectangle);
        Rectangle RectangleToScreen(Rectangle rectangle);
        void ShowBrickCenter(Brick brick, Page page);
        void StartScroller();
        void StopScroller();
        void UpdatePrintControlCommands(Action<CommandSetBase> callback);

        float Zoom { get; }

        PointF ScrollPos { get; }

        bool IsDefaultCursor { get; }

        bool IsModifierKeysPressed { get; }

        PointF ScrollValue { get; }
    }
}

