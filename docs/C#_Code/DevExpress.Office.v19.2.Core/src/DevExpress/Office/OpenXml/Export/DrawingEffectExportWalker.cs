namespace DevExpress.Office.OpenXml.Export
{
    using DevExpress.Office.Drawing;
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class DrawingEffectExportWalker : OpenXmlExportWalkerBase, IDrawingEffectVisitor
    {
        private readonly DrawingEffectCollection effects;
        private readonly IOpenXmlOfficeImageExporter imageExporter;

        public DrawingEffectExportWalker(XmlWriter documentContentWriter, DrawingEffectCollection effects, IOpenXmlOfficeImageExporter imageExporter) : base(documentContentWriter)
        {
            Guard.ArgumentNotNull(imageExporter, "imageExporter");
            this.effects = effects;
            this.imageExporter = imageExporter;
        }

        void IDrawingEffectVisitor.AlphaCeilingEffectVisit()
        {
            this.GenerateEmptyEffectContent("alphaCeiling");
        }

        void IDrawingEffectVisitor.AlphaFloorEffectVisit()
        {
            this.GenerateEmptyEffectContent("alphaFloor");
        }

        void IDrawingEffectVisitor.GrayScaleEffectVisit()
        {
            this.GenerateEmptyEffectContent("grayscl");
        }

        void IDrawingEffectVisitor.Visit(AlphaBiLevelEffect drawingEffect)
        {
            this.GenerateEffectRequiredIntValueContent("alphaBiLevel", "thresh", drawingEffect.Thresh);
        }

        void IDrawingEffectVisitor.Visit(AlphaInverseEffect drawingEffect)
        {
            this.WriteStartElement("alphaInv");
            try
            {
                if (!drawingEffect.Color.IsEmpty)
                {
                    new OpemXmlDrawingColorExporter(base.DocumentContentWriter).GenerateDrawingColorContent(drawingEffect.Color);
                }
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(AlphaModulateEffect drawingEffect)
        {
            this.WriteStartElement("alphaMod");
            try
            {
                OpenXmlDrawingExportHelper.GenerateContainerEffectContent(base.DocumentContentWriter, "cont", drawingEffect.Container, this.imageExporter);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(AlphaModulateFixedEffect drawingEffect)
        {
            this.WriteStartElement("alphaModFix");
            try
            {
                this.WriteIntValue("amt", drawingEffect.Amount, 0x186a0);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(AlphaOutsetEffect drawingEffect)
        {
            this.WriteStartElement("alphaOutset");
            try
            {
                long radius = drawingEffect.Radius;
                this.WriteLongValue("rad", radius, 0);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(AlphaReplaceEffect drawingEffect)
        {
            this.GenerateEffectRequiredIntValueContent("alphaRepl", "a", drawingEffect.Alpha);
        }

        void IDrawingEffectVisitor.Visit(BiLevelEffect drawingEffect)
        {
            this.GenerateEffectRequiredIntValueContent("biLevel", "thresh", drawingEffect.Thresh);
        }

        void IDrawingEffectVisitor.Visit(BlendEffect drawingEffect)
        {
            this.WriteStartElement("blend");
            try
            {
                this.WriteStringValue("blend", OpenXmlExporterBase.BlendModeTable[drawingEffect.BlendMode]);
                OpenXmlDrawingExportHelper.GenerateContainerEffectContent(base.DocumentContentWriter, "cont", drawingEffect.Container, this.imageExporter);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(BlurEffect drawingEffect)
        {
            this.WriteStartElement("blur");
            try
            {
                long radius = drawingEffect.Radius;
                this.WriteLongValue("rad", radius, 0);
                if (!drawingEffect.Grow)
                {
                    this.WriteBoolValue("grow", drawingEffect.Grow);
                }
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(ColorChangeEffect drawingEffect)
        {
            this.WriteStartElement("clrChange");
            try
            {
                this.GenerateDrawingColorContentFromEffect(drawingEffect.ColorFrom, "clrFrom");
                this.GenerateDrawingColorContentFromEffect(drawingEffect.ColorTo, "clrTo");
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(ContainerEffect drawingEffect)
        {
            OpenXmlDrawingExportHelper.GenerateContainerEffectContent(base.DocumentContentWriter, "cont", drawingEffect, this.imageExporter);
        }

        void IDrawingEffectVisitor.Visit(DuotoneEffect drawingEffect)
        {
            this.WriteStartElement("duotone");
            try
            {
                OpemXmlDrawingColorExporter exporter = new OpemXmlDrawingColorExporter(base.DocumentContentWriter);
                exporter.GenerateDrawingColorContent(drawingEffect.FirstColor);
                exporter.GenerateDrawingColorContent(drawingEffect.SecondColor);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(Effect drawingEffect)
        {
            this.WriteStartElement("effect");
            try
            {
                this.WriteStringValue("ref", drawingEffect.Reference);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(FillEffect drawingEffect)
        {
            this.WriteStartElement("fill");
            try
            {
                OpenXmlDrawingExportHelper.GenerateDrawingFillContent(base.DocumentContentWriter, drawingEffect.Fill, this.imageExporter);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(FillOverlayEffect drawingEffect)
        {
            this.WriteStartElement("fillOverlay");
            try
            {
                this.WriteStringValue("blend", OpenXmlExporterBase.BlendModeTable[drawingEffect.BlendMode]);
                OpenXmlDrawingExportHelper.GenerateDrawingFillContent(base.DocumentContentWriter, drawingEffect.Fill, this.imageExporter);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(GlowEffect drawingEffect)
        {
            this.WriteStartElement("glow");
            try
            {
                long radius = drawingEffect.Radius;
                this.WriteLongValue("rad", radius, 0);
                new OpemXmlDrawingColorExporter(base.DocumentContentWriter).GenerateDrawingColorContent(drawingEffect.Color);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(HSLEffect drawingEffect)
        {
            this.WriteStartElement("hsl");
            try
            {
                this.WriteIntValue("hue", drawingEffect.Hue, 0);
                this.WriteIntValue("sat", drawingEffect.Saturation, 0);
                this.WriteIntValue("lum", drawingEffect.Luminance, 0);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(InnerShadowEffect drawingEffect)
        {
            this.WriteStartElement("innerShdw");
            try
            {
                long blurRadius = drawingEffect.BlurRadius;
                this.WriteLongValue("blurRad", blurRadius, 0);
                this.WriteOffsetShadowInfo(drawingEffect.OffsetShadow);
                new OpemXmlDrawingColorExporter(base.DocumentContentWriter).GenerateDrawingColorContent(drawingEffect.Color);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(LuminanceEffect drawingEffect)
        {
            this.WriteStartElement("lum");
            try
            {
                this.WriteIntValue("bright", drawingEffect.Bright, 0);
                this.WriteIntValue("contrast", drawingEffect.Contrast, 0);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(OuterShadowEffect drawingEffect)
        {
            this.WriteStartElement("outerShdw");
            try
            {
                this.WriteOuterShadowEffectInfo(drawingEffect.Info);
                new OpemXmlDrawingColorExporter(base.DocumentContentWriter).GenerateDrawingColorContent(drawingEffect.Color);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(PresetShadowEffect drawingEffect)
        {
            this.WriteStartElement("prstShdw");
            try
            {
                this.WriteEnumValue<PresetShadowType>("prst", drawingEffect.Type, OpenXmlExporterBase.PresetShadowTypeTable);
                this.WriteOffsetShadowInfo(drawingEffect.OffsetShadow);
                new OpemXmlDrawingColorExporter(base.DocumentContentWriter).GenerateDrawingColorContent(drawingEffect.Color);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(ReflectionEffect drawingEffect)
        {
            this.WriteStartElement("reflection");
            try
            {
                this.WriteReflectionOpacityInfo(drawingEffect.ReflectionOpacity);
                this.WriteOuterShadowEffectInfo(drawingEffect.OuterShadowEffectInfo);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(RelativeOffsetEffect drawingEffect)
        {
            this.WriteStartElement("relOff");
            try
            {
                this.WriteIntValue("tx", drawingEffect.OffsetX, 0);
                this.WriteIntValue("ty", drawingEffect.OffsetY, 0);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(SoftEdgeEffect drawingEffect)
        {
            this.WriteStartElement("softEdge");
            try
            {
                long radius = drawingEffect.Radius;
                this.WriteLongValue("rad", radius);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(SolidColorReplacementEffect drawingEffect)
        {
            this.GenerateDrawingColorContentFromEffect(drawingEffect.Color, "clrRepl");
        }

        void IDrawingEffectVisitor.Visit(TintEffect drawingEffect)
        {
            this.WriteStartElement("tint");
            try
            {
                this.WriteIntValue("hue", drawingEffect.Hue, 0);
                this.WriteIntValue("amt", drawingEffect.Amount, 0);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        void IDrawingEffectVisitor.Visit(TransformEffect drawingEffect)
        {
            this.WriteStartElement("xfrm");
            try
            {
                this.WriteScalingFactorInfo(drawingEffect.ScalingFactor);
                this.WriteSkewAnglesInfo(drawingEffect.SkewAngles);
                long horizontal = drawingEffect.CoordinateShift.Horizontal;
                this.WriteLongValue("tx", horizontal, 0);
                long vertical = drawingEffect.CoordinateShift.Vertical;
                this.WriteLongValue("ty", vertical, 0);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void GenerateDrawingColorContentFromEffect(DrawingColor color, string attr)
        {
            this.WriteStartElement(attr);
            try
            {
                new OpemXmlDrawingColorExporter(base.DocumentContentWriter).GenerateDrawingColorContent(color);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void GenerateEffectRequiredIntValueContent(string tagName, string attr, int value)
        {
            this.WriteStartElement(tagName);
            try
            {
                this.WriteIntValue(attr, value);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void GenerateEmptyEffectContent(string attr)
        {
            this.WriteStartElement(attr);
            this.WriteEndElement();
        }

        public void Walk()
        {
            this.effects.ForEach(effect => effect.Visit(this));
        }

        private void WriteOffsetShadowInfo(OffsetShadowInfo info)
        {
            long distance = info.Distance;
            this.WriteLongValue("dist", distance, 0);
            this.WriteIntValue("dir", info.Direction, 0);
        }

        private void WriteOuterShadowEffectInfo(OuterShadowEffectInfo info)
        {
            long blurRadius = info.BlurRadius;
            this.WriteLongValue("blurRad", blurRadius, 0);
            this.WriteOffsetShadowInfo(info.OffsetShadow);
            this.WriteScalingFactorInfo(info.ScalingFactor);
            this.WriteSkewAnglesInfo(info.SkewAngles);
            this.WriteEnumValue<RectangleAlignType>("algn", info.ShadowAlignment, OpenXmlExporterBase.RectangleAlignTypeTable, RectangleAlignType.Bottom);
            if (!info.RotateWithShape)
            {
                this.WriteBoolValue("rotWithShape", info.RotateWithShape);
            }
        }

        private void WriteReflectionOpacityInfo(ReflectionOpacityInfo info)
        {
            this.WriteIntValue("stA", info.StartOpacity, 0x186a0);
            this.WriteIntValue("stPos", info.StartPosition, 0);
            this.WriteIntValue("endA", info.EndOpacity, 0);
            this.WriteIntValue("endPos", info.EndPosition, 0x186a0);
            this.WriteIntValue("fadeDir", info.FadeDirection, 0x5265c0);
        }

        private void WriteScalingFactorInfo(ScalingFactorInfo info)
        {
            this.WriteIntValue("sx", info.Horizontal, 0x186a0);
            this.WriteIntValue("sy", info.Vertical, 0x186a0);
        }

        private void WriteSkewAnglesInfo(SkewAnglesInfo info)
        {
            this.WriteIntValue("kx", info.Horizontal, 0);
            this.WriteIntValue("ky", info.Vertical, 0);
        }
    }
}

