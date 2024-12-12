namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class RelativeOffsetEffectDestination : DrawingEffectDestinationBase
    {
        private int offsetX;
        private int offsetY;

        public RelativeOffsetEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override IDrawingEffect CreateEffect() => 
            new RelativeOffsetEffect(this.offsetX, this.offsetY);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.offsetX = this.Importer.GetIntegerValue(reader, "tx", 0);
            this.offsetY = this.Importer.GetIntegerValue(reader, "ty", 0);
        }
    }
}

