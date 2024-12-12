namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    internal static class OrientationExtensions
    {
        public static Orientation OrthogonalValue(this Orientation value) => 
            (value == Orientation.Horizontal) ? Orientation.Vertical : Orientation.Horizontal;
    }
}

