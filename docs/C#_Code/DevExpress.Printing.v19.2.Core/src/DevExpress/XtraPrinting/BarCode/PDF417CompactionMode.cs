namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum PDF417CompactionMode
    {
        public const PDF417CompactionMode Binary = PDF417CompactionMode.Binary;,
        public const PDF417CompactionMode Text = PDF417CompactionMode.Text;
    }
}

