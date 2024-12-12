namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum GlyphStyle
    {
        StandardBox1,
        StandardBox2,
        YesNoBox,
        YesNoSolidBox,
        YesNo,
        RadioButton,
        Smiley,
        Thumb,
        Toggle,
        Star,
        Heart
    }
}

