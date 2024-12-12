namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class RenderOffsetHelper
    {
        private static Point ApplyLeftRotation(Point pt);
        private static Point ApplyRightRotation(Point pt);
        public static Point GetCorrectedRenderOffset(UIElement element, UIElement topElement);
        private static bool IsRotatedLeft(Matrix mat);
        private static bool IsRotatedRight(Matrix mat);
        public static void UpdateRenderOffset(UIElement element, ref Point prevOffset);
    }
}

