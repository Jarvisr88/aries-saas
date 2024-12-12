namespace DevExpress.Office.Printing
{
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Layout;
    using DevExpress.Office.PInvoke;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using DevExpress.Utils.Text;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public class GdiGraphicsModifier : GraphicsModifier
    {
        private readonly DocumentLayoutUnitConverter unitConverter;
        private HdcDpiToDocuments dpiModifier;
        private HdcZoomModifier zoomModifier;
        private Graphics measureGraphics;
        private HdcDpiToDocuments measureDpiModifier;
        private DrawStringExtTextOutDelegate extTextOut;
        private DevExpress.Office.PInvoke.Win32.EtoFlags etoFlagsForTextOutput = DevExpress.Office.PInvoke.Win32.EtoFlags.ETO_CLIPPED;
        private GraphicsState previousGraphicsState;
        private GraphicsToLayoutUnitsModifier modifier;
        private const float defaultZoomFactor = 1f;
        private float zoomFactor = 1f;

        public GdiGraphicsModifier(DocumentLayoutUnitConverter unitConverter)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
            this.UseGlyphs = true;
        }

        private DevExpress.Office.PInvoke.Win32.DrawTextFlags CalculateDrawTextFlags(StringFormat format)
        {
            DevExpress.Office.PInvoke.Win32.DrawTextFlags flags = DevExpress.Office.PInvoke.Win32.DrawTextFlags.DT_NOPREFIX;
            flags = ((format.FormatFlags & StringFormatFlags.NoWrap) != 0) ? (flags | DevExpress.Office.PInvoke.Win32.DrawTextFlags.DT_SINGLELINE) : (flags | DevExpress.Office.PInvoke.Win32.DrawTextFlags.DT_WORDBREAK);
            switch (format.Alignment)
            {
                case StringAlignment.Near:
                    flags |= DevExpress.Office.PInvoke.Win32.DrawTextFlags.DT_LEFT;
                    break;

                case StringAlignment.Center:
                    flags |= DevExpress.Office.PInvoke.Win32.DrawTextFlags.DT_CENTER;
                    break;

                case StringAlignment.Far:
                    flags |= DevExpress.Office.PInvoke.Win32.DrawTextFlags.DT_RIGHT;
                    break;

                default:
                    break;
            }
            if ((flags & DevExpress.Office.PInvoke.Win32.DrawTextFlags.DT_SINGLELINE) != DevExpress.Office.PInvoke.Win32.DrawTextFlags.DT_LEFT)
            {
                switch (format.LineAlignment)
                {
                    case StringAlignment.Near:
                        flags |= DevExpress.Office.PInvoke.Win32.DrawTextFlags.DT_LEFT;
                        break;

                    case StringAlignment.Center:
                        flags |= DevExpress.Office.PInvoke.Win32.DrawTextFlags.DT_VCENTER;
                        break;

                    case StringAlignment.Far:
                        flags |= DevExpress.Office.PInvoke.Win32.DrawTextFlags.DT_BOTTOM;
                        break;

                    default:
                        break;
                }
            }
            return flags;
        }

        private int CalculateDrawTextVerticalOffset(IntPtr hdc, string text, DevExpress.Office.PInvoke.Win32.RECT rect, StringFormat format, DevExpress.Office.PInvoke.Win32.DrawTextFlags flags)
        {
            if ((format.FormatFlags & StringFormatFlags.NoWrap) != 0)
            {
                return 0;
            }
            if (format.LineAlignment == StringAlignment.Near)
            {
                return 0;
            }
            int height = rect.Height;
            flags &= ~(DevExpress.Office.PInvoke.Win32.DrawTextFlags.DT_BOTTOM | DevExpress.Office.PInvoke.Win32.DrawTextFlags.DT_VCENTER);
            int num2 = DevExpress.Office.PInvoke.Win32.DrawTextEx(hdc, text, ref rect, flags | DevExpress.Office.PInvoke.Win32.DrawTextFlags.DT_CALCRECT);
            return ((format.LineAlignment != StringAlignment.Center) ? (height - num2) : ((height - num2) / 2));
        }

        [SecuritySafeCritical]
        private ExtendedGcpResults CalculateTextHeightAndGlyphs(string text, IntPtr fontHandle)
        {
            ExtendedGcpResults results = new ExtendedGcpResults();
            DevExpress.Office.PInvoke.Win32.GCP_RESULTS lpResults = new DevExpress.Office.PInvoke.Win32.GCP_RESULTS {
                lStructSize = Marshal.SizeOf(typeof(DevExpress.Office.PInvoke.Win32.GCP_RESULTS)),
                lpOutString = IntPtr.Zero,
                lpOrder = IntPtr.Zero,
                lpDx = Marshal.AllocCoTaskMem(4 * text.Length),
                lpCaretPos = IntPtr.Zero,
                lpClass = IntPtr.Zero,
                lpGlyphs = Marshal.AllocCoTaskMem(2 * text.Length),
                nGlyphs = text.Length
            };
            if (text.Length > 0)
            {
                Marshal.WriteInt16(lpResults.lpGlyphs, (short) 0);
            }
            Graphics measureGraphics = this.measureGraphics;
            lock (measureGraphics)
            {
                IntPtr hdc = this.measureGraphics.GetHdc();
                try
                {
                    DevExpress.Office.PInvoke.Win32.SelectObject(hdc, fontHandle);
                    int num = DevExpress.Office.PInvoke.Win32.GetCharacterPlacement(hdc, text, text.Length, 0, ref lpResults, DevExpress.Office.PInvoke.Win32.GcpFlags.GCP_LIGATE | DevExpress.Office.PInvoke.Win32.GcpFlags.GCP_USEKERNING);
                    if ((num == 0) && (text.Length > 0))
                    {
                        num = this.MeasureWithGetCharacterPlacementSlow(hdc, text, ref lpResults);
                    }
                    results.TextHeight = num >> 0x10;
                }
                finally
                {
                    this.measureGraphics.ReleaseHdc(hdc);
                }
            }
            results.Results = lpResults;
            return results;
        }

        private int CalculateYOffset(Rectangle bounds, int textHeight, StringAlignment alignment)
        {
            if (textHeight <= 0)
            {
                textHeight = bounds.Height;
            }
            switch (alignment)
            {
                case StringAlignment.Center:
                    return ((bounds.Height - textHeight) / 2);

                case StringAlignment.Far:
                    return (bounds.Height - textHeight);
            }
            return 0;
        }

        protected internal virtual void CleanupTransforms()
        {
            if (this.measureDpiModifier != null)
            {
                this.measureDpiModifier.Dispose();
                this.measureDpiModifier = null;
            }
            if (this.dpiModifier != null)
            {
                this.dpiModifier.Dispose();
                this.dpiModifier = null;
            }
            if (this.zoomModifier != null)
            {
                this.zoomModifier.Dispose();
                this.zoomModifier = null;
            }
            this.zoomFactor = 1f;
        }

        public override void Dispose()
        {
            if (this.measureGraphics != null)
            {
                this.measureGraphics.Dispose();
                this.measureGraphics = null;
            }
            this.CleanupTransforms();
        }

        public override void DrawImage(Graphics gr, System.Drawing.Image image, Point position)
        {
            Size sizeInPixel = image.Size;
            RectangleF ef2 = SnapToDevicePixelsHelper.GetCorrectedBounds(gr, sizeInPixel, new Rectangle(position, GraphicsUnitConverter.Convert(sizeInPixel, GraphicsUnit.Pixel, gr.PageUnit)));
            gr.DrawImage(image, ef2.Location);
        }

        public override void DrawImage(Graphics gr, System.Drawing.Image image, RectangleF bounds)
        {
            RectangleF rect = SnapToDevicePixelsHelper.GetCorrectedBounds(gr, image.Size, bounds);
            gr.DrawImage(image, rect);
        }

        public override void DrawString(Graphics gr, string text, Font font, Brush brush, RectangleF bounds, StringFormat format)
        {
            if (this.UseGdiPlusDrawString)
            {
                gr.DrawString(text, font, brush, bounds, format);
            }
            else
            {
                RectangleF clipBounds = gr.ClipBounds;
                IntPtr hdc = gr.GetHdc();
                try
                {
                    SolidBrush brush2 = brush as SolidBrush;
                    if (brush2 != null)
                    {
                        DevExpress.Office.PInvoke.Win32.SetTextColor(hdc, brush2.Color);
                    }
                    DevExpress.Office.PInvoke.Win32.SetBkMode(hdc, DevExpress.Office.PInvoke.Win32.BkMode.TRANSPARENT);
                    IntPtr fontHandle = this.GetFontHandle(font, this.unitConverter);
                    IntPtr hGdiObj = DevExpress.Office.PInvoke.Win32.SelectObject(hdc, fontHandle);
                    GdiPainter.SetClipToHDC(Rectangle.Ceiling(clipBounds), hdc);
                    try
                    {
                        if ((format.FormatFlags & StringFormatFlags.NoWrap) == 0)
                        {
                            this.DrawStringDrawTextEx(hdc, text, DevExpress.Office.PInvoke.Win32.RECT.FromRectangle(Rectangle.Ceiling(bounds)), format);
                        }
                        else
                        {
                            RectangleF ef2 = this.unitConverter.DocumentsToLayoutUnits(bounds);
                            ef2.Intersect(clipBounds);
                            this.extTextOut(hdc, text, Rectangle.Ceiling(bounds), fontHandle, DevExpress.Office.PInvoke.Win32.RECT.FromRectangle(Rectangle.Ceiling(ef2)), format);
                        }
                    }
                    finally
                    {
                        GdiPainter.ExcludeClipFromHDC(Rectangle.Ceiling(bounds), hdc);
                        DevExpress.Office.PInvoke.Win32.SelectObject(hdc, hGdiObj);
                        DevExpress.Office.PInvoke.Win32.DeleteObject(fontHandle);
                    }
                }
                finally
                {
                    gr.ReleaseHdc(hdc);
                }
            }
        }

        private unsafe void DrawStringDrawTextEx(IntPtr hdc, string text, DevExpress.Office.PInvoke.Win32.RECT rect, StringFormat format)
        {
            DevExpress.Office.PInvoke.Win32.DrawTextFlags flags = this.CalculateDrawTextFlags(format);
            int num = this.CalculateDrawTextVerticalOffset(hdc, text, rect, format, flags);
            int* numPtr1 = &rect.Top;
            numPtr1[0] += num;
            DevExpress.Office.PInvoke.Win32.DrawTextEx(hdc, text, ref rect, flags);
        }

        [SecuritySafeCritical]
        protected virtual void DrawStringExtTextOutGlyph(IntPtr hdc, string text, Rectangle bounds, IntPtr fontHandle, DevExpress.Office.PInvoke.Win32.RECT clipRect, StringFormat format)
        {
            ExtendedGcpResults results = this.CalculateTextHeightAndGlyphs(text, fontHandle);
            this.ExtTextOut(hdc, bounds, clipRect, results.Results, format, results.TextHeight);
            Marshal.FreeCoTaskMem(results.Results.lpGlyphs);
            Marshal.FreeCoTaskMem(results.Results.lpDx);
        }

        [SecuritySafeCritical]
        private void DrawStringExtTextOutText(IntPtr hdc, string text, Rectangle bounds, IntPtr fontHandle, DevExpress.Office.PInvoke.Win32.RECT clipRect, StringFormat format)
        {
            ExtendedGcpResults results = this.CalculateTextHeightAndGlyphs(text, fontHandle);
            this.ExtTextOut(hdc, bounds, clipRect, text, format, results.TextHeight);
            Marshal.FreeCoTaskMem(results.Results.lpGlyphs);
            Marshal.FreeCoTaskMem(results.Results.lpDx);
        }

        protected void ExtTextOut(IntPtr hdc, Rectangle bounds, DevExpress.Office.PInvoke.Win32.RECT clipRect, DevExpress.Office.PInvoke.Win32.GCP_RESULTS gcpResults, StringFormat format, int textHeight)
        {
            int yOffset = this.CalculateYOffset(bounds, textHeight, format.LineAlignment);
            switch (format.Alignment)
            {
                case StringAlignment.Near:
                    this.ExtTextOutAlignLeft(hdc, bounds, clipRect, gcpResults, format, yOffset);
                    return;

                case StringAlignment.Center:
                    this.ExtTextOutAlignCenter(hdc, bounds, clipRect, gcpResults, format, yOffset);
                    return;

                case StringAlignment.Far:
                    this.ExtTextOutAlignRight(hdc, bounds, clipRect, gcpResults, format, yOffset);
                    return;
            }
        }

        private void ExtTextOut(IntPtr hdc, Rectangle bounds, DevExpress.Office.PInvoke.Win32.RECT clipRect, string text, StringFormat format, int textHeight)
        {
            int yOffset = this.CalculateYOffset(bounds, textHeight, format.LineAlignment);
            switch (format.Alignment)
            {
                case StringAlignment.Near:
                    this.ExtTextOutAlignLeft(hdc, bounds, clipRect, text, format, yOffset);
                    return;

                case StringAlignment.Center:
                    this.ExtTextOutAlignCenter(hdc, bounds, clipRect, text, format, yOffset);
                    return;

                case StringAlignment.Far:
                    this.ExtTextOutAlignRight(hdc, bounds, clipRect, text, format, yOffset);
                    return;
            }
        }

        private void ExtTextOutAlignCenter(IntPtr hdc, Rectangle bounds, DevExpress.Office.PInvoke.Win32.RECT clipRect, DevExpress.Office.PInvoke.Win32.GCP_RESULTS gcpResults, StringFormat format, int yOffset)
        {
            int num = DevExpress.Office.PInvoke.Win32.SetTextAlign(hdc, format);
            try
            {
                this.ExtTextOutCore(hdc, (bounds.Left + bounds.Right) / 2, bounds.Top + yOffset, clipRect, gcpResults);
            }
            finally
            {
                DevExpress.Office.PInvoke.Win32.SetTextAlign(hdc, num);
            }
        }

        private void ExtTextOutAlignCenter(IntPtr hdc, Rectangle bounds, DevExpress.Office.PInvoke.Win32.RECT clipRect, string text, StringFormat format, int yOffset)
        {
            int num = DevExpress.Office.PInvoke.Win32.SetTextAlign(hdc, format);
            try
            {
                this.ExtTextOutCore(hdc, (bounds.Left + bounds.Right) / 2, bounds.Top + yOffset, clipRect, text);
            }
            finally
            {
                DevExpress.Office.PInvoke.Win32.SetTextAlign(hdc, num);
            }
        }

        protected virtual void ExtTextOutAlignLeft(IntPtr hdc, Rectangle bounds, DevExpress.Office.PInvoke.Win32.RECT clipRect, DevExpress.Office.PInvoke.Win32.GCP_RESULTS gcpResults, StringFormat format, int yOffset)
        {
            this.ExtTextOutCore(hdc, bounds.Left, bounds.Top + yOffset, clipRect, gcpResults);
        }

        protected virtual void ExtTextOutAlignLeft(IntPtr hdc, Rectangle bounds, DevExpress.Office.PInvoke.Win32.RECT clipRect, string text, StringFormat format, int yOffset)
        {
            this.ExtTextOutCore(hdc, bounds.Left, bounds.Top + yOffset, clipRect, text);
        }

        private void ExtTextOutAlignRight(IntPtr hdc, Rectangle bounds, DevExpress.Office.PInvoke.Win32.RECT clipRect, DevExpress.Office.PInvoke.Win32.GCP_RESULTS gcpResults, StringFormat format, int yOffset)
        {
            int num = DevExpress.Office.PInvoke.Win32.SetTextAlign(hdc, format);
            try
            {
                this.ExtTextOutCore(hdc, bounds.Right, bounds.Top + yOffset, clipRect, gcpResults);
            }
            finally
            {
                DevExpress.Office.PInvoke.Win32.SetTextAlign(hdc, num);
            }
        }

        private void ExtTextOutAlignRight(IntPtr hdc, Rectangle bounds, DevExpress.Office.PInvoke.Win32.RECT clipRect, string text, StringFormat format, int yOffset)
        {
            int num = DevExpress.Office.PInvoke.Win32.SetTextAlign(hdc, format);
            try
            {
                this.ExtTextOutCore(hdc, bounds.Right, bounds.Top + yOffset, clipRect, text);
            }
            finally
            {
                DevExpress.Office.PInvoke.Win32.SetTextAlign(hdc, num);
            }
        }

        private void ExtTextOutCore(IntPtr hdc, int x, int y, DevExpress.Office.PInvoke.Win32.RECT clipRect, DevExpress.Office.PInvoke.Win32.GCP_RESULTS gcpResults)
        {
            DevExpress.Office.PInvoke.Win32.ExtTextOut(hdc, x, y, this.UsePdy ? (DevExpress.Office.PInvoke.Win32.EtoFlags.ETO_PDY | DevExpress.Office.PInvoke.Win32.EtoFlags.ETO_GLYPH_INDEX) : DevExpress.Office.PInvoke.Win32.EtoFlags.ETO_GLYPH_INDEX, ref clipRect, gcpResults.lpGlyphs, gcpResults.nGlyphs, gcpResults.lpDx);
        }

        private void ExtTextOutCore(IntPtr hdc, int x, int y, DevExpress.Office.PInvoke.Win32.RECT clipRect, string text)
        {
            DevExpress.Office.PInvoke.Win32.ExtTextOut(hdc, x, y, this.etoFlagsForTextOutput, ref clipRect, text, text.Length, null);
        }

        protected virtual IntPtr GetFontHandle(Font font, DocumentLayoutUnitConverter unitConverter) => 
            GdiPlusFontInfo.CreateGdiFont(font, unitConverter, true);

        [SecuritySafeCritical]
        private int MeasureWithGetCharacterPlacementSlow(IntPtr hdc, string text, ref DevExpress.Office.PInvoke.Win32.GCP_RESULTS gcpResults)
        {
            int num = Math.Max(1, (int) Math.Ceiling((double) (((double) text.Length) / 2.0)));
            int num2 = num;
            int num3 = 0;
            while (num3 < 3)
            {
                gcpResults.lpDx = Marshal.ReAllocCoTaskMem(gcpResults.lpDx, 4 * (text.Length + num2));
                gcpResults.lpGlyphs = Marshal.ReAllocCoTaskMem(gcpResults.lpGlyphs, 2 * (text.Length + num2));
                gcpResults.nGlyphs = text.Length + num2;
                if (text.Length > 0)
                {
                    Marshal.WriteInt16(gcpResults.lpGlyphs, (short) 0);
                }
                int num4 = DevExpress.Office.PInvoke.Win32.GetCharacterPlacement(hdc, text, text.Length, 0, ref gcpResults, DevExpress.Office.PInvoke.Win32.GcpFlags.GCP_LIGATE | DevExpress.Office.PInvoke.Win32.GcpFlags.GCP_USEKERNING);
                if (num4 != 0)
                {
                    return num4;
                }
                num3++;
                num2 += num;
            }
            return 0;
        }

        public override void OnGraphicsDispose()
        {
            this.CleanupTransforms();
        }

        public override void ScaleTransform(Graphics gr, float sx, float sy, MatrixOrder order)
        {
            gr.ScaleTransform(sx, sy, order);
            this.zoomModifier = new HdcZoomModifier(gr, sx);
            this.zoomFactor = sx;
        }

        public override void SetPageUnit(Graphics gr, GraphicsUnit value)
        {
            gr.PageUnit = value;
            this.dpiModifier = new HdcDpiToDocuments(gr, new Size(0x1000, 0x1000));
            this.measureGraphics ??= Graphics.FromHwnd(IntPtr.Zero);
            this.measureGraphics.PageUnit = value;
            this.measureDpiModifier = new HdcDpiToDocuments(gr, new Size(0x1000, 0x1000));
            float zoomFactor = gr.Transform.Elements[0];
            this.zoomModifier = new HdcZoomModifier(gr, zoomFactor);
            this.zoomFactor = zoomFactor;
        }

        public void SwitchToDocuments(Graphics gr)
        {
            this.zoomModifier.Dispose();
            this.modifier.Dispose();
            gr.Restore(this.previousGraphicsState);
        }

        public void SwitchToLayoutUnits(Graphics gr)
        {
            this.previousGraphicsState = gr.Save();
            this.modifier = new GraphicsToLayoutUnitsModifier(gr, this.unitConverter);
            gr.ScaleTransform(this.zoomFactor, this.zoomFactor);
            this.zoomModifier = new HdcZoomModifier(gr, this.zoomFactor);
        }

        public bool UsePdy { get; set; }

        public bool UseGlyphs
        {
            get => 
                this.extTextOut == new DrawStringExtTextOutDelegate(this.DrawStringExtTextOutGlyph);
            set
            {
                if (this.UseGlyphs != value)
                {
                    if (value)
                    {
                        this.extTextOut = new DrawStringExtTextOutDelegate(this.DrawStringExtTextOutGlyph);
                    }
                    else
                    {
                        this.extTextOut = new DrawStringExtTextOutDelegate(this.DrawStringExtTextOutText);
                    }
                }
            }
        }

        public bool UseGdiPlusDrawString { get; set; }

        public bool UseClipBoundsWithoutGlyphs
        {
            get => 
                this.etoFlagsForTextOutput == DevExpress.Office.PInvoke.Win32.EtoFlags.ETO_CLIPPED;
            set
            {
                if (this.UseClipBoundsWithoutGlyphs != value)
                {
                    if (value)
                    {
                        this.etoFlagsForTextOutput = DevExpress.Office.PInvoke.Win32.EtoFlags.ETO_CLIPPED;
                    }
                    else
                    {
                        this.etoFlagsForTextOutput = DevExpress.Office.PInvoke.Win32.EtoFlags.ETO_NONE;
                    }
                }
            }
        }

        private delegate void DrawStringExtTextOutDelegate(IntPtr hdc, string text, Rectangle bounds, IntPtr fontHandle, DevExpress.Office.PInvoke.Win32.RECT clipRect, StringFormat format);

        [StructLayout(LayoutKind.Sequential)]
        private struct ExtendedGcpResults
        {
            public DevExpress.Office.PInvoke.Win32.GCP_RESULTS Results { get; set; }
            public int TextHeight { get; set; }
        }

        private delegate void ExtTextOutAction(IntPtr hdc, Rectangle bounds, DevExpress.Office.PInvoke.Win32.RECT clipRect, StringFormat format, int textHeight);
    }
}

