namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using System;

    public class ButtonEditSettingsService : TextEditSettingsService
    {
        public ButtonEditSettingsService(ButtonEdit editor) : base(editor)
        {
        }

        protected override bool GetAllowEditing() => 
            base.GetAllowEditing() && this.GetIsTextEditableInternal();

        protected virtual bool GetIsTextEditableInternal() => 
            this.PropertyProvider.IsTextEditable;

        private ButtonEdit OwnerEdit =>
            (ButtonEdit) base.OwnerEdit;

        private ButtonEditPropertyProvider PropertyProvider =>
            (ButtonEditPropertyProvider) base.PropertyProvider;
    }
}

