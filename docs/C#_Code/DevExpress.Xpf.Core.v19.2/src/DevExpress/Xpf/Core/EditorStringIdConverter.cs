namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Localization;
    using DevExpress.Xpf.Editors;

    public class EditorStringIdConverter : StringIdConverter<EditorStringId>
    {
        protected override XtraLocalizer<EditorStringId> Localizer =>
            EditorLocalizer.Active;
    }
}

