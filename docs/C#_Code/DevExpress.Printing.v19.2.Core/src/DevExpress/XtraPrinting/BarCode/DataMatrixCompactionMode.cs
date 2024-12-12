namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum DataMatrixCompactionMode
    {
        public const DataMatrixCompactionMode ASCII = DataMatrixCompactionMode.ASCII;,
        public const DataMatrixCompactionMode C40 = DataMatrixCompactionMode.C40;,
        public const DataMatrixCompactionMode Text = DataMatrixCompactionMode.Text;,
        public const DataMatrixCompactionMode X12 = DataMatrixCompactionMode.X12;,
        public const DataMatrixCompactionMode Edifact = DataMatrixCompactionMode.Edifact;,
        public const DataMatrixCompactionMode Binary = DataMatrixCompactionMode.Binary;
    }
}

