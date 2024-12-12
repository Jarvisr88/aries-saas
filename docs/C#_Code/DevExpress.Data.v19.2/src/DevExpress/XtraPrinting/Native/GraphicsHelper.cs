namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public static class GraphicsHelper
    {
        [ThreadStatic]
        private static Bitmap staticBitmap;
        [ThreadStatic]
        private static Bitmap staticHiResBitmap;
        private static bool? canCreateFromZeroHwnd;
        private static bool? canUseGetHdc;
        private static bool? canUseFontSizeInPoints;

        public static Graphics CreateGraphics() => 
            CreateGraphicsCore(true);

        private static Graphics CreateGraphicsCore(bool checkAsp)
        {
            if (CanCreateFromZeroHwnd)
            {
                Graphics graphics = CreateGraphicsFromZeroHwnd();
                if (!checkAsp || (!PSNativeMethods.AspIsRunning || ((graphics.DpiX == 96.0) && (graphics.DpiY == 96.0))))
                {
                    return graphics;
                }
                graphics.Dispose();
            }
            return CreateGraphicsFromImage();
        }

        public static Graphics CreateGraphicsFromHiResImage()
        {
            if (staticHiResBitmap == null)
            {
                staticHiResBitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
                staticHiResBitmap.SetResolution(600f, 600f);
            }
            return Graphics.FromImage(staticHiResBitmap);
        }

        public static Graphics CreateGraphicsFromImage()
        {
            if (staticBitmap == null)
            {
                staticBitmap = new Bitmap(10, 10, PixelFormat.Format32bppArgb);
                staticBitmap.SetResolution(96f, 96f);
            }
            return Graphics.FromImage(staticBitmap);
        }

        private static Graphics CreateGraphicsFromZeroHwnd() => 
            Graphics.FromHwnd(IntPtr.Zero);

        public static Graphics CreateGraphicsWithoutAspCheck() => 
            CreateGraphicsCore(false);

        public static void ResetCanCreateFromZeroHwnd()
        {
            canCreateFromZeroHwnd = null;
        }

        public static bool CanCreateFromZeroHwnd
        {
            get
            {
                if (canCreateFromZeroHwnd == null)
                {
                    try
                    {
                        using (CreateGraphicsFromZeroHwnd())
                        {
                            canCreateFromZeroHwnd = true;
                        }
                    }
                    catch
                    {
                        canCreateFromZeroHwnd = false;
                    }
                }
                return canCreateFromZeroHwnd.Value;
            }
        }

        public static bool CanUseGetHdc
        {
            get
            {
                if (canUseGetHdc == null)
                {
                    try
                    {
                        using (Bitmap bitmap = new Bitmap(1, 1))
                        {
                            using (Graphics graphics = Graphics.FromImage(bitmap))
                            {
                                try
                                {
                                    graphics.GetHdc();
                                }
                                finally
                                {
                                    try
                                    {
                                        graphics.ReleaseHdc();
                                    }
                                    catch
                                    {
                                        canUseGetHdc = false;
                                    }
                                }
                            }
                        }
                        if (canUseGetHdc == null)
                        {
                            canUseGetHdc = true;
                        }
                    }
                    catch
                    {
                        canUseGetHdc = false;
                    }
                }
                return canUseGetHdc.GetValueOrDefault();
            }
        }

        public static bool CanUseFontSizeInPoints
        {
            get
            {
                if (canUseFontSizeInPoints == null)
                {
                    try
                    {
                        using (Font font = new Font("Arial", 12f, GraphicsUnit.Document))
                        {
                            float sizeInPoints = font.SizeInPoints;
                        }
                        canUseFontSizeInPoints = true;
                    }
                    catch
                    {
                        canUseFontSizeInPoints = false;
                    }
                }
                return canUseFontSizeInPoints.GetValueOrDefault();
            }
        }
    }
}

