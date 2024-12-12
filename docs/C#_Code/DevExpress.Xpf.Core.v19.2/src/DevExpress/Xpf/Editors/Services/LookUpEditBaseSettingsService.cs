namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using System;

    public class LookUpEditBaseSettingsService : PopupBaseEditSettingsService
    {
        public LookUpEditBaseSettingsService(LookUpEditBase editor) : base(editor)
        {
        }

        protected override bool GetIsTextEditableInternal() => 
            ((LookUpEditBasePropertyProvider) base.PropertyProvider).IsTextEditable;
    }
}

