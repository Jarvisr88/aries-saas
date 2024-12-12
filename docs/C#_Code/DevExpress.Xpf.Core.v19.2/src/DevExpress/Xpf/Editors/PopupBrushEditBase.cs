namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public abstract class PopupBrushEditBase : PopupBaseEdit, IPopupBrushEdit, IBrushEdit, IBaseEdit, IInputElement
    {
        public static readonly DependencyProperty ChipBorderBrushProperty;
        private static readonly DependencyPropertyKey BrushTypePropertyKey;
        public static readonly DependencyProperty BrushTypeProperty;
        public static readonly DependencyProperty AllowEditBrushTypeProperty;

        event RoutedEventHandler IBaseEdit.Loaded
        {
            add
            {
                base.Loaded += value;
            }
            remove
            {
                base.Loaded -= value;
            }
        }

        event RoutedEventHandler IBaseEdit.Unloaded
        {
            add
            {
                base.Unloaded += value;
            }
            remove
            {
                base.Unloaded -= value;
            }
        }

        static PopupBrushEditBase()
        {
            Type forType = typeof(PopupBrushEditBase);
            PopupBaseEdit.FocusPopupOnOpenProperty.OverrideMetadata(typeof(PopupBrushEditBase), new FrameworkPropertyMetadata(true));
            BaseEdit.ValidateOnTextInputProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(false));
            BrushTypePropertyKey = DependencyPropertyManager.RegisterReadOnly("BrushType", typeof(DevExpress.Xpf.Editors.BrushType), forType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.BrushType.SolidColorBrush, FrameworkPropertyMetadataOptions.None, (o, args) => ((PopupBrushEditBase) o).BrushTypeChanged((DevExpress.Xpf.Editors.BrushType) args.OldValue, (DevExpress.Xpf.Editors.BrushType) args.NewValue)));
            BrushTypeProperty = BrushTypePropertyKey.DependencyProperty;
            ChipBorderBrushProperty = DependencyPropertyManager.Register("ChipBorderBrush", typeof(SolidColorBrush), forType, new FrameworkPropertyMetadata(null));
            AllowEditBrushTypeProperty = DependencyPropertyManager.Register("AllowEditBrushType", typeof(bool), forType, new FrameworkPropertyMetadata(true));
            PopupBaseEdit.PopupMinWidthProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(300.0));
        }

        protected PopupBrushEditBase()
        {
            this.SetDefaultStyleKey(typeof(PopupBrushEditBase));
        }

        protected override void AcceptPopupValue()
        {
            base.AcceptPopupValue();
            this.EditStrategy.AcceptPopupValue();
        }

        protected virtual void BrushTypeChanged(DevExpress.Xpf.Editors.BrushType oldValue, DevExpress.Xpf.Editors.BrushType newValue)
        {
            this.EditStrategy.BrushTypeChanged(oldValue, newValue);
        }

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new PopupBrushEditPropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new PopupBrushEditStrategy(this);

        protected internal override BaseEditStyleSettings CreateStyleSettings() => 
            new PopupBrushEditStyleSettings();

        protected internal override TextInputSettingsBase CreateTextInputSettings() => 
            new TextInputColorEditAutoCompleteSettings(this);

        protected internal override void DestroyPopupContent(EditorPopupBase popup)
        {
            base.DestroyPopupContent(popup);
            this.EditStrategy.DestroyPopup();
        }

        void IBaseEdit.ClearValue(DependencyProperty dp)
        {
            base.ClearValue(dp);
        }

        object IBaseEdit.GetValue(DependencyProperty d) => 
            base.GetValue(d);

        PopupBrushValue IPopupBrushEdit.GetPopupBrushValue(DevExpress.Xpf.Editors.BrushType brushType) => 
            this.EditStrategy.GetPopupEditValue(brushType);

        protected override bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers) => 
            base.IsClosePopupWithAcceptGesture(key, modifiers) || this.EditStrategy.IsClosePopupWithAcceptGesture(key, modifiers);

        public override FrameworkElement PopupElement =>
            base.VisualClient.InnerEditor;

        protected internal override Type StyleSettingsType =>
            typeof(PopupBrushEditStyleSettingsBase);

        private PopupBrushEditStrategy EditStrategy =>
            base.EditStrategy as PopupBrushEditStrategy;

        public bool AllowEditBrushType
        {
            get => 
                (bool) base.GetValue(AllowEditBrushTypeProperty);
            set => 
                base.SetValue(AllowEditBrushTypeProperty, value);
        }

        public DevExpress.Xpf.Editors.BrushType BrushType
        {
            get => 
                (DevExpress.Xpf.Editors.BrushType) base.GetValue(BrushTypeProperty);
            internal set => 
                base.SetValue(BrushTypePropertyKey, value);
        }

        public SolidColorBrush ChipBorderBrush
        {
            get => 
                (SolidColorBrush) base.GetValue(ChipBorderBrushProperty);
            set => 
                base.SetValue(ChipBorderBrushProperty, value);
        }

        object IBaseEdit.DataContext
        {
            get => 
                base.DataContext;
            set => 
                base.DataContext = value;
        }

        HorizontalAlignment IBaseEdit.HorizontalContentAlignment
        {
            get => 
                base.HorizontalContentAlignment;
            set => 
                base.HorizontalContentAlignment = value;
        }

        VerticalAlignment IBaseEdit.VerticalContentAlignment
        {
            get => 
                base.VerticalContentAlignment;
            set => 
                base.VerticalContentAlignment = value;
        }

        double IBaseEdit.MaxWidth
        {
            get => 
                base.MaxWidth;
            set => 
                base.MaxWidth = value;
        }

        bool IBaseEdit.ShowEditorButtons
        {
            get => 
                base.ShowEditorButtons;
            set => 
                base.ShowEditorButtons = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PopupBrushEditBase.<>c <>9 = new PopupBrushEditBase.<>c();

            internal void <.cctor>b__4_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((PopupBrushEditBase) o).BrushTypeChanged((BrushType) args.OldValue, (BrushType) args.NewValue);
            }
        }
    }
}

