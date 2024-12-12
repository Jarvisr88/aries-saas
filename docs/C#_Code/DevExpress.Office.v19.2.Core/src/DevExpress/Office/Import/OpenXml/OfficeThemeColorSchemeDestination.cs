namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using System;
    using System.Xml;

    public class OfficeThemeColorSchemeDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();

        public OfficeThemeColorSchemeDestination(DestinationAndXmlBasedImporter importer) : base(importer)
        {
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("dk1", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeColorSchemeDestination.OnColorDark1));
            table.Add("lt1", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeColorSchemeDestination.OnColorLight1));
            table.Add("dk2", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeColorSchemeDestination.OnColorDark2));
            table.Add("lt2", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeColorSchemeDestination.OnColorLight2));
            table.Add("accent1", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeColorSchemeDestination.OnColorAccent1));
            table.Add("accent2", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeColorSchemeDestination.OnColorAccent2));
            table.Add("accent3", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeColorSchemeDestination.OnColorAccent3));
            table.Add("accent4", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeColorSchemeDestination.OnColorAccent4));
            table.Add("accent5", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeColorSchemeDestination.OnColorAccent5));
            table.Add("accent6", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeColorSchemeDestination.OnColorAccent6));
            table.Add("hlink", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeColorSchemeDestination.OnColorHyperlink));
            table.Add("folHlink", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeColorSchemeDestination.OnColorFollowedHyperlink));
            return table;
        }

        private static Destination OnColorAccent1(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeColorDestination(importer, ThemeColorIndex.Accent1);

        private static Destination OnColorAccent2(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeColorDestination(importer, ThemeColorIndex.Accent2);

        private static Destination OnColorAccent3(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeColorDestination(importer, ThemeColorIndex.Accent3);

        private static Destination OnColorAccent4(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeColorDestination(importer, ThemeColorIndex.Accent4);

        private static Destination OnColorAccent5(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeColorDestination(importer, ThemeColorIndex.Accent5);

        private static Destination OnColorAccent6(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeColorDestination(importer, ThemeColorIndex.Accent6);

        private static Destination OnColorDark1(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeColorDestination(importer, ThemeColorIndex.Dark1);

        private static Destination OnColorDark2(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeColorDestination(importer, ThemeColorIndex.Dark2);

        private static Destination OnColorFollowedHyperlink(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeColorDestination(importer, ThemeColorIndex.FollowedHyperlink);

        private static Destination OnColorHyperlink(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeColorDestination(importer, ThemeColorIndex.Hyperlink);

        private static Destination OnColorLight1(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeColorDestination(importer, ThemeColorIndex.Light1);

        private static Destination OnColorLight2(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeColorDestination(importer, ThemeColorIndex.Light2);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.Importer.DocumentModel.OfficeTheme.Colors.Name = this.Importer.ReadAttribute(reader, "name");
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

