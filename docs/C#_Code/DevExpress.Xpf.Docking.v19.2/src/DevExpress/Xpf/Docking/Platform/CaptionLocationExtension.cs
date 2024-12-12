namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public static class CaptionLocationExtension
    {
        internal static Thickness RotateThickness(this CaptionLocation captionLocation, Thickness arg) => 
            captionLocation.ToDock(Dock.Top).RotateThickness(arg);

        public static Dock ToDock(this CaptionLocation captionLocation, Dock @default = 1)
        {
            switch (captionLocation)
            {
                case CaptionLocation.Left:
                    return Dock.Left;

                case CaptionLocation.Right:
                    return Dock.Right;

                case CaptionLocation.Bottom:
                    return Dock.Bottom;
            }
            return @default;
        }

        internal static Thickness ToThickness(this CaptionLocation captionLocation, Thickness arg) => 
            captionLocation.ToDock(Dock.Top).ToThickness(arg);
    }
}

