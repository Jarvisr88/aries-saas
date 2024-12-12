namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;

    public class ImageEditSaveToolButton : ImageEditToolButton
    {
        public ImageEditSaveToolButton() : base(EditorLocalizer.GetString(EditorStringId.Save), "save", ApplicationCommands.Save)
        {
            this.SetDefaultStyleKey(typeof(ImageEditToolButton));
        }
    }
}

