namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class ContainerEffectDestination : DrawingEffectsDAGDestination
    {
        private DrawingEffectCollection effects;

        public ContainerEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer)
        {
            this.effects = effects;
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            this.effects.Add(base.ContainerEffect);
        }
    }
}

