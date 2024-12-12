namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public static class BrickPSHelper
    {
        public static IPrintingSystem GetPS(BrickStyle style);
        public static void SetPS(BrickStyle style, IPrintingSystem ps);
    }
}

