namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class PopupImageEditButton : ImageEditToolButton
    {
        internal PopupImageEditButton(string toolTip, string imageId, ICommand command) : base(toolTip, imageId, command)
        {
        }

        protected override IInputElement Owner =>
            PopupBaseEdit.GetPopupOwnerEdit(this) as PopupImageEdit;
    }
}

