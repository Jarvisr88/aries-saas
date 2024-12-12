namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public static class LayoutControl
    {
        private static bool IsValidBounds(RectangleF bounds) => 
            !FloatsComparer.Default.RectangleIsEmpty(bounds);

        public static ILayoutControl Validate(ILayoutControl control) => 
            Validate(control, true);

        public static ILayoutControl Validate(ILayoutControl control, bool useBoundsF) => 
            IsValidBounds(useBoundsF ? control.BoundsF : control.Bounds) ? control : null;
    }
}

