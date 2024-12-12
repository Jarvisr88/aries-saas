namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class ColorTransformDestinationAlphaOffset : ColorTransformDestinationBase
    {
        public ColorTransformDestinationAlphaOffset(DestinationAndXmlBasedImporter importer, DrawingColor color) : base(importer, color)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.Color.Transforms.AddCore(new AlphaOffsetColorTransform(base.GetIntegerValue(reader)));
        }
    }
}

