namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using System;

    public class PopupBaseEditSettingsService : ButtonEditSettingsService
    {
        public PopupBaseEditSettingsService(ButtonEdit editor) : base(editor)
        {
        }

        protected override bool GetAllowEditing() => 
            base.GetAllowEditing() && this.OwnerEdit.ShowText;

        private PopupBaseEdit OwnerEdit =>
            (PopupBaseEdit) base.OwnerEdit;
    }
}

