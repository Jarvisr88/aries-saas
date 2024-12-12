namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_PRINT_CONTROL_PROPERTIES
    {
        public static readonly D2D1_PRINT_CONTROL_PROPERTIES Default;
        private readonly D2D1_PRINT_FONT_SUBSET_MODE fontSubset;
        private readonly float rasterDPI;
        private readonly D2D1_COLOR_SPACE colorSpace;
        public D2D1_PRINT_CONTROL_PROPERTIES(D2D1_PRINT_FONT_SUBSET_MODE fontSubset, float rasterDPI, D2D1_COLOR_SPACE colorSpace)
        {
            this.fontSubset = fontSubset;
            this.rasterDPI = rasterDPI;
            this.colorSpace = colorSpace;
        }

        static D2D1_PRINT_CONTROL_PROPERTIES()
        {
            Default = new D2D1_PRINT_CONTROL_PROPERTIES(D2D1_PRINT_FONT_SUBSET_MODE.Default, 150f, D2D1_COLOR_SPACE.SRgb);
        }
    }
}

