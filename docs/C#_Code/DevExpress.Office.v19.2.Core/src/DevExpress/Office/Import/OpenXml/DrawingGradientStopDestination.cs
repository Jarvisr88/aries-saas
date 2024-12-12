namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class DrawingGradientStopDestination : DrawingColorDestination
    {
        private readonly DrawingGradientStop gradientStop;

        public DrawingGradientStopDestination(DestinationAndXmlBasedImporter importer, DrawingGradientStop gradientStop) : base(importer, gradientStop.Color)
        {
            this.gradientStop = gradientStop;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.ProcessElementOpen(reader);
            this.gradientStop.Position = this.Importer.GetWpSTIntegerValue(reader, "pos");
        }
    }
}

