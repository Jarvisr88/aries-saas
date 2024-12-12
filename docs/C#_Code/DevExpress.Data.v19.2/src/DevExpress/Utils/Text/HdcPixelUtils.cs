namespace DevExpress.Utils.Text
{
    using System;

    public static class HdcPixelUtils
    {
        public static int GetLogicPixelPerInchX(IntPtr hdc) => 
            Win32Util.GetLogicPixelPerInchX(hdc);

        public static int GetLogicPixelPerInchY(IntPtr hdc) => 
            Win32Util.GetLogicPixelPerInchY(hdc);
    }
}

