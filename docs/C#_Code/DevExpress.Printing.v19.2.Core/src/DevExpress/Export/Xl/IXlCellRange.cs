namespace DevExpress.Export.Xl
{
    public interface IXlCellRange
    {
        XlCellPosition TopLeft { get; }

        XlCellPosition BottomRight { get; }
    }
}

