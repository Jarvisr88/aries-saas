namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public abstract class BrushEditBase : BaseEdit, IBrushEdit, IBaseEdit, IInputElement
    {
        private static readonly DependencyPropertyKey BrushTypePropertyKey;
        public static readonly DependencyProperty BrushTypeProperty;

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

        static BrushEditBase()
        {
            Type ownerType = typeof(BrushEditBase);
            BaseEdit.ValidateOnTextInputProperty.OverrideMetadata(typeof(BrushEditBase), new FrameworkPropertyMetadata(true));
            BrushTypePropertyKey = DependencyPropertyManager.RegisterReadOnly("BrushType", typeof(DevExpress.Xpf.Editors.BrushType), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.BrushType.SolidColorBrush, FrameworkPropertyMetadataOptions.None, (o, args) => ((BrushEditBase) o).BrushTypeChanged((DevExpress.Xpf.Editors.BrushType) args.NewValue)));
            BrushTypeProperty = BrushTypePropertyKey.DependencyProperty;
        }

        protected BrushEditBase()
        {
            this.SetDefaultStyleKey(typeof(BrushEditBase));
        }

        private void BrushPickerBrushChanged(object sender, EventArgs eventArgs)
        {
            this.EditStrategy.SyncWithEditor();
        }

        protected virtual void BrushTypeChanged(DevExpress.Xpf.Editors.BrushType newValue)
        {
            this.BrushPicker.Do<IBrushPicker>(x => x.BrushType = newValue);
        }

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new BrushEditPropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new BrushEditStrategy(this);

        protected internal override BaseEditStyleSettings CreateStyleSettings() => 
            new SolidColorBrushEditStyleSettings();

        void IBaseEdit.ClearValue(DependencyProperty dp)
        {
            base.ClearValue(dp);
        }

        object IBaseEdit.GetValue(DependencyProperty d) => 
            base.GetValue(d);

        protected internal override bool NeedsKey(Key key, ModifierKeys modifiers)
        {
            Func<bool> fallback = <>c.<>9__20_1;
            if (<>c.<>9__20_1 == null)
            {
                Func<bool> local1 = <>c.<>9__20_1;
                fallback = <>c.<>9__20_1 = () => true;
            }
            return (this.BrushPicker.Return<IBrushPicker, bool>(x => x.NeedsKey(key, modifiers), fallback) && base.NeedsKey(key, modifiers));
        }

        protected override bool NeedsTab() => 
            true;

        protected override void SubscribeEditEventsCore()
        {
            base.SubscribeEditEventsCore();
            this.BrushPicker.Do<IBrushPicker>(delegate (IBrushPicker x) {
                x.BrushChanged += new EventHandler(this.BrushPickerBrushChanged);
            });
        }

        protected override void UnsubscribeEditEventsCore()
        {
            base.UnsubscribeEditEventsCore();
            this.BrushPicker.Do<IBrushPicker>(delegate (IBrushPicker x) {
                x.BrushChanged -= new EventHandler(this.BrushPickerBrushChanged);
            });
        }

        protected internal override Type StyleSettingsType =>
            typeof(BrushEditStyleSettingsBase);

        public DevExpress.Xpf.Editors.BrushType BrushType
        {
            get => 
                (DevExpress.Xpf.Editors.BrushType) base.GetValue(BrushTypeProperty);
            internal set => 
                base.SetValue(BrushTypePropertyKey, value);
        }

        internal IBrushPicker BrushPicker =>
            base.EditCore as IBrushPicker;

        private BrushEditStrategy EditStrategy =>
            base.EditStrategy as BrushEditStrategy;

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

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BrushEditBase.<>c <>9 = new BrushEditBase.<>c();
            public static Func<bool> <>9__20_1;

            internal void <.cctor>b__2_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((BrushEditBase) o).BrushTypeChanged((BrushType) args.NewValue);
            }

            internal bool <NeedsKey>b__20_1() => 
                true;
        }
    }
}

