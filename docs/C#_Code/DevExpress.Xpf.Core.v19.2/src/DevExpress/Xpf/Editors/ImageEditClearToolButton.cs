namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;

    public class ImageEditClearToolButton : ImageEditToolButton
    {
        public ImageEditClearToolButton() : base(EditorLocalizer.GetString(EditorStringId.Clear), "clear", ApplicationCommands.Delete)
        {
            this.SetDefaultStyleKey(typeof(ImageEditToolButton));
        }
    }
}

