namespace DevExpress.Utils.Text
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.InteropServices;

    internal class FontsCache : IDisposable
    {
        private readonly Dictionary<GraphicsUnit, ConstDpiFontsCache> dpiHash = new Dictionary<GraphicsUnit, ConstDpiFontsCache>(new GraphicsUnitComparer());

        private void Clear()
        {
            foreach (IDisposable disposable in this.dpiHash.Values)
            {
                disposable.Dispose();
            }
            this.dpiHash.Clear();
        }

        public void DrawSingleLineString(Graphics g, string text, Font font, Color foreColor, Rectangle drawBounds, StringFormat format)
        {
            this[g, font].DrawSingleLineStringSC(g, foreColor, text, drawBounds, format);
        }

        public void DrawString(Graphics graphics, string text, Font font, Color foreColor, Rectangle drawBounds, Rectangle clipBounds, StringFormat stringFormat, TextHighLight highLight, IWordBreakProvider wordBreakProvider)
        {
            if ((graphics != null) && ((text != null) && ((font != null) && (stringFormat != null))))
            {
                this[graphics, font].DrawString(graphics, foreColor, text, drawBounds, clipBounds, stringFormat, highLight, wordBreakProvider);
            }
        }

        ~FontsCache()
        {
            ((IDisposable) this).Dispose();
        }

        public int GetFontAscentHeight(Graphics graphics, Font font) => 
            this[graphics, font].AscentHeight;

        private FontCache GetFontCacheByFont(Graphics graphics, Font font)
        {
            ConstDpiFontsCache cache;
            if (!this.dpiHash.TryGetValue(graphics.PageUnit, out cache))
            {
                cache = new ConstDpiFontsCache();
                this.dpiHash.Add(graphics.PageUnit, cache);
            }
            return cache[graphics, font];
        }

        public int GetFontHeight(Graphics graphics, Font font) => 
            this[graphics, font].Height;

        public int GetFontInternalLeading(Graphics graphics, Font font) => 
            this[graphics, font].InternalLeading;

        public int[] GetMeasureString(Graphics graphics, string text, Font font, StringFormat stringFormat) => 
            this[graphics, font].GetMeasureString(graphics, text, stringFormat);

        public int GetStringHeight(Graphics graphics, string text, Font font, int width, StringFormat stringFormat) => 
            this[graphics, font].GetStringHeight(graphics, text, width, stringFormat);

        public Size GetStringSize(Graphics graphics, string text, Font font, StringFormat stringFormat) => 
            this[graphics, font].GetStringSize(graphics, text, stringFormat);

        public Size GetStringSize(Graphics graphics, string text, Font font, StringFormat stringFormat, int maxWidth, IWordBreakProvider wordBreakProvider) => 
            this[graphics, font].GetStringSize(graphics, text, stringFormat, maxWidth, wordBreakProvider);

        public Size GetStringSize(Graphics graphics, string text, Font font, StringFormat stringFormat, int maxWidth, int maxHeight) => 
            this[graphics, font].GetStringSize(graphics, text, stringFormat, maxWidth, maxHeight);

        public Size GetStringSize(Graphics graphics, string text, Font font, StringFormat stringFormat, int maxWidth, int maxHeight, IWordBreakProvider wordBreakProvider) => 
            this[graphics, font].GetStringSize(graphics, text, stringFormat, maxWidth, maxHeight, wordBreakProvider);

        public Size GetStringSize(Graphics graphics, string text, Font font, StringFormat stringFormat, int maxWidth, int maxHeight, out bool isCropped) => 
            this[graphics, font].GetStringSize(graphics, text, stringFormat, maxWidth, maxHeight, out isCropped);

        public Size GetStringSize(Graphics graphics, string text, Font font, StringFormat stringFormat, int maxWidth, int maxHeight, IWordBreakProvider wordBreakProvider, out bool isCropped) => 
            this[graphics, font].GetStringSize(graphics, text, stringFormat, maxWidth, maxHeight, wordBreakProvider, out isCropped);

        internal void ResetFontCache(GraphicsUnit pageUnit, Font font)
        {
            ConstDpiFontsCache cache;
            if (this.dpiHash.TryGetValue(pageUnit, out cache))
            {
                cache.RemoveFontCache(font);
            }
        }

        void IDisposable.Dispose()
        {
            this.Clear();
            GC.SuppressFinalize(this);
        }

        public FontCache this[Graphics graphics, Font font] =>
            this.GetFontCacheByFont(graphics, font);

        private sealed class GraphicsUnitComparer : IEqualityComparer<GraphicsUnit>
        {
            public bool Equals(GraphicsUnit x, GraphicsUnit y) => 
                x == y;

            public int GetHashCode(GraphicsUnit obj) => 
                ((int) obj).GetHashCode();
        }
    }
}

