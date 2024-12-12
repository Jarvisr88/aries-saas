namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.Drawing;

    public static class VisualBrickHelper
    {
        public static void DrawBrick(VisualBrick brick, IGraphics gr, RectangleF rect, RectangleF parentRect)
        {
            BrickBaseExporter.GetExporter(gr, brick).Draw(gr, rect, parentRect);
        }

        public static RectangleF GetBrickBounds(VisualBrick brick, float dpi) => 
            brick.GetBounds(dpi);

        public static RectangleF GetBrickInitialRect(Brick brick) => 
            brick.InitialRect;

        public static bool GetCanGrow(VisualBrick brick) => 
            brick.CanGrow;

        public static bool GetCanOverflow(VisualBrick brick) => 
            brick.CanOverflow;

        public static bool GetCanShrink(VisualBrick brick) => 
            brick.CanShrink;

        public static float GetTabInterval(Font font, StringFormat sf, GraphicsUnit unit, Measurer measurer) => 
            measurer.MeasureString("Q", font, (float) 0f, sf, unit).Width * 8f;

        public static void InitializeBrick(Brick brick, PrintingSystemBase ps, RectangleF rect)
        {
            brick.Initialize(ps, rect);
        }

        public static void SetBrickBounds(VisualBrick brick, RectangleF bounds, float dpi)
        {
            brick.SetBounds(bounds, dpi);
        }

        public static void SetBrickBoundsHeight(VisualBrick brick, float height, float dpi)
        {
            brick.SetBoundsHeight(height, dpi);
        }

        public static void SetBrickBoundsWidth(VisualBrick brick, float width, float dpi)
        {
            brick.SetBoundsWidth(width, dpi);
        }

        public static void SetBrickBoundsY(VisualBrick brick, float y, float dpi)
        {
            brick.SetBoundsY(y, dpi);
        }

        public static void SetBrickInitialRect(Brick brick, RectangleF value)
        {
            brick.InitialRect = value;
        }

        public static void SetCanGrow(VisualBrick brick, bool value)
        {
            brick.CanGrow = value;
        }

        public static void SetCanOverflow(VisualBrick brick, bool value)
        {
            brick.CanOverflow = value;
        }

        public static void SetCanShrink(VisualBrick brick, bool value)
        {
            brick.CanShrink = value;
        }
    }
}

