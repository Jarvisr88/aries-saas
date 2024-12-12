namespace DevExpress.Text.Fonts
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DXFontSearchHelper<T> where T: IFontFace
    {
        private readonly string familyName;
        private readonly IList<T> fontPatterns;

        private DXFontSearchHelper(string familyName, IList<T> fontPatterns)
        {
            this.familyName = familyName;
            this.fontPatterns = fontPatterns;
        }

        private IEnumerable<T> FilterByStretch(IEnumerable<T> fontPatterns, DXFontStretch requestedStretch)
        {
            IEnumerable<T> source = from f in fontPatterns
                where f.Stretch == requestedStretch
                select f;
            return (!source.Any<T>() ? (from f in fontPatterns
                orderby Math.Abs((int) (f.Stretch - requestedStretch))
                select f) : source);
        }

        private IEnumerable<T> FilterByStyle(IEnumerable<T> fontPatterns, DXFontStyle requestedStyle, out bool emulateItalic)
        {
            emulateItalic = false;
            IEnumerable<T> source = from f in fontPatterns
                where f.Style == requestedStyle
                select f;
            if (source.Any<T>())
            {
                return source;
            }
            if (requestedStyle == DXFontStyle.Italic)
            {
                return this.FilterByStyle(fontPatterns, DXFontStyle.Oblique, out emulateItalic);
            }
            if (requestedStyle != DXFontStyle.Oblique)
            {
                return fontPatterns;
            }
            emulateItalic = true;
            Func<T, bool> predicate = <>c<T>.<>9__8_1;
            if (<>c<T>.<>9__8_1 == null)
            {
                Func<T, bool> local1 = <>c<T>.<>9__8_1;
                predicate = <>c<T>.<>9__8_1 = f => f.Style == DXFontStyle.Regular;
            }
            return fontPatterns.Where<T>(predicate);
        }

        private IEnumerable<T> FilterByWeight(IEnumerable<T> fontPatterns, DXFontWeight requestedWeight, out bool emulateBold)
        {
            emulateBold = false;
            IEnumerable<T> source = from f in fontPatterns
                where f.Weight == requestedWeight
                select f;
            if (source.Any<T>())
            {
                return source;
            }
            IOrderedEnumerable<T> enumerable2 = from f in fontPatterns
                orderby Math.Abs((int) (f.Weight - requestedWeight))
                select f;
            if (Math.Abs((int) (700 - requestedWeight)) >= Math.Abs((int) (enumerable2.First<T>().Weight - requestedWeight)))
            {
                return enumerable2;
            }
            emulateBold = true;
            return this.FindNormalWeightedFonts(fontPatterns);
        }

        private IEnumerable<T> FindNormalWeightedFonts(IEnumerable<T> fontPatterns)
        {
            Func<T, bool> predicate = <>c<T>.<>9__6_0;
            if (<>c<T>.<>9__6_0 == null)
            {
                Func<T, bool> local1 = <>c<T>.<>9__6_0;
                predicate = <>c<T>.<>9__6_0 = f => f.Weight == DXFontWeight.Normal;
            }
            IEnumerable<T> source = fontPatterns.Where<T>(predicate);
            return (!source.Any<T>() ? fontPatterns : source);
        }

        private DXFontSearchResult<T> GetFirstMatchingFont(DXFontWeight weight, DXFontStretch fontStretch, DXFontStyle style)
        {
            T fontFace = this.fontPatterns.FirstOrDefault<T>(f => (f.Family == ((DXFontSearchHelper<T>) this).familyName) && ((f.Weight == weight) && ((f.Stretch == fontStretch) && (f.Style == style))));
            if (fontFace == null)
            {
                fontFace = this.fontPatterns.FirstOrDefault<T>(delegate (T f) {
                    DXFontDescriptor descriptor = PdfFontNamesHelper.GetDescriptor(f.Family, PdfFontStyle.Regular);
                    return ((descriptor.Weight == weight) || (f.Weight == weight)) ? (((descriptor.Stretch == fontStretch) || (f.Stretch == fontStretch)) && ((descriptor.Style == style) || (f.Style == style))) : false;
                });
            }
            bool emulateBold = false;
            bool emulateItalic = false;
            if (fontFace == null)
            {
                fontFace = this.fontPatterns.First<T>();
                IEnumerable<T> source = this.FilterByStretch(this.fontPatterns, fontStretch);
                if (source.Any<T>())
                {
                    IEnumerable<T> enumerable2 = this.FilterByWeight(source, weight, out emulateBold);
                    if (enumerable2.Any<T>())
                    {
                        fontFace = this.FilterByStyle(enumerable2, style, out emulateItalic).FirstOrDefault<T>();
                    }
                }
            }
            DXFontSimulations none = DXFontSimulations.None;
            if (emulateBold)
            {
                none |= DXFontSimulations.Bold;
            }
            if (emulateItalic)
            {
                none |= DXFontSimulations.Italic;
            }
            return new DXFontSearchResult<T>(fontFace, none);
        }

        public static DXFontSearchResult<T> GetFirstMatchingFont(string familyName, IList<T> fontPatterns, DXFontWeight weight, DXFontStretch fontStretch, DXFontStyle style) => 
            new DXFontSearchHelper<T>(familyName, fontPatterns).GetFirstMatchingFont(weight, fontStretch, style);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXFontSearchHelper<T>.<>c <>9;
            public static Func<T, bool> <>9__6_0;
            public static Func<T, bool> <>9__8_1;

            static <>c()
            {
                DXFontSearchHelper<T>.<>c.<>9 = new DXFontSearchHelper<T>.<>c();
            }

            internal bool <FilterByStyle>b__8_1(T f) => 
                f.Style == DXFontStyle.Regular;

            internal bool <FindNormalWeightedFonts>b__6_0(T f) => 
                f.Weight == DXFontWeight.Normal;
        }
    }
}

