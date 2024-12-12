namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class OfficeThemeFontCollectionSchemeDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly ThemeFontSchemePart fontPart;
        private bool hasLatin;
        private bool hasEastAsian;
        private bool hasComplexScript;

        public OfficeThemeFontCollectionSchemeDestination(DestinationAndXmlBasedImporter importer, ThemeFontSchemePart fontPart) : base(importer)
        {
            Guard.ArgumentNotNull(fontPart, "ThemeFontSchemePart");
            this.fontPart = fontPart;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("latin", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFontCollectionSchemeDestination.OnLatin));
            table.Add("ea", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFontCollectionSchemeDestination.OnEastAsian));
            table.Add("cs", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFontCollectionSchemeDestination.OnComplexScript));
            table.Add("font", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFontCollectionSchemeDestination.OnFont));
            return table;
        }

        private static OfficeThemeFontCollectionSchemeDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (OfficeThemeFontCollectionSchemeDestination) importer.PeekDestination();

        private static Destination OnComplexScript(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            OfficeThemeFontCollectionSchemeDestination @this = GetThis(importer);
            @this.hasComplexScript = true;
            return new DrawingTextFontDestination(importer, @this.ComplexScript);
        }

        private static Destination OnEastAsian(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            OfficeThemeFontCollectionSchemeDestination @this = GetThis(importer);
            @this.hasEastAsian = true;
            return new DrawingTextFontDestination(importer, @this.EastAsian);
        }

        private static Destination OnFont(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeSupplementalFontSchemeDestination(importer, GetThis(importer).fontPart);

        private static Destination OnLatin(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            OfficeThemeFontCollectionSchemeDestination @this = GetThis(importer);
            @this.hasLatin = true;
            return new DrawingTextFontDestination(importer, @this.Latin);
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            this.fontPart.IsValid = (this.hasLatin && (this.hasEastAsian && (this.hasComplexScript && (!string.IsNullOrEmpty(this.Latin.Typeface) && (this.EastAsian.Typeface != null))))) && (this.ComplexScript.Typeface != null);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;

        protected internal DrawingTextFont Latin =>
            this.fontPart.Latin;

        protected internal DrawingTextFont EastAsian =>
            this.fontPart.EastAsian;

        protected internal DrawingTextFont ComplexScript =>
            this.fontPart.ComplexScript;

        protected internal Dictionary<string, string> Fonts =>
            this.fontPart.SupplementalFonts;
    }
}

