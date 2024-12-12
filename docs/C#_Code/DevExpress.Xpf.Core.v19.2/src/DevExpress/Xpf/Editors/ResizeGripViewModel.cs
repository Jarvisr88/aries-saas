namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class ResizeGripViewModel : EditorViewModelBase
    {
        public static readonly DependencyProperty IsDownProperty;

        static ResizeGripViewModel()
        {
            Type ownerType = typeof(ResizeGripViewModel);
            IsDownProperty = DependencyPropertyManager.Register("IsDown", typeof(bool), ownerType, new PropertyMetadata(true));
        }

        public ResizeGripViewModel(BaseEdit editor) : base(editor)
        {
        }

        private Cursor GetCursor() => 
            !((this.DropOpposite ^ this.IsRTL) ^ this.IsLeft) ? Cursors.SizeNWSE : Cursors.SizeNESW;

        private VerticalAlignment GetVerticalAlignment() => 
            !this.DropOpposite ? VerticalAlignment.Bottom : VerticalAlignment.Top;

        public void Update()
        {
            this.Visibility = this.PropertyProvider.GetShowSizeGrip() ? Visibility.Visible : Visibility.Collapsed;
            this.IsDown = !this.DropOpposite;
            base.Cursor = this.GetCursor();
            base.VerticalAlignment = this.GetVerticalAlignment();
        }

        public bool IsDown
        {
            get => 
                (bool) base.GetValue(IsDownProperty);
            set => 
                base.SetValue(IsDownProperty, value);
        }

        private bool IsLeft =>
            this.OwnerEdit.PopupSettings.PopupResizingStrategy.IsLeft;

        private bool DropOpposite =>
            this.OwnerEdit.PopupSettings.PopupResizingStrategy.DropOpposite;

        private bool IsRTL =>
            this.OwnerEdit.PopupSettings.PopupResizingStrategy.IsRTL;

        private PopupBaseEditPropertyProvider PropertyProvider =>
            (PopupBaseEditPropertyProvider) base.PropertyProvider;

        private PopupBaseEdit OwnerEdit =>
            (PopupBaseEdit) base.OwnerEdit;
    }
}

