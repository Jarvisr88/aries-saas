namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TextRendererContext
    {
        public TextRendererContext(DrawingTextParagraphCollection paragraphs, DrawingTextBodyProperties bodyProperties, RectangleF textRectangle, DevExpress.Office.Drawing.ShapeStyle shapeStyle, DrawingTextListStyles listStyles)
        {
            this.Paragraphs = paragraphs;
            this.BodyProperties = bodyProperties;
            this.TextRectangle = textRectangle;
            this.ShapeStyle = shapeStyle;
            this.ListStyles = listStyles;
            this.BlackAndWhitePrintMode = false;
            this.ShouldApplyEffects = true;
        }

        public DrawingTextParagraphCollection Paragraphs { get; private set; }
        public DrawingTextBodyProperties BodyProperties { get; private set; }
        public RectangleF TextRectangle { get; private set; }
        public DevExpress.Office.Drawing.ShapeStyle ShapeStyle { get; private set; }
        public DrawingTextListStyles ListStyles { get; private set; }
        public bool BlackAndWhitePrintMode { get; set; }
        public bool ShouldApplyEffects { get; set; }
    }
}

