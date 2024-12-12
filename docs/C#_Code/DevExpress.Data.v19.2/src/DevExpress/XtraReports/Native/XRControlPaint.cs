namespace DevExpress.XtraReports.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public static class XRControlPaint
    {
        private const int DCX_WINDOW = 1;
        private const int DCX_LOCKWINDOWUPDATE = 0x400;
        private const int DCX_CACHE = 2;
        private static HandleRef NullHandleRef;
        private static Brush controlBrush;
        private const int msgWM_PRINT = 0x317;

        static XRControlPaint();
        private static Rectangle CalcTriangleRect(Rectangle baseRect, bool center);
        [DllImport("gdi32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        private static extern IntPtr CreatePen(int nStyle, int nWidth, int crColor);
        [DllImport("gdi32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        private static extern IntPtr CreateSolidBrush(int crColor);
        [DllImport("gdi32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        private static extern bool DeleteObject(HandleRef hObject);
        public static void DrawBorder(Graphics gr, RectangleF rect, Brush brush, float borderWidth, BorderSide sides);
        public static void DrawGrid(Graphics gr, Rectangle bounds, SizeF pixelsBetweenDots, Color foreColor);
        private static void DrawHorizDashLine(Graphics gr, XRControlPaint.DashStyle style, float x, float y, float width, float height);
        [DllImport("gdi32.dll", EntryPoint="Rectangle", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        private static extern bool DrawRectangle(HandleRef hdc, int left, int top, int right, int bottom);
        public static void DrawReversibleFrame(Rectangle rect, Color color, FrameStyle style, Control control);
        public static void DrawTriangleDown(Graphics gr, Rectangle rect, bool center);
        private static void DrawVertDashLine(Graphics gr, XRControlPaint.DashStyle style, float x, float y, float width, float height);
        private static void DrawWinControl(Control control, Graphics gr, WinControlDrawMethod_Utils drawMethod);
        private static void DrawWinControlWMPaint(Control control, IntPtr hdc);
        private static void DrawWinControlWMPaintRecursive(Control control, IntPtr hdc, Rectangle rect);
        private static void DrawWinControlWMPrint(Control control, IntPtr hdc);
        private static void DrawWinControlWMPrintRecursive(Control control, IntPtr hdc, Rectangle rect);
        private static bool EnableAllPaintingInWmPaint(Control control, bool enable);
        private static bool EnableControlStyle(Control control, bool enable, int style);
        private static bool EnableDoubleBuffer(Control control, bool enable);
        public static void FillReversibleRectangle(Rectangle rectangle, Color backColor, Control control);
        private static IList GetChildControlsWithCorrectZOrder(Control control);
        private static int GetColorRop(Color color, int darkROP, int lightROP);
        public static Image GetControlImage(Control ctl, WinControlDrawMethod_Utils drawMethod, WinControlImageType_Utils imageType);
        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        private static extern IntPtr GetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags);
        private static PointF GetFirstDrawnPoint(RectangleF clipBounds, Rectangle controlBounds, SizeF pixelsBetweenDots);
        [DllImport("gdi32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        private static extern IntPtr GetStockObject(int nIndex);
        private static object InvokeMethod(Control control, string method, object[] args);
        [DllImport("gdi32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        private static extern bool PatBlt(HandleRef hdc, int left, int top, int width, int height, int rop);
        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        private static extern int ReleaseDC(HandleRef hWnd, HandleRef hDC);
        [DllImport("gdi32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        private static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);
        [DllImport("gdi32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        private static extern int SetBkColor(HandleRef hDC, int clr);
        [DllImport("gdi32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        private static extern int SetROP2(HandleRef hDC, int nDrawMode);

        private class DashStyle : IDisposable
        {
            private System.Drawing.Brush brush;
            private float dash;
            private float spell;

            public DashStyle(System.Drawing.Brush brush, float dash, float spell);
            public void Dispose();

            public System.Drawing.Brush Brush { get; }

            public float Dash { get; }

            public float Spell { get; }
        }

        [Flags]
        private enum msgWM_PRINTOptions
        {
            public const XRControlPaint.msgWM_PRINTOptions PRF_CHECKVISIBLE = XRControlPaint.msgWM_PRINTOptions.PRF_CHECKVISIBLE;,
            public const XRControlPaint.msgWM_PRINTOptions PRF_NONCLIENT = XRControlPaint.msgWM_PRINTOptions.PRF_NONCLIENT;,
            public const XRControlPaint.msgWM_PRINTOptions PRF_CLIENT = XRControlPaint.msgWM_PRINTOptions.PRF_CLIENT;,
            public const XRControlPaint.msgWM_PRINTOptions PRF_ERASEBKGND = XRControlPaint.msgWM_PRINTOptions.PRF_ERASEBKGND;,
            public const XRControlPaint.msgWM_PRINTOptions PRF_CHILDREN = XRControlPaint.msgWM_PRINTOptions.PRF_CHILDREN;,
            public const XRControlPaint.msgWM_PRINTOptions PRF_OWNED = XRControlPaint.msgWM_PRINTOptions.PRF_OWNED;
        }
    }
}

