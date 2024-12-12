namespace DevExpress.XtraPrinting.Shape.Native
{
    public interface ILinesAdjuster
    {
        ShapeLineCommandCollection AdjustLines(ShapeLineCommand line1, ShapeLineCommand line2);
    }
}

