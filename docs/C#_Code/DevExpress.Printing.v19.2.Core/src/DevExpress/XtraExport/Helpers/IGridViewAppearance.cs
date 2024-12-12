namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;

    public interface IGridViewAppearance
    {
        XlCellFormatting AppearanceEvenRow { get; }

        XlCellFormatting AppearanceOddRow { get; }

        XlCellFormatting AppearanceGroupRow { get; }

        XlCellFormatting AppearanceFooter { get; }

        XlCellFormatting AppearanceGroupFooter { get; }

        XlCellFormatting AppearanceRow { get; }

        XlCellFormatting AppearanceHeader { get; }
    }
}

