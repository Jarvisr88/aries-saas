namespace DevExpress.XtraPrinting.Native.MarkupText
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils.Text;
    using DevExpress.Utils.Text.Internal;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class MarkupTextPainter : StringPainterBase
    {
        private const string ImageNotFound = "DevExpress.Printing.Core.Images.ImageNotFound.svg";
        private const char ZeroWidthSpace = '​';
        private readonly SizeF imageNotFoundSize;
        private static MarkupTextPainter defaultPainter;

        public MarkupTextPainter();
        protected override Size CalcStringBlockSize(TextProcessInfoBase te, StringBlock block, string s, StringFormat format);
        private Size CalcTextSize(IMeasurer measurer, string text, Font font, StringFormat sf, float width);
        protected override Size CalcTextSizeInt(StringInfoBase info, StringCalculateArgsBase e);
        public PrintingStringInfo Calculate(MarkupTextBrick markupTextBrick);
        public PrintingStringInfo Calculate(PrintingStringCalculateArgs e);
        protected override StringInfoBase CreateAndInitializeStringInfo(StringCalculateArgsBase e);
        protected override TextProcessInfoBase CreateTextProcessInfo(StringCalculateArgsBase e);
        public void DrawContent(PrintingStringInfo info, IGraphics gr, RectangleF clientRectangle, Action<IPdfGraphics, string, Rectangle> createPdfNavigation);
        private void DrawStringBlock(IGraphics gr, object context, StringFormat format, StringBlock sb, Rectangle rect, Color color);
        private static Rectangle GetActualInnerBounds(MarkupTextBrick markupTextBrick);
        private Color GetColor(Color firstColor, Color secondColor);
        internal SizeF GetContentSize(string text, int maxWidth, BrickStyle style, IMeasurer measurer, ImageItemCollection imageResources);
        private Font GetFont(Font font, StringFontSettings settings, BrickStyle brickStyle);
        private Font GetFontCore(BrickStyle brickStyle, Font baseFont, string fontName, float size, FontStyle style);
        private static Tuple<int, int, int> GetFontMetrics(Font font);
        protected override Size GetImageBlockSize(object context, StringBlock imageBlock);
        protected ImageSource GetImageSource(object context, string id);
        private Point GetLineBottomRight(PrintingStringInfo info, int startIndex, out int count);
        internal string GetSimpleContentString(PrintingStringInfo info);
        private List<XlRichTextRun> GetSimpleXlRichTextRun(PrintingStringInfo info);
        internal float GetSplitValue(PrintingStringInfo info, float brickTop, float pageBottom);
        private static XlFont GetXlFont(StringBlock block);
        private static XlFont GetXlFontFromBrick(PrintingStringInfo info);
        internal List<XlRichTextRun> GetXlRichTextRuns(PrintingStringInfo info);
        internal bool IsSimpleString(string text);
        private void SetFont(StringBlock block, Font font);
        public override void SetModifierFontInfo(StringCalculateArgsBase e, Font font, StringBlockTextModifierInfo modifier);
        protected override void UpdateBlockFont(StringCalculateArgsBase e, StringBlock block, Font initialFont);

        public static MarkupTextPainter Default { get; }
    }
}

