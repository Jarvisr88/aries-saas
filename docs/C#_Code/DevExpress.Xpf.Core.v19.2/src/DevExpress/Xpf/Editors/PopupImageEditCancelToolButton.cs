namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;

    public class PopupImageEditCancelToolButton : PopupImageEditButton
    {
        public PopupImageEditCancelToolButton() : base(EditorLocalizer.GetString(EditorStringId.Cancel), "cancel", PopupImageEdit.CancelCommand)
        {
            this.SetDefaultStyleKey(typeof(ImageEditToolButton));
        }
    }
}

