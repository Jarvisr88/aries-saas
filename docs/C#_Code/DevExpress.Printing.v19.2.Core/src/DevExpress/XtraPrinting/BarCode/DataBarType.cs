namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum DataBarType
    {
        public const DataBarType Omnidirectional = DataBarType.Omnidirectional;,
        public const DataBarType Truncated = DataBarType.Truncated;,
        public const DataBarType Stacked = DataBarType.Stacked;,
        public const DataBarType StackedOmnidirectional = DataBarType.StackedOmnidirectional;,
        public const DataBarType Limited = DataBarType.Limited;,
        public const DataBarType Expanded = DataBarType.Expanded;,
        public const DataBarType ExpandedStacked = DataBarType.ExpandedStacked;
    }
}

