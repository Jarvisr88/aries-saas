namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;

    public interface IBandedViewAppearance : IGridViewAppearance
    {
        XlCellFormatting BandPanel { get; }

        XlCellFormatting BandPanelBackground { get; }

        XlCellFormatting HeaderPanelBackground { get; }
    }
}

