namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class ITabHeaderInfoExtensions
    {
        public static bool IsRightAligned(this ITabHeaderInfo info) => 
            info.IsPinned && (info.PinLocation == TabHeaderPinLocation.Far);
    }
}

