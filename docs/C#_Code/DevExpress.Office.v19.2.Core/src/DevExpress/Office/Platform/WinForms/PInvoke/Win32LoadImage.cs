namespace DevExpress.Office.Platform.WinForms.PInvoke
{
    using System;
    using System.Security;

    [CLSCompliant(false)]
    public static class Win32LoadImage
    {
        [SecuritySafeCritical]
        public static IntPtr LoadImage(IntPtr hinst, string lpszName, uint type, int cxDesired, int cyDesired, uint fuLoad) => 
            PInvokeSafeNativeMethods.LoadImage(hinst, lpszName, type, cxDesired, cyDesired, fuLoad);

        public class LoadOptionsFlags
        {
            public const uint LR_DEFAULTCOLOR = 0;
            public const uint LR_MONOCHROME = 1;
            public const uint LR_LOADFROMFILE = 0x10;
        }

        public class TypeImageFlags
        {
            public const uint IMAGE_BITMAP = 0;
            public const uint IMAGE_ICON = 1;
            public const uint IMAGE_CURSOR = 2;
        }
    }
}

