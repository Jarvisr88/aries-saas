namespace DevExpress.XtraPrinting.Shape.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Shape;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public static class ShapeFactory
    {
        private static List<ShapeId> nameIds = new List<ShapeId>();
        private static Dictionary<ShapeId, IShapeFactory> shapesHT = new Dictionary<ShapeId, IShapeFactory>();
        private static Dictionary<ShapeId, Type> shapeTypesHT = new Dictionary<ShapeId, Type>();
        internal static readonly IShapeFactory DefaultFactory = new ShapePrototypeFactory(new ShapeEllipse());
        private static Dictionary<Size, List<System.Drawing.Image>> sampleImages;

        static ShapeFactory()
        {
            RegisterFactories();
        }

        public static ShapeBase CloneShape(ShapeBase shape) => 
            (ShapeBase) ((ICloneable) shape).Clone();

        public static RotatedShape Create(ShapeId shapeId)
        {
            IShapeFactory defaultFactory = shapesHT[shapeId];
            defaultFactory ??= DefaultFactory;
            return defaultFactory.CreateShape();
        }

        public static ShapeBase CreateByType(ShapeId shapeId) => 
            (ShapeBase) Activator.CreateInstance(shapeTypesHT[shapeId]);

        public static List<System.Drawing.Image> GetSampleImages(Size size)
        {
            if ((sampleImages == null) || !sampleImages.ContainsKey(size))
            {
                InitSampleImages(size);
            }
            return sampleImages[size];
        }

        private static void InitSampleImages(Size size)
        {
            sampleImages ??= new Dictionary<Size, List<System.Drawing.Image>>();
            sampleImages.Add(size, new List<System.Drawing.Image>());
            shapesHT.ForEach<KeyValuePair<ShapeId, IShapeFactory>>(x => RegisterSampleBitmap(x.Value, size));
        }

        private static void RegisterFactories()
        {
            RegisterShapeFactory(new ShapePrototypeFactory(new ShapeRectangle()), ShapeId.Rectangle);
            RegisterShapeFactory(DefaultFactory, ShapeId.Ellipse);
            ShapeArrow prototype = new ShapeArrow();
            RegisterShapeFactory(new ShapePrototypeAngleFactory(prototype, 0), ShapeId.TopArrow);
            RegisterShapeFactory(new ShapePrototypeAngleFactory(prototype, 270), ShapeId.RightArrow);
            RegisterShapeFactory(new ShapePrototypeAngleFactory(prototype, 180), ShapeId.BottomArrow);
            RegisterShapeFactory(new ShapePrototypeAngleFactory(prototype, 90), ShapeId.LeftArrow);
            ShapePolygon polygon = new ShapePolygon {
                NumberOfSides = 3
            };
            RegisterShapeFactory(new ShapePrototypeFactory(polygon), ShapeId.Triangle);
            polygon.NumberOfSides = 4;
            RegisterShapeFactory(new ShapePrototypeFactory(polygon), ShapeId.Square);
            polygon.NumberOfSides = 5;
            RegisterShapeFactory(new ShapePrototypeFactory(polygon), ShapeId.Pentagon);
            polygon.NumberOfSides = 6;
            RegisterShapeFactory(new ShapePrototypeFactory(polygon), ShapeId.Hexagon);
            polygon.NumberOfSides = 8;
            RegisterShapeFactory(new ShapePrototypeFactory(polygon), ShapeId.Octagon);
            ShapeStar star = new ShapeStar {
                StarPointCount = 3
            };
            RegisterShapeFactory(new ShapePrototypeFactory(star), ShapeId.ThreePointStar);
            star.StarPointCount = 4;
            RegisterShapeFactory(new ShapePrototypeFactory(star), ShapeId.FourPointStar);
            star.StarPointCount = 5;
            RegisterShapeFactory(new ShapePrototypeFactory(star), ShapeId.FivePointStar);
            star.StarPointCount = 6;
            RegisterShapeFactory(new ShapePrototypeFactory(star), ShapeId.SixPointStar);
            star.StarPointCount = 8;
            RegisterShapeFactory(new ShapePrototypeFactory(star), ShapeId.EightPointStar);
            ShapeLine line = new ShapeLine();
            RegisterShapeFactory(new ShapePrototypeAngleFactory(line, 0), ShapeId.VerticalLine);
            RegisterShapeFactory(new ShapePrototypeAngleFactory(line, 270), ShapeId.HorizontalLine);
            RegisterShapeFactory(new ShapePrototypeAngleFactory(line, 0x87), ShapeId.SlantLine);
            RegisterShapeFactory(new ShapePrototypeAngleFactory(line, -135), ShapeId.BackslantLine);
            RegisterShapeFactory(new ShapePrototypeFactory(new ShapeCross()), ShapeId.Cross);
            RegisterShapeFactory(new ShapePrototypeFactory(new ShapeBracket()), ShapeId.Bracket);
            RegisterShapeFactory(new ShapePrototypeFactory(new ShapeBrace()), ShapeId.Brace);
            RegisterShapeType(typeof(ShapeArrow), ShapeId.Arrow);
            RegisterShapeType(typeof(ShapePolygon), ShapeId.Polygon);
            RegisterShapeType(typeof(ShapeStar), ShapeId.Star);
            RegisterShapeType(typeof(ShapeLine), ShapeId.Line);
        }

        private static void RegisterSampleBitmap(IShapeFactory factory, Size size)
        {
            using (new ShapeBrick())
            {
                RotatedShape shape = factory.CreateShape();
                ShapeBase base2 = shape.Shape;
                SampleShapeDrawingInfo.Instance.Angle = shape.Angle.GetValueOrDefault(0);
                Bitmap img = new Bitmap(size.Width, size.Height);
                using (PrintingSystemBase base3 = new PrintingSystemBase())
                {
                    using (GdiGraphics graphics = new ImageGraphics(img, base3))
                    {
                        SizeF ef = new SizeF(size.Width * 0.875f, size.Height * 0.875f);
                        PointF location = new PointF((size.Width - ef.Width) / 2f, (size.Height - ef.Height) / 2f);
                        ShapeHelper.DrawShapeContent(base2, graphics, GraphicsUnitConverter.PixelToDoc(new RectangleF(location, ef)), SampleShapeDrawingInfo.Instance);
                    }
                }
                sampleImages[size].Add(img);
            }
        }

        private static void RegisterShapeFactory(IShapeFactory factory, ShapeId shapeId)
        {
            nameIds.Add(shapeId);
            shapesHT[shapeId] = factory;
            RegisterShapeType(factory.ShapeType, shapeId);
        }

        private static void RegisterShapeType(Type shapeType, ShapeId shapeId)
        {
            shapeTypesHT[shapeId] = shapeType;
        }

        public static ShapeId[] ShapeNamesIds =>
            nameIds.ToArray();

        private static Size defaultSampleImageSize =>
            new Size(0x10, 0x10);

        public static List<System.Drawing.Image> SampleImages
        {
            get
            {
                if ((sampleImages == null) || !sampleImages.ContainsKey(defaultSampleImageSize))
                {
                    InitSampleImages(defaultSampleImageSize);
                }
                return sampleImages[defaultSampleImageSize];
            }
        }

        private class SampleShapeDrawingInfo : IShapeDrawingInfo
        {
            public static readonly ShapeFactory.SampleShapeDrawingInfo Instance = new ShapeFactory.SampleShapeDrawingInfo();
            private int angle;

            private SampleShapeDrawingInfo()
            {
            }

            float IShapeDrawingInfo.LineWidth =>
                GraphicsUnitConverter.Convert((float) 1f, (float) 96f, (float) 300f);

            DashStyle IShapeDrawingInfo.LineStyle =>
                DashStyle.Solid;

            public int Angle
            {
                get => 
                    this.angle;
                set => 
                    this.angle = value;
            }

            bool IShapeDrawingInfo.Stretch =>
                false;

            Color IShapeDrawingInfo.FillColor =>
                Color.Transparent;

            Color IShapeDrawingInfo.ForeColor =>
                Color.Black;
        }
    }
}

