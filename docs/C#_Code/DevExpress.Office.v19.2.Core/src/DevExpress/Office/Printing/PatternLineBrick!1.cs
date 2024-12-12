namespace DevExpress.Office.Printing
{
    using DevExpress.Office.Layout;
    using DevExpress.Office.Model;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export.Pdf;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public abstract class PatternLineBrick<T> : VisualBrick where T: struct
    {
        private readonly DocumentLayoutUnitConverter unitConverter;
        private T patternLineType;
        private RectangleF lineBounds;

        protected PatternLineBrick(DocumentLayoutUnitConverter unitConverter)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
        }

        protected internal virtual RectangleF CalculateLineBounds(IGraphics gr, RectangleF clientRect)
        {
            if (gr is PdfGraphics)
            {
                RectangleF ef = this.unitConverter.LayoutUnitsToDocuments(Rectangle.Round(this.LineBounds));
                ef.Offset(clientRect.Location);
                return ef;
            }
            RectangleF lineBounds = this.LineBounds;
            int num2 = this.unitConverter.DocumentsToLayoutUnits((int) Math.Round((double) clientRect.Y));
            lineBounds.Offset((float) this.unitConverter.DocumentsToLayoutUnits((int) Math.Round((double) clientRect.X)), (float) num2);
            return lineBounds;
        }

        protected internal virtual DocumentLayoutUnitConverter GetPainterUnitConverter(IGraphics gr) => 
            !(gr is PdfGraphics) ? this.unitConverter : new DocumentLayoutUnitDocumentConverter();

        protected internal abstract PatternLine<T> GetPatternLine();
        protected override void Scale(double scaleFactor)
        {
            base.Scale(scaleFactor);
            this.LineBounds = MathMethods.Scale(this.LineBounds, scaleFactor);
        }

        public T PatternLineType
        {
            get => 
                this.patternLineType;
            set => 
                this.patternLineType = value;
        }

        public RectangleF LineBounds
        {
            get => 
                this.lineBounds;
            set => 
                this.lineBounds = value;
        }
    }
}

