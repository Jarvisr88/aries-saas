namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using System;

    public class BaseEditingSettingsService : BaseEditBaseService
    {
        public BaseEditingSettingsService(BaseEdit editor) : base(editor)
        {
        }

        protected virtual bool GetAllowEditing() => 
            !base.OwnerEdit.IsReadOnly && (base.OwnerEdit.IsEnabled && this.AllowKeyHandling);

        public bool AllowEditing =>
            this.GetAllowEditing();

        public bool AllowKeyHandling =>
            (base.OwnerEdit.EditMode != EditMode.InplaceInactive) && base.OwnerEdit.IsEnabled;

        public bool AllowTextInput =>
            base.EditStrategy.AllowTextInput;

        public virtual bool AllowSpin =>
            false;

        public virtual bool IsInLookUpMode =>
            false;
    }
}

