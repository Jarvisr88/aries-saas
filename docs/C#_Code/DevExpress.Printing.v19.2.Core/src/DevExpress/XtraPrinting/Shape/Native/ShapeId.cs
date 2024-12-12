namespace DevExpress.XtraPrinting.Shape.Native
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum ShapeId
    {
        Rectangle,
        Ellipse,
        Arrow,
        TopArrow,
        BottomArrow,
        LeftArrow,
        RightArrow,
        Polygon,
        Triangle,
        Square,
        Pentagon,
        Hexagon,
        Octagon,
        Star,
        ThreePointStar,
        FourPointStar,
        FivePointStar,
        SixPointStar,
        EightPointStar,
        Line,
        SlantLine,
        BackslantLine,
        HorizontalLine,
        VerticalLine,
        Cross,
        Brace,
        Bracket
    }
}

