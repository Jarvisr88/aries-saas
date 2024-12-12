namespace DevExpress.Office.OpenXml.Export
{
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Import.OpenXml;
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;
    using System.Xml;

    public class OpemXmlDrawingColorExporter : OpenXmlExportWalkerBase, IColorTransformVisitor
    {
        private readonly XmlWriter documentContentWriter;

        public OpemXmlDrawingColorExporter(XmlWriter documentContentWriter) : base(documentContentWriter)
        {
            this.documentContentWriter = documentContentWriter;
        }

        private void ExportColorTransformation(ColorTransformCollection transforms)
        {
            int count = transforms.Count;
            for (int i = 0; i < count; i++)
            {
                transforms[i].Visit(this);
            }
        }

        private void ExportColorTransformMember(string name)
        {
            this.WriteStartElement(name);
            this.WriteEndElement();
        }

        private void ExportColorTransformMember(string name, int transformation)
        {
            this.WriteStartElement(name);
            try
            {
                OpenXmlDrawingExportHelper.WriteIntValue(this.documentContentWriter, "val", transformation);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        protected internal virtual void ExportHslColor(DrawingColor color)
        {
            this.WriteStartElement("hslClr");
            try
            {
                this.ExportHslColorAttributes(color.OriginalColor.Hsl);
                this.ExportColorTransformation(color.Transforms);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void ExportHslColorAttributes(ColorHSL color)
        {
            this.WriteIntValue("hue", color.Hue);
            this.WriteIntValue("sat", color.Saturation);
            this.WriteIntValue("lum", color.Luminance);
        }

        protected internal virtual void ExportPresetColor(DrawingColor color)
        {
            this.WriteStartElement("prstClr");
            try
            {
                this.WriteStringValue("val", color.OriginalColor.Preset);
                this.ExportColorTransformation(color.Transforms);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        protected internal virtual void ExportRgbColor(DrawingColor color)
        {
            this.WriteStartElement("srgbClr");
            try
            {
                this.ExportRgbColorAttributes(color.OriginalColor.Rgb);
                this.ExportColorTransformation(color.Transforms);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void ExportRgbColorAttributes(Color color)
        {
            this.WriteStringValue("val", $"{color.R:X2}" + $"{color.G:X2}" + $"{color.B:X2}");
        }

        protected internal virtual void ExportSchemeColor(DrawingColor color)
        {
            this.WriteStartElement("schemeClr");
            try
            {
                this.ExportSchemeColorAttributes(color);
                this.ExportColorTransformation(color.Transforms);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void ExportSchemeColorAttributes(DrawingColor color)
        {
            string str;
            if (SchemeColorDestination.schemeColorTable.TryGetValue(color.Info.SchemeColor, out str))
            {
                this.WriteStringValue("val", str);
            }
        }

        protected internal virtual void ExportScRgbColor(DrawingColor color)
        {
            this.WriteStartElement("scrgbClr");
            try
            {
                this.ExportScRgbColorAttributes(color.OriginalColor.ScRgb);
                this.ExportColorTransformation(color.Transforms);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void ExportScRgbColorAttributes(ScRGBColor color)
        {
            this.WriteIntValue("r", color.ScR);
            this.WriteIntValue("g", color.ScG);
            this.WriteIntValue("b", color.ScB);
        }

        protected internal virtual void ExportSystemColor(DrawingColor color)
        {
            this.WriteStartElement("sysClr");
            try
            {
                this.ExportSystemColorAttributes(color.Info.SystemColor);
                this.ExportColorTransformation(color.Transforms);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void ExportSystemColorAttributes(SystemColorValues color)
        {
            string str;
            if (SystemColorDestination.systemColorTable.TryGetValue(color, out str))
            {
                this.WriteStringValue("val", str);
            }
        }

        public virtual void GenerateDrawingColorContent(DrawingColor color)
        {
            switch (color.ColorType)
            {
                case DrawingColorType.Rgb:
                    this.ExportRgbColor(color);
                    return;

                case DrawingColorType.System:
                    this.ExportSystemColor(color);
                    return;

                case DrawingColorType.Scheme:
                    this.ExportSchemeColor(color);
                    return;

                case DrawingColorType.Preset:
                    this.ExportPresetColor(color);
                    return;

                case DrawingColorType.ScRgb:
                    this.ExportScRgbColor(color);
                    return;

                case DrawingColorType.Hsl:
                    this.ExportHslColor(color);
                    return;
            }
        }

        public void Visit(AlphaColorTransform transform)
        {
            this.ExportColorTransformMember("alpha", transform.Value);
        }

        public void Visit(AlphaModulationColorTransform transform)
        {
            this.ExportColorTransformMember("alphaMod", transform.Value);
        }

        public void Visit(AlphaOffsetColorTransform transform)
        {
            this.ExportColorTransformMember("alphaOff", transform.Value);
        }

        public void Visit(BlueColorTransform transform)
        {
            this.ExportColorTransformMember("blue", transform.Value);
        }

        public void Visit(BlueModulationColorTransform transform)
        {
            this.ExportColorTransformMember("blueMod", transform.Value);
        }

        public void Visit(BlueOffsetColorTransform transform)
        {
            this.ExportColorTransformMember("blueOff", transform.Value);
        }

        public void Visit(ComplementColorTransform transform)
        {
            this.ExportColorTransformMember("comp");
        }

        public void Visit(GammaColorTransform transform)
        {
            this.ExportColorTransformMember("gamma");
        }

        public void Visit(GrayscaleColorTransform transform)
        {
            this.ExportColorTransformMember("gray");
        }

        public void Visit(GreenColorTransform transform)
        {
            this.ExportColorTransformMember("green", transform.Value);
        }

        public void Visit(GreenModulationColorTransform transform)
        {
            this.ExportColorTransformMember("greenMod", transform.Value);
        }

        public void Visit(GreenOffsetColorTransform transform)
        {
            this.ExportColorTransformMember("greenOff", transform.Value);
        }

        public void Visit(HueColorTransform transform)
        {
            this.ExportColorTransformMember("hue", transform.Value);
        }

        public void Visit(HueModulationColorTransform transform)
        {
            this.ExportColorTransformMember("hueMod", transform.Value);
        }

        public void Visit(HueOffsetColorTransform transform)
        {
            this.ExportColorTransformMember("hueOff", transform.Value);
        }

        public void Visit(InverseColorTransform transform)
        {
            this.ExportColorTransformMember("inv");
        }

        public void Visit(InverseGammaColorTransform transform)
        {
            this.ExportColorTransformMember("invGamma");
        }

        public void Visit(LuminanceColorTransform transform)
        {
            this.ExportColorTransformMember("lum", transform.Value);
        }

        public void Visit(LuminanceModulationColorTransform transform)
        {
            this.ExportColorTransformMember("lumMod", transform.Value);
        }

        public void Visit(LuminanceOffsetColorTransform transform)
        {
            this.ExportColorTransformMember("lumOff", transform.Value);
        }

        public void Visit(RedColorTransform transform)
        {
            this.ExportColorTransformMember("red", transform.Value);
        }

        public void Visit(RedModulationColorTransform transform)
        {
            this.ExportColorTransformMember("redMod", transform.Value);
        }

        public void Visit(RedOffsetColorTransform transform)
        {
            this.ExportColorTransformMember("redOff", transform.Value);
        }

        public void Visit(SaturationColorTransform transform)
        {
            this.ExportColorTransformMember("sat", transform.Value);
        }

        public void Visit(SaturationModulationColorTransform transform)
        {
            this.ExportColorTransformMember("satMod", transform.Value);
        }

        public void Visit(SaturationOffsetColorTransform transform)
        {
            this.ExportColorTransformMember("satOff", transform.Value);
        }

        public void Visit(ShadeColorTransform transform)
        {
            this.ExportColorTransformMember("shade", transform.Value);
        }

        public void Visit(TintColorTransform transform)
        {
            this.ExportColorTransformMember("tint", transform.Value);
        }
    }
}

