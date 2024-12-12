namespace DevExpress.Office.Drawing
{
    public interface IDrawingTextSpacings
    {
        IDrawingTextSpacing Line { get; }

        IDrawingTextSpacing SpaceAfter { get; }

        IDrawingTextSpacing SpaceBefore { get; }
    }
}

