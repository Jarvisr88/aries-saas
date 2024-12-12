namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;

    public interface IUnderlineFill
    {
        IUnderlineFill CloneTo(IDocumentModel documentModel);

        DrawingUnderlineFillType Type { get; }
    }
}

