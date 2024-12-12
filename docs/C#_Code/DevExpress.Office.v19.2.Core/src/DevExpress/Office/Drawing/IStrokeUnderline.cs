namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;

    public interface IStrokeUnderline
    {
        IStrokeUnderline CloneTo(IDocumentModel documentModel);

        DrawingStrokeUnderlineType Type { get; }
    }
}

