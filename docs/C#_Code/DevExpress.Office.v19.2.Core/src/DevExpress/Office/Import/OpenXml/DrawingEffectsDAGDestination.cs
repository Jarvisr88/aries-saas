namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Xml;

    public class DrawingEffectsDAGDestination : DrawingEffectsListDestination
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();

        public DrawingEffectsDAGDestination(DestinationAndXmlBasedImporter importer) : base(importer)
        {
        }

        public DrawingEffectsDAGDestination(DestinationAndXmlBasedImporter importer, ContainerEffect containerEffect) : base(importer, containerEffect)
        {
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("alphaBiLevel", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnAlphaBiLevelEffect));
            table.Add("alphaCeiling", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnAlphaCeilingEffect));
            table.Add("alphaFloor", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnAlphaFloorEffect));
            table.Add("alphaInv", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnAlphaInverseEffect));
            table.Add("alphaMod", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnAlphaModulateEffect));
            table.Add("alphaModFix", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnAlphaModulateFixedEffect));
            table.Add("alphaRepl", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnAlphaReplaceEffect));
            table.Add("alphaOutset", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnAlphaOutsetEffect));
            table.Add("biLevel", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnBiLevelEffect));
            table.Add("blend", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnBlendEffect));
            table.Add("clrChange", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnColorChangeEffect));
            table.Add("cont", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnContainerEffect));
            table.Add("duotone", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnDuotoneEffect));
            table.Add("effect", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnEffect));
            table.Add("fill", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnFillEffect));
            table.Add("grayscl", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnGrayScaleEffect));
            table.Add("hsl", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnHSLEffect));
            table.Add("lum", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnLuminanceEffect));
            table.Add("relOff", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnRelativeOffsetEffect));
            table.Add("clrRepl", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnSolidColorReplacement));
            table.Add("tint", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnTintEffect));
            table.Add("xfrm", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnTransformEffect));
            table.Add("extLst", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsDAGDestination.OnExtensionList));
            AddEffectListHandlerTable(table);
            return table;
        }

        private static DrawingEffectsDAGDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingEffectsDAGDestination) importer.PeekDestination();

        private static Destination OnAlphaBiLevelEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new AlphaBiLevelEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnAlphaCeilingEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new AlphaCeilingEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnAlphaFloorEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new AlphaFloorEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnAlphaInverseEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new AlphaInverseEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnAlphaModulateEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new AlphaModulateEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnAlphaModulateFixedEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new AlphaModulateFixedEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnAlphaOutsetEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new AlphaOutsetEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnAlphaReplaceEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new AlphaReplaceEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnBiLevelEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new BiLevelEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnBlendEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new BlendEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnColorChangeEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorChangeEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnContainerEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ContainerEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnDuotoneEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DuotoneEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new EffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnExtensionList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new EmptyDestination<DestinationAndXmlBasedImporter>(importer);

        private static Destination OnFillEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new FillEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnGrayScaleEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new GrayScaleEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnHSLEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new HSLEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnLuminanceEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new LuminanceEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnRelativeOffsetEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new RelativeOffsetEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnSolidColorReplacement(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new SolidColorReplacementDestination(importer, GetThis(importer).Effects);

        private static Destination OnTintEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new TintEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnTransformEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new TransformEffectDestination(importer, GetThis(importer).Effects);

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.ProcessElementOpen(reader);
            this.Importer.ActualDocumentModel.BeginUpdate();
            try
            {
                base.ContainerEffect.Name = this.Importer.ReadAttribute(reader, "name");
                base.ContainerEffect.Type = this.Importer.GetWpEnumValue<DrawingEffectContainerType>(reader, "type", OpenXmlExporterBase.DrawingEffectContainerTypeTable, DrawingEffectContainerType.Sibling);
                base.ContainerEffect.HasEffectsList = false;
            }
            finally
            {
                IDocumentModel model;
                model.EndUpdate();
            }
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

