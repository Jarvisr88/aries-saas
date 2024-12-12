namespace DevExpress.Office.Import.Binary
{
    using DevExpress.Export.Binary;
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Model;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public static class BinaryDrawingImportHelper
    {
        private const string DefaultGuideName = "G";
        private static object syncShapeGuideCalculatedParamDescriptions = new object();
        private static Dictionary<int, string> shapeGuideCalculatedParamDescriptions = CreateShapeGuideCalculatedParamDescriptions();
        private static Dictionary<int, DrawingPatternType> patternsByTag;
        public const int DefaultLineOpacity = 0x10000;
        public const int DefaultLineWidthInEmu = 0x2535;
        public const int MaxAngle = 0x1499700;

        static BinaryDrawingImportHelper()
        {
            Dictionary<int, DrawingPatternType> dictionary1 = new Dictionary<int, DrawingPatternType>();
            dictionary1.Add(0xc4, DrawingPatternType.Percent5);
            dictionary1.Add(0xc5, DrawingPatternType.Percent50);
            dictionary1.Add(0xc6, DrawingPatternType.LightDownwardDiagonal);
            dictionary1.Add(0xc7, DrawingPatternType.LightVertical);
            dictionary1.Add(200, DrawingPatternType.DashedDownwardDiagonal);
            dictionary1.Add(0xc9, DrawingPatternType.ZigZag);
            dictionary1.Add(0xca, DrawingPatternType.Divot);
            dictionary1.Add(0xcb, DrawingPatternType.SmallGrid);
            dictionary1.Add(0xcc, DrawingPatternType.Percent10);
            dictionary1.Add(0xcd, DrawingPatternType.Percent60);
            dictionary1.Add(0xce, DrawingPatternType.LightUpwardDiagonal);
            dictionary1.Add(0xcf, DrawingPatternType.LightHorizontal);
            dictionary1.Add(0xd0, DrawingPatternType.DashedUpwardDiagonal);
            dictionary1.Add(0xd1, DrawingPatternType.Wave);
            dictionary1.Add(210, DrawingPatternType.DottedGrid);
            dictionary1.Add(0xd3, DrawingPatternType.LargeGrid);
            dictionary1.Add(0xd4, DrawingPatternType.Percent20);
            dictionary1.Add(0xd5, DrawingPatternType.Percent70);
            dictionary1.Add(0xd6, DrawingPatternType.DarkDownwardDiagonal);
            dictionary1.Add(0xd7, DrawingPatternType.NarrowVertical);
            dictionary1.Add(0xd8, DrawingPatternType.DashedHorizontal);
            dictionary1.Add(0xd9, DrawingPatternType.DiagonalBrick);
            dictionary1.Add(0xda, DrawingPatternType.DottedDiamond);
            dictionary1.Add(0xdb, DrawingPatternType.SmallCheckerBoard);
            dictionary1.Add(220, DrawingPatternType.Percent25);
            dictionary1.Add(0xdd, DrawingPatternType.Percent75);
            dictionary1.Add(0xde, DrawingPatternType.DarkUpwardDiagonal);
            dictionary1.Add(0xdf, DrawingPatternType.NarrowHorizontal);
            dictionary1.Add(0xe0, DrawingPatternType.DashedVertical);
            dictionary1.Add(0xe1, DrawingPatternType.HorizontalBrick);
            dictionary1.Add(0xe2, DrawingPatternType.Shingle);
            dictionary1.Add(0xe3, DrawingPatternType.LargeCheckerBoard);
            dictionary1.Add(0xe4, DrawingPatternType.Percent30);
            dictionary1.Add(0xe5, DrawingPatternType.Percent80);
            dictionary1.Add(230, DrawingPatternType.WideDownwardDiagonal);
            dictionary1.Add(0xe7, DrawingPatternType.DarkVertical);
            dictionary1.Add(0xe8, DrawingPatternType.SmallConfetti);
            dictionary1.Add(0xe9, DrawingPatternType.Weave);
            dictionary1.Add(0xea, DrawingPatternType.Trellis);
            dictionary1.Add(0xeb, DrawingPatternType.OpenDiamond);
            dictionary1.Add(0xec, DrawingPatternType.Percent40);
            dictionary1.Add(0xed, DrawingPatternType.Percent90);
            dictionary1.Add(0xee, DrawingPatternType.WideUpwardDiagonal);
            dictionary1.Add(0xef, DrawingPatternType.DarkHorizontal);
            dictionary1.Add(240, DrawingPatternType.LargeConfetti);
            dictionary1.Add(0xf1, DrawingPatternType.Plaid);
            dictionary1.Add(0xf2, DrawingPatternType.Sphere);
            dictionary1.Add(0xf3, DrawingPatternType.SolidDiamond);
            patternsByTag = dictionary1;
        }

        private static void AddGuide(ModelShapeCustomGeometry customGeometry, string name, int value, XlsAdjustableCoordinateCache adjustableCoordinateCache)
        {
            ModelShapeGuideFormula formula = new ModelShapeGuideFormula(ModelShapeGuideFormulaType.Value, adjustableCoordinateCache.GetCachedAdjustableCoordinate(value), null, null);
            ModelShapeGuide item = new ModelShapeGuide(name, formula);
            customGeometry.Guides.Add(item);
        }

        public static int CalculateDirection(double shadowOffsetX, double shadowOffsetY)
        {
            int num2 = (int) Math.Round(ShapeGuideCalculator.RadianToEMUDegree(Math.Atan2(shadowOffsetY, shadowOffsetX)));
            if (num2 < 0)
            {
                num2 += 0x1499700;
            }
            return (num2 % 0x1499700);
        }

        public static long CalculateDistance(double shadowOffsetX, double shadowOffsetY) => 
            (long) Math.Round(Math.Sqrt(Math.Pow(shadowOffsetX, 2.0) + Math.Pow(shadowOffsetY, 2.0)));

        private static int CalculateHorizontalFactor(OfficeArtPropertiesBase artProperties) => 
            CalculateScalingFactorCore(GetShadowHorizontalScalingFactor(artProperties));

        private static int CalculateHorizontalSkewAngle(OfficeArtPropertiesBase artProperties) => 
            CalculateSkewAngleCore(GetShadowHorizontalSkewAngle(artProperties), GetShadowHorizontalScalingFactor(artProperties));

        private static Point[] CalculatePoints(DrawingGeometryPoint[] points, ShapeGuideCalculator calculator)
        {
            if ((points == null) || (points.Length == 0))
            {
                return null;
            }
            Point[] pointArray = new Point[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                DrawingGeometryPoint point = points[i];
                int x = (int) Math.Round(point.X.IsConstant ? ((double) point.X.Value) : calculator.GetGuideValue("G" + point.X.GuideIndex.ToString()));
                pointArray[i] = new Point(x, (int) Math.Round(point.Y.IsConstant ? ((double) point.Y.Value) : calculator.GetGuideValue("G" + point.Y.GuideIndex.ToString())));
            }
            return pointArray;
        }

        private static int CalculateScalingFactorCore(int value)
        {
            double num = 1.52587890625;
            return (int) Math.Round((double) (value * num));
        }

        private static AdjustableCoordinate CalculateShapeGuideParam(int value, XlsAdjustableCoordinateCache adjustableCoordinateCache)
        {
            string str;
            return (((value < 0x400) || (value > 0x47f)) ? (!ShapeGuideCalculatedParamDescriptions.TryGetValue(value, out str) ? adjustableCoordinateCache.GetCachedAdjustableCoordinate(0) : adjustableCoordinateCache.GetCachedAdjustableCoordinate(str)) : adjustableCoordinateCache.GetCachedAdjustableCoordinate("G" + (value - 0x400).ToString()));
        }

        public static int CalculateSkewAngleCore(int value, int scalingFactor) => 
            (scalingFactor != 0) ? (((int) Math.Round((double) ((Math.Atan(((double) value) / ((double) scalingFactor)) * 180.0) / 3.1415926535897931), 2)) * 0xea60) : 0;

        private static int CalculateVerticalFactor(OfficeArtPropertiesBase artProperties) => 
            CalculateScalingFactorCore(GetShadowVerticalScalingFactor(artProperties));

        private static int CalculateVerticalSkewAngle(OfficeArtPropertiesBase artProperties) => 
            CalculateSkewAngleCore(GetShadowVerticalSkewAngle(artProperties), GetShadowVerticalScalingFactor(artProperties));

        private static OpenXmlBlackWhiteMode ConvertBlackWhiteMode(BlackWhiteMode mode)
        {
            switch (mode)
            {
                case BlackWhiteMode.Normal:
                    return OpenXmlBlackWhiteMode.Clr;

                case BlackWhiteMode.GrayScale:
                    return OpenXmlBlackWhiteMode.Gray;

                case BlackWhiteMode.LightGrayScale:
                    return OpenXmlBlackWhiteMode.LtGray;

                case BlackWhiteMode.InverseGray:
                    return OpenXmlBlackWhiteMode.InvGray;

                case BlackWhiteMode.GrayOutline:
                    return OpenXmlBlackWhiteMode.GrayWhite;

                case BlackWhiteMode.BlackTextLine:
                    return OpenXmlBlackWhiteMode.BlackGray;

                case BlackWhiteMode.HighContrast:
                    return OpenXmlBlackWhiteMode.BlackWhite;

                case BlackWhiteMode.Black:
                    return OpenXmlBlackWhiteMode.Black;

                case BlackWhiteMode.White:
                    return OpenXmlBlackWhiteMode.White;

                case BlackWhiteMode.DontShow:
                    return OpenXmlBlackWhiteMode.Hidden;
            }
            return OpenXmlBlackWhiteMode.Auto;
        }

        private static ModelShapeGuideFormulaType ConvertFormulaType(ShapeGuideFormula formula)
        {
            switch (formula)
            {
                case ShapeGuideFormula.Sum:
                    return ModelShapeGuideFormulaType.AddSubtract;

                case ShapeGuideFormula.Product:
                    return ModelShapeGuideFormulaType.MultiplyDivide;

                case ShapeGuideFormula.Mid:
                    return ModelShapeGuideFormulaType.Mid;

                case ShapeGuideFormula.Absolute:
                    return ModelShapeGuideFormulaType.Abs;

                case ShapeGuideFormula.Min:
                    return ModelShapeGuideFormulaType.Min;

                case ShapeGuideFormula.Max:
                    return ModelShapeGuideFormulaType.Max;

                case ShapeGuideFormula.If:
                    return ModelShapeGuideFormulaType.IfElse;

                case ShapeGuideFormula.Mod:
                    return ModelShapeGuideFormulaType.Mod;

                case ShapeGuideFormula.ATan2:
                    return ModelShapeGuideFormulaType.ArcTan;

                case ShapeGuideFormula.Sin:
                    return ModelShapeGuideFormulaType.Sin;

                case ShapeGuideFormula.Cos:
                    return ModelShapeGuideFormulaType.Cos;

                case ShapeGuideFormula.CosATan2:
                    return ModelShapeGuideFormulaType.CosArcTan;

                case ShapeGuideFormula.SinATan2:
                    return ModelShapeGuideFormulaType.SinArcTan;

                case ShapeGuideFormula.Sqrt:
                    return ModelShapeGuideFormulaType.Sqrt;

                case ShapeGuideFormula.SumAngle:
                    return ModelShapeGuideFormulaType.SumAngle;

                case ShapeGuideFormula.Ellipse:
                    return ModelShapeGuideFormulaType.Ellipse;

                case ShapeGuideFormula.Tan:
                    return ModelShapeGuideFormulaType.Tan;
            }
            return ModelShapeGuideFormulaType.Undefined;
        }

        internal static void CreateComplexPath(ModelShapeCustomGeometry customGeometry, MsoPathInfo[] msoPathInfos, Point[] points, XlsAdjustableCoordinateCache adjustableCoordinateCache, XlsAdjustableAngleCache adjustableAngleCache)
        {
            if ((points != null) && ((points.Length != 0) && ((msoPathInfos != null) && (msoPathInfos.Length != 0))))
            {
                new CustomGeometryPathBuilder(customGeometry, points, msoPathInfos, adjustableCoordinateCache, adjustableAngleCache).CreateComplexPath();
            }
        }

        private static void CreateConnectionSites(Point[] connectionSitesPoints, FixedPoint[] connectionSitesDirPoints, ModelShapeCustomGeometry geometry, XlsAdjustableCoordinateCache adjustableCoordinateCache, XlsAdjustableAngleCache adjustableAngleCache)
        {
            if ((connectionSitesPoints != null) && (connectionSitesDirPoints != null))
            {
                for (int i = 0; i < Math.Min(connectionSitesPoints.Length, connectionSitesDirPoints.Length); i++)
                {
                    Point point = connectionSitesPoints[i];
                    FixedPoint point2 = connectionSitesDirPoints[i];
                    int num2 = (int) Math.Round((double) (point2.Value * 60000.0));
                    ModelShapeConnection item = new ModelShapeConnection(adjustableAngleCache.GetCachedAdjustableAngle(num2), adjustableCoordinateCache.GetCachedAdjustableCoordinate(point.X), adjustableCoordinateCache.GetCachedAdjustableCoordinate(point.Y));
                    geometry.ConnectionSites.Add(item);
                }
            }
        }

        private static void CreateCurvesPath(Point[] points, bool closed, ModelShapePath path, XlsAdjustableCoordinateCache adjustableCoordinateCache)
        {
            if ((points != null) && (points.Length != 0))
            {
                path.Instructions.Add(new PathMove(adjustableCoordinateCache.GetCachedAdjustableCoordinate(points[0].X), adjustableCoordinateCache.GetCachedAdjustableCoordinate(points[0].Y)));
                for (int i = 1; (i + 2) < points.Length; i += 3)
                {
                    path.Instructions.Add(new PathCubicBezier(path.DocumentModelPart, adjustableCoordinateCache.GetCachedAdjustableCoordinate(points[i].X), adjustableCoordinateCache.GetCachedAdjustableCoordinate(points[i].Y), adjustableCoordinateCache.GetCachedAdjustableCoordinate(points[i + 1].X), adjustableCoordinateCache.GetCachedAdjustableCoordinate(points[i + 1].Y), adjustableCoordinateCache.GetCachedAdjustableCoordinate(points[i + 2].X), adjustableCoordinateCache.GetCachedAdjustableCoordinate(points[i + 2].Y)));
                }
                if (closed)
                {
                    path.Instructions.Add(new PathClose());
                }
            }
        }

        public static IDrawingFill CreateDrawingFill(IDocumentModel documentModel, Palette palette, OfficeArtBlipStoreContainer blipContainer, OfficeArtPropertiesBase artProperties, OfficeArtTertiaryProperties tertiaryArtProperties)
        {
            OfficeFillType fillType = GetFillType(artProperties);
            switch (fillType)
            {
                case OfficeFillType.Solid:
                    return CreateShapeSolidFill(documentModel, palette, artProperties);

                case OfficeFillType.Pattern:
                    return CreateShapePatternFill(documentModel, palette, artProperties, blipContainer);

                case OfficeFillType.Texture:
                    return CreateShapeTextureFill(documentModel, artProperties, blipContainer);

                case OfficeFillType.Picture:
                    return CreateShapePictureFill(documentModel, artProperties, blipContainer);

                case OfficeFillType.Shade:
                case OfficeFillType.ShadeCenter:
                case OfficeFillType.ShadeShape:
                case OfficeFillType.ShadeScale:
                case OfficeFillType.ShadeTile:
                    return CreateShapeGradientFill(documentModel, palette, fillType, artProperties, tertiaryArtProperties);

                case OfficeFillType.Background:
                    return DrawingFill.Group;
            }
            return null;
        }

        private static ModelShapeCustomGeometry CreateFakeCustomGeometry(IEnumerable<object> properties, int widthEmu, int heightEmu, DocumentModelUnitConverter unitConverter, XlsAdjustableCoordinateCache adjustableCoordinateCache)
        {
            ModelShapeCustomGeometry customGeometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            SetupCustomAdjustValues(customGeometry, properties, adjustableCoordinateCache);
            SetupCalculatedValues(customGeometry, properties, widthEmu, heightEmu, unitConverter, adjustableCoordinateCache);
            SetupShapeGuides(customGeometry, properties, adjustableCoordinateCache);
            return customGeometry;
        }

        private static void CreateLinesPath(Point[] points, bool closed, ModelShapePath path, XlsAdjustableCoordinateCache adjustableCoordinateCache)
        {
            if ((points != null) && (points.Length != 0))
            {
                path.Instructions.Add(new PathMove(adjustableCoordinateCache.GetCachedAdjustableCoordinate(points[0].X), adjustableCoordinateCache.GetCachedAdjustableCoordinate(points[0].Y)));
                for (int i = 1; i < points.Length; i++)
                {
                    path.Instructions.Add(new PathLine(adjustableCoordinateCache.GetCachedAdjustableCoordinate(points[i].X), adjustableCoordinateCache.GetCachedAdjustableCoordinate(points[i].Y)));
                }
                if (closed)
                {
                    path.Instructions.Add(new PathClose());
                }
            }
        }

        private static ModelShapeGuideFormula CreateModelShapeGuideFormula(ShapeGuide guide, XlsAdjustableCoordinateCache adjustableCoordinateCache)
        {
            ModelShapeGuideFormulaType type = ConvertFormulaType(guide.Formula);
            AdjustableCoordinate coordinate = guide.CalculatedParam1 ? CalculateShapeGuideParam(guide.Param1, adjustableCoordinateCache) : adjustableCoordinateCache.GetCachedAdjustableCoordinate(guide.Param1);
            return new ModelShapeGuideFormula(type, coordinate, guide.CalculatedParam2 ? CalculateShapeGuideParam(guide.Param2, adjustableCoordinateCache) : adjustableCoordinateCache.GetCachedAdjustableCoordinate(guide.Param2), guide.CalculatedParam3 ? CalculateShapeGuideParam(guide.Param3, adjustableCoordinateCache) : adjustableCoordinateCache.GetCachedAdjustableCoordinate(guide.Param3));
        }

        private static IDrawingFill CreateShapeGradientFill(IDocumentModel documentModel, Palette palette, OfficeFillType fillType, OfficeArtPropertiesBase artProperties, OfficeArtTertiaryProperties tertiaryArtProperties)
        {
            DrawingGradientFill fill = new DrawingGradientFill(documentModel);
            if (fillType == OfficeFillType.ShadeCenter)
            {
                fill.GradientType = GradientType.Rectangle;
                SetupFillRect(fill, artProperties);
            }
            else if (fillType != OfficeFillType.ShadeShape)
            {
                fill.GradientType = GradientType.Linear;
            }
            else
            {
                fill.GradientType = GradientType.Shape;
                fill.FillRect = new RectangleOffset(0xc350, 0xc350, 0xc350, 0xc350);
            }
            SetupGradientStops(fill, documentModel, palette, artProperties);
            SetupGradientAngle(fill, artProperties);
            SetupRotateWithShape(fill, artProperties, tertiaryArtProperties);
            return fill;
        }

        private static Dictionary<int, string> CreateShapeGuideCalculatedParamDescriptions() => 
            new Dictionary<int, string> { 
                { 
                    320,
                    "xcenter"
                },
                { 
                    0x141,
                    "ycenter"
                },
                { 
                    0x142,
                    "width"
                },
                { 
                    0x143,
                    "height"
                },
                { 
                    0x147,
                    "adj1"
                },
                { 
                    0x148,
                    "adj2"
                },
                { 
                    0x149,
                    "adj3"
                },
                { 
                    330,
                    "adj4"
                },
                { 
                    0x14b,
                    "adj5"
                },
                { 
                    0x14c,
                    "adj6"
                },
                { 
                    0x14d,
                    "adj7"
                },
                { 
                    0x14e,
                    "adj8"
                },
                { 
                    0x153,
                    "xlimo"
                },
                { 
                    340,
                    "ylimo"
                },
                { 
                    0x1fc,
                    "lineDrawn"
                },
                { 
                    0x4f7,
                    "pixellinewidth"
                },
                { 
                    0x4f8,
                    "pixelwidth"
                },
                { 
                    0x4f9,
                    "pixelheight"
                },
                { 
                    0x4fc,
                    "emuwidth"
                },
                { 
                    0x4fd,
                    "emuheight"
                },
                { 
                    0x4fe,
                    "emuwidth2"
                },
                { 
                    0x4ff,
                    "emuheight2"
                }
            };

        private static IDrawingFill CreateShapePatternFill(IDocumentModel documentModel, Palette palette, OfficeArtPropertiesBase artProperties, OfficeArtBlipStoreContainer blipContainer)
        {
            DrawingPatternFill fill = new DrawingPatternFill(documentModel);
            SetupColor(fill.ForegroundColor, GetColorRecord<DrawingFillColor>(artProperties), documentModel, palette, artProperties);
            SetupColor(fill.BackgroundColor, GetColorRecord<DrawingFillBackColor>(artProperties), documentModel, palette, artProperties);
            SetupFillOpacity(fill.ForegroundColor, GetFillOpacity<DrawingFillOpacity>(artProperties));
            SetupFillOpacity(fill.BackgroundColor, GetFillOpacity<DrawingFillBackOpacity>(artProperties));
            fill.PatternType = GetDrawingPatternType(artProperties, blipContainer);
            return fill;
        }

        private static IDrawingFill CreateShapePictureFill(IDocumentModel documentModel, OfficeArtPropertiesBase artProperties, OfficeArtBlipStoreContainer blipContainer)
        {
            OfficeImage image = GetImage(artProperties, blipContainer);
            return ((image != null) ? DrawingBlipFill.Create(documentModel, image) : null);
        }

        private static IDrawingFill CreateShapeSolidFill(IDocumentModel documentModel, Palette palette, OfficeArtPropertiesBase artProperties)
        {
            DrawingSolidFill fill = new DrawingSolidFill(documentModel);
            SetupFillColor(fill.Color, documentModel, palette, artProperties);
            double fillOpacity = GetFillOpacity<DrawingFillOpacity>(artProperties);
            SetupFillOpacity(fill.Color, fillOpacity);
            return fill;
        }

        private static IDrawingFill CreateShapeTextureFill(IDocumentModel documentModel, OfficeArtPropertiesBase artProperties, OfficeArtBlipStoreContainer blipContainer)
        {
            OfficeImage image = GetImage(artProperties, blipContainer);
            return ((image != null) ? CreateTextureBlipFill(documentModel, image) : null);
        }

        public static IDrawingFill CreateTextureBlipFill(IDocumentModel documentModel, OfficeImage image) => 
            new DrawingBlipFill(documentModel) { 
                Blip = { Image = image },
                Stretch = false,
                TileAlign = RectangleAlignType.TopLeft,
                TileFlip = TileFlipType.None,
                ScaleX = 0x186a0,
                ScaleY = 0x186a0,
                OffsetX = 0L,
                OffsetY = 0L
            };

        public static int?[] GetAdjustValues(OfficeArtPropertiesBase artProperties) => 
            (artProperties != null) ? GetAdjustValues(artProperties.Properties) : new int?[8];

        public static int?[] GetAdjustValues(IEnumerable<object> properties)
        {
            int?[] nullableArray = new int?[8];
            foreach (object obj2 in properties)
            {
                OfficeDrawingIntPropertyBase base2 = obj2 as OfficeDrawingIntPropertyBase;
                IDrawingGeometryAdjustValue value2 = base2 as IDrawingGeometryAdjustValue;
                if (value2 != null)
                {
                    nullableArray[value2.Index] = new int?(base2.Value);
                }
            }
            return nullableArray;
        }

        private static int GetAlphaColorTransform(int lineOpacity)
        {
            int num = 0x10000;
            return (int) Math.Round((double) ((((double) lineOpacity) / ((double) num)) * 100000.0));
        }

        public static OpenXmlBlackWhiteMode GetBlackAndWhiteMode(OfficeArtPropertiesBase artProperties) => 
            ConvertBlackWhiteMode(GetDrawingBlackWhiteMode(artProperties));

        public static int GetBlipTagIndex(DrawingPatternType patternType)
        {
            int num2;
            using (Dictionary<int, DrawingPatternType>.KeyCollection.Enumerator enumerator = patternsByTag.Keys.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        int current = enumerator.Current;
                        if (((DrawingPatternType) patternsByTag[current]) != patternType)
                        {
                            continue;
                        }
                        num2 = current;
                    }
                    else
                    {
                        return 0x21;
                    }
                    break;
                }
            }
            return num2;
        }

        private static OfficeColorRecord GetColorRecord<TColor>(OfficeArtPropertiesBase artProperties) where TColor: OfficeDrawingColorPropertyBase
        {
            OfficeDrawingColorPropertyBase officeArtProperty = GetOfficeArtProperty<TColor>(artProperties);
            return officeArtProperty?.ColorRecord;
        }

        private static DrawingGeometryPoint[] GetConnectionSites(IEnumerable<object> properties) => 
            OfficeArtPropertiesHelper.GetArrayOfElements<DrawingGeometryPoint, IDrawingGeometryConnectionSites>(properties);

        private static FixedPoint[] GetConnectionSitesDirections(IEnumerable<object> properties) => 
            OfficeArtPropertiesHelper.GetArrayOfElements<FixedPoint, IDrawingGeometryConnectionSitesDir>(properties);

        private static BlackWhiteMode GetDrawingBlackWhiteMode(OfficeArtPropertiesBase artProperties)
        {
            DrawingBlackWhiteMode officeArtProperty = GetOfficeArtProperty<DrawingBlackWhiteMode>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Mode : BlackWhiteMode.Automatic);
        }

        private static int GetDrawingFillBlipIdentifier(OfficeArtPropertiesBase artProperties)
        {
            DrawingFillBlipIdentifier officeArtProperty = GetOfficeArtProperty<DrawingFillBlipIdentifier>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 0);
        }

        private static int GetDrawingFillFocus(OfficeArtPropertiesBase artProperties)
        {
            DrawingFillFocus propertyByType = OfficeArtPropertiesHelper.GetPropertyByType(artProperties, typeof(DrawingFillFocus)) as DrawingFillFocus;
            return ((propertyByType != null) ? propertyByType.Value : 0);
        }

        private static MsoPathInfo[] GetDrawingGeometrySegmentInfos(IEnumerable<object> properties) => 
            OfficeArtPropertiesHelper.GetArrayOfElements<MsoPathInfo, IDrawingGeometrySegmentInfo>(properties);

        private static DrawingGeometryPoint[] GetDrawingGeometryVertices(IEnumerable<object> properties) => 
            OfficeArtPropertiesHelper.GetArrayOfElements<DrawingGeometryPoint, IDrawingGeometryVerticies>(properties);

        private static DrawingLineStyleBooleanProperties GetDrawingLineBooleanProperties(IEnumerable<object> properties) => 
            GetOfficeArtProperty<DrawingLineStyleBooleanProperties>(properties);

        public static DrawingPatternType GetDrawingPatternType(int tagIndex)
        {
            DrawingPatternType type;
            return (patternsByTag.TryGetValue(tagIndex, out type) ? type : DrawingPatternType.Percent50);
        }

        private static DrawingPatternType GetDrawingPatternType(OfficeArtPropertiesBase artProperties, OfficeArtBlipStoreContainer blipContainer)
        {
            DrawingPatternType type2;
            DrawingPatternType type = DrawingPatternType.Percent50;
            int drawingFillBlipIdentifier = GetDrawingFillBlipIdentifier(artProperties);
            if ((drawingFillBlipIdentifier == 0) || ((blipContainer == null) || (blipContainer.Blips.Count < drawingFillBlipIdentifier)))
            {
                return type;
            }
            BlipBase base2 = blipContainer.Blips[drawingFillBlipIdentifier - 1];
            return (patternsByTag.TryGetValue(base2.TagValue, out type2) ? type2 : type);
        }

        private static double GetFillOpacity<TOpacity>(OfficeArtPropertiesBase artProperties) where TOpacity: OfficeDrawingFixedPointPropertyBase
        {
            OfficeDrawingFixedPointPropertyBase propertyByType = OfficeArtPropertiesHelper.GetPropertyByType(artProperties, typeof(TOpacity)) as OfficeDrawingFixedPointPropertyBase;
            return ((propertyByType != null) ? propertyByType.Value : 1.0);
        }

        private static OfficeFillType GetFillType(OfficeArtPropertiesBase artProperties)
        {
            OfficeDrawingFillType officeArtProperty = GetOfficeArtProperty<OfficeDrawingFillType>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.FillType : OfficeFillType.Solid);
        }

        public static string GetHyperlinkTargetUri(BinaryHyperlinkObject hyperlink)
        {
            if (!hyperlink.HasMoniker)
            {
                if (hyperlink.HasLocationString)
                {
                    string location = hyperlink.Location;
                    if (!location.StartsWith("#"))
                    {
                        location = "#" + location;
                    }
                    return location;
                }
            }
            else if (hyperlink.IsMonkerSavedAsString)
            {
                Uri uri;
                if (Uri.TryCreate(hyperlink.Moniker, UriKind.RelativeOrAbsolute, out uri))
                {
                    return hyperlink.Moniker;
                }
            }
            else
            {
                if (hyperlink.OleMoniker.ClassId == BinaryHyperlinkMonikerFactory.CLSID_URLMoniker)
                {
                    return ((BinaryHyperlinkURLMoniker) hyperlink.OleMoniker).Url;
                }
                if (hyperlink.OleMoniker.ClassId == BinaryHyperlinkMonikerFactory.CLSID_FileMoniker)
                {
                    return ((BinaryHyperlinkFileMoniker) hyperlink.OleMoniker).Path;
                }
            }
            return null;
        }

        private static OfficeImage GetImage(OfficeArtPropertiesBase artProperties, OfficeArtBlipStoreContainer blipContainer)
        {
            int drawingFillBlipIdentifier = GetDrawingFillBlipIdentifier(artProperties);
            return (((drawingFillBlipIdentifier == 0) || ((blipContainer == null) || (blipContainer.Blips.Count < drawingFillBlipIdentifier))) ? null : blipContainer.Blips[drawingFillBlipIdentifier - 1].Image);
        }

        private static int GetLimoX(IEnumerable<object> properties)
        {
            IDrawingGeometryLimoX officeArtProperty = GetOfficeArtProperty<IDrawingGeometryLimoX>(properties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 0x7fffffff);
        }

        private static int GetLimoY(IEnumerable<object> properties)
        {
            IDrawingGeometryLimoY officeArtProperty = GetOfficeArtProperty<IDrawingGeometryLimoY>(properties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 0x7fffffff);
        }

        private static OutlineEndCapStyle GetLineCapStyle(OfficeArtPropertiesBase artProperties)
        {
            DrawingLineCapStyle officeArtProperty = GetOfficeArtProperty<DrawingLineCapStyle>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Style : OutlineEndCapStyle.Flat);
        }

        private static OutlineCompoundType GetLineCompoundType(OfficeArtPropertiesBase artProperties)
        {
            DrawingLineCompoundType officeArtProperty = GetOfficeArtProperty<DrawingLineCompoundType>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.CompoundType : OutlineCompoundType.Single);
        }

        private static OutlineDashing GetLineDashing(OfficeArtPropertiesBase artProperties)
        {
            DrawingLineDashing officeArtProperty = GetOfficeArtProperty<DrawingLineDashing>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Dashing : OutlineDashing.Solid);
        }

        private static MsoLineEnd GetLineEndArrowhead(OfficeArtPropertiesBase artProperties)
        {
            DrawingLineEndArrowhead officeArtProperty = GetOfficeArtProperty<DrawingLineEndArrowhead>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Arrowhead : MsoLineEnd.NoEnd);
        }

        private static OutlineHeadTailSize GetLineEndArrowheadLength(OfficeArtPropertiesBase artProperties)
        {
            DrawingLineEndArrowLength officeArtProperty = GetOfficeArtProperty<DrawingLineEndArrowLength>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.HeadTailSize : OutlineHeadTailSize.Medium);
        }

        private static OutlineHeadTailSize GetLineEndArrowheadWidth(OfficeArtPropertiesBase artProperties)
        {
            DrawingLineEndArrowWidth officeArtProperty = GetOfficeArtProperty<DrawingLineEndArrowWidth>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.HeadTailSize : OutlineHeadTailSize.Medium);
        }

        private static LineJoinStyle GetLineJoinStyle(OfficeArtPropertiesBase artProperties)
        {
            DrawingLineJoinStyle officeArtProperty = GetOfficeArtProperty<DrawingLineJoinStyle>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Style : LineJoinStyle.Miter);
        }

        private static double GetLineMiterLimit(OfficeArtPropertiesBase artProperties)
        {
            DrawingLineMiterLimit officeArtProperty = GetOfficeArtProperty<DrawingLineMiterLimit>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 8.0);
        }

        private static int GetLineOpacity(OfficeArtPropertiesBase artProperties)
        {
            DrawingLineOpacity officeArtProperty = GetOfficeArtProperty<DrawingLineOpacity>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 0x10000);
        }

        private static MsoLineEnd GetLineStartArrowhead(OfficeArtPropertiesBase artProperties)
        {
            DrawingLineStartArrowhead officeArtProperty = GetOfficeArtProperty<DrawingLineStartArrowhead>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Arrowhead : MsoLineEnd.NoEnd);
        }

        private static OutlineHeadTailSize GetLineStartArrowheadLength(OfficeArtPropertiesBase artProperties)
        {
            DrawingLineStartArrowLength officeArtProperty = GetOfficeArtProperty<DrawingLineStartArrowLength>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.HeadTailSize : OutlineHeadTailSize.Medium);
        }

        private static OutlineHeadTailSize GetLineStartArrowheadWidth(OfficeArtPropertiesBase artProperties)
        {
            DrawingLineStartArrowWidth officeArtProperty = GetOfficeArtProperty<DrawingLineStartArrowWidth>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.HeadTailSize : OutlineHeadTailSize.Medium);
        }

        private static int GetLineWidthInEMUs(IEnumerable<object> properties)
        {
            DrawingLineWidth officeArtProperty = GetOfficeArtProperty<DrawingLineWidth>(properties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 0x2535);
        }

        private static T GetOfficeArtProperty<T>(OfficeArtPropertiesBase artProperties) where T: IOfficeDrawingProperty => 
            (T) OfficeArtPropertiesHelper.GetPropertyByType(artProperties, typeof(T));

        private static T GetOfficeArtProperty<T>(IEnumerable<object> properties) => 
            (T) OfficeArtPropertiesHelper.GetPropertyByType(properties, typeof(T));

        private static double GetRelativePosition<TPos>(OfficeArtPropertiesBase artProperties) where TPos: OfficeDrawingFixedPointPropertyBase
        {
            OfficeDrawingFixedPointPropertyBase propertyByType = OfficeArtPropertiesHelper.GetPropertyByType(artProperties, typeof(TPos)) as TPos;
            return ((propertyByType == null) ? 0.0 : propertyByType.Value);
        }

        private static bool GetRotateWithShape(OfficeArtPropertiesBase artProperties, OfficeArtTertiaryProperties tertiaryArtProperties)
        {
            DrawingFillStyleBooleanProperties officeArtProperty = GetOfficeArtProperty<DrawingFillStyleBooleanProperties>(artProperties);
            DrawingFillStyleBooleanProperties properties2 = null;
            if (tertiaryArtProperties != null)
            {
                properties2 = GetOfficeArtProperty<DrawingFillStyleBooleanProperties>(tertiaryArtProperties);
            }
            return (((officeArtProperty == null) || (!officeArtProperty.UseShapeAnchor || !officeArtProperty.ShapeAnchor)) ? ((properties2 != null) && (properties2.UseShapeAnchor && properties2.ShapeAnchor)) : true);
        }

        private static float GetRotation(OfficeArtPropertiesBase artProperties)
        {
            DrawingRotation officeArtProperty = GetOfficeArtProperty<DrawingRotation>(artProperties);
            return ((officeArtProperty != null) ? ((float) officeArtProperty.Value) : 0f);
        }

        private static void GetShadeColors(List<OfficeShadeColor> shadeColors, List<double> shadeOpacities, OfficeArtPropertiesBase artProperties)
        {
            int drawingFillFocus = GetDrawingFillFocus(artProperties);
            double fillOpacity = GetFillOpacity<DrawingFillOpacity>(artProperties);
            GetShadeColorsCore(shadeColors, shadeOpacities, ((float) drawingFillFocus) / 100f, fillOpacity, GetFillOpacity<DrawingFillBackOpacity>(artProperties), GetColorRecord<DrawingFillColor>(artProperties), GetColorRecord<DrawingFillBackColor>(artProperties));
        }

        private static void GetShadeColors(List<OfficeShadeColor> shadeColors, List<double> shadeOpacities, OfficeShadeColor[] officeShadeColors, OfficeArtPropertiesBase artProperties)
        {
            int length = officeShadeColors.Length;
            shadeColors.AddRange(officeShadeColors);
            double fillOpacity = GetFillOpacity<DrawingFillOpacity>(artProperties);
            double num3 = GetFillOpacity<DrawingFillBackOpacity>(artProperties);
            double position = shadeColors[0].Position;
            double num5 = shadeColors[length - 1].Position;
            double num6 = (num5 == position) ? 0.0 : ((num3 - fillOpacity) / (num5 - position));
            for (int i = 0; i < length; i++)
            {
                double item = fillOpacity + (num6 * (shadeColors[i].Position - position));
                shadeOpacities.Add(item);
            }
        }

        public static void GetShadeColorsCore(List<OfficeShadeColor> shadeColors, List<double> shadeOpacities, float focus, double fillOpacity, double fillBackOpacity, OfficeColorRecord fillColor, OfficeColorRecord fillBackColor)
        {
            if ((focus <= -1.0) || (focus >= 1.0))
            {
                OfficeShadeColor item = new OfficeShadeColor();
                item.ColorRecord = fillColor;
                item.Position = 0.0;
                shadeColors.Add(item);
                OfficeShadeColor color2 = new OfficeShadeColor();
                color2.ColorRecord = fillBackColor;
                color2.Position = 1.0;
                shadeColors.Add(color2);
                shadeOpacities.Add(fillOpacity);
                shadeOpacities.Add(fillBackOpacity);
            }
            else if (focus > 0f)
            {
                OfficeShadeColor item = new OfficeShadeColor();
                item.ColorRecord = fillColor;
                item.Position = 0.0;
                shadeColors.Add(item);
                OfficeShadeColor color4 = new OfficeShadeColor();
                color4.ColorRecord = fillBackColor;
                color4.Position = focus;
                shadeColors.Add(color4);
                OfficeShadeColor color5 = new OfficeShadeColor();
                color5.ColorRecord = fillColor;
                color5.Position = 1.0;
                shadeColors.Add(color5);
                shadeOpacities.Add(fillOpacity);
                shadeOpacities.Add(fillBackOpacity);
                shadeOpacities.Add(fillOpacity);
            }
            else if (focus >= 0f)
            {
                OfficeShadeColor item = new OfficeShadeColor();
                item.ColorRecord = fillBackColor;
                item.Position = 0.0;
                shadeColors.Add(item);
                OfficeShadeColor color10 = new OfficeShadeColor();
                color10.ColorRecord = fillColor;
                color10.Position = 1.0;
                shadeColors.Add(color10);
                shadeOpacities.Add(fillBackOpacity);
                shadeOpacities.Add(fillOpacity);
            }
            else
            {
                OfficeShadeColor item = new OfficeShadeColor();
                item.ColorRecord = fillBackColor;
                item.Position = 0.0;
                shadeColors.Add(item);
                OfficeShadeColor color7 = new OfficeShadeColor();
                color7.ColorRecord = fillColor;
                color7.Position = -focus;
                shadeColors.Add(color7);
                OfficeShadeColor color8 = new OfficeShadeColor();
                color8.ColorRecord = fillBackColor;
                color8.Position = 1.0;
                shadeColors.Add(color8);
                shadeOpacities.Add(fillBackOpacity);
                shadeOpacities.Add(fillOpacity);
                shadeOpacities.Add(fillBackOpacity);
            }
        }

        private static RectangleAlignType GetShadowAlign(OfficeArtPropertiesBase artProperties) => 
            GetShadowAlignCore(GetShadowOriginX(artProperties), GetShadowOriginY(artProperties));

        public static RectangleAlignType GetShadowAlignCore(double shadowOriginX, double shadowOriginY) => 
            ((shadowOriginX != 0.0) || (shadowOriginY != 0.0)) ? (((shadowOriginX != 0.0) || (shadowOriginY != 0.5)) ? (((shadowOriginX != -0.5) || (shadowOriginY != 0.5)) ? (((shadowOriginX != 0.5) || (shadowOriginY != 0.5)) ? (((shadowOriginX != -0.5) || (shadowOriginY != 0.0)) ? (((shadowOriginX != 0.5) || (shadowOriginY != 0.0)) ? (((shadowOriginX != 0.0) || (shadowOriginY != -0.5)) ? (((shadowOriginX != 0.5) || (shadowOriginY != -0.5)) ? RectangleAlignType.TopLeft : RectangleAlignType.TopRight) : RectangleAlignType.Top) : RectangleAlignType.Right) : RectangleAlignType.Left) : RectangleAlignType.BottomRight) : RectangleAlignType.BottomLeft) : RectangleAlignType.Bottom) : RectangleAlignType.Center;

        private static OfficeColorRecord GetShadowColor(OfficeArtPropertiesBase artProperties)
        {
            DrawingShadowColor officeArtProperty = GetOfficeArtProperty<DrawingShadowColor>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.ColorRecord : new OfficeColorRecord(DXColor.Gray));
        }

        private static int GetShadowHorizontalScalingFactor(OfficeArtPropertiesBase artProperties)
        {
            DrawingShadowHorizontalScalingFactor officeArtProperty = GetOfficeArtProperty<DrawingShadowHorizontalScalingFactor>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 0x10000);
        }

        private static int GetShadowHorizontalSkewAngle(OfficeArtPropertiesBase artProperties)
        {
            DrawingShadowHorizontalSkewAngle officeArtProperty = GetOfficeArtProperty<DrawingShadowHorizontalSkewAngle>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 0);
        }

        private static int GetShadowOffsetX(OfficeArtPropertiesBase artProperties)
        {
            DrawingShadowOffsetX officeArtProperty = GetOfficeArtProperty<DrawingShadowOffsetX>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 0x6338);
        }

        private static int GetShadowOffsetY(OfficeArtPropertiesBase artProperties)
        {
            DrawingShadowOffsetY officeArtProperty = GetOfficeArtProperty<DrawingShadowOffsetY>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 0x6338);
        }

        private static double GetShadowOpacity(OfficeArtPropertiesBase artProperties)
        {
            DrawingShadowOpacity officeArtProperty = GetOfficeArtProperty<DrawingShadowOpacity>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 1.0);
        }

        private static double GetShadowOriginX(OfficeArtPropertiesBase artProperties)
        {
            DrawingShadowOriginX officeArtProperty = GetOfficeArtProperty<DrawingShadowOriginX>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 0.0);
        }

        private static double GetShadowOriginY(OfficeArtPropertiesBase artProperties)
        {
            DrawingShadowOriginY officeArtProperty = GetOfficeArtProperty<DrawingShadowOriginY>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 0.0);
        }

        private static int GetShadowSoftness(OfficeArtTertiaryProperties tertiaryArtProperties)
        {
            if (tertiaryArtProperties == null)
            {
                return 0;
            }
            DrawingShadowSoftness officeArtProperty = GetOfficeArtProperty<DrawingShadowSoftness>(tertiaryArtProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 0);
        }

        private static MsoShadowType GetShadowType(OfficeArtPropertiesBase artProperties)
        {
            DrawingShadowType officeArtProperty = GetOfficeArtProperty<DrawingShadowType>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.ShadowType : MsoShadowType.MsoShadowOffset);
        }

        private static int GetShadowVerticalScalingFactor(OfficeArtPropertiesBase artProperties)
        {
            DrawingShadowVerticalScalingFactor officeArtProperty = GetOfficeArtProperty<DrawingShadowVerticalScalingFactor>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 0x10000);
        }

        private static int GetShadowVerticalSkewAngle(OfficeArtPropertiesBase artProperties)
        {
            DrawingShadowVerticalSkewAngle officeArtProperty = GetOfficeArtProperty<DrawingShadowVerticalSkewAngle>(artProperties);
            return ((officeArtProperty != null) ? officeArtProperty.Value : 0);
        }

        private static Rectangle GetShapeGeometrySpace(IEnumerable<object> properties)
        {
            int left = 0;
            int right = 0x5460;
            int top = 0;
            int bottom = 0x5460;
            foreach (object obj2 in properties)
            {
                OfficeDrawingIntPropertyBase base2 = obj2 as OfficeDrawingIntPropertyBase;
                switch (base2)
                {
                    case (null):
                        break;

                    case (DrawingGeometryLeft _):
                        left = base2.Value;
                        continue;
                        break;

                    case (DrawingGeometryRight _):
                        right = base2.Value;
                        continue;
                        break;

                    case (DrawingGeometryTop _):
                        top = base2.Value;
                        continue;
                        break;

                    case (DrawingGeometryBottom _):
                        bottom = base2.Value;
                        break;
                }
            }
            return Rectangle.FromLTRB(left, top, right, bottom);
        }

        private static ShapeGuide[] GetShapeGuides(IEnumerable<object> properties) => 
            OfficeArtPropertiesHelper.GetArrayOfElements<ShapeGuide, IDrawingGeometryGuides>(properties);

        private static ShapePathType GetShapePathType(IEnumerable<object> properties)
        {
            DrawingGeometryShapePath propertyByType = OfficeArtPropertiesHelper.GetPropertyByType(properties, typeof(DrawingGeometryShapePath)) as DrawingGeometryShapePath;
            return ((propertyByType != null) ? propertyByType.ShapePath : ShapePathType.LinesClosed);
        }

        public static ShapePreset GetShapeType(int value, ShapePreset defaultShapeType)
        {
            switch (value)
            {
                case 1:
                    return ShapePreset.Rect;

                case 2:
                    return ShapePreset.RoundRect;

                case 3:
                    return ShapePreset.Ellipse;

                case 4:
                    return ShapePreset.Diamond;

                case 5:
                    return ShapePreset.Triangle;

                case 6:
                    return ShapePreset.RtTriangle;

                case 7:
                    return ShapePreset.Parallelogram;

                case 9:
                    return ShapePreset.Hexagon;

                case 10:
                    return ShapePreset.Octagon;

                case 11:
                    return ShapePreset.Plus;

                case 12:
                    return ShapePreset.Star5;

                case 13:
                    return ShapePreset.RightArrow;

                case 15:
                    return ShapePreset.HomePlate;

                case 0x10:
                    return ShapePreset.Cube;

                case 0x11:
                    return ShapePreset.Rect;

                case 0x12:
                    return ShapePreset.Star16;

                case 20:
                    return ShapePreset.Line;

                case 0x15:
                    return ShapePreset.Plaque;

                case 0x16:
                    return ShapePreset.Can;

                case 0x20:
                    return ShapePreset.StraightConnector1;

                case 0x21:
                    return ShapePreset.BentConnector2;

                case 0x22:
                    return ShapePreset.BentConnector3;

                case 0x23:
                    return ShapePreset.BentConnector4;

                case 0x24:
                    return ShapePreset.BentConnector5;

                case 0x25:
                    return ShapePreset.CurvedConnector2;

                case 0x26:
                    return ShapePreset.CurvedConnector3;

                case 0x27:
                    return ShapePreset.CurvedConnector4;

                case 40:
                    return ShapePreset.CurvedConnector5;

                case 0x29:
                    return ShapePreset.Callout1;

                case 0x2a:
                    return ShapePreset.Callout2;

                case 0x2b:
                    return ShapePreset.Callout3;

                case 0x2c:
                    return ShapePreset.AccentCallout1;

                case 0x2d:
                    return ShapePreset.AccentCallout2;

                case 0x2e:
                    return ShapePreset.AccentCallout3;

                case 0x2f:
                    return ShapePreset.BorderCallout1;

                case 0x30:
                    return ShapePreset.BorderCallout2;

                case 0x31:
                    return ShapePreset.BorderCallout3;

                case 50:
                    return ShapePreset.AccentBorderCallout1;

                case 0x33:
                    return ShapePreset.AccentBorderCallout2;

                case 0x34:
                    return ShapePreset.AccentBorderCallout3;

                case 0x35:
                    return ShapePreset.Ribbon;

                case 0x36:
                    return ShapePreset.Ribbon2;

                case 0x37:
                    return ShapePreset.Chevron;

                case 0x38:
                    return ShapePreset.Pentagon;

                case 0x3a:
                    return ShapePreset.Star8;

                case 0x3b:
                    return ShapePreset.Star16;

                case 60:
                    return ShapePreset.Star32;

                case 0x3d:
                    return ShapePreset.WedgeRectCallout;

                case 0x3e:
                    return ShapePreset.WedgeRoundRectCallout;

                case 0x3f:
                    return ShapePreset.WedgeEllipseCallout;

                case 0x40:
                    return ShapePreset.Wave;

                case 0x41:
                    return ShapePreset.FoldedCorner;

                case 0x42:
                    return ShapePreset.LeftArrow;

                case 0x43:
                    return ShapePreset.DownArrow;

                case 0x44:
                    return ShapePreset.UpArrow;

                case 0x45:
                    return ShapePreset.LeftRightArrow;

                case 70:
                    return ShapePreset.UpDownArrow;

                case 0x47:
                    return ShapePreset.IrregularSeal1;

                case 0x48:
                    return ShapePreset.IrregularSeal2;

                case 0x49:
                    return ShapePreset.LightningBolt;

                case 0x4d:
                    return ShapePreset.LeftArrowCallout;

                case 0x4e:
                    return ShapePreset.RightArrowCallout;

                case 0x4f:
                    return ShapePreset.UpArrowCallout;

                case 80:
                    return ShapePreset.DownArrowCallout;

                case 0x51:
                    return ShapePreset.LeftRightArrowCallout;

                case 0x52:
                    return ShapePreset.UpDownArrowCallout;

                case 0x54:
                    return ShapePreset.Bevel;

                case 0x55:
                    return ShapePreset.LeftBracket;

                case 0x56:
                    return ShapePreset.RightBracket;

                case 0x57:
                    return ShapePreset.LeftBrace;

                case 0x58:
                    return ShapePreset.RightBrace;

                case 0x5c:
                    return ShapePreset.Star24;

                case 0x5e:
                    return ShapePreset.NotchedRightArrow;

                case 0x60:
                    return ShapePreset.SmileyFace;

                case 0x61:
                    return ShapePreset.VerticalScroll;

                case 0x62:
                    return ShapePreset.HorizontalScroll;

                case 0x66:
                    return ShapePreset.CurvedRightArrow;

                case 0x67:
                    return ShapePreset.CurvedLeftArrow;

                case 0x68:
                    return ShapePreset.CurvedUpArrow;

                case 0x69:
                    return ShapePreset.CurvedDownArrow;

                case 0x6a:
                    return ShapePreset.CloudCallout;

                case 0x6b:
                    return ShapePreset.EllipseRibbon;

                case 0x6c:
                    return ShapePreset.EllipseRibbon2;

                case 0x6d:
                    return ShapePreset.FlowChartProcess;

                case 110:
                    return ShapePreset.FlowChartDecision;

                case 0x6f:
                    return ShapePreset.FlowChartInputOutput;

                case 0x70:
                    return ShapePreset.FlowChartPredefinedProcess;

                case 0x71:
                    return ShapePreset.FlowChartInternalStorage;

                case 0x72:
                    return ShapePreset.FlowChartDocument;

                case 0x73:
                    return ShapePreset.FlowChartMultidocument;

                case 0x74:
                    return ShapePreset.FlowChartTerminator;

                case 0x75:
                    return ShapePreset.FlowChartPreparation;

                case 0x76:
                    return ShapePreset.FlowChartManualInput;

                case 0x77:
                    return ShapePreset.FlowChartManualOperation;

                case 120:
                    return ShapePreset.FlowChartConnector;

                case 0x79:
                    return ShapePreset.FlowChartPunchedCard;

                case 0x7a:
                    return ShapePreset.FlowChartPunchedTape;

                case 0x7b:
                    return ShapePreset.FlowChartSummingJunction;

                case 0x7c:
                    return ShapePreset.FlowChartOr;

                case 0x7d:
                    return ShapePreset.FlowChartCollate;

                case 0x7e:
                    return ShapePreset.FlowChartSort;

                case 0x7f:
                    return ShapePreset.FlowChartExtract;

                case 0x80:
                    return ShapePreset.FlowChartMerge;

                case 0x81:
                    return ShapePreset.FlowChartOfflineStorage;

                case 130:
                    return ShapePreset.FlowChartOnlineStorage;

                case 0x83:
                    return ShapePreset.FlowChartMagneticTape;

                case 0x84:
                    return ShapePreset.FlowChartMagneticDisk;

                case 0x85:
                    return ShapePreset.FlowChartMagneticDrum;

                case 0x86:
                    return ShapePreset.FlowChartDisplay;

                case 0x87:
                    return ShapePreset.FlowChartDelay;

                case 0x88:
                case 0x89:
                case 0x8a:
                case 0x8b:
                case 140:
                case 0x8d:
                case 0x8e:
                case 0x8f:
                case 0x90:
                case 0x91:
                case 0x92:
                case 0x93:
                case 0x94:
                case 0x95:
                case 150:
                case 0x97:
                case 0x98:
                case 0x99:
                case 0x9a:
                case 0x9b:
                case 0x9c:
                case 0x9d:
                case 0x9e:
                case 0x9f:
                case 160:
                case 0xa1:
                case 0xa2:
                case 0xa3:
                case 0xa4:
                case 0xa5:
                case 0xa6:
                case 0xa7:
                case 0xa8:
                case 0xa9:
                case 170:
                case 0xab:
                case 0xac:
                case 0xad:
                case 0xae:
                case 0xaf:
                    return ShapePreset.Rect;

                case 0xb0:
                    return ShapePreset.FlowChartAlternateProcess;

                case 0xb1:
                    return ShapePreset.FlowChartOffpageConnector;

                case 0xb2:
                    return ShapePreset.Callout1;

                case 0xb3:
                    return ShapePreset.AccentCallout1;

                case 180:
                    return ShapePreset.BorderCallout1;

                case 0xb5:
                    return ShapePreset.AccentBorderCallout1;

                case 0xb7:
                    return ShapePreset.Sun;

                case 0xb8:
                    return ShapePreset.Moon;

                case 0xb9:
                    return ShapePreset.BracketPair;

                case 0xba:
                    return ShapePreset.BracePair;

                case 0xbb:
                    return ShapePreset.Star4;

                case 0xbc:
                    return ShapePreset.DoubleWave;

                case 0xbd:
                    return ShapePreset.ActionButtonBlank;

                case 190:
                    return ShapePreset.ActionButtonHome;

                case 0xbf:
                    return ShapePreset.ActionButtonHelp;

                case 0xc0:
                    return ShapePreset.ActionButtonInformation;

                case 0xc1:
                    return ShapePreset.ActionButtonForwardNext;

                case 0xc2:
                    return ShapePreset.ActionButtonBackPrevious;

                case 0xc3:
                    return ShapePreset.ActionButtonEnd;

                case 0xc4:
                    return ShapePreset.ActionButtonBeginning;

                case 0xc5:
                    return ShapePreset.ActionButtonReturn;

                case 0xc6:
                    return ShapePreset.ActionButtonDocument;

                case 0xc7:
                    return ShapePreset.ActionButtonSound;

                case 200:
                    return ShapePreset.ActionButtonMovie;

                case 0xca:
                    return ShapePreset.Rect;
            }
            return defaultShapeType;
        }

        private static List<long> GetWordArtAdjustValues(OfficeArtPropertiesBase artProperties, DrawingPresetTextWarp presetTextWarp, float shapeWidth, float shapeHeight)
        {
            int?[] adjustValues = GetAdjustValues(artProperties);
            return AdjustValuesConverterFromBinaryFormat.GetWordArtAdjustValues(presetTextWarp, shapeWidth, shapeHeight, adjustValues);
        }

        public static DrawingPresetTextWarp GetWordArtPreset(int value)
        {
            switch (value)
            {
                case 0x18:
                case 0x19:
                case 0x1a:
                case 0x1b:
                case 0x1c:
                case 0x1d:
                case 30:
                case 0x1f:
                    return DrawingPresetTextWarp.NoShape;
            }
            switch (value)
            {
                case 0x88:
                    return DrawingPresetTextWarp.Plain;

                case 0x89:
                    return DrawingPresetTextWarp.Stop;

                case 0x8a:
                    return DrawingPresetTextWarp.Triangle;

                case 0x8b:
                    return DrawingPresetTextWarp.TriangleInverted;

                case 140:
                    return DrawingPresetTextWarp.Chevron;

                case 0x8d:
                    return DrawingPresetTextWarp.ChevronInverted;

                case 0x8e:
                    return DrawingPresetTextWarp.RingInside;

                case 0x8f:
                    return DrawingPresetTextWarp.RingOutside;

                case 0x90:
                    return DrawingPresetTextWarp.ArchUp;

                case 0x91:
                    return DrawingPresetTextWarp.ArchDown;

                case 0x92:
                    return DrawingPresetTextWarp.Circle;

                case 0x93:
                    return DrawingPresetTextWarp.Button;

                case 0x94:
                    return DrawingPresetTextWarp.ArchUpPour;

                case 0x95:
                    return DrawingPresetTextWarp.ArchDownPour;

                case 150:
                    return DrawingPresetTextWarp.CirclePour;

                case 0x97:
                    return DrawingPresetTextWarp.ButtonPour;

                case 0x98:
                    return DrawingPresetTextWarp.CurveUp;

                case 0x99:
                    return DrawingPresetTextWarp.CurveDown;

                case 0x9a:
                    return DrawingPresetTextWarp.CascadeUp;

                case 0x9b:
                    return DrawingPresetTextWarp.CascadeDown;

                case 0x9c:
                    return DrawingPresetTextWarp.Wave1;

                case 0x9d:
                    return DrawingPresetTextWarp.Wave2;

                case 0x9e:
                    return DrawingPresetTextWarp.DoubleWave1;

                case 0x9f:
                    return DrawingPresetTextWarp.Wave4;

                case 160:
                    return DrawingPresetTextWarp.Inflate;

                case 0xa1:
                    return DrawingPresetTextWarp.Deflate;

                case 0xa2:
                    return DrawingPresetTextWarp.InflateBottom;

                case 0xa3:
                    return DrawingPresetTextWarp.DeflateBottom;

                case 0xa4:
                    return DrawingPresetTextWarp.InflateTop;

                case 0xa5:
                    return DrawingPresetTextWarp.DeflateTop;

                case 0xa6:
                    return DrawingPresetTextWarp.DeflateInflate;

                case 0xa7:
                    return DrawingPresetTextWarp.InflateDeflate;

                case 0xa8:
                    return DrawingPresetTextWarp.FadeRight;

                case 0xa9:
                    return DrawingPresetTextWarp.FadeLeft;

                case 170:
                    return DrawingPresetTextWarp.FadeUp;

                case 0xab:
                    return DrawingPresetTextWarp.FadeDown;

                case 0xac:
                    return DrawingPresetTextWarp.SlantUp;

                case 0xad:
                    return DrawingPresetTextWarp.SlantDown;

                case 0xae:
                    return DrawingPresetTextWarp.CanUp;

                case 0xaf:
                    return DrawingPresetTextWarp.CanDown;
            }
            throw new ArgumentException("Unknown wordart preset type:" + value);
        }

        public static bool IsWordArtShape(int value)
        {
            switch (value)
            {
                case 0x18:
                case 0x19:
                case 0x1a:
                case 0x1b:
                case 0x1c:
                case 0x1d:
                case 30:
                case 0x1f:
                    break;

                default:
                    switch (value)
                    {
                        case 0x88:
                        case 0x89:
                        case 0x8a:
                        case 0x8b:
                        case 140:
                        case 0x8d:
                        case 0x8e:
                        case 0x8f:
                        case 0x90:
                        case 0x91:
                        case 0x92:
                        case 0x93:
                        case 0x94:
                        case 0x95:
                        case 150:
                        case 0x97:
                        case 0x98:
                        case 0x99:
                        case 0x9a:
                        case 0x9b:
                        case 0x9c:
                        case 0x9d:
                        case 0x9e:
                        case 0x9f:
                        case 160:
                        case 0xa1:
                        case 0xa2:
                        case 0xa3:
                        case 0xa4:
                        case 0xa5:
                        case 0xa6:
                        case 0xa7:
                        case 0xa8:
                        case 0xa9:
                        case 170:
                        case 0xab:
                        case 0xac:
                        case 0xad:
                        case 0xae:
                        case 0xaf:
                            break;

                        default:
                            return false;
                    }
                    break;
            }
            return true;
        }

        public static void SetupAdjustValues(IOfficeShape shape, OfficeArtPropertiesBase artProperties)
        {
            DocumentModelUnitConverter unitConverter = shape.DocumentModel.UnitConverter;
            int widthEmu = unitConverter.CeilingModelUnitsToEmu(shape.Width);
            SetupAdjustValues(shape.ShapeProperties, widthEmu, unitConverter.CeilingModelUnitsToEmu(shape.Height), artProperties);
        }

        public static void SetupAdjustValues(ShapeProperties shapeProperties, int widthEmu, int heightEmu, OfficeArtPropertiesBase artProperties)
        {
            SetupAdjustValues(shapeProperties, widthEmu, heightEmu, artProperties?.Properties);
        }

        public static void SetupAdjustValues(ShapeProperties shapeProperties, int widthEmu, int heightEmu, IEnumerable<object> artProperties)
        {
            if (artProperties != null)
            {
                int?[] adjustValues = GetAdjustValues(artProperties);
                AdjustValuesConverterFromBinaryFormat.Convert(widthEmu, heightEmu, shapeProperties.ShapeType, adjustValues);
                shapeProperties.SetupShapeAdjustList(adjustValues);
            }
        }

        public static void SetupBlackAndWhiteMode(ShapePropertiesBase shapeProperties, OfficeArtPropertiesBase artProperties)
        {
            shapeProperties.BlackAndWhiteMode = ConvertBlackWhiteMode(GetDrawingBlackWhiteMode(artProperties));
        }

        private static void SetupCalculatedValues(ModelShapeCustomGeometry customGeometry, IEnumerable<object> properties, int widthEmu, int heightEmu, DocumentModelUnitConverter unitConverter, XlsAdjustableCoordinateCache adjustableCoordinateCache)
        {
            Rectangle shapeGeometrySpace = GetShapeGeometrySpace(properties);
            Point point = RectangleUtils.CenterPoint(shapeGeometrySpace);
            AddGuide(customGeometry, "xcenter", point.X, adjustableCoordinateCache);
            AddGuide(customGeometry, "ycenter", point.Y, adjustableCoordinateCache);
            AddGuide(customGeometry, "width", shapeGeometrySpace.Width, adjustableCoordinateCache);
            AddGuide(customGeometry, "height", shapeGeometrySpace.Height, adjustableCoordinateCache);
            AddGuide(customGeometry, "xlimo", GetLimoX(properties), adjustableCoordinateCache);
            AddGuide(customGeometry, "ylimo", GetLimoY(properties), adjustableCoordinateCache);
            DrawingLineStyleBooleanProperties drawingLineBooleanProperties = GetDrawingLineBooleanProperties(properties);
            AddGuide(customGeometry, "lineDrawn", ((drawingLineBooleanProperties == null) || (!drawingLineBooleanProperties.UseLine || !drawingLineBooleanProperties.Line)) ? 0 : 1, adjustableCoordinateCache);
            AddGuide(customGeometry, "pixellinewidth", unitConverter.ModelUnitsToPixels(unitConverter.EmuToModelUnits(GetLineWidthInEMUs(properties))), adjustableCoordinateCache);
            AddGuide(customGeometry, "pixelwidth", unitConverter.ModelUnitsToPixels(unitConverter.EmuToModelUnits(widthEmu)), adjustableCoordinateCache);
            AddGuide(customGeometry, "pixelheight", unitConverter.ModelUnitsToPixels(unitConverter.EmuToModelUnits(heightEmu)), adjustableCoordinateCache);
            AddGuide(customGeometry, "emuwidth", widthEmu, adjustableCoordinateCache);
            AddGuide(customGeometry, "emuheight", heightEmu, adjustableCoordinateCache);
            AddGuide(customGeometry, "emuwidth2", widthEmu / 2, adjustableCoordinateCache);
            AddGuide(customGeometry, "emuheight2", heightEmu / 2, adjustableCoordinateCache);
        }

        public static void SetupChildAnchor(Transform2D transform2D, OfficeArtChildAnchor childAnchor)
        {
            DocumentModelUnitConverter unitConverter = transform2D.DocumentModel.UnitConverter;
            transform2D.OffsetX = unitConverter.EmuToModelUnitsF(childAnchor.Left);
            transform2D.OffsetY = unitConverter.EmuToModelUnitsF(childAnchor.Top);
            transform2D.Cx = unitConverter.EmuToModelUnitsF(childAnchor.Right - childAnchor.Left);
            transform2D.Cy = unitConverter.EmuToModelUnitsF(childAnchor.Bottom - childAnchor.Top);
        }

        public static void SetupColor(IDrawingColor drawingColor, OfficeColorRecord officeColor, IDocumentModel documentModel, Palette palette, OfficeArtPropertiesBase artProperties)
        {
            if ((officeColor == null) || officeColor.IsDefault)
            {
                drawingColor.Rgb = DXColor.FromArgb(0xff, 0xff, 0xff);
            }
            else if (officeColor.SystemColorUsed)
            {
                SetupSystemColor(drawingColor, officeColor, documentModel, palette, artProperties);
            }
            else if (officeColor.ColorSchemeUsed)
            {
                drawingColor.Rgb = ColorModelInfo.Create(officeColor.ColorSchemeIndex).ToRgb(palette, documentModel.OfficeTheme.Colors);
            }
            else
            {
                drawingColor.Rgb = officeColor.Color;
            }
        }

        public static void SetupCommonProtectionProperties(ICommonDrawingLocks drawingLocks, OfficeArtPropertiesBase artProperties)
        {
            if ((drawingLocks != null) && (artProperties != null))
            {
                DrawingBooleanProtectionProperties officeArtProperty = GetOfficeArtProperty<DrawingBooleanProtectionProperties>(artProperties);
                if (officeArtProperty != null)
                {
                    if (officeArtProperty.UseLockGroup)
                    {
                        drawingLocks.NoGroup = officeArtProperty.LockGroup;
                    }
                    if (officeArtProperty.UseLockSelect)
                    {
                        drawingLocks.NoSelect = officeArtProperty.LockSelect;
                    }
                    if (officeArtProperty.UseLockPosition)
                    {
                        drawingLocks.NoMove = officeArtProperty.LockPosition;
                    }
                    if (officeArtProperty.UseLockAspectRatio)
                    {
                        drawingLocks.NoChangeAspect = officeArtProperty.LockAspectRatio;
                    }
                }
            }
        }

        public static void SetupConnectionShapeProperties(ShapeProperties shapeProperties, OfficeArtPropertiesBase artProperties)
        {
            if (artProperties != null)
            {
                shapeProperties.Outline.HeadType = OutlineHeadTailTypeConverter.GetOutlineHeadTailType(GetLineStartArrowhead(artProperties));
                shapeProperties.Outline.HeadLength = GetLineStartArrowheadLength(artProperties);
                shapeProperties.Outline.HeadWidth = GetLineStartArrowheadWidth(artProperties);
                shapeProperties.Outline.TailType = OutlineHeadTailTypeConverter.GetOutlineHeadTailType(GetLineEndArrowhead(artProperties));
                shapeProperties.Outline.TailLength = GetLineEndArrowheadLength(artProperties);
                shapeProperties.Outline.TailWidth = GetLineEndArrowheadWidth(artProperties);
            }
        }

        private static void SetupCustomAdjustValues(ModelShapeCustomGeometry customGeometry, IEnumerable<object> properties, XlsAdjustableCoordinateCache adjustableCoordinateCache)
        {
            int?[] adjustValues = GetAdjustValues(properties);
            for (int i = 0; i < adjustValues.Length; i++)
            {
                if (adjustValues[i] != null)
                {
                    ModelShapeGuideFormula formula = new ModelShapeGuideFormula(ModelShapeGuideFormulaType.Value, adjustableCoordinateCache.GetCachedAdjustableCoordinate(adjustValues[i].Value), null, null);
                    customGeometry.AdjustValues.Add(new ModelShapeGuide("adj" + (i + 1).ToString(), formula));
                }
            }
        }

        public static void SetupDrawingProtectionProperties(IDrawingLocks drawingLocks, OfficeArtPropertiesBase artProperties)
        {
            if ((drawingLocks != null) && (artProperties != null))
            {
                DrawingShapeBooleanProperties officeArtProperty = GetOfficeArtProperty<DrawingShapeBooleanProperties>(artProperties);
                if ((officeArtProperty != null) && officeArtProperty.UseLockShapeType)
                {
                    drawingLocks.NoChangeShapeType = officeArtProperty.LockShapeType;
                }
                DrawingBooleanProtectionProperties properties2 = GetOfficeArtProperty<DrawingBooleanProtectionProperties>(artProperties);
                if (properties2 != null)
                {
                    SetupCommonProtectionProperties(drawingLocks, artProperties);
                    if (properties2.UseLockRotation)
                    {
                        drawingLocks.NoRotate = properties2.LockRotation;
                    }
                    if (properties2.UseLockAdjustHandles)
                    {
                        drawingLocks.NoAdjustHandles = properties2.LockAdjustHandles;
                    }
                    if (properties2.UseLockVertices)
                    {
                        drawingLocks.NoEditPoints = properties2.LockVertices;
                    }
                }
            }
        }

        private static void SetupFillColor(IDrawingColor drawingColor, IDocumentModel documentModel, Palette palette, OfficeArtPropertiesBase artProperties)
        {
            OfficeColorRecord colorRecord = GetColorRecord<DrawingFillColor>(artProperties);
            SetupColor(drawingColor, colorRecord, documentModel, palette, artProperties);
        }

        public static void SetupFillOpacity(IDrawingColor drawingColor, double opacity)
        {
            if (opacity < 1.0)
            {
                if (opacity < 0.0)
                {
                    opacity = 0.0;
                }
                drawingColor.Transforms.Add(new AlphaColorTransform((int) Math.Round((double) (opacity * 100000.0))));
            }
        }

        private static void SetupFillRect(DrawingGradientFill fill, OfficeArtPropertiesBase artProperties)
        {
            int bottomOffset = (int) ((1.0 - GetRelativePosition<DrawingFillToBottom>(artProperties)) * 100000.0);
            fill.FillRect = new RectangleOffset(bottomOffset, (int) (GetRelativePosition<DrawingFillToLeft>(artProperties) * 100000.0), (int) ((1.0 - GetRelativePosition<DrawingFillToRight>(artProperties)) * 100000.0), (int) (GetRelativePosition<DrawingFillToTop>(artProperties) * 100000.0));
        }

        private static void SetupGradientAngle(DrawingGradientFill gradientFill, OfficeArtPropertiesBase artProperties)
        {
            double num = 0.0;
            DrawingFillAngle propertyByType = OfficeArtPropertiesHelper.GetPropertyByType(artProperties, typeof(DrawingFillAngle)) as DrawingFillAngle;
            if (propertyByType != null)
            {
                num = propertyByType.Value;
            }
            gradientFill.Angle = SetupGradientAngleCore(num);
        }

        public static int SetupGradientAngleCore(double angle)
        {
            angle = (angle < 0.0) ? -(angle + 90.0) : (90.0 - angle);
            if (angle < 0.0)
            {
                angle += 360.0;
            }
            if (angle > 360.0)
            {
                angle -= 360.0;
            }
            return (int) (angle * 60000.0);
        }

        private static void SetupGradientStops(DrawingGradientFill gradientFill, IDocumentModel documentModel, Palette palette, OfficeArtPropertiesBase artProperties)
        {
            OfficeShadeColor[] arrayOfElements = OfficeArtPropertiesHelper.GetArrayOfElements<OfficeShadeColor, DrawingFillShadeColors>(artProperties);
            List<OfficeShadeColor> shadeColors = new List<OfficeShadeColor>();
            List<double> shadeOpacities = new List<double>();
            if ((arrayOfElements != null) && (arrayOfElements.Length != 0))
            {
                GetShadeColors(shadeColors, shadeOpacities, arrayOfElements, artProperties);
            }
            else
            {
                GetShadeColors(shadeColors, shadeOpacities, artProperties);
            }
            for (int i = 0; i < shadeColors.Count; i++)
            {
                DrawingGradientStop stop = new DrawingGradientStop(documentModel);
                SetupColor(stop.Color, shadeColors[i].ColorRecord, documentModel, palette, artProperties);
                stop.Position = (int) (shadeColors[i].Position * 100000.0);
                SetupFillOpacity(stop.Color, shadeOpacities[i]);
                gradientFill.AddGradientStop(stop);
            }
        }

        public static void SetupGroupCoordinateSystem(Transform2D childTransform2D, OfficeArtShapeGroupCoordinateSystem coordinateSystem)
        {
            DocumentModelUnitConverter unitConverter = childTransform2D.DocumentModel.UnitConverter;
            childTransform2D.OffsetX = unitConverter.EmuToModelUnitsF(coordinateSystem.Left);
            childTransform2D.OffsetY = unitConverter.EmuToModelUnitsF(coordinateSystem.Top);
            childTransform2D.Cx = unitConverter.EmuToModelUnitsF(coordinateSystem.Right - coordinateSystem.Left);
            childTransform2D.Cy = unitConverter.EmuToModelUnitsF(coordinateSystem.Bottom - coordinateSystem.Top);
        }

        public static void SetupOuterShadowProperties(ShapeProperties shapeProperties, Palette palette, OfficeArtPropertiesBase artProperties, OfficeArtTertiaryProperties tertiaryArtProperties)
        {
            DrawingShadowStyleBooleanProperties officeArtProperty = GetOfficeArtProperty<DrawingShadowStyleBooleanProperties>(artProperties);
            if ((officeArtProperty != null) && (officeArtProperty.UseShadow && officeArtProperty.Shadow))
            {
                MsoShadowType shadowType = GetShadowType(artProperties);
                OfficeColorRecord shadowColor = GetShadowColor(artProperties);
                double shadowOpacity = GetShadowOpacity(artProperties);
                int shadowOffsetX = GetShadowOffsetX(artProperties);
                int shadowOffsetY = GetShadowOffsetY(artProperties);
                long dist = CalculateDistance((double) shadowOffsetX, (double) shadowOffsetY);
                int dir = CalculateDirection((double) shadowOffsetX, (double) shadowOffsetY);
                int shadowSoftness = GetShadowSoftness(tertiaryArtProperties);
                int sx = (shadowType != MsoShadowType.MsoShadowOffset) ? CalculateHorizontalFactor(artProperties) : 0x186a0;
                OuterShadowEffectInfo info = OuterShadowEffectInfo.Create((long) shadowSoftness, dist, dir, sx, (shadowType != MsoShadowType.MsoShadowOffset) ? CalculateVerticalFactor(artProperties) : 0x186a0, CalculateHorizontalSkewAngle(artProperties), CalculateVerticalSkewAngle(artProperties), GetShadowAlign(artProperties), false);
                DrawingColor drawingColor = new DrawingColor(shapeProperties.DocumentModel);
                SetupColor(drawingColor, shadowColor, shapeProperties.DocumentModel, palette, artProperties);
                if (shadowOpacity != 0.0)
                {
                    drawingColor.Transforms.Add(new AlphaColorTransform((int) (Math.Round(shadowOpacity, 5) * 100000.0)));
                }
                ContainerEffect containerEffect = shapeProperties.EffectStyle.ContainerEffect;
                containerEffect.SetApplyEffectListCore(true);
                containerEffect.Effects.Add(new OuterShadowEffect(drawingColor, info));
            }
        }

        public static void SetupOutlineProperties(Outline outline, IDocumentModel documentModel, Palette palette, OfficeArtPropertiesBase artProperties, bool isPictureFrame)
        {
            if (artProperties != null)
            {
                outline.BeginUpdate();
                try
                {
                    if (!UseLine(GetDrawingLineBooleanProperties(artProperties.Properties), isPictureFrame))
                    {
                        outline.Type = OutlineType.None;
                        outline.CompoundType = OutlineCompoundType.Single;
                    }
                    else
                    {
                        outline.Type = OutlineType.Solid;
                        outline.CompoundType = GetLineCompoundType(artProperties);
                        outline.EndCapStyle = GetLineCapStyle(artProperties);
                        outline.Dashing = GetLineDashing(artProperties);
                        int lineOpacity = GetLineOpacity(artProperties);
                        if (lineOpacity != 0x10000)
                        {
                            outline.Color.Transforms.AddCore(new AlphaColorTransform(GetAlphaColorTransform(lineOpacity)));
                        }
                        OfficeColorRecord colorRecord = GetColorRecord<DrawingLineColor>(artProperties);
                        if (colorRecord != null)
                        {
                            if (colorRecord.SystemColorUsed)
                            {
                                SystemColorValues empty = SystemColorValues.Empty;
                                if (Enum.IsDefined(typeof(SystemColorValues), colorRecord.SystemColorIndex))
                                {
                                    empty = (SystemColorValues) colorRecord.SystemColorIndex;
                                }
                                outline.Color.OriginalColor.System = empty;
                            }
                            else
                            {
                                Color color = colorRecord.Color;
                                if (colorRecord.ColorSchemeUsed)
                                {
                                    color = ColorModelInfo.Create(colorRecord.ColorSchemeIndex).ToRgb(palette, documentModel.OfficeTheme.Colors);
                                }
                                outline.Color.OriginalColor.Rgb = color;
                            }
                        }
                        outline.Width = documentModel.UnitConverter.EmuToModelUnits(GetLineWidthInEMUs(artProperties.Properties));
                    }
                    outline.JoinStyle = GetLineJoinStyle(artProperties);
                    outline.MiterLimit = DrawingValueConverter.ToPercentage(GetLineMiterLimit(artProperties));
                }
                finally
                {
                    outline.EndUpdate();
                }
            }
        }

        private static void SetupRgbColor(IDrawingColor drawingColor, OfficeColorRecord officeColor, IDocumentModel documentModel, Palette palette)
        {
            if ((officeColor == null) || (officeColor.IsDefault || officeColor.SystemColorUsed))
            {
                drawingColor.Rgb = DXColor.FromArgb(0xff, 0xff, 0xff);
            }
            else if (officeColor.ColorSchemeUsed)
            {
                drawingColor.Rgb = ColorModelInfo.Create(officeColor.ColorSchemeIndex).ToRgb(palette, documentModel.OfficeTheme.Colors);
            }
            else
            {
                drawingColor.Rgb = officeColor.Color;
            }
        }

        private static void SetupRotateWithShape(DrawingGradientFill fill, OfficeArtPropertiesBase artProperties, OfficeArtTertiaryProperties tertiaryArtProperties)
        {
            fill.RotateWithShape = GetRotateWithShape(artProperties, tertiaryArtProperties);
        }

        public static void SetupShapeCustomGeometry(IOfficeShape shape, OfficeArtPropertiesBase artProperties)
        {
            DocumentModelUnitConverter unitConverter = shape.DocumentModel.UnitConverter;
            int widthEmu = unitConverter.CeilingModelUnitsToEmu(shape.Width);
            SetupShapeCustomGeometry(shape.ShapeProperties.CustomGeometry, widthEmu, unitConverter.CeilingModelUnitsToEmu(shape.Height), artProperties);
        }

        public static void SetupShapeCustomGeometry(ModelShapeCustomGeometry customGeometry, int widthEmu, int heightEmu, OfficeArtPropertiesBase artProperties)
        {
            if (artProperties != null)
            {
                SetupShapeCustomGeometry(customGeometry, widthEmu, heightEmu, artProperties.Properties);
            }
        }

        public static void SetupShapeCustomGeometry(ModelShapeCustomGeometry customGeometry, int widthEmu, int heightEmu, IEnumerable<object> properties)
        {
            Rectangle shapeGeometrySpace = GetShapeGeometrySpace(properties);
            ModelShapePath item = new ModelShapePath(customGeometry.DocumentModelPart) {
                Width = shapeGeometrySpace.Width,
                Height = shapeGeometrySpace.Height
            };
            customGeometry.Paths.Add(item);
            XlsAdjustableCoordinateCache adjustableCoordinateCache = new XlsAdjustableCoordinateCache();
            XlsAdjustableAngleCache adjustableAngleCache = new XlsAdjustableAngleCache();
            ShapeGuideCalculator calculator = new ShapeGuideCalculator(CreateFakeCustomGeometry(properties, widthEmu, heightEmu, customGeometry.DocumentModelPart.DocumentModel.UnitConverter, adjustableCoordinateCache), (double) widthEmu, (double) heightEmu, new ModelShapeGuideList(FakeDocumentModel.Instance));
            Point[] points = CalculatePoints(GetDrawingGeometryVertices(properties), calculator);
            if (points != null)
            {
                MsoPathInfo[] drawingGeometrySegmentInfos = GetDrawingGeometrySegmentInfos(properties);
                switch ((((drawingGeometrySegmentInfos == null) || (drawingGeometrySegmentInfos.Length == 0)) ? GetShapePathType(properties) : ShapePathType.Complex))
                {
                    case ShapePathType.Lines:
                        CreateLinesPath(points, false, item, adjustableCoordinateCache);
                        break;

                    case ShapePathType.LinesClosed:
                        CreateLinesPath(points, true, item, adjustableCoordinateCache);
                        break;

                    case ShapePathType.Curves:
                        CreateCurvesPath(points, false, item, adjustableCoordinateCache);
                        break;

                    case ShapePathType.CurvesClosed:
                        CreateCurvesPath(points, true, item, adjustableCoordinateCache);
                        break;

                    case ShapePathType.Complex:
                        CreateComplexPath(customGeometry, drawingGeometrySegmentInfos, points, adjustableCoordinateCache, adjustableAngleCache);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
                CreateConnectionSites(CalculatePoints(GetConnectionSites(properties), calculator), GetConnectionSitesDirections(properties), customGeometry, adjustableCoordinateCache, adjustableAngleCache);
            }
        }

        private static void SetupShapeGuides(ModelShapeCustomGeometry customGeometry, IEnumerable<object> properties, XlsAdjustableCoordinateCache adjustableCoordinateCache)
        {
            ShapeGuide[] shapeGuides = GetShapeGuides(properties);
            if ((shapeGuides != null) && (shapeGuides.Length != 0))
            {
                for (int i = 0; i < shapeGuides.Length; i++)
                {
                    ModelShapeGuideFormula formula = CreateModelShapeGuideFormula(shapeGuides[i], adjustableCoordinateCache);
                    ModelShapeGuide item = new ModelShapeGuide("G" + i.ToString(), formula);
                    customGeometry.Guides.Add(item);
                }
            }
        }

        public static void SetupShapeProtectionProperties(IDrawingLocks locks, OfficeArtPropertiesBase artProperties)
        {
            if (locks != null)
            {
                SetupDrawingProtectionProperties(locks, artProperties);
                DrawingBooleanProtectionProperties officeArtProperty = GetOfficeArtProperty<DrawingBooleanProtectionProperties>(artProperties);
                if (officeArtProperty != null)
                {
                    IShapeLocks locks2 = locks as IShapeLocks;
                    if ((locks2 != null) && officeArtProperty.UseLockText)
                    {
                        locks2.NoTextEdit = officeArtProperty.LockText;
                    }
                }
            }
        }

        private static void SetupSpecialColor(IDrawingColor drawingColor, OfficeColorRecord officeColor, IDocumentModel documentModel, Palette palette, OfficeArtPropertiesBase artProperties)
        {
            switch (officeColor.ColorUse)
            {
                case OfficeColorUse.UseFillColor:
                    SetupRgbColor(drawingColor, GetColorRecord<DrawingFillColor>(artProperties), documentModel, palette);
                    break;

                case OfficeColorUse.UseLineOrFillColor:
                {
                    OfficeColorRecord colorRecord = GetColorRecord<DrawingLineColor>(artProperties);
                    OfficeColorRecord record1 = colorRecord;
                    if (colorRecord == null)
                    {
                        OfficeColorRecord local2 = colorRecord;
                        record1 = GetColorRecord<DrawingFillColor>(artProperties);
                    }
                    SetupRgbColor(drawingColor, record1, documentModel, palette);
                    break;
                }
                case OfficeColorUse.UseLineColor:
                    SetupRgbColor(drawingColor, GetColorRecord<DrawingLineColor>(artProperties), documentModel, palette);
                    break;

                case OfficeColorUse.UseFillBackColor:
                    SetupRgbColor(drawingColor, GetColorRecord<DrawingFillBackColor>(artProperties), documentModel, palette);
                    break;

                case OfficeColorUse.UseFillOrLineColor:
                {
                    OfficeColorRecord colorRecord = GetColorRecord<DrawingFillColor>(artProperties);
                    OfficeColorRecord record3 = colorRecord;
                    if (colorRecord == null)
                    {
                        OfficeColorRecord local1 = colorRecord;
                        record3 = GetColorRecord<DrawingLineColor>(artProperties);
                    }
                    SetupRgbColor(drawingColor, record3, documentModel, palette);
                    break;
                }
                default:
                    SetupRgbColor(drawingColor, null, documentModel, palette);
                    break;
            }
            if ((officeColor.Transform == OfficeColorTransform.Darken) && (officeColor.TransformValue < 0xff))
            {
                drawingColor.Transforms.Add(new GammaColorTransform());
                drawingColor.Transforms.Add(new ShadeColorTransform((officeColor.TransformValue * 0x186a0) / 0xff));
                drawingColor.Transforms.Add(new InverseGammaColorTransform());
            }
            if ((officeColor.Transform == OfficeColorTransform.Lighten) && (officeColor.TransformValue < 0xff))
            {
                drawingColor.Transforms.Add(new GammaColorTransform());
                drawingColor.Transforms.Add(new TintColorTransform((officeColor.TransformValue * 0x186a0) / 0xff));
                drawingColor.Transforms.Add(new InverseGammaColorTransform());
            }
        }

        private static void SetupSystemColor(IDrawingColor drawingColor, OfficeColorRecord officeColor, IDocumentModel documentModel, Palette palette, OfficeArtPropertiesBase artProperties)
        {
            if (officeColor.ColorUse != OfficeColorUse.None)
            {
                SetupSpecialColor(drawingColor, officeColor, documentModel, palette, artProperties);
            }
            else
            {
                SystemColorValues empty = SystemColorValues.Empty;
                if (Enum.IsDefined(typeof(SystemColorValues), officeColor.SystemColorIndex))
                {
                    empty = (SystemColorValues) officeColor.SystemColorIndex;
                }
                drawingColor.System = empty;
            }
        }

        public static void SetupTransformProperties(Transform2D transform2D, OfficeArtPropertiesBase artProperties, OfficeArtShapeRecord shapeRecord)
        {
            if (shapeRecord != null)
            {
                transform2D.FlipH = shapeRecord.FlipH;
                transform2D.FlipV = shapeRecord.FlipV;
            }
            DrawingShapeBooleanProperties propertyByType = OfficeArtPropertiesHelper.GetPropertyByType(artProperties, typeof(DrawingShapeBooleanProperties)) as DrawingShapeBooleanProperties;
            if (propertyByType != null)
            {
                if (propertyByType.UseFlipHOverride)
                {
                    transform2D.FlipH = propertyByType.FlipHOverride;
                }
                if (propertyByType.UseFlipVOverride)
                {
                    transform2D.FlipV = propertyByType.FlipVOverride;
                }
            }
            float rotation = GetRotation(artProperties);
            if (rotation != 0.0)
            {
                if (transform2D.FlipH)
                {
                    rotation = 360f - rotation;
                }
                if (transform2D.FlipV)
                {
                    rotation = 360f - rotation;
                }
                transform2D.RotateCore(transform2D.DocumentModel.UnitConverter.DegreeToModelUnits(rotation));
            }
        }

        public static void SetupWordArtAdjustValues(ModelShapeGuideList adjustValues, DrawingPresetTextWarp presetTextWarp, float shapeWidth, float shapeHeight, OfficeArtPropertiesBase artProperties)
        {
            List<long> list = GetWordArtAdjustValues(artProperties, presetTextWarp, shapeWidth, shapeHeight);
            for (int i = 0; i < list.Count; i++)
            {
                ModelShapeGuide item = new ModelShapeGuide((list.Count == 1) ? "adj" : $"adj{(i + 1)}", $"val {list[i]}");
                adjustValues.Add(item);
            }
        }

        private static bool UseLine(DrawingLineStyleBooleanProperties bpLine, bool isPictureFrame) => 
            (bpLine != null) ? (!bpLine.UseLine || bpLine.Line) : !isPictureFrame;

        private static Dictionary<int, string> ShapeGuideCalculatedParamDescriptions
        {
            get
            {
                if (shapeGuideCalculatedParamDescriptions == null)
                {
                    object syncShapeGuideCalculatedParamDescriptions = BinaryDrawingImportHelper.syncShapeGuideCalculatedParamDescriptions;
                    lock (syncShapeGuideCalculatedParamDescriptions)
                    {
                        shapeGuideCalculatedParamDescriptions ??= CreateShapeGuideCalculatedParamDescriptions();
                    }
                }
                return shapeGuideCalculatedParamDescriptions;
            }
        }
    }
}

