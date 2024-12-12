namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class OfficeThemeSupplementalFontSchemeDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly ThemeFontSchemePart fontPart;

        public OfficeThemeSupplementalFontSchemeDestination(DestinationAndXmlBasedImporter importer, ThemeFontSchemePart fontPart) : base(importer)
        {
            Guard.ArgumentNotNull(fontPart, "ThemeFontSchemePart");
            this.fontPart = fontPart;
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            string str = this.Importer.ReadAttribute(reader, "script");
            string str2 = this.Importer.ReadAttribute(reader, "typeface");
            if (string.IsNullOrEmpty(str) && string.IsNullOrEmpty(str2))
            {
                this.Importer.ThrowInvalidFile();
            }
            this.fontPart.AddSupplementalFont(str, str2);
        }
    }
}

