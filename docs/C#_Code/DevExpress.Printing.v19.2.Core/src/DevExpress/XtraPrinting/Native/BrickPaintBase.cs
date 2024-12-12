namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Design;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class BrickPaintBase
    {
        protected DevExpress.XtraPrinting.BrickStyle style;
        private GdiHashtable gdi;
        private readonly bool patchTransparentBackground;
        private Color defaultCheckBoxBackColor;
        private Color defaultCheckBoxMarkColor;
        private Color defaultCheckBoxBorderColor;

        public BrickPaintBase(GdiHashtable gdi, bool patchTransparentBackground);
        protected void AdjustClientRect(ref RectangleF rect);
        private static RectangleF CalculateInnerRect(IPixelAdjuster adjuster, RectangleF rect, float borderWidth);
        private static RectangleF CalculateInnerRect2(IGraphics gr, RectangleF rect, float borderWidth);
        public void DrawBorder(IGraphics gr, RectangleF rect);
        public void DrawBorder(IGraphics gr, RectangleF rect, BorderSide sides);
        [Obsolete("Use the DrawBorder(IGraphics, RectangleF) method instead")]
        public void DrawBorder(IGraphics gr, RectangleF rect, GdiHashtable gdi, float toDpi);
        protected virtual void DrawBorder(IGraphics gr, RectangleF rect, Brush brush, BorderSide sides);
        public virtual void DrawCheck(CheckState state, IGraphics gr, RectangleF rect);
        [Obsolete("Use the DrawRect(IGraphics, RectangleF, float) and the DrawCheck(CheckState, IGraphics, RectangleF) method instead")]
        public void DrawCheck(CheckState state, IGraphics gr, RectangleF rect, GdiHashtable gdi);
        [Obsolete("Use the DrawRect(IGraphics, RectangleF, float) and the DrawCheck(CheckState, IGraphics, RectangleF) method instead")]
        public virtual void DrawCheck(CheckState state, IGraphics gr, RectangleF rect, SizeF sizeInPixels, GdiHashtable gdi);
        [Obsolete("Use the DrawRect(IGraphics, RectangleF, float) and the DrawCheck(CheckState, IGraphics, RectangleF) method instead")]
        public virtual void DrawCheck(CheckState state, IGraphics gr, RectangleF rect, SizeF sizeInPixels, GdiHashtable gdi, bool shouldAlignToBottom);
        [Obsolete("Use the DrawRect(IGraphics, RectangleF, float) and the DrawCheck(CheckState, IGraphics, RectangleF) method instead")]
        public virtual void DrawCheck(CheckState state, IGraphics gr, RectangleF rect, SizeF sizeInPixels, GdiHashtable gdi, bool shouldAlignToBottom, float toDpi);
        private void DrawCheckMark(IGraphics gr, RectangleF checkBoxRect, SizeF size, Color color);
        private void DrawCheckUndefinedMark(IGraphics gr, RectangleF checkBoxRect, SizeF size, Color color);
        public virtual void DrawGlyph(GlyphStyle glyphStyle, CheckState state, CheckBoxGlyphCollection customGlyphs, IGraphics gr, RectangleF rect);
        private void DrawImage(IGraphics gr, Image image, RectangleF imageRect, RectangleF clipRect);
        public void DrawImage(IGraphics gr, Image image, RectangleF boundsRect, SizeF imageSize, ImageSizeMode sizeMode);
        [Obsolete("Use the DrawImage(IGraphics, Image, RectangleF, SizeF, ImageSizeMode) method instead")]
        public void DrawImage(IGraphics gr, Image image, RectangleF boundsRect, SizeF imageSize, GdiHashtable gdi, ImageSizeMode sizeMode);
        public void DrawImage(IGraphics gr, Image image, RectangleF boundsRect, SizeF imageSize, PointF point, ImageSizeMode sizeMode);
        [Obsolete("Use the DrawImage(IGraphics, Image, RectangleF, SizeF, PointF, ImageSizeMode) method instead")]
        public void DrawImage(IGraphics gr, Image image, RectangleF boundsRect, SizeF imageSize, PointF point, GdiHashtable gdi, ImageSizeMode sizeMode);
        public virtual void DrawRadioButton(CheckState state, IGraphics gr, RectangleF rect);
        private void DrawRadioButtonMark(IGraphics gr, RectangleF rect);
        public void DrawRect(IGraphics gr, RectangleF rect);
        [Obsolete("Use the DrawRect(IGraphics, RectangleF) method instead")]
        public void DrawRect(IGraphics gr, RectangleF rect, GdiHashtable gdi);
        [Obsolete("Use the DrawRect(IGraphics, RectangleF) method instead")]
        public void DrawRect(IGraphics gr, RectangleF rect, GdiHashtable gdi, float toDpi);
        public void DrawString(string s, IGraphics gr, RectangleF rect);
        [Obsolete("Use the DrawString(string, IGraphics, RectangleF) method instead")]
        public void DrawString(string s, IGraphics gr, RectangleF rect, GdiHashtable gdi);
        private void DrawSvgGlyph(string svgName, IGraphics gr, RectangleF rect);
        public void DrawToggleSwitch(bool isOn, IGraphics gr, object[] images, RectangleF rect);
        [Obsolete("Use the DrawToggleSwitch(bool, IGraphics, object[], RectangleF) method instead")]
        public void DrawToggleSwitch(bool isOn, IGraphics gr, object[] images, RectangleF rect, GdiHashtable gdi);
        public void DrawTrackBar(IGraphics gr, RectangleF rect, int position, int min, int max);
        [Obsolete("Use the DrawTrackBar(IGraphics, RectangleF, int, int, int) method instead")]
        public void DrawTrackBar(IGraphics gr, RectangleF rect, GdiHashtable gdi, int position, int min, int max);
        public void ExecUsingStyle(Action action, DevExpress.XtraPrinting.BrickStyle style);
        public void FillRect(IGraphics gr, RectangleF rect);
        [Obsolete("Use the FillRect(IGraphics, RectangleF) method instead")]
        public void FillRect(IGraphics gr, RectangleF rect, GdiHashtable gdi);
        public SolidBrush GetBrush(Color color);
        public virtual RectangleF GetClientRect(RectangleF rect, bool rtlLayout = false);
        public Pen GetPen(Color color, float width);
        private static float SnapValue(float value, float sourceDpi, float snapDpi);

        public DevExpress.XtraPrinting.BrickStyle BrickStyle { get; set; }

        private class SolidColorSvgPalette : ISvgPaletteProvider
        {
            public SolidColorSvgPalette(System.Drawing.Color color);
            ISvgPaletteProvider ISvgPaletteProvider.Clone();
            bool ISvgPaletteProvider.Equals(ISvgPaletteProvider obj);
            System.Drawing.Color ISvgPaletteProvider.GetColor(System.Drawing.Color defaultColor);
            System.Drawing.Color ISvgPaletteProvider.GetColor(string defaultColor);
            System.Drawing.Color ISvgPaletteProvider.GetColorByStyleName(string style, string defaultColor, object tag);
            int ISvgPaletteProvider.GetHashCode();

            public System.Drawing.Color Color { get; set; }

            double ISvgPaletteProvider.Opacity { get; set; }
        }
    }
}

