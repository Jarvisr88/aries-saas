namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class PopupViewModel : EditorViewModelBase
    {
        public static readonly DependencyProperty NullValueButtonVisibilityProperty;
        public static readonly DependencyProperty AddNewButtonVisibilityProperty;
        public static readonly DependencyProperty OkButtonVisibilityProperty;
        public static readonly DependencyProperty CancelButtonVisibilityProperty;
        public static readonly DependencyProperty CloseButtonVisibilityProperty;
        public static readonly DependencyProperty FooterVisibilityProperty;
        public static readonly DependencyProperty DropOppositeProperty;
        public static readonly DependencyProperty OkButtonIsEnabledProperty;
        public static readonly DependencyProperty IsLeftProperty;

        static PopupViewModel()
        {
            Type ownerType = typeof(PopupViewModel);
            NullValueButtonVisibilityProperty = DependencyPropertyManager.Register("NullValueButtonVisibility", typeof(Visibility), ownerType, new PropertyMetadata(Visibility.Collapsed));
            AddNewButtonVisibilityProperty = DependencyPropertyManager.Register("AddNewButtonVisibility", typeof(Visibility), ownerType, new PropertyMetadata(Visibility.Collapsed));
            OkButtonVisibilityProperty = DependencyPropertyManager.Register("OkButtonVisibility", typeof(Visibility), ownerType, new PropertyMetadata(Visibility.Collapsed));
            CancelButtonVisibilityProperty = DependencyPropertyManager.Register("CancelButtonVisibility", typeof(Visibility), ownerType, new PropertyMetadata(Visibility.Collapsed));
            CloseButtonVisibilityProperty = DependencyPropertyManager.Register("CloseButtonVisibility", typeof(Visibility), ownerType, new PropertyMetadata(Visibility.Collapsed));
            FooterVisibilityProperty = DependencyPropertyManager.Register("FooterVisibility", typeof(Visibility), ownerType, new PropertyMetadata(Visibility.Collapsed));
            DropOppositeProperty = DependencyPropertyManager.Register("DropOpposite", typeof(bool), ownerType, new PropertyMetadata(false));
            IsLeftProperty = DependencyPropertyManager.Register("IsLeft", typeof(bool), ownerType, new PropertyMetadata(false));
            OkButtonIsEnabledProperty = DependencyPropertyManager.Register("OkButtonIsEnabled", typeof(bool), ownerType, new PropertyMetadata(true));
        }

        public PopupViewModel(BaseEdit editor) : base(editor)
        {
        }

        private Visibility BooleanToVisibility(bool visible) => 
            visible ? Visibility.Visible : Visibility.Collapsed;

        private bool EditorPlacementToBoolean(EditorPlacement placement) => 
            placement == EditorPlacement.Popup;

        private Visibility EditorPlacementToVisibility(EditorPlacement placement) => 
            (placement == EditorPlacement.Popup) ? Visibility.Visible : Visibility.Collapsed;

        public virtual void Update()
        {
            bool visible = this.PropertyProvider.GetPopupFooterButtons() == PopupFooterButtons.OkCancel;
            bool flag2 = this.PropertyProvider.GetPopupFooterButtons() == PopupFooterButtons.OkCancel;
            bool flag3 = this.PropertyProvider.GetPopupFooterButtons() == PopupFooterButtons.Close;
            bool flag4 = this.EditorPlacementToBoolean(this.PropertyProvider.GetNullValueButtonPlacement());
            bool flag5 = this.EditorPlacementToBoolean(this.PropertyProvider.GetAddNewButtonPlacement());
            this.FooterVisibility = this.BooleanToVisibility(((((this.PropertyProvider.GetShowSizeGrip() | visible) | flag2) | flag3) | flag4) | flag5);
            this.OkButtonVisibility = this.BooleanToVisibility(visible);
            this.CancelButtonVisibility = this.BooleanToVisibility(flag2);
            this.CloseButtonVisibility = this.BooleanToVisibility(flag3);
            this.NullValueButtonVisibility = this.BooleanToVisibility(flag4);
            this.AddNewButtonVisibility = this.BooleanToVisibility(flag5);
        }

        public virtual void UpdateDropOpposite()
        {
            this.DropOpposite = this.OwnerEdit.PopupSettings.PopupResizingStrategy.DropOpposite;
            this.IsLeft = this.OwnerEdit.PopupSettings.PopupResizingStrategy.IsLeft;
        }

        private PopupBaseEdit OwnerEdit =>
            (PopupBaseEdit) base.OwnerEdit;

        private PopupBaseEditPropertyProvider PropertyProvider =>
            (PopupBaseEditPropertyProvider) base.PropertyProvider;

        public Visibility NullValueButtonVisibility
        {
            get => 
                (Visibility) base.GetValue(NullValueButtonVisibilityProperty);
            set => 
                base.SetValue(NullValueButtonVisibilityProperty, value);
        }

        public Visibility AddNewButtonVisibility
        {
            get => 
                (Visibility) base.GetValue(AddNewButtonVisibilityProperty);
            set => 
                base.SetValue(AddNewButtonVisibilityProperty, value);
        }

        public Visibility OkButtonVisibility
        {
            get => 
                (Visibility) base.GetValue(OkButtonVisibilityProperty);
            set => 
                base.SetValue(OkButtonVisibilityProperty, value);
        }

        public Visibility CancelButtonVisibility
        {
            get => 
                (Visibility) base.GetValue(CancelButtonVisibilityProperty);
            set => 
                base.SetValue(CancelButtonVisibilityProperty, value);
        }

        public Visibility CloseButtonVisibility
        {
            get => 
                (Visibility) base.GetValue(CloseButtonVisibilityProperty);
            set => 
                base.SetValue(CloseButtonVisibilityProperty, value);
        }

        public Visibility FooterVisibility
        {
            get => 
                (Visibility) base.GetValue(FooterVisibilityProperty);
            set => 
                base.SetValue(FooterVisibilityProperty, value);
        }

        public bool DropOpposite
        {
            get => 
                (bool) base.GetValue(DropOppositeProperty);
            set => 
                base.SetValue(DropOppositeProperty, value);
        }

        public bool IsLeft
        {
            get => 
                (bool) base.GetValue(IsLeftProperty);
            set => 
                base.SetValue(IsLeftProperty, value);
        }

        public bool OkButtonIsEnabled
        {
            get => 
                (bool) base.GetValue(OkButtonIsEnabledProperty);
            set => 
                base.SetValue(OkButtonIsEnabledProperty, value);
        }
    }
}

