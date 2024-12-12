namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class ParagraphLayoutInfo
    {
        public ParagraphLayoutInfo()
        {
            this.RunLayouts = new List<RunLayoutInfo>();
        }

        public ParagraphLayoutInfo(IDrawingTextParagraphProperties paragraphProperties) : this()
        {
            this.ParagraphProperties = paragraphProperties;
        }

        public IDrawingTextParagraphProperties ParagraphProperties { get; set; }

        public List<RunLayoutInfo> RunLayouts { get; set; }

        public RunLayoutInfo Bullet { get; set; }

        public DrawingColor BulletColor { get; set; }

        public IDrawingFill BulletFill { get; set; }
    }
}

