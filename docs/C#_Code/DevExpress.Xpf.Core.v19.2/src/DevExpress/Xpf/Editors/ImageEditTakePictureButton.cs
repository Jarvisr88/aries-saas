namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Camera;
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class ImageEditTakePictureButton : ImageEditToolButton
    {
        public ImageEditTakePictureButton() : base(EditorLocalizer.GetString(EditorStringId.CameraTakePictureToolTip), "takepicture", null)
        {
            this.SetDefaultStyleKey(typeof(ImageEditToolButton));
            base.Command = this.GetCommand();
        }

        private ICommand GetCommand() => 
            new DelegateCommand(new Action(this.ShowTakePictureDialog));

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.Visibility = (DeviceHelper.GetDevices().Count > 0) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ShowTakePictureDialog()
        {
            int num = 350;
            DXWindow window1 = new DXWindow();
            window1.Width = num;
            window1.Height = num;
            window1.MinHeight = num;
            window1.MinWidth = num;
            window1.ShowIcon = false;
            window1.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            DXWindow window = window1;
            Window window2 = LayoutHelper.FindLayoutOrVisualParentObject<Window>(this.Owner as DependencyObject, true, null);
            if (window2 != null)
            {
                window.Owner = window2;
            }
            window.Title = EditorLocalizer.GetString(EditorStringId.CameraTakePictureCaption);
            TakePictureControl control = new TakePictureControl();
            ImageEdit owner = (ImageEdit) this.Owner;
            control.SetEditor(owner, owner.PopupOwnerEdit as PopupImageEdit);
            window.Content = control;
            window.ShowDialog();
        }
    }
}

