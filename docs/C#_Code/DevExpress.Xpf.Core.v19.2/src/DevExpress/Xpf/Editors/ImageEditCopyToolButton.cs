namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;

    public class ImageEditCopyToolButton : ImageEditToolButton
    {
        public ImageEditCopyToolButton() : base(EditorLocalizer.GetString(EditorStringId.Copy), "copy", ApplicationCommands.Copy)
        {
            this.SetDefaultStyleKey(typeof(ImageEditToolButton));
        }
    }
}

