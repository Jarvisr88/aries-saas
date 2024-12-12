namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Layout;
    using DevExpress.Office.PInvoke;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using DevExpress.Utils.Drawing;
    using DevExpress.Utils.Text;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public abstract class GdiPainter : GdiPlusPainter
    {
        private readonly DocumentLayoutUnitConverter unitConverter;
        private const int initialBufferItemCount = 0x40;
        private int glyphsBufferSize;
        private IntPtr glyphsBuffer;
        private int characterWidthsBufferSize;
        private IntPtr characterWidthsBuffer;
        private const int RGN_AND = 1;
        private const int RGN_OR = 2;
        private const int RGN_XOR = 3;
        private const int RGN_DIFF = 4;
        private const int RGN_COPY = 5;

        public GdiPainter(IGraphicsCache cache, DocumentLayoutUnitConverter unitConverter) : base(cache, unitConverter)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
            this.GetCharacterWidthsBuffer(0x40);
            this.GetGlyphsBuffer(0x40);
        }

        [SecuritySafeCritical]
        private void APIExcludeClip(Rectangle bounds)
        {
            if ((bounds.Width >= 1) && (bounds.Height >= 1))
            {
                IntPtr hdc = base.Graphics.GetHdc();
                try
                {
                    bounds = ExcludeClipFromHDC(bounds, hdc);
                }
                finally
                {
                    base.Graphics.ReleaseHdc(hdc);
                }
            }
        }

        [SecuritySafeCritical]
        private void APISetClip(Rectangle bounds)
        {
            IntPtr hdc = base.Graphics.GetHdc();
            try
            {
                bounds = SetClipToHDC(bounds, hdc);
            }
            finally
            {
                base.Graphics.ReleaseHdc(hdc);
            }
        }

        private PInvokeSafeNativeMethods.TextAlignment CalculateHorizontalTextAlign(StringFormat stringFormat)
        {
            switch (stringFormat.Alignment)
            {
                case StringAlignment.Center:
                    return PInvokeSafeNativeMethods.TextAlignment.TA_CENTER;

                case StringAlignment.Far:
                    return PInvokeSafeNativeMethods.TextAlignment.TA_RIGHT;
            }
            return PInvokeSafeNativeMethods.TextAlignment.TA_LEFT;
        }

        private PInvokeSafeNativeMethods.TextAlignment CalculateVerticalTextAlign(StringFormat stringFormat)
        {
            switch (stringFormat.LineAlignment)
            {
                case StringAlignment.Center:
                    return PInvokeSafeNativeMethods.TextAlignment.TA_LEFT;

                case StringAlignment.Far:
                    return PInvokeSafeNativeMethods.TextAlignment.TA_BOTTOM;
            }
            return PInvokeSafeNativeMethods.TextAlignment.TA_LEFT;
        }

        [DllImport("GDI32.dll")]
        private static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        protected internal virtual void DefaultDrawStringImplementation(IntPtr hdc, string text, FontInfo fontInfo, Rectangle bounds, StringFormat stringFormat, IWordBreakProvider wordBreakProvider)
        {
            this.DrawNonCachedString(hdc, fontInfo, text, bounds, stringFormat, wordBreakProvider);
        }

        public override void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        [SecuritySafeCritical]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (this.glyphsBuffer != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(this.glyphsBuffer);
                    this.glyphsBuffer = IntPtr.Zero;
                }
                if (this.characterWidthsBuffer != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(this.characterWidthsBuffer);
                    this.characterWidthsBuffer = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private void DrawMultilineString(IntPtr hdc, string text, Rectangle bounds, StringFormat stringFormat, FontInfo fontInfo, IWordBreakProvider wordBreakProvider)
        {
            if (text.Length != 0)
            {
                GdiPlusFontInfo gdiFontInfo = (GdiPlusFontInfo) fontInfo;
                if (gdiFontInfo != null)
                {
                    this.GetTextOutDraw(hdc, text, bounds, stringFormat, wordBreakProvider, gdiFontInfo).DrawString(hdc);
                }
            }
        }

        protected internal void DrawNonCachedString(IntPtr hdc, FontInfo fontInfo, string text, Rectangle bounds, StringFormat stringFormat, IWordBreakProvider wordBreakProvider)
        {
            if (this.IsMultiLine(stringFormat))
            {
                this.DrawMultilineString(hdc, text, bounds, stringFormat, fontInfo, wordBreakProvider);
            }
            else if (RotatedTextUtils.IsRotated(fontInfo.TextRotation))
            {
                this.DrawRotatedString(hdc, text, bounds, stringFormat, fontInfo.TextRotation);
            }
            else if ((stringFormat.Alignment == StringAlignment.Near) && (stringFormat.LineAlignment == StringAlignment.Near))
            {
                this.DrawNonCachedStringCore(hdc, text, bounds, bounds.Location);
            }
            else if (stringFormat.LineAlignment == StringAlignment.Center)
            {
                this.DrawNonCachedStringVerticalCentered(hdc, text, bounds, stringFormat);
            }
            else
            {
                this.DrawNonCachedStringAligned(hdc, text, bounds, stringFormat);
            }
        }

        private void DrawNonCachedStringAligned(IntPtr hdc, string text, Rectangle bounds, StringFormat stringFormat)
        {
            Point position = new Point(this.GetAlignedTextLeft(bounds, stringFormat), this.GetAlignedTextTop(bounds, stringFormat));
            this.DrawStringAligned(hdc, text, bounds, stringFormat, position);
        }

        private void DrawNonCachedStringCore(IntPtr hdc, string text, Rectangle bounds, Point position)
        {
            Win32.RECT clip = Win32.RECT.FromRectangle(bounds);
            Win32.ExtTextOut(hdc, position.X, position.Y, Win32.EtoFlags.ETO_NONE, ref clip, text, text.Length, null);
        }

        private void DrawNonCachedStringVerticalCentered(IntPtr hdc, string text, Rectangle bounds, StringFormat stringFormat)
        {
            Size textSize = this.GetTextSize(hdc, text);
            Point position = new Point(this.GetAlignedTextLeft(bounds, stringFormat), bounds.Top + ((bounds.Height - textSize.Height) / 2));
            this.DrawStringAligned(hdc, text, bounds, stringFormat, position);
        }

        private void DrawRotatedString(IntPtr hdc, string text, Rectangle bounds, StringFormat stringFormat, int rotation)
        {
            Size textSize = this.GetTextSize(hdc, text);
            Point position = RotatedTextUtils.GetRotatedTextPosition(bounds, textSize, rotation, stringFormat);
            this.DrawStringAligned(hdc, text, bounds, stringFormat, position);
        }

        public override void DrawSpacesString(string text, FontInfo fontInfo, Rectangle bounds)
        {
            if (base.HasTransform)
            {
                base.DrawSpacesString(text, fontInfo, bounds);
            }
            else
            {
                this.DrawStringImpl(text, fontInfo, base.TextForeColor, bounds, base.StringFormat, null, new DrawStringActualImplementationDelegate(this.SpacesPlaceholdersDrawStringImplementation));
            }
        }

        public override void DrawString(string text, FontInfo fontInfo, Rectangle bounds)
        {
            this.DrawString(text, fontInfo, bounds, base.StringFormat, null);
        }

        public override void DrawString(string text, FontInfo fontInfo, Rectangle bounds, StringFormat stringFormat, IWordBreakProvider wordBreakProvider)
        {
            if (base.HasTransform)
            {
                base.DrawString(text, fontInfo, bounds, stringFormat, wordBreakProvider);
            }
            else
            {
                this.DrawStringImpl(text, fontInfo, base.TextForeColor, bounds, stringFormat, wordBreakProvider, new DrawStringActualImplementationDelegate(this.DefaultDrawStringImplementation));
            }
        }

        private void DrawStringAligned(IntPtr hdc, string text, Rectangle bounds, StringFormat stringFormat, Point position)
        {
            int num = this.SetTextAlign(hdc, stringFormat);
            try
            {
                this.DrawNonCachedStringCore(hdc, text, bounds, position);
            }
            finally
            {
                Win32.SetTextAlign(hdc, num);
            }
        }

        private unsafe void DrawStringImpl(string text, FontInfo fontInfo, Color foreColor, Rectangle bounds, StringFormat stringFormat, IWordBreakProvider wordBreakProvider, DrawStringActualImplementationDelegate impl)
        {
            bounds = this.CorrectTextDrawingBounds(fontInfo, bounds);
            bounds.X = this.unitConverter.SnapToPixels(bounds.X, base.Graphics.DpiX);
            GdiPlusFontInfo info = (GdiPlusFontInfo) fontInfo;
            Rectangle* rectanglePtr1 = &bounds;
            rectanglePtr1.Y -= info.GdiOffset;
            IntPtr hdc = base.Graphics.GetHdc();
            try
            {
                Win32.RECT lprc = Win32.RECT.FromRectangle(bounds);
                if (Win32.RectVisible(hdc, ref lprc))
                {
                    Win32.SelectObject(hdc, info.GdiFontHandle);
                    Win32.SetTextColor(hdc, foreColor);
                    Win32.SetBkMode(hdc, Win32.BkMode.TRANSPARENT);
                    impl(hdc, text, fontInfo, bounds, stringFormat, wordBreakProvider);
                }
            }
            finally
            {
                base.Graphics.ReleaseHdc(hdc);
            }
        }

        public override void ExcludeCellBounds(Rectangle rect, Rectangle rowBounds)
        {
            base.ExcludeCellBounds(rect, rowBounds);
            this.APIExcludeClip(rect);
        }

        [SecuritySafeCritical]
        internal static Rectangle ExcludeClipFromHDC(Rectangle bounds, IntPtr hdc)
        {
            Win32.POINT[] lpPoints = new Win32.POINT[] { new Win32.POINT(bounds.Left, bounds.Top), new Win32.POINT(bounds.Right, bounds.Bottom) };
            LPtoDP(hdc, lpPoints, lpPoints.Length);
            ExcludeClipRect(hdc, lpPoints[0].X, lpPoints[0].Y, lpPoints[1].X, lpPoints[1].Y);
            return bounds;
        }

        [DllImport("GDI32.dll")]
        private static extern int ExcludeClipRect(IntPtr hdc, int left, int top, int right, int bottom);
        [DllImport("GDI32.dll")]
        private static extern int ExtSelectClipRgn(IntPtr hdc, IntPtr hrgn, int mode);
        ~GdiPainter()
        {
            this.Dispose(false);
        }

        [SecuritySafeCritical]
        protected internal GdiStringViewInfo GenerateStringViewInfo(IntPtr hdc, string text)
        {
            Win32.GCP_RESULTS lpResults = new Win32.GCP_RESULTS {
                lStructSize = Marshal.SizeOf(typeof(Win32.GCP_RESULTS)),
                lpOutString = IntPtr.Zero,
                lpOrder = IntPtr.Zero,
                lpDx = this.GetCharacterWidthsBuffer(text.Length),
                lpCaretPos = IntPtr.Zero,
                lpClass = IntPtr.Zero,
                lpGlyphs = this.GetGlyphsBuffer(text.Length),
                nGlyphs = text.Length
            };
            if (text.Length > 0)
            {
                Marshal.WriteInt16(lpResults.lpGlyphs, (short) 0);
            }
            if ((Win32.GetCharacterPlacement(hdc, text, text.Length, 0, ref lpResults, Win32.GcpFlags.GCP_LIGATE | Win32.GcpFlags.GCP_USEKERNING) == 0) && (text.Length > 0))
            {
                lpResults.lpDx = IntPtr.Zero;
                lpResults.lpGlyphs = IntPtr.Zero;
                int num = this.MeasureWithGetCharacterPlacementSlow(hdc, text, ref lpResults);
            }
            return new GdiStringViewInfo { 
                Glyphs = lpResults.lpGlyphs,
                GlyphCount = lpResults.nGlyphs,
                CharacterWidths = lpResults.lpDx
            };
        }

        private int GetAlignedTextLeft(Rectangle bounds, StringFormat stringFormat)
        {
            switch (stringFormat.Alignment)
            {
                case StringAlignment.Center:
                    return ((bounds.Left + bounds.Right) / 2);

                case StringAlignment.Far:
                    return bounds.Right;
            }
            return bounds.Left;
        }

        private int GetAlignedTextTop(Rectangle bounds, StringFormat stringFormat)
        {
            switch (stringFormat.LineAlignment)
            {
                case StringAlignment.Center:
                    throw new InvalidOperationException("Text cannot be centrally aligned without text height info");

                case StringAlignment.Far:
                    return bounds.Bottom;
            }
            return bounds.Top;
        }

        [SecuritySafeCritical]
        protected internal IntPtr GetCharacterWidthsBuffer(int itemsCount)
        {
            int cb = 4 * itemsCount;
            if (cb > this.characterWidthsBufferSize)
            {
                this.characterWidthsBuffer = Marshal.ReAllocCoTaskMem(this.characterWidthsBuffer, cb);
                this.characterWidthsBufferSize = cb;
            }
            return this.characterWidthsBuffer;
        }

        [SecuritySafeCritical]
        protected internal IntPtr GetGlyphsBuffer(int itemsCount)
        {
            int cb = 2 * itemsCount;
            if (cb > this.glyphsBufferSize)
            {
                this.glyphsBuffer = Marshal.ReAllocCoTaskMem(this.glyphsBuffer, cb);
                this.glyphsBufferSize = cb;
            }
            return this.glyphsBuffer;
        }

        protected internal virtual OfficeTextOutDraw GetTextOutDraw(IntPtr hdc, string text, Rectangle bounds, StringFormat stringFormat, IWordBreakProvider wordBreakProvider, GdiPlusFontInfo gdiFontInfo) => 
            new OfficeTextOutDraw(gdiFontInfo, hdc, text, bounds, Rectangle.Empty, stringFormat, null, wordBreakProvider);

        [SecuritySafeCritical]
        private Size GetTextSize(IntPtr hdc, string text)
        {
            Win32.GCP_RESULTS lpResults = new Win32.GCP_RESULTS {
                lStructSize = Marshal.SizeOf(typeof(Win32.GCP_RESULTS)),
                lpDx = this.GetCharacterWidthsBuffer(text.Length),
                lpGlyphs = this.GetGlyphsBuffer(text.Length),
                nGlyphs = text.Length
            };
            if (text.Length > 0)
            {
                Marshal.WriteInt16(lpResults.lpGlyphs, (short) 0);
            }
            int num = Win32.GetCharacterPlacement(hdc, text, text.Length, 0x7fffffff, ref lpResults, Win32.GcpFlags.GCP_LIGATE | Win32.GcpFlags.GCP_USEKERNING);
            if ((num == 0) && (text.Length > 0))
            {
                lpResults.lpDx = IntPtr.Zero;
                lpResults.lpGlyphs = IntPtr.Zero;
                num = this.MeasureWithGetCharacterPlacementSlow(hdc, text, ref lpResults);
            }
            return new Size(num & 0xffff, (num & 0xffff0000UL) >> 0x10);
        }

        private bool IsMultiLine(StringFormat stringFormat) => 
            (stringFormat.FormatFlags & StringFormatFlags.NoWrap) == 0;

        [DllImport("gdi32.dll")]
        private static extern bool LPtoDP(IntPtr hdc, [In, Out] Win32.POINT[] lpPoints, int nCount);
        [SecuritySafeCritical]
        private int MeasureWithGetCharacterPlacementSlow(IntPtr hdc, string text, ref Win32.GCP_RESULTS gcpResults)
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
                int num4 = Win32.GetCharacterPlacement(hdc, text, text.Length, 0, ref gcpResults, Win32.GcpFlags.GCP_LIGATE | Win32.GcpFlags.GCP_USEKERNING);
                if (num4 != 0)
                {
                    if ((num4 & 0xffff) == 0)
                    {
                        num4 |= ((num4 & 0xffff0000UL) >> 0x10) * text.Length;
                    }
                    return num4;
                }
                num3++;
                num2 += num;
            }
            return 0;
        }

        protected override void SetClipBounds(RectangleF bounds)
        {
            base.SetClipBounds(bounds);
            this.APISetClip(Rectangle.Round(bounds));
        }

        [SecuritySafeCritical]
        internal static Rectangle SetClipToHDC(Rectangle clipBounds, IntPtr hdc)
        {
            Win32.POINT[] lpPoints = new Win32.POINT[] { new Win32.POINT(clipBounds.Left, clipBounds.Top), new Win32.POINT(clipBounds.Right, clipBounds.Bottom) };
            LPtoDP(hdc, lpPoints, lpPoints.Length);
            IntPtr hrgn = CreateRectRgn(lpPoints[0].X, lpPoints[0].Y, lpPoints[1].X, lpPoints[1].Y);
            try
            {
                ExtSelectClipRgn(hdc, hrgn, 5);
            }
            finally
            {
                Win32.DeleteObject(hrgn);
            }
            return clipBounds;
        }

        private int SetTextAlign(IntPtr hdc, StringFormat stringFormat) => 
            Win32.SetTextAlign(hdc, (int) (this.CalculateHorizontalTextAlign(stringFormat) | this.CalculateVerticalTextAlign(stringFormat)));

        [SecuritySafeCritical]
        protected internal void SpacesPlaceholdersDrawStringImplementation(IntPtr hdc, string text, FontInfo fontInfo, Rectangle bounds, StringFormat stringFormat, IWordBreakProvider wordBreakProvider)
        {
            int length = text.Length;
            if (length > 0)
            {
                GdiStringViewInfo info = this.GenerateStringViewInfo(hdc, text);
                Win32.RECT clip = Win32.RECT.FromRectangle(bounds);
                int num2 = bounds.Width / length;
                int num3 = bounds.Width - (num2 * length);
                for (int i = 0; i < length; i++)
                {
                    int val = num2 + ((num3 > 0) ? 1 : 0);
                    Marshal.WriteInt32(info.CharacterWidths, i * 4, val);
                }
                Win32.ExtTextOut(hdc, bounds.X, bounds.Y, Win32.EtoFlags.ETO_GLYPH_INDEX, ref clip, info.Glyphs, info.GlyphCount, info.CharacterWidths);
            }
        }

        protected DocumentLayoutUnitConverter LayoutUnitConverter =>
            this.unitConverter;

        private protected delegate void DrawStringActualImplementationDelegate(IntPtr hdc, string text, FontInfo fontInfo, Rectangle bounds, StringFormat stringFormat, IWordBreakProvider wordBreakProvider);
    }
}

