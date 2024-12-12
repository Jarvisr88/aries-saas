namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Input;

    public class CustomColorEdit : ColorEdit
    {
        private void ShowMoreColors()
        {
            IColorEdit owner = this;
            base.CloseOwnedPopup(false);
            ColorEditHelper.ShowColorChooserDialog(owner);
        }

        public ICommand MoreColorsCommand =>
            DelegateCommandFactory.Create(new Action(this.ShowMoreColors));
    }
}

