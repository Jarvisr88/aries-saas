namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;

    public class ImageEditCutToolButton : ImageEditToolButton
    {
        public ImageEditCutToolButton() : base(EditorLocalizer.GetString(EditorStringId.Cut), "cut", ApplicationCommands.Cut)
        {
            this.SetDefaultStyleKey(typeof(ImageEditToolButton));
        }
    }
}

