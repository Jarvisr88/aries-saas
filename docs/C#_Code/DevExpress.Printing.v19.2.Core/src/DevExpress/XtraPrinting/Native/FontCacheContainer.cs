namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class FontCacheContainer : ObjectContainer<Font>, IFontCacheService
    {
        public Font GetOrAdd(object key, Func<Font> create)
        {
            Font font;
            if (!base.Items.TryGetValue(key, out font))
            {
                font = create();
                base.Items.Add(key, font);
            }
            return font;
        }
    }
}

