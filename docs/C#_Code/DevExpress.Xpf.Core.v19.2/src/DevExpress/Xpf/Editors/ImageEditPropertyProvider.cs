namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class ImageEditPropertyProvider : ActualPropertyProvider
    {
        public static readonly DependencyProperty InplaceMenuVisibilityProperty;

        static ImageEditPropertyProvider()
        {
            Type ownerType = typeof(ImageEditPropertyProvider);
            InplaceMenuVisibilityProperty = DependencyPropertyManager.Register("InplaceMenuVisibility", typeof(Visibility), ownerType, new FrameworkPropertyMetadata(Visibility.Collapsed));
        }

        public ImageEditPropertyProvider(BaseEdit editor) : base(editor)
        {
            this.CanShowMenuPopupCommand = DelegateCommandFactory.Create<CancelEventArgs>(args => args.Cancel = !this.Editor.CanShowMenuPopup, false);
        }

        public override bool CalcSuppressFeatures() => 
            false;

        public void UpdateInplaceMenuVisibility()
        {
            this.InplaceMenuVisibility = (!this.Editor.ShowMenu || (this.Editor.ShowMenuMode != ShowMenuMode.Always)) ? Visibility.Collapsed : Visibility.Visible;
        }

        public ImageEdit Editor =>
            base.Editor as ImageEdit;

        public Visibility InplaceMenuVisibility
        {
            get => 
                (Visibility) base.GetValue(InplaceMenuVisibilityProperty);
            set => 
                base.SetValue(InplaceMenuVisibilityProperty, value);
        }

        public ICommand CanShowMenuPopupCommand { get; private set; }
    }
}

