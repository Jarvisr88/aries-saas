namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class StyleReferenceDestination : DrawingColorDestination
    {
        private readonly Action1<string> setReferenceDelegate;

        public StyleReferenceDestination(DestinationAndXmlBasedImporter importer, Action1<string> setReferenceDelegate, DrawingColor color) : base(importer, color)
        {
            this.setReferenceDelegate = setReferenceDelegate;
            color.Clear();
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            string str = this.Importer.ReadAttribute(reader, "idx");
            this.setReferenceDelegate(str);
        }
    }
}

