namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public class TextProcessor : PdfTextMeasuringProcessor
    {
        private PdfDrawContext context;

        public TextProcessor(PdfDrawContext context, float tabStopInterval, Font font, bool rtl) : base(context, tabStopInterval, font, rtl)
        {
            this.context = context;
        }

        public void ShowText(string source)
        {
            string[] tabbedPieces = TextUtils.GetTabbedPieces(source);
            if (base.Rtl)
            {
                Array.Reverse(tabbedPieces);
            }
            if ((base.TabStopInterval > 0f) && (tabbedPieces.Length > 1))
            {
                this.ShowTextWithTabs(tabbedPieces);
            }
            else
            {
                this.ShowTextWithoutTabs(tabbedPieces);
            }
        }

        private void ShowTextInternal(string text)
        {
            this.context.ShowText(base.GetTextRun(text));
        }

        private void ShowTextWithoutTabs(string[] tabbedPieces)
        {
            foreach (string str in tabbedPieces)
            {
                this.ShowTextInternal(str);
            }
        }

        private void ShowTextWithTabs(string[] tabbedPieces)
        {
            int num = 0;
            foreach (string str in tabbedPieces)
            {
                this.ShowTextInternal(str);
                float simpleTextWidth = base.GetSimpleTextWidth(str);
                int num4 = ((int) (simpleTextWidth / base.TabStopInterval)) + 1;
                this.context.MoveTextPoint(base.TabStopInterval * num4, 0f);
                num += num4;
            }
            this.context.MoveTextPoint(-base.TabStopInterval * num, 0f);
        }
    }
}

