namespace DevExpress.Text.Fonts.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using DevExpress.Text.Fonts;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DirectWriteFontFamily : DXFontFamily
    {
        private readonly DWriteFontFamily family;
        private readonly DirectWriteFontCollection collection;
        private readonly Lazy<FamilyNameInfo> familyName;

        public DirectWriteFontFamily(DirectWriteFontCollection collection, DWriteFontFamily family)
        {
            this.collection = collection;
            this.family = family;
            this.familyName = new Lazy<FamilyNameInfo>(new Func<FamilyNameInfo>(this.CreateFamilyNameInfo), false);
        }

        private FamilyNameInfo CreateFamilyNameInfo()
        {
            using (DWriteLocalizedStrings strings = this.family.GetFamilyNames())
            {
                int num;
                bool flag;
                strings.FindLocaleName(CultureInfo.CurrentUICulture.Name, out num, out flag);
                if (!flag)
                {
                    strings.FindLocaleName("en-us", out num, out flag);
                }
                if (!flag)
                {
                    num = 0;
                }
                return new FamilyNameInfo(strings.GetString(num), strings.GetLocaleName(num));
            }
        }

        public DWriteTextFormat CreateTextFormat(DWriteFont font, float sizeInPoints) => 
            this.collection.CreateTextFormat(this.familyName.Value.FamilyName, font.GetWeight(), font.GetStyle(), font.GetStretch(), sizeInPoints, this.familyName.Value.LocaleName);

        public override void Dispose()
        {
            this.family.Dispose();
        }

        public override DXFontFace GetFirstMatchingFontFace(DXFontWeight weight, DXFontStretch fontStretch, DXFontStyle style) => 
            new DirectWriteFont(this, this.collection.Measurer, this.family.GetFirstMatchingFont((DWRITE_FONT_WEIGHT) weight, (DWRITE_FONT_STRETCH) fontStretch, (DWRITE_FONT_STYLE) style));

        public override string Name =>
            this.familyName.Value.FamilyName;

        [StructLayout(LayoutKind.Sequential)]
        private struct FamilyNameInfo
        {
            public string FamilyName { get; }
            public string LocaleName { get; }
            public FamilyNameInfo(string familyName, string localeName)
            {
                this.<FamilyName>k__BackingField = familyName;
                this.<LocaleName>k__BackingField = localeName;
            }
        }
    }
}

