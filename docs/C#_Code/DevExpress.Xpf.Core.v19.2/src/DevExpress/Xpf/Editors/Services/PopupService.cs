namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using System;

    public class PopupService : BaseEditBaseService
    {
        public PopupService(TextEditBase editor) : base(editor)
        {
        }

        public void AcceptPopupValue()
        {
            if (this.OwnerEdit != null)
            {
                this.OwnerEdit.PopupSettings.AcceptPopupValue();
            }
        }

        public void SetPopupSource(IPopupSource popupSource)
        {
            if (this.OwnerEdit != null)
            {
                this.OwnerEdit.PopupSettings.SetPopupSource(popupSource);
                if (popupSource != null)
                {
                    this.OwnerEdit.ShowPopup();
                }
            }
        }

        private PopupBaseEdit OwnerEdit =>
            base.OwnerEdit as PopupBaseEdit;
    }
}

