namespace DevExpress.Utils.Text
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class ConstDpiFontsCache : IDisposable
    {
        private readonly Dictionary<Font, FontCache> fonts = new Dictionary<Font, FontCache>();

        private FontCache AddFontCache(Graphics graphics, Font font)
        {
            FontCache cache = new FontCache(graphics, font);
            this.fonts[font] = cache;
            return cache;
        }

        private void Clear()
        {
            foreach (FontCache cache in this.fonts.Values)
            {
                cache.Dispose();
            }
            this.fonts.Clear();
        }

        public void DrawString(Graphics graphics, string text, Font font, Color foreColor, Rectangle drawBounds, Rectangle clipBounds, StringFormat stringFormat, TextHighLight highLight, IWordBreakProvider wordBreakProvider)
        {
            if ((graphics != null) && ((text != null) && ((font != null) && (stringFormat != null))))
            {
                this[graphics, font].DrawString(graphics, foreColor, text, drawBounds, clipBounds, stringFormat, highLight, wordBreakProvider);
            }
        }

        public int GetFontAscentHeight(Graphics graphics, Font font) => 
            this[graphics, font].AscentHeight;

        private FontCache GetFontCacheByFont(Graphics graphics, Font font)
        {
            FontCache cache;
            this.fonts.TryGetValue(font, out cache);
            return ((cache != null) ? cache : this.AddFontCache(graphics, font));
        }

        public int GetFontHeight(Graphics graphics, Font font) => 
            this[graphics, font].Height;

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

        internal void RemoveFontCache(Font font)
        {
            FontCache cache;
            if (this.fonts.TryGetValue(font, out cache))
            {
                this.fonts.Remove(font);
                if (cache != null)
                {
                    cache.Dispose();
                }
            }
        }

        void IDisposable.Dispose()
        {
            this.Clear();
            GC.SuppressFinalize(this);
        }

        public FontCache this[Graphics graphics, Font font] =>
            this.GetFontCacheByFont(graphics, font);
    }
}

