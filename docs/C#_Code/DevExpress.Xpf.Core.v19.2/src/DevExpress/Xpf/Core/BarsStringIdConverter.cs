namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Localization;
    using DevExpress.Xpf.Bars;

    public class BarsStringIdConverter : StringIdConverter<BarsStringId>
    {
        protected override XtraLocalizer<BarsStringId> Localizer =>
            BarsLocalizer.Active;
    }
}

