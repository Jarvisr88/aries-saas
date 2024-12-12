namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Import.Binary;
    using DevExpress.Office.Model;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Xml;

    public static class VmlDrawingImportHelper
    {
        public const string VmlDrawingNamespace = "urn:schemas-microsoft-com:vml";
        public const string VmlDrawingOfficeNamespace = "urn:schemas-microsoft-com:office:office";

        private static void AddEffect(ShapeProperties shapeProperties, IDrawingEffect effect)
        {
            ContainerEffect containerEffect = shapeProperties.EffectStyle.ContainerEffect;
            containerEffect.SetApplyEffectListCore(true);
            containerEffect.Effects.Add(effect);
        }

        public static int CalculateCropUnit(string crop)
        {
            if (string.IsNullOrEmpty(crop))
            {
                return 0;
            }
            float num = !crop.EndsWith("%") ? GetFloatOrVulgarFractionValue(crop, 0f, 65536f) : GetPercentageValue(crop);
            return (int) Math.Round((double) (num * 100000f));
        }

        private static List<double> CalculateOpacities(VmlIntermediateColors intermediateColors, double firstOpacity, double lastOpacity)
        {
            List<double> list = new List<double>();
            double position = intermediateColors.First.Position;
            double num2 = intermediateColors.Last.Position;
            double num3 = (num2 == position) ? 0.0 : ((lastOpacity - firstOpacity) / (num2 - position));
            foreach (OfficeShadeColor color in intermediateColors)
            {
                double item = lastOpacity - (num3 * (color.Position - position));
                list.Add(item);
            }
            return list;
        }

        private static int ConvertBlacklevelToBrightness(float blacklevel) => 
            (int) Math.Round((double) ((((blacklevel < -0.5) || (blacklevel > 0.5)) ? 0.5f : (blacklevel + 0.5f)) * 100000f));

        private static OpenXmlBlackWhiteMode ConvertBlackWhiteMode(VmlBlackAndWhiteMode mode)
        {
            switch (mode)
            {
                case VmlBlackAndWhiteMode.Black:
                    return OpenXmlBlackWhiteMode.Black;

                case VmlBlackAndWhiteMode.BlackTextAndLines:
                    return OpenXmlBlackWhiteMode.BlackGray;

                case VmlBlackAndWhiteMode.Color:
                    return OpenXmlBlackWhiteMode.Clr;

                case VmlBlackAndWhiteMode.GrayOutline:
                    return OpenXmlBlackWhiteMode.GrayWhite;

                case VmlBlackAndWhiteMode.GrayScale:
                    return OpenXmlBlackWhiteMode.Gray;

                case VmlBlackAndWhiteMode.HighContrast:
                    return OpenXmlBlackWhiteMode.BlackWhite;

                case VmlBlackAndWhiteMode.InverseGray:
                    return OpenXmlBlackWhiteMode.InvGray;

                case VmlBlackAndWhiteMode.LightGrayscale:
                    return OpenXmlBlackWhiteMode.LtGray;

                case VmlBlackAndWhiteMode.Undrawn:
                    return OpenXmlBlackWhiteMode.Hidden;

                case VmlBlackAndWhiteMode.White:
                    return OpenXmlBlackWhiteMode.White;
            }
            return OpenXmlBlackWhiteMode.Auto;
        }

        public static int ConvertGainToContrast(float gain)
        {
            float num = 1f;
            if (gain <= 1f)
            {
                num = gain / 2f;
            }
            else if ((gain > 1f) && (gain <= 100f))
            {
                num = 1f - (1f / (2f * gain));
            }
            return (int) Math.Round((double) (num * 100000f));
        }

        private static DrawingBlipFill CreateBlipFillCore(IDocumentModel documentModel, VmlShapeFillProperties fillProperties)
        {
            if (fillProperties.EmbeddedImage == null)
            {
                return null;
            }
            DrawingBlipFill fill = DrawingBlipFill.Create(documentModel, fillProperties.EmbeddedImage.RootImage);
            fill.RotateWithShape = fillProperties.Rotate;
            int amount = (int) Math.Round((double) (fillProperties.Opacity * 100000f));
            fill.Blip.Effects.Add(new AlphaModulateFixedEffect(amount));
            return fill;
        }

        public static Color CreateDarkenColor(string value, Color color)
        {
            double colorTransform = GetColorTransform(value);
            return DrawingValueConverter.GetDarkenTransformColor(color, colorTransform);
        }

        internal static DrawingPatternFill CreateDrawingPatternFillCore(IDocumentModel documentModel, Color foregroundColor, Color backgroundColor, OfficeImage image)
        {
            DrawingPatternFill fill = new DrawingPatternFill(documentModel);
            SetupFillColor(fill.ForegroundColor, foregroundColor);
            SetupFillColor(fill.BackgroundColor, backgroundColor);
            fill.PatternType = SetupPatternType(image, DrawingPatternType.Percent50);
            return fill;
        }

        private static DrawingSolidFill CreateDrawingSolidFill(IDocumentModel documentModel, VmlShapeFillProperties fillProperties, Color fillColor)
        {
            Color color = (fillProperties.Color != DXColor.White) ? fillProperties.Color : fillColor;
            return CreateDrawingSolidFillCore(documentModel, color, fillProperties.Opacity);
        }

        private static DrawingSolidFill CreateDrawingSolidFillCore(IDocumentModel documentModel, Color color, float opacity)
        {
            DrawingSolidFill fill = DrawingSolidFill.Create(documentModel, color);
            BinaryDrawingImportHelper.SetupFillOpacity(fill.Color, (double) opacity);
            return fill;
        }

        private static IDrawingFill CreateFrameFill(IDocumentModel documentModel, VmlShapeFillProperties fillProperties) => 
            CreateBlipFillCore(documentModel, fillProperties) ?? DrawingFill.None;

        private static DrawingGradientFill CreateGradientFill(IDocumentModel documentModel, ShapePreset shapeType, VmlShapeFillProperties fillProperties, Color fillColor)
        {
            DrawingGradientFill fill = new DrawingGradientFill(documentModel) {
                RotateWithShape = fillProperties.Rotate,
                GradientType = (fillProperties.Type == VmlFillType.GradientRadial) ? ((shapeType == ShapePreset.Rect) ? GradientType.Rectangle : GradientType.Shape) : GradientType.Linear
            };
            SetupFillRectangle(fill, fillProperties);
            SetupGradientAngle(fill, fillProperties);
            SetupGradientStops(documentModel, fill, fillProperties, fillColor);
            fill.Scaled = true;
            return fill;
        }

        private static VmlIntermediateColors CreateIntermediateColors(Color first, Color last)
        {
            VmlIntermediateColors colors = new VmlIntermediateColors();
            OfficeColorRecord record1 = new OfficeColorRecord();
            record1.Color = first;
            OfficeShadeColor item = new OfficeShadeColor();
            item.ColorRecord = record1;
            item.Position = 0.0;
            colors.Add(item);
            OfficeColorRecord record2 = new OfficeColorRecord();
            record2.Color = last;
            OfficeShadeColor color2 = new OfficeShadeColor();
            color2.ColorRecord = record2;
            color2.Position = 1.0;
            colors.Add(color2);
            return colors;
        }

        public static Color CreateLightenColor(string value, Color color)
        {
            double colorTransform = GetColorTransform(value);
            return DrawingValueConverter.GetLightenTransformColor(color, 1.0 - colorTransform);
        }

        private static DrawingPatternFill CreatePatternFill(IDocumentModel documentModel, VmlShapeFillProperties fillProperties, Color fillColor)
        {
            Color foregroundColor = (fillProperties.Color != DXColor.White) ? fillProperties.Color : fillColor;
            return CreateDrawingPatternFillCore(documentModel, foregroundColor, fillProperties.Color2, fillProperties.EmbeddedImage);
        }

        private static IDrawingFill CreateTileFill(IDocumentModel documentModel, VmlShapeFillProperties fillProperties)
        {
            DrawingBlipFill fill = CreateBlipFillCore(documentModel, fillProperties);
            if (fill == null)
            {
                return DrawingFill.None;
            }
            fill.ScaleX = 0x186a0;
            fill.ScaleY = 0x186a0;
            return fill;
        }

        internal static ModelShapeCustomGeometry CreateVmlFakeGeometry(VmlSingleFormulasCollection formulas, VmlFormulaNamedValues namedValues)
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            if (namedValues != null)
            {
                int?[] adjustValues = namedValues.AdjustValues;
                int index = 0;
                while (true)
                {
                    if (index >= adjustValues.Length)
                    {
                        geometry.Guides.Add(new ModelShapeGuide("width", "val " + namedValues.Width.ToString(CultureInfo.InvariantCulture)));
                        geometry.Guides.Add(new ModelShapeGuide("height", "val " + namedValues.Height.ToString(CultureInfo.InvariantCulture)));
                        geometry.Guides.Add(new ModelShapeGuide("xcenter", "val " + namedValues.CenterX.ToString(CultureInfo.InvariantCulture)));
                        geometry.Guides.Add(new ModelShapeGuide("ycenter", "val " + namedValues.CenterY.ToString(CultureInfo.InvariantCulture)));
                        geometry.Guides.Add(new ModelShapeGuide("xlimo", "val " + namedValues.LimoX.ToString(CultureInfo.InvariantCulture)));
                        geometry.Guides.Add(new ModelShapeGuide("ylimo", "val " + namedValues.LimoY.ToString(CultureInfo.InvariantCulture)));
                        geometry.Guides.Add(new ModelShapeGuide("lineDrawn", "val " + (namedValues.LineDrawn ? "1" : "0")));
                        geometry.Guides.Add(new ModelShapeGuide("pixellinewidth", "val " + namedValues.PixelLineWidth.ToString(CultureInfo.InvariantCulture)));
                        geometry.Guides.Add(new ModelShapeGuide("pixelwidth", "val " + namedValues.PixelWidth.ToString(CultureInfo.InvariantCulture)));
                        geometry.Guides.Add(new ModelShapeGuide("pixelheight", "val " + namedValues.PixelHeight.ToString(CultureInfo.InvariantCulture)));
                        geometry.Guides.Add(new ModelShapeGuide("emuwidth", "val " + namedValues.EmuWidth.ToString(CultureInfo.InvariantCulture)));
                        geometry.Guides.Add(new ModelShapeGuide("emuheight", "val " + namedValues.EmuHeight.ToString(CultureInfo.InvariantCulture)));
                        geometry.Guides.Add(new ModelShapeGuide("emuwidth2", "val " + namedValues.EmuWidth2.ToString(CultureInfo.InvariantCulture)));
                        geometry.Guides.Add(new ModelShapeGuide("emuheight2", "val " + namedValues.EmuHeight2.ToString(CultureInfo.InvariantCulture)));
                        break;
                    }
                    if (adjustValues[index] != null)
                    {
                        geometry.AdjustValues.Add(new ModelShapeGuide("adj" + index.ToString(CultureInfo.InvariantCulture), "val " + adjustValues[index].Value));
                    }
                    index++;
                }
            }
            if ((formulas != null) && (formulas.Count > 0))
            {
                for (int i = 0; i < formulas.Count; i++)
                {
                    geometry.Guides.Add(new ModelShapeGuide("G" + i.ToString(CultureInfo.InvariantCulture), VmlFormulaParser.Parse(formulas[i].Equation)));
                }
            }
            return geometry;
        }

        private static double GetColorTransform(string value)
        {
            int num3;
            int index = value.IndexOf("(");
            int num2 = value.IndexOf(")");
            if ((index == -1) || ((num2 == -1) || (index > num2)))
            {
                return 0.0;
            }
            int.TryParse(value.Substring(index + 1, (num2 - index) - 1), out num3);
            return (((double) num3) / 255.0);
        }

        public static float GetFloatOrPercentageValue(string value)
        {
            bool flag = false;
            if (value.EndsWith("%"))
            {
                flag = true;
                value = value.Replace("%", string.Empty);
            }
            float num = GetWpSTFloatValue(value, NumberStyles.Float, 0f);
            if (flag)
            {
                num /= 100f;
            }
            return num;
        }

        public static float GetFloatOrVulgarFractionValue(string value, float defaultValue, float denominator)
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            value = value.ToLower();
            return (!value.EndsWith("f") ? GetWpSTFloatValue(value, NumberStyles.Float, defaultValue) : (GetWpSTFloatValue(value.Replace("f", ""), NumberStyles.Float, defaultValue) / denominator));
        }

        public static float GetModelUnitValue(string value, DocumentModelUnitConverter unitConverter) => 
            !string.IsNullOrEmpty(value) ? new VmlUnitConverter(unitConverter).ToModelUnitsF(new DXVmlUnit(value)) : 0f;

        private static OutlineCompoundType GetOutlineCompoundType(VmlLineStyle vmlLineStyle)
        {
            switch (vmlLineStyle)
            {
                case VmlLineStyle.Single:
                    return OutlineCompoundType.Single;

                case VmlLineStyle.ThinThin:
                    return OutlineCompoundType.Double;

                case VmlLineStyle.ThinThick:
                    return OutlineCompoundType.ThinThick;

                case VmlLineStyle.ThickThin:
                    return OutlineCompoundType.ThickThin;

                case VmlLineStyle.ThickBetweenThin:
                    return OutlineCompoundType.Triple;
            }
            return OutlineCompoundType.Single;
        }

        private static OutlineDashing GetOutlineDashing(VmlDashStyle vmlDashStyle)
        {
            switch (vmlDashStyle)
            {
                case VmlDashStyle.Solid:
                    return OutlineDashing.Solid;

                case VmlDashStyle.ShortDash:
                    return OutlineDashing.SystemDash;

                case VmlDashStyle.ShortDot:
                    return OutlineDashing.SystemDot;

                case VmlDashStyle.ShortDashDot:
                    return OutlineDashing.SystemDashDot;

                case VmlDashStyle.ShortDashDotDot:
                    return OutlineDashing.SystemDashDotDot;

                case VmlDashStyle.Dot:
                    return OutlineDashing.Dot;

                case VmlDashStyle.Dash:
                    return OutlineDashing.Dash;

                case VmlDashStyle.LongDash:
                    return OutlineDashing.LongDash;

                case VmlDashStyle.DashDot:
                    return OutlineDashing.DashDot;

                case VmlDashStyle.LongDashDot:
                    return OutlineDashing.LongDashDot;

                case VmlDashStyle.LongDashDotDot:
                    return OutlineDashing.LongDashDotDot;
            }
            return OutlineDashing.Solid;
        }

        private static OutlineHeadTailSize GetOutlineHeadTailSize(VMLStrokeArrowLength strokeArrowLength)
        {
            switch (strokeArrowLength)
            {
                case VMLStrokeArrowLength.Short:
                    return OutlineHeadTailSize.Small;

                case VMLStrokeArrowLength.Medium:
                    return OutlineHeadTailSize.Medium;

                case VMLStrokeArrowLength.Long:
                    return OutlineHeadTailSize.Large;
            }
            return OutlineHeadTailSize.Medium;
        }

        private static OutlineHeadTailSize GetOutlineHeadTailSize(VMLStrokeArrowWidth strokeArrowWidth)
        {
            switch (strokeArrowWidth)
            {
                case VMLStrokeArrowWidth.Narrow:
                    return OutlineHeadTailSize.Small;

                case VMLStrokeArrowWidth.Medium:
                    return OutlineHeadTailSize.Medium;

                case VMLStrokeArrowWidth.Wide:
                    return OutlineHeadTailSize.Large;
            }
            return OutlineHeadTailSize.Medium;
        }

        private static OutlineHeadTailType GetOutlineHeadTailType(VMLStrokeArrowType strokeArrowType)
        {
            switch (strokeArrowType)
            {
                case VMLStrokeArrowType.None:
                    return OutlineHeadTailType.None;

                case VMLStrokeArrowType.Block:
                    return OutlineHeadTailType.TriangleArrow;

                case VMLStrokeArrowType.Classic:
                    return OutlineHeadTailType.StealthArrow;

                case VMLStrokeArrowType.Diamond:
                    return OutlineHeadTailType.Diamond;

                case VMLStrokeArrowType.Open:
                    return OutlineHeadTailType.Arrow;

                case VMLStrokeArrowType.Oval:
                    return OutlineHeadTailType.Oval;
            }
            return OutlineHeadTailType.None;
        }

        private static float GetPercentageValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0f;
            }
            value = value.Replace("%", "");
            return (GetWpSTFloatValue(value, NumberStyles.Float, 0f) / 100f);
        }

        public static int GetRotation(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            DXRotationUnit unit = new DXRotationUnit(value);
            return ((unit.Type != DXUnitType.Fd) ? ((int) Math.Round((double) (unit.Value * 60000f))) : ((int) Math.Round((double) ((unit.Value / 65536f) * 60000f))));
        }

        public static Color GetVmlSTColorValue(XmlReader reader, string attributeName, Color defaultValue)
        {
            string attribute = reader.GetAttribute(attributeName);
            return (string.IsNullOrEmpty(attribute) ? defaultValue : ParseColor(attribute, defaultValue));
        }

        public static float GetWpSTFloatOrVulgarFractionValue(XmlReader reader, string attributeName, float defaultValue, float denominator) => 
            GetFloatOrVulgarFractionValue(reader.GetAttribute(attributeName), defaultValue, denominator);

        private static float GetWpSTFloatValue(string value, NumberStyles numberStyles, float defaultValue)
        {
            double num;
            return ((string.IsNullOrEmpty(value) || !double.TryParse(value, numberStyles, CultureInfo.InvariantCulture, out num)) ? defaultValue : ((float) num));
        }

        private static bool IsShadowPerspective(VmlShadowEffect shadow)
        {
            switch (shadow.ShadowType)
            {
                case VmlShadowType.Perspective:
                case VmlShadowType.ShapeRelative:
                case VmlShadowType.DrawingRelative:
                    return true;
            }
            return false;
        }

        public static Color ParseColor(string value, Color defaultValue)
        {
            if (value == "auto")
            {
                return defaultValue;
            }
            int index = value.IndexOf(" [");
            if (index != -1)
            {
                value = value.Remove(index);
            }
            if (value[0] == '#')
            {
                uint num2;
                value = value.Substring(1, value.Length - 1);
                Color color2 = DXColor.FromName(value);
                if (color2.IsKnownColor)
                {
                    return color2;
                }
                value = (value.Length != 3) ? (new string('0', 6 - value.Length) + value) : string.Format("{0}{0}{1}{1}{2}{2}", value[0], value[1], value[2]);
                if (((value.Length == 8) || (value.Length == 6)) && uint.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2))
                {
                    if (value.Length == 6)
                    {
                        num2 += 0xff000000;
                    }
                    return DXColor.FromArgb((int) num2);
                }
            }
            Color color = DXColor.FromName(value);
            return (!(color != DXColor.Empty) ? defaultValue : color);
        }

        public static void PrepareAdjustValues(int?[] adjustValues, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string[] separator = new string[] { "," };
                string[] strArray = value.Split(separator, StringSplitOptions.None);
                for (int i = 0; i < Math.Min(strArray.Length, adjustValues.Length); i++)
                {
                    int num2;
                    string s = strArray[i];
                    if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
                    {
                        adjustValues[i] = new int?(num2);
                    }
                }
            }
        }

        public static void PrepareCoordOrigin(string coodorigin, VmlCoordUnit coord)
        {
            PrepareCoordValueCore(coodorigin, coord);
        }

        public static void PrepareCoordSize(string coodsize, VmlCoordUnit coord)
        {
            PrepareCoordValueCore(coodsize, coord);
        }

        private static void PrepareCoordValueCore(string value, VmlCoordUnit coord)
        {
            if (!string.IsNullOrEmpty(value))
            {
                char[] separator = new char[] { ',' };
                string[] strArray = value.Split(separator);
                if (strArray.Length == 2)
                {
                    int num;
                    coord.X = !int.TryParse(strArray[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out num) ? 0 : num;
                    if (int.TryParse(strArray[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
                    {
                        coord.Y = num;
                    }
                    else
                    {
                        coord.Y = 0;
                    }
                }
            }
        }

        public static void PrepareShapeStyleProperties(VmlShapeStyleProperties styleProperties, string style)
        {
            if (!string.IsNullOrEmpty(style))
            {
                char[] separator = new char[] { ';' };
                foreach (string str in style.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                {
                    char[] chArray2 = new char[] { ':' };
                    string[] strArray3 = str.Split(chArray2, StringSplitOptions.None);
                    if ((strArray3.Length >= 2) && !string.IsNullOrWhiteSpace(strArray3[0]))
                    {
                        string key = strArray3[0].Trim();
                        string str3 = strArray3[1].Trim();
                        if (styleProperties.ContainsKey(key))
                        {
                            styleProperties[key] = str3;
                        }
                        else
                        {
                            styleProperties.Add(key, str3);
                        }
                    }
                }
            }
        }

        public static float ReadPercentageAttribute(XmlReader reader, string attributeName) => 
            GetPercentageValue(reader.GetAttribute(attributeName));

        private static void SetFill(Outline outline, VmlLineStrokeSettings strokeSettings, Color strokeColor)
        {
            VmlFillType type = (strokeSettings != null) ? strokeSettings.FillType : VmlFillType.Solid;
            Color color = ((strokeSettings == null) || (strokeSettings.Color == DXColor.Black)) ? strokeColor : strokeSettings.Color;
            switch (type)
            {
                case VmlFillType.Solid:
                    outline.Fill = CreateDrawingSolidFillCore(outline.DocumentModel, color, (strokeSettings != null) ? strokeSettings.Opacity : 1f);
                    return;

                case VmlFillType.Gradient:
                case VmlFillType.GradientRadial:
                case VmlFillType.Tile:
                case VmlFillType.Frame:
                    return;

                case VmlFillType.Pattern:
                    if (strokeSettings != null)
                    {
                        outline.Fill = CreateDrawingPatternFillCore(outline.DocumentModel, color, strokeSettings.Color2, strokeSettings.EmbeddedImage);
                    }
                    return;
            }
        }

        public static void SetupBlackAndWhiteMode(ShapePropertiesBase shapeProperties, VmlBlackAndWhiteMode mode)
        {
            shapeProperties.BlackAndWhiteMode = ConvertBlackWhiteMode(mode);
        }

        private static void SetupCommonShapeProtections(ICommonDrawingLocks locks, VmlShapeProtections vmlShapeProtections)
        {
            bool? aspectRatio = vmlShapeProtections.AspectRatio;
            locks.NoChangeAspect = (aspectRatio != null) ? aspectRatio.GetValueOrDefault() : false;
            aspectRatio = vmlShapeProtections.Selection;
            locks.NoSelect = (aspectRatio != null) ? aspectRatio.GetValueOrDefault() : false;
            aspectRatio = vmlShapeProtections.Grouping;
            locks.NoGroup = (aspectRatio != null) ? aspectRatio.GetValueOrDefault() : false;
            aspectRatio = vmlShapeProtections.Position;
            locks.NoMove = (aspectRatio != null) ? aspectRatio.GetValueOrDefault() : false;
        }

        public static void SetupCustomGeometry(IOfficeShape shape, string path, VmlSingleFormulasCollection formulas, VmlFormulaNamedValues namedValues)
        {
            SetupCustomGeometry(shape.ShapeProperties, shape.DocumentModel, path, formulas, namedValues);
        }

        public static void SetupCustomGeometry(ShapeProperties shapeProperties, IDocumentModel documentModel, string path, VmlSingleFormulasCollection formulas, VmlFormulaNamedValues namedValues)
        {
            if (!string.IsNullOrEmpty(path))
            {
                Guard.ArgumentNotNull(namedValues, "namedValues");
                VmlPathParser parser = new VmlPathParser(new VmlShapeGuideCalculator(CreateVmlFakeGeometry(formulas, namedValues), (double) namedValues.EmuWidth, (double) namedValues.EmuHeight, new ModelShapeGuideList(FakeDocumentModel.Instance)));
                parser.Parse(path);
                parser.OffsetPath(namedValues.CoordOriginX, namedValues.CoordOriginY);
                ModelShapeCustomGeometry customGeometry = shapeProperties.CustomGeometry;
                ModelShapePath item = new ModelShapePath(documentModel.MainPart) {
                    Width = namedValues.Width,
                    Height = namedValues.Height
                };
                customGeometry.Paths.Add(item);
                BinaryDrawingImportHelper.CreateComplexPath(customGeometry, parser.MsoPathInfos, parser.Vertices, new XlsAdjustableCoordinateCache(), new XlsAdjustableAngleCache());
            }
        }

        public static void SetupDrawingFill(ShapeProperties shapeProperties, VmlShapeFillProperties fillProperties, Color fillColor)
        {
            if (fillProperties == null)
            {
                shapeProperties.Fill = CreateDrawingSolidFillCore(shapeProperties.DocumentModel, fillColor, 1f);
            }
            else
            {
                shapeProperties.Fill = SetupDrawingFillCore(shapeProperties.DocumentModel, shapeProperties.ShapeType, fillProperties, fillColor);
            }
        }

        private static IDrawingFill SetupDrawingFillCore(IDocumentModel documentModel, ShapePreset shapeType, VmlShapeFillProperties fillProperties, Color fillColor)
        {
            switch (fillProperties.Type)
            {
                case VmlFillType.Solid:
                    return CreateDrawingSolidFill(documentModel, fillProperties, fillColor);

                case VmlFillType.Gradient:
                case VmlFillType.GradientRadial:
                    return CreateGradientFill(documentModel, shapeType, fillProperties, fillColor);

                case VmlFillType.Tile:
                    return CreateTileFill(documentModel, fillProperties);

                case VmlFillType.Pattern:
                    return CreatePatternFill(documentModel, fillProperties, fillColor);

                case VmlFillType.Frame:
                    return CreateFrameFill(documentModel, fillProperties);
            }
            return DrawingFill.None;
        }

        private static void SetupDrawingShapeProtections(IDrawingLocks locks, VmlShapeProtections vmlShapeProtections)
        {
            SetupCommonShapeProtections(locks, vmlShapeProtections);
            bool? position = vmlShapeProtections.Position;
            locks.NoResize = (position != null) ? position.GetValueOrDefault() : false;
            position = vmlShapeProtections.Rotation;
            locks.NoRotate = (position != null) ? position.GetValueOrDefault() : false;
            position = vmlShapeProtections.Vertices;
            locks.NoEditPoints = (position != null) ? position.GetValueOrDefault() : false;
            position = vmlShapeProtections.AdjustHandles;
            locks.NoAdjustHandles = (position != null) ? position.GetValueOrDefault() : false;
            position = vmlShapeProtections.ShapeType;
            locks.NoChangeShapeType = (position != null) ? position.GetValueOrDefault() : false;
        }

        private static void SetupFillColor(IDrawingColor drawingColor, Color color)
        {
            drawingColor.Rgb = color;
        }

        private static void SetupFillRectangle(DrawingGradientFill fill, VmlShapeFillProperties fillProperties)
        {
            if (fill.GradientType != GradientType.Linear)
            {
                int bottomOffset = (int) Math.Round((double) ((1.0 - (fillProperties.FocusPosition.Top + fillProperties.FocusSize.Height)) * 100000.0));
                fill.FillRect = new RectangleOffset(bottomOffset, (int) Math.Round((double) (fillProperties.FocusPosition.Left * 100000f)), (int) Math.Round((double) ((1.0 - (fillProperties.FocusPosition.Left + fillProperties.FocusSize.Width)) * 100000.0)), (int) Math.Round((double) (fillProperties.FocusPosition.Top * 100000f)));
            }
        }

        private static void SetupGradientAngle(DrawingGradientFill fill, VmlShapeFillProperties fillProperties)
        {
            if (fill.GradientType == GradientType.Linear)
            {
                fill.Angle = BinaryDrawingImportHelper.SetupGradientAngleCore((double) fillProperties.Angle);
            }
        }

        private static void SetupGradientStops(IDocumentModel documentModel, DrawingGradientFill fill, VmlShapeFillProperties fillProperties, Color fillColor)
        {
            Color first = (fillProperties.Color != DXColor.White) ? fillProperties.Color : fillColor;
            Color last = fillProperties.Color2;
            float opacity = fillProperties.Opacity;
            float num2 = fillProperties.Opacity2;
            VmlIntermediateColors intermediateColors = (fillProperties.Colors.Count > 0) ? fillProperties.Colors : CreateIntermediateColors(first, last);
            List<double> list = CalculateOpacities(intermediateColors, (double) opacity, (double) num2);
            List<Tuple<OfficeShadeColor, double>> list2 = new List<Tuple<OfficeShadeColor, double>>();
            float focus = fillProperties.Focus;
            if (focus <= -1f)
            {
                focus = 1f;
            }
            if (focus > 0f)
            {
                focus = Math.Min(focus, 1f);
                int num4 = 0;
                while (true)
                {
                    if (num4 >= intermediateColors.Count)
                    {
                        if (focus < 1f)
                        {
                            for (int j = intermediateColors.Count - 2; j >= 0; j--)
                            {
                                OfficeShadeColor color4 = intermediateColors[j];
                                double position = 1.0 - (color4.Position * (1f - focus));
                                list2.Add(new Tuple<OfficeShadeColor, double>(new OfficeShadeColor(color4.ColorRecord.Color, position), list[j]));
                            }
                        }
                        break;
                    }
                    OfficeShadeColor color3 = intermediateColors[num4];
                    list2.Add(new Tuple<OfficeShadeColor, double>(new OfficeShadeColor(color3.ColorRecord.Color, color3.Position * focus), list[num4]));
                    num4++;
                }
            }
            else
            {
                int num7 = intermediateColors.Count - 1;
                while (true)
                {
                    if (num7 < 0)
                    {
                        if (focus < 0f)
                        {
                            for (int j = 1; j < intermediateColors.Count; j++)
                            {
                                OfficeShadeColor color6 = intermediateColors[j];
                                double num10 = ((1.0 - color6.Position) * focus) + 1.0;
                                list2.Add(new Tuple<OfficeShadeColor, double>(new OfficeShadeColor(color6.ColorRecord.Color, num10), list[j]));
                            }
                        }
                        break;
                    }
                    OfficeShadeColor color5 = intermediateColors[num7];
                    double position = (1.0 - color5.Position) * (focus + 1f);
                    list2.Add(new Tuple<OfficeShadeColor, double>(new OfficeShadeColor(color5.ColorRecord.Color, position), list[num7]));
                    num7--;
                }
            }
            GradientStopCollection gradientStops = fill.GradientStops;
            for (int i = 0; i < list2.Count; i++)
            {
                Tuple<OfficeShadeColor, double> tuple = list2[i];
                DrawingGradientStop item = new DrawingGradientStop(documentModel);
                OfficeShadeColor color7 = tuple.Item1;
                SetupFillColor(item.Color, color7.ColorRecord.Color);
                BinaryDrawingImportHelper.SetupFillOpacity(item.Color, tuple.Item2);
                item.Position = (int) Math.Round((double) (color7.Position * 100000.0));
                gradientStops.Add(item);
            }
        }

        public static void SetupGroupShapeProtections(IGroupLocks locks, VmlShapeProtections vmlShapeProtections)
        {
            SetupCommonShapeProtections(locks, vmlShapeProtections);
            bool? ungrouping = vmlShapeProtections.Ungrouping;
            locks.NoUngroup = (ungrouping != null) ? ungrouping.GetValueOrDefault() : false;
            ungrouping = vmlShapeProtections.Rotation;
            locks.NoRotate = (ungrouping != null) ? ungrouping.GetValueOrDefault() : false;
            ungrouping = vmlShapeProtections.Position;
            locks.NoResize = (ungrouping != null) ? ungrouping.GetValueOrDefault() : false;
        }

        public static void SetupImageData(ShapeProperties shapeProperties, VmlShapeImageData imageData)
        {
            DrawingBlipFill fill = DrawingBlipFill.Create(shapeProperties.DocumentModel, imageData.Image);
            fill.SetSourceRectangleCore(new RectangleOffset(CalculateCropUnit(imageData.CropBottom), CalculateCropUnit(imageData.CropLeft), CalculateCropUnit(imageData.CropRight), CalculateCropUnit(imageData.CropTop)));
            float gain = GetFloatOrVulgarFractionValue(imageData.Gain, 1f, 65536f);
            float blacklevel = GetFloatOrVulgarFractionValue(imageData.BlackLevel, 0f, 65536f);
            if ((gain != 1f) && (blacklevel != 0f))
            {
                fill.Blip.Effects.Add(new LuminanceEffect(ConvertBlacklevelToBrightness(blacklevel), ConvertGainToContrast(gain)));
            }
            shapeProperties.Fill = fill;
        }

        public static void SetupOutlineProperties(ShapeProperties shapeProperties, VmlLineStrokeSettings strokeSettings, Color strokeColor, int strokeWeight)
        {
            SetupOutlineProperties(shapeProperties, strokeSettings, strokeColor, strokeWeight, true);
        }

        public static void SetupOutlineProperties(ShapeProperties shapeProperties, VmlLineStrokeSettings strokeSettings, Color strokeColor, int strokeWeight, bool hasArrows)
        {
            Outline outline = shapeProperties.Outline;
            outline.BeginUpdate();
            try
            {
                outline.Dashing = (strokeSettings != null) ? GetOutlineDashing(strokeSettings.DashStyle) : OutlineDashing.Solid;
                int num = shapeProperties.DocumentModel.UnitConverter.PointsToModelUnits(1);
                outline.Width = ((strokeSettings == null) || (strokeSettings.StrokeWeight == num)) ? strokeWeight : strokeSettings.StrokeWeight;
                outline.CompoundType = (strokeSettings != null) ? GetOutlineCompoundType(strokeSettings.LineStyle) : OutlineCompoundType.Single;
                if (hasArrows)
                {
                    outline.HeadType = (strokeSettings != null) ? GetOutlineHeadTailType(strokeSettings.StartArrowType) : OutlineHeadTailType.None;
                    outline.HeadLength = (strokeSettings != null) ? GetOutlineHeadTailSize(strokeSettings.StartArrowLength) : OutlineHeadTailSize.Medium;
                    outline.HeadWidth = (strokeSettings != null) ? GetOutlineHeadTailSize(strokeSettings.StartArrowWidth) : OutlineHeadTailSize.Medium;
                    outline.TailType = (strokeSettings != null) ? GetOutlineHeadTailType(strokeSettings.EndArrowType) : OutlineHeadTailType.None;
                    outline.TailLength = (strokeSettings != null) ? GetOutlineHeadTailSize(strokeSettings.EndArrowLength) : OutlineHeadTailSize.Medium;
                    outline.TailWidth = (strokeSettings != null) ? GetOutlineHeadTailSize(strokeSettings.EndArrowWidth) : OutlineHeadTailSize.Medium;
                }
                SetFill(outline, strokeSettings, strokeColor);
            }
            finally
            {
                outline.EndUpdate();
            }
        }

        private static DrawingPatternType SetupPatternType(OfficeImage image, DrawingPatternType defaultPatternType) => 
            defaultPatternType;

        public static void SetupPictureProtections(IDrawingLocks locks, VmlShapeProtections vmlShapeProtections)
        {
            SetupDrawingShapeProtections(locks, vmlShapeProtections);
            IPictureLocks locks2 = locks as IPictureLocks;
            if (locks2 != null)
            {
                bool? cropping = vmlShapeProtections.Cropping;
                locks2.NoCrop = (cropping != null) ? cropping.GetValueOrDefault() : false;
            }
        }

        public static void SetupShadowProperties(ShapeProperties shapeProperties, VmlShadowEffect shadow)
        {
            DocumentModelUnitConverter unitConverter = shapeProperties.DocumentModel.UnitConverter;
            long distance = BinaryDrawingImportHelper.CalculateDistance((double) unitConverter.ModelUnitsToEmuF((float) shadow.Offset.X), (double) unitConverter.ModelUnitsToEmuF((float) shadow.Offset.Y));
            int direction = BinaryDrawingImportHelper.CalculateDirection((double) shadow.Offset.X, (double) shadow.Offset.Y);
            DrawingColor color = DrawingColor.Create(shapeProperties.DocumentModel, shadow.Color);
            if (shadow.Opacity != 0f)
            {
                color.Transforms.Add(new AlphaColorTransform((int) Math.Round((double) (shadow.Opacity * 100000f))));
            }
            if (shadow.ShadowType == VmlShadowType.Double)
            {
                AddEffect(shapeProperties, new PresetShadowEffect(color, PresetShadowType.TopLeftDoubleDrop, new OffsetShadowInfo(distance, direction)));
            }
            else if (shadow.ShadowType == VmlShadowType.Emboss)
            {
                AddEffect(shapeProperties, new PresetShadowEffect(color, PresetShadowType.OuterBox3d, new OffsetShadowInfo(distance, direction)));
            }
            else
            {
                VmlMatrix matrix = shadow.Matrix;
                bool flag = IsShadowPerspective(shadow);
                int sx = flag ? ((int) Math.Round((double) (matrix.Sxx * 100000f))) : 0x186a0;
                int sy = flag ? ((int) Math.Round((double) (matrix.Syy * 100000f))) : 0x186a0;
                int kx = flag ? ((int) Math.Round((double) (((Math.Atan((matrix.Sxx != 0f) ? ((double) (matrix.Sxy / matrix.Sxx)) : ((double) matrix.Sxy)) * 180.0) / 3.1415926535897931) * 60000.0))) : 0;
                AddEffect(shapeProperties, new OuterShadowEffect(color, OuterShadowEffectInfo.Create(0L, distance, direction, sx, sy, kx, flag ? ((int) Math.Round((double) (((Math.Atan((matrix.Syy != 0f) ? ((double) (matrix.Syx / matrix.Syy)) : ((double) matrix.Syx)) * 180.0) / 3.1415926535897931) * 60000.0))) : 0, BinaryDrawingImportHelper.GetShadowAlignCore((double) shadow.OriginX, (double) shadow.OriginY), false)));
            }
        }

        public static void SetupShapeProtections(IDrawingLocks locks, VmlShapeProtections vmlShapeProtections)
        {
            SetupDrawingShapeProtections(locks, vmlShapeProtections);
            IShapeLocks locks2 = locks as IShapeLocks;
            if (locks2 != null)
            {
                bool? text = vmlShapeProtections.Text;
                locks2.NoTextEdit = (text != null) ? text.GetValueOrDefault() : false;
            }
        }

        public static void SetupShapeTransform(Transform2D transform2D, VmlShapeStyleProperties styleProperties)
        {
            DocumentModelUnitConverter unitConverter = transform2D.DocumentModel.UnitConverter;
            float modelUnitValue = GetModelUnitValue(styleProperties.GetProperty("left"), unitConverter);
            SetupShapeTransform(transform2D, styleProperties, modelUnitValue, GetModelUnitValue(styleProperties.GetProperty("top"), unitConverter), GetModelUnitValue(styleProperties.GetProperty("width"), unitConverter), GetModelUnitValue(styleProperties.GetProperty("height"), unitConverter));
        }

        public static void SetupShapeTransform(Transform2D transform2D, VmlShapeStyleProperties styleProperties, float x, float y, float width, float height)
        {
            string property = styleProperties.GetProperty("flip");
            transform2D.SetFlipHCore(property.Contains("x"));
            transform2D.SetFlipVCore(property.Contains("y"));
            int rotation = GetRotation(styleProperties.GetProperty("rotation"));
            if (rotation != 0)
            {
                if (transform2D.FlipH)
                {
                    rotation = 0x1499700 - rotation;
                }
                if (transform2D.FlipV)
                {
                    rotation = 0x1499700 - rotation;
                }
                transform2D.SetRotationCore(rotation);
            }
            transform2D.SetCxCore(width);
            transform2D.SetCyCore(height);
            transform2D.SetOffsetXCore(x);
            transform2D.SetOffsetYCore(y);
        }
    }
}

