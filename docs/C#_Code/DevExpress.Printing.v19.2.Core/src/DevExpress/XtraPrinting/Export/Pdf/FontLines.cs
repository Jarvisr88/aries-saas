namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections;
    using System.Drawing;

    public class FontLines
    {
        private float thickness;
        private ArrayList fontLineList = new ArrayList();

        public FontLines(Font font, float ascent, float descent)
        {
            this.thickness = font.Size / 14f;
            if (font.Underline)
            {
                this.fontLineList.Add(new Underline(ascent, descent));
            }
            if (font.Strikeout)
            {
                this.fontLineList.Add(new Strikeout(ascent, descent));
            }
        }

        public void AddLines(float textX, float textY, float textWidth)
        {
            foreach (FontLine line in this.fontLineList)
            {
                line.AddLine(textX, textY, textWidth);
            }
        }

        public void DrawContents(PdfDrawContext context, Color color)
        {
            foreach (FontLine line in this.fontLineList)
            {
                line.DrawContents(context, color, this.thickness);
            }
        }
    }
}

