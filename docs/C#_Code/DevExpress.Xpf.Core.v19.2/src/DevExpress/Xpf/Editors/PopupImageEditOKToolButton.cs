namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;

    public class PopupImageEditOKToolButton : PopupImageEditButton
    {
        public PopupImageEditOKToolButton() : base(EditorLocalizer.GetString(EditorStringId.OK), "ok", PopupImageEdit.OKCommand)
        {
            this.SetDefaultStyleKey(typeof(ImageEditToolButton));
        }
    }
}

