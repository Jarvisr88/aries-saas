namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;

    public class ImageEditLoadToolButton : ImageEditToolButton
    {
        public ImageEditLoadToolButton() : base(EditorLocalizer.GetString(EditorStringId.Open), "open", ApplicationCommands.Open)
        {
            this.SetDefaultStyleKey(typeof(ImageEditToolButton));
        }
    }
}

