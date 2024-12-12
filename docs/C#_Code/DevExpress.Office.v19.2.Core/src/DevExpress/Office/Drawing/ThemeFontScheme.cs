namespace DevExpress.Office.Drawing
{
    using DevExpress.Export.Xl;
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Globalization;

    public class ThemeFontScheme
    {
        private readonly ThemeFontSchemePart minorFont;
        private readonly ThemeFontSchemePart majorFont;
        private string name = string.Empty;

        public ThemeFontScheme(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.minorFont = new ThemeFontSchemePart(documentModel);
            this.majorFont = new ThemeFontSchemePart(documentModel);
        }

        protected internal void Clear()
        {
            this.name = string.Empty;
            this.majorFont.Clear();
            this.minorFont.Clear();
        }

        public void CopyFrom(ThemeFontScheme sourceObj)
        {
            this.name = sourceObj.Name;
            this.majorFont.CopyFrom(sourceObj.majorFont);
            this.minorFont.CopyFrom(sourceObj.minorFont);
        }

        public string GetTypeface(XlFontSchemeStyles schemeStyle, CultureInfo currentUICulture) => 
            (schemeStyle != XlFontSchemeStyles.None) ? ((schemeStyle != XlFontSchemeStyles.Minor) ? this.MajorFont.GetTypeface(currentUICulture) : this.MinorFont.GetTypeface(currentUICulture)) : string.Empty;

        public string Name
        {
            get => 
                this.name;
            set => 
                this.name = value;
        }

        public ThemeFontSchemePart MinorFont =>
            this.minorFont;

        public ThemeFontSchemePart MajorFont =>
            this.majorFont;

        public bool IsValidate =>
            !string.IsNullOrEmpty(this.name) && (this.minorFont.IsValid && this.majorFont.IsValid);
    }
}

