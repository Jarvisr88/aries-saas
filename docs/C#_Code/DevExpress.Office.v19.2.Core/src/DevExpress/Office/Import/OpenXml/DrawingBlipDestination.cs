namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using DevExpress.Office.Utils;
    using System;
    using System.Xml;

    public class DrawingBlipDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly DrawingBlip blip;

        public DrawingBlipDestination(DestinationAndXmlBasedImporter importer, DrawingBlip blip) : base(importer)
        {
            this.blip = blip;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("alphaBiLevel", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnAlphaBiLevelEffect));
            table.Add("alphaCeiling", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnAlphaCeilingEffect));
            table.Add("alphaFloor", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnAlphaFloorEffect));
            table.Add("alphaInv", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnAlphaInverseEffect));
            table.Add("alphaMod", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnAlphaModulateEffect));
            table.Add("alphaModFix", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnAlphaModulateFixedEffect));
            table.Add("alphaRepl", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnAlphaReplaceEffect));
            table.Add("alphaOutset", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnAlphaOutsetEffect));
            table.Add("biLevel", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnBiLevelEffect));
            table.Add("blend", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnBlendEffect));
            table.Add("blur", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnBlurEffect));
            table.Add("clrChange", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnColorChangeEffect));
            table.Add("clrRepl", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnSolidColorReplacement));
            table.Add("duotone", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnDuotoneEffect));
            table.Add("fillOverlay", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnFillOverlayEffect));
            table.Add("grayscl", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnGrayScaleEffect));
            table.Add("hsl", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnHueSaturationLuminanceEffect));
            table.Add("lum", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnLuminanceEffect));
            table.Add("tint", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnTintEffect));
            table.Add("extLst", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipDestination.OnExtensionList));
            return table;
        }

        private OfficeImage GetEmbededImage(IDocumentModel documentModel, string relationId) => 
            this.GetImageCore(this.Importer.LookupImageByRelationId(this.blip.DocumentModel, relationId, this.Importer.DocumentRootFolder));

        private OfficeImage GetImageCore(OfficeImage image) => 
            (image != null) ? image : UriBasedOfficeImageBase.CreatePlaceHolder(this.Importer.DocumentModel, 0x1c, 0x1c);

        private static DrawingBlipDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingBlipDestination) importer.PeekDestination();

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

        private static Destination OnBlurEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new BlurEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnColorChangeEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorChangeEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnDuotoneEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DuotoneEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnExtensionList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new EmptyDestination<DestinationAndXmlBasedImporter>(importer);

        private static Destination OnFillOverlayEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new FillOverlayEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnGrayScaleEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new GrayScaleEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnHueSaturationLuminanceEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new HSLEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnLuminanceEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new LuminanceEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnSolidColorReplacement(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new SolidColorReplacementDestination(importer, GetThis(importer).Effects);

        private static Destination OnTintEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new TintEffectDestination(importer, GetThis(importer).Effects);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.blip.CompressionState = this.Importer.GetWpEnumValue<CompressionState>(reader, "cstate", OpenXmlExporterBase.CompressionStateTable, CompressionState.None);
            string attribute = reader.GetAttribute("embed", this.Importer.RelationsNamespace);
            if (!string.IsNullOrEmpty(attribute))
            {
                this.blip.Image = this.GetEmbededImage(this.blip.DocumentModel, attribute);
            }
            else
            {
                string str2 = reader.GetAttribute("link", this.Importer.RelationsNamespace);
                if (!string.IsNullOrEmpty(str2))
                {
                    this.SetExternalOfficeImage(this.blip, str2);
                }
            }
        }

        private void SetExternalOfficeImage(DrawingBlip blip, string relationId)
        {
            OpenXmlRelation externalRelationById = this.Importer.LookupExternalRelationById(relationId);
            if (externalRelationById != null)
            {
                blip.Image = this.GetImageCore(this.Importer.LookupExternalImageByRelation(externalRelationById, this.Importer.DocumentRootFolder));
                blip.SetExternal(externalRelationById.Target);
            }
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;

        protected internal DrawingBlip Blip =>
            this.blip;

        protected internal DrawingEffectCollection Effects =>
            this.blip.Effects;
    }
}

