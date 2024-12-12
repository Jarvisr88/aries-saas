namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;

    public interface IFontCacheService
    {
        Font GetOrAdd(object key, Func<Font> add);
    }
}

