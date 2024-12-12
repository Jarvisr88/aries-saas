namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum BarCodeOrientation
    {
        public const BarCodeOrientation Normal = BarCodeOrientation.Normal;,
        public const BarCodeOrientation UpsideDown = BarCodeOrientation.UpsideDown;,
        public const BarCodeOrientation RotateLeft = BarCodeOrientation.RotateLeft;,
        public const BarCodeOrientation RotateRight = BarCodeOrientation.RotateRight;
    }
}

