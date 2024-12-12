namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;

    public class ImageEditPasteToolButton : ImageEditToolButton
    {
        public ImageEditPasteToolButton() : base(EditorLocalizer.GetString(EditorStringId.Paste), "paste", ApplicationCommands.Paste)
        {
            this.SetDefaultStyleKey(typeof(ImageEditToolButton));
        }
    }
}

