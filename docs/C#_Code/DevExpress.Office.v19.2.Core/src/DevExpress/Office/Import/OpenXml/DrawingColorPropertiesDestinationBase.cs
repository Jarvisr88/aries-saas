namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public abstract class DrawingColorPropertiesDestinationBase : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly DrawingColor color;
        private readonly DrawingColorModelInfo colorModelInfo;

        protected DrawingColorPropertiesDestinationBase(DestinationAndXmlBasedImporter importer, DrawingColor color) : base(importer)
        {
            this.color = color;
            this.colorModelInfo = new DrawingColorModelInfo();
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("alpha", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnAlpha));
            table.Add("alphaMod", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnAlphaModulation));
            table.Add("alphaOff", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnAlphaOffset));
            table.Add("blue", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnBlue));
            table.Add("blueMod", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnBlueModification));
            table.Add("blueOff", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnBlueOffset));
            table.Add("comp", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnComplement));
            table.Add("gamma", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnGamma));
            table.Add("gray", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnGray));
            table.Add("green", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnGreen));
            table.Add("greenMod", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnGreenModification));
            table.Add("greenOff", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnGreenOffset));
            table.Add("hue", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnHue));
            table.Add("hueMod", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnHueModulate));
            table.Add("hueOff", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnHueOffset));
            table.Add("inv", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnInverse));
            table.Add("invGamma", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnInverseGamma));
            table.Add("lum", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnLuminance));
            table.Add("lumMod", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnLuminanceModulation));
            table.Add("lumOff", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnLuminanceOffset));
            table.Add("red", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnRed));
            table.Add("redMod", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnRedModulation));
            table.Add("redOff", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnRedOffset));
            table.Add("sat", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnSaturation));
            table.Add("satMod", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnSaturationModulation));
            table.Add("satOff", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnSaturationOffset));
            table.Add("shade", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnShade));
            table.Add("tint", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorPropertiesDestinationBase.OnTint));
            return table;
        }

        private static DrawingColorPropertiesDestinationBase GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingColorPropertiesDestinationBase) importer.PeekDestination();

        private static Destination OnAlpha(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationAlpha(importer, GetThis(importer).color);

        private static Destination OnAlphaModulation(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationAlphaModulation(importer, GetThis(importer).color);

        private static Destination OnAlphaOffset(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationAlphaOffset(importer, GetThis(importer).color);

        private static Destination OnBlue(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationBlue(importer, GetThis(importer).color);

        private static Destination OnBlueModification(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationBlueModification(importer, GetThis(importer).color);

        private static Destination OnBlueOffset(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationBlueOffset(importer, GetThis(importer).color);

        private static Destination OnComplement(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationComplement(importer, GetThis(importer).color);

        private static Destination OnGamma(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationGamma(importer, GetThis(importer).color);

        private static Destination OnGray(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationGray(importer, GetThis(importer).color);

        private static Destination OnGreen(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationGreen(importer, GetThis(importer).color);

        private static Destination OnGreenModification(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationGreenModification(importer, GetThis(importer).color);

        private static Destination OnGreenOffset(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationGreenOffset(importer, GetThis(importer).color);

        private static Destination OnHue(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationHue(importer, GetThis(importer).color);

        private static Destination OnHueModulate(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationHueModulate(importer, GetThis(importer).color);

        private static Destination OnHueOffset(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationHueOffset(importer, GetThis(importer).color);

        private static Destination OnInverse(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationInverse(importer, GetThis(importer).color);

        private static Destination OnInverseGamma(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationInverseGamma(importer, GetThis(importer).color);

        private static Destination OnLuminance(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationLuminance(importer, GetThis(importer).color);

        private static Destination OnLuminanceModulation(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationLuminanceModulation(importer, GetThis(importer).color);

        private static Destination OnLuminanceOffset(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationLuminanceOffset(importer, GetThis(importer).color);

        private static Destination OnRed(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationRed(importer, GetThis(importer).color);

        private static Destination OnRedModulation(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationRedModulation(importer, GetThis(importer).color);

        private static Destination OnRedOffset(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationRedOffset(importer, GetThis(importer).color);

        private static Destination OnSaturation(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationSaturation(importer, GetThis(importer).color);

        private static Destination OnSaturationModulation(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationSaturationModulation(importer, GetThis(importer).color);

        private static Destination OnSaturationOffset(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationSaturationOffset(importer, GetThis(importer).color);

        private static Destination OnShade(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationShade(importer, GetThis(importer).color);

        private static Destination OnTint(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ColorTransformDestinationTint(importer, GetThis(importer).color);

        public override void ProcessElementClose(XmlReader reader)
        {
            this.color.AssignInfo(this.colorModelInfo);
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.SetColorPropertyValue(reader);
        }

        protected abstract void SetColorPropertyValue(XmlReader reader);

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;

        public DrawingColor Color =>
            this.color;

        protected DrawingColorModelInfo ColorModelInfo =>
            this.colorModelInfo;
    }
}

