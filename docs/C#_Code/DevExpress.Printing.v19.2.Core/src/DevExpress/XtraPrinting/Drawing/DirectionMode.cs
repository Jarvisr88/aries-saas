namespace DevExpress.XtraPrinting.Drawing
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum DirectionMode
    {
        Horizontal = 0,
        ForwardDiagonal = 1,
        BackwardDiagonal = 2,
        Vertical = 4
    }
}

