namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Localization;
    using DevExpress.Xpf.Core;

    public class GridControlStringIdConverter : StringIdConverter<GridControlStringId>
    {
        protected override XtraLocalizer<GridControlStringId> Localizer =>
            XtraLocalizer<GridControlStringId>.Active;
    }
}

