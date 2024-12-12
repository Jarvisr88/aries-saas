namespace DevExpress.Office.Services.Implementation
{
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Layout;
    using DevExpress.Office.Services;
    using System;

    public class FontCharsetService : IFontCharacterSetService, IDisposable
    {
        private FontCache cache;
        private FontCharacterSet characterSet;

        public void BeginProcessing(string fontName)
        {
            this.cache = new GdiPlusFontCache(new DocumentLayoutUnitDocumentConverter(), false, true);
            this.characterSet = this.cache.GetFontCharacterSet(fontName);
        }

        private void ClearCache()
        {
            if (this.cache != null)
            {
                this.cache.Dispose();
                this.cache = null;
            }
            this.characterSet = null;
        }

        public bool ContainsChar(char ch) => 
            (this.characterSet != null) ? this.characterSet.ContainsChar(ch) : false;

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.ClearCache();
            }
        }

        public void EndProcessing()
        {
            this.ClearCache();
        }
    }
}

