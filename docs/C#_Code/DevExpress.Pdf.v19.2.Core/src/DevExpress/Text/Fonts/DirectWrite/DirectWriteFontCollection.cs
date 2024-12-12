namespace DevExpress.Text.Fonts.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using DevExpress.Text.Fonts;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class DirectWriteFontCollection : DXFontCollection
    {
        protected DirectWriteFontCollection(DWriteFactory factory, DirectWriteFontMeasurer measurer)
        {
            this.<Factory>k__BackingField = factory;
            this.<Measurer>k__BackingField = measurer;
        }

        protected DXFontFamily[] CreateFamilies()
        {
            int fontFamilyCount = this.Collection.GetFontFamilyCount();
            DXFontFamily[] familyArray = new DXFontFamily[fontFamilyCount];
            for (int i = 0; i < fontFamilyCount; i++)
            {
                familyArray[i] = new DirectWriteFontFamily(this, this.Collection.GetFontFamily(i));
            }
            return familyArray;
        }

        public DWriteTextFormat CreateTextFormat(string familyName, DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STYLE style, DWRITE_FONT_STRETCH stretch, float sizeInPoints, string locale) => 
            this.Factory.CreateTextFormat(familyName, this.Collection, weight, style, stretch, sizeInPoints, locale);

        protected DWriteFactory Factory { get; }

        protected abstract DWriteFontCollection Collection { get; }

        public DirectWriteFontMeasurer Measurer { get; }
    }
}

