namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Data.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class PdfTextMeasuringProcessor : IDisposable
    {
        private float tabStopInterval;
        private PdfMeasuringContext context;
        private StringDirection stringProcessor;
        private Font font;
        private bool rtl;
        private Dictionary<string, TextRun> textRunCache = new Dictionary<string, TextRun>();

        public PdfTextMeasuringProcessor(PdfMeasuringContext context, float tabStopInterval, Font font, bool rtl)
        {
            this.context = context;
            this.tabStopInterval = tabStopInterval;
            this.font = font;
            this.rtl = rtl;
        }

        public void Dispose()
        {
            if (this.stringProcessor != null)
            {
                this.stringProcessor.Dispose();
                this.stringProcessor = null;
            }
            this.font = null;
        }

        protected float GetSimpleTextWidth(string text) => 
            this.context.GetTextWidth(this.GetTextRun(text));

        private StringDirection GetStringProcessor()
        {
            if ((this.stringProcessor == null) && (SecurityHelper.IsUnmanagedCodeGrantedAndHasZeroHwnd && !AzureCompatibility.Enable))
            {
                this.stringProcessor = new StringDirection(this.font, this.rtl);
            }
            return this.stringProcessor;
        }

        protected TextRun GetTextRun(string text)
        {
            TextRun run;
            if (!this.textRunCache.TryGetValue(text, out run))
            {
                TextRun run2;
                StringDirection direction = HasUnicodeChars(text) ? this.GetStringProcessor() : null;
                if (direction != null)
                {
                    run2 = direction.ProcessString(text);
                }
                else
                {
                    TextRun run1 = new TextRun();
                    run1.Text = text;
                    run2 = run1;
                }
                run = run2;
            }
            return run;
        }

        public float GetTextWidth(string source)
        {
            string[] tabbedPieces = TextUtils.GetTabbedPieces(source);
            return ((this.tabStopInterval > 0f) ? this.GetTextWidthWithTabs(tabbedPieces) : this.GetTextWidthWithoutTabs(tabbedPieces));
        }

        private float GetTextWidthWithoutTabs(string[] tabbedPieces)
        {
            float num = 0f;
            foreach (string str in tabbedPieces)
            {
                num += this.GetSimpleTextWidth(str);
            }
            return num;
        }

        private float GetTextWidthWithTabs(string[] tabbedPieces)
        {
            float num = 0f;
            for (int i = 0; i < (tabbedPieces.Length - 1); i++)
            {
                float simpleTextWidth = this.GetSimpleTextWidth(tabbedPieces[i]);
                int num4 = ((int) (simpleTextWidth / this.tabStopInterval)) + 1;
                num += this.tabStopInterval * num4;
            }
            return (num + this.GetSimpleTextWidth(tabbedPieces[tabbedPieces.Length - 1]));
        }

        private static bool HasUnicodeChars(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] >= '\x00ff')
                {
                    return true;
                }
            }
            return false;
        }

        protected float TabStopInterval =>
            this.tabStopInterval;

        protected bool Rtl =>
            this.rtl;
    }
}

