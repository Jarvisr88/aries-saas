namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Utils;
    using System;
    using System.Xml;

    public class OutlineMiterLineJoinDestination : OutlinePropertiesDestinationBase
    {
        public OutlineMiterLineJoinDestination(DestinationAndXmlBasedImporter importer, Outline outline) : base(importer, outline)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.Outline.JoinStyle = LineJoinStyle.Miter;
            int num = this.Importer.GetIntegerValue(reader, "lim", OutlineInfo.DefaultInfo.MiterLimit);
            if (num < 0)
            {
                this.Importer.ThrowInvalidFile();
            }
            base.Outline.MiterLimit = num;
        }
    }
}

