namespace DevExpress.Text.Fonts
{
    using System;
    using System.Runtime.CompilerServices;

    public class DXFontSearchResult<T> where T: IFontFace
    {
        public DXFontSearchResult(T fontFace, DXFontSimulations fontSimulations)
        {
            this.<FontFace>k__BackingField = fontFace;
            this.<FontSimulations>k__BackingField = fontSimulations;
        }

        public T FontFace { get; }

        public DXFontSimulations FontSimulations { get; }
    }
}

