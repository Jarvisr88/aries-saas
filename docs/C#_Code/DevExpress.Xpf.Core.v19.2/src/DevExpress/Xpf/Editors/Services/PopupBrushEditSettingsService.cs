namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using System;

    public class PopupBrushEditSettingsService : PopupBaseEditSettingsService
    {
        public PopupBrushEditSettingsService(PopupBrushEditBase editor) : base(editor)
        {
        }

        public override bool IsInLookUpMode =>
            true;

        private PopupBrushEditBase OwnerEdit =>
            (PopupBrushEditBase) base.OwnerEdit;
    }
}

