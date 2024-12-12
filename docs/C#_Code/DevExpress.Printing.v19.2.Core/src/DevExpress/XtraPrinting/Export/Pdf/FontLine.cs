namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections;
    using System.Drawing;

    public abstract class FontLine
    {
        private float yOffset;
        private ArrayList infos = new ArrayList();

        protected FontLine(float ascent, float descent)
        {
            this.yOffset = this.CalculateYOffset(ascent, descent);
        }

        public void AddLine(float textX, float textY, float textWidth)
        {
            this.infos.Add(new Info(textX, textY + this.yOffset, textWidth));
        }

        protected abstract float CalculateYOffset(float ascent, float descent);
        public void DrawContents(PdfDrawContext context, Color color, float thickness)
        {
            context.SetRGBStrokeColor(color);
            context.SetLineWidth(thickness);
            foreach (Info info in this.infos)
            {
                info.DrawContents(context);
            }
            context.Stroke();
        }

        private class Info
        {
            private float x;
            private float y;
            private float length;

            public Info(float x, float y, float length)
            {
                this.x = x;
                this.y = y;
                this.length = length;
            }

            public void DrawContents(PdfDrawContext context)
            {
                context.MoveTo(this.x, this.y);
                context.LineTo(this.x + this.length, this.y);
            }
        }
    }
}

