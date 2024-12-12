namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using System;

    public class HyperlinkEditEditSettingsService : BaseEditingSettingsService
    {
        public HyperlinkEditEditSettingsService(HyperlinkEdit editor) : base(editor)
        {
        }

        protected override bool GetAllowEditing() => 
            false;

        private PopupBaseEdit OwnerEdit =>
            (PopupBaseEdit) base.OwnerEdit;
    }
}

