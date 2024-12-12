namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Automation;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Input;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider))]
    public class SpinEdit : ButtonEdit
    {
        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;
        public static readonly DependencyProperty IncrementProperty;
        public static readonly DependencyProperty IsFloatValueProperty;
        public static readonly DependencyProperty AllowRoundOutOfRangeValueProperty;

        static SpinEdit()
        {
            Type ownerType = typeof(SpinEdit);
            AllowRoundOutOfRangeValueProperty = DependencyPropertyManager.Register("AllowRoundOutOfRangeValue", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (d, e) => ((SpinEdit) d).AllowRoundOutOfRangeValueChanged((bool) e.NewValue)));
            ValueProperty = DependencyPropertyManager.Register("Value", typeof(decimal), ownerType, new FrameworkPropertyMetadata(0M, new PropertyChangedCallback(SpinEdit.OnDecimalValueChanged), new CoerceValueCallback(SpinEdit.CoerceDecimalValue)));
            MinValueProperty = DependencyPropertyManager.Register("MinValue", typeof(decimal?), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(SpinEdit.OnMinValueChanged), new CoerceValueCallback(SpinEdit.CoerceMinValue)));
            MaxValueProperty = DependencyPropertyManager.Register("MaxValue", typeof(decimal?), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(SpinEdit.OnMaxValueChanged), new CoerceValueCallback(SpinEdit.CoerceMaxValue)));
            IncrementProperty = DependencyPropertyManager.Register("Increment", typeof(decimal), ownerType, new FrameworkPropertyMetadata(1M));
            IsFloatValueProperty = DependencyPropertyManager.Register("IsFloatValue", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, new PropertyChangedCallback(SpinEdit.OnIsFloatValueChanged)));
            Control.HorizontalContentAlignmentProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(HorizontalAlignment.Right));
            TextEdit.MaskTypeProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(MaskType.Numeric));
            Control.VerticalContentAlignmentProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(VerticalAlignment.Center));
        }

        public SpinEdit()
        {
            this.SetCommands();
        }

        protected virtual void AllowRoundOutOfRangeValueChanged(bool value)
        {
            this.EditStrategy.RoundToBoundsChanged(value);
        }

        protected void CanMaximize(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.EditStrategy != null)
            {
                e.CanExecute = this.EditStrategy.CanMaximize();
            }
        }

        protected void CanMinimize(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.EditStrategy != null)
            {
                e.CanExecute = this.EditStrategy.CanMinimize();
            }
        }

        protected virtual decimal CoerceDecimalValue(decimal baseValue) => 
            this.EditStrategy.CoerceDecimalValue(baseValue);

        protected static object CoerceDecimalValue(DependencyObject d, object baseValue) => 
            ((SpinEdit) d).CoerceDecimalValue((decimal) baseValue);

        protected virtual decimal? CoerceMaxValue(decimal? baseValue) => 
            this.EditStrategy.CoerceMaxValue(baseValue);

        protected static object CoerceMaxValue(DependencyObject d, object baseValue) => 
            ((SpinEdit) d).CoerceMaxValue((decimal?) baseValue);

        protected virtual decimal? CoerceMinValue(decimal? baseValue) => 
            this.EditStrategy.CoerceMinValue(baseValue);

        protected static object CoerceMinValue(DependencyObject d, object baseValue) => 
            ((SpinEdit) d).CoerceMinValue((decimal?) baseValue);

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new SpinEditPropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new SpinEditStrategy(this);

        protected internal override TextInputSettingsBase CreateTextInputSettings() => 
            new TextInputMaskSettings(this);

        protected internal override MaskType[] GetSupportedMaskTypes() => 
            new MaskType[] { MaskType.Numeric };

        public void Maximize()
        {
            this.EditStrategy.Maximize();
        }

        public void Minimize()
        {
            this.EditStrategy.Minimize();
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new SpinEditAutomationPeer(this);

        protected virtual void OnDecimalValueChanged(decimal oldValue, decimal value)
        {
            this.EditStrategy.SyncWithDecimalValue(oldValue, value);
        }

        private static void OnDecimalValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((SpinEdit) obj).OnDecimalValueChanged((decimal) e.OldValue, (decimal) e.NewValue);
        }

        protected virtual void OnIsFloatValueChanged()
        {
            this.EditStrategy.OnIsFloatValueChanged();
        }

        private static void OnIsFloatValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((SpinEdit) obj).OnIsFloatValueChanged();
        }

        protected virtual void OnMaxValueChanged(decimal? value)
        {
            this.EditStrategy.MaxValueChanged(value);
        }

        private static void OnMaxValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((SpinEdit) obj).OnMaxValueChanged((decimal?) e.NewValue);
        }

        protected virtual void OnMinValueChanged(decimal? value)
        {
            this.EditStrategy.MinValueChanged(value);
        }

        private static void OnMinValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((SpinEdit) obj).OnMinValueChanged((decimal?) e.NewValue);
        }

        private void SetCommands()
        {
            Func<object, bool> canExecuteMethod = <>c.<>9__15_1;
            if (<>c.<>9__15_1 == null)
            {
                Func<object, bool> local1 = <>c.<>9__15_1;
                canExecuteMethod = <>c.<>9__15_1 = parameter => true;
            }
            this.MinimizeCommand = DelegateCommandFactory.Create<object>(parameter => this.Minimize(), canExecuteMethod, false);
            Func<object, bool> func2 = <>c.<>9__15_3;
            if (<>c.<>9__15_3 == null)
            {
                Func<object, bool> local2 = <>c.<>9__15_3;
                func2 = <>c.<>9__15_3 = parameter => true;
            }
            this.MaximizeCommand = DelegateCommandFactory.Create<object>(parameter => this.Maximize(), func2, false);
        }

        [Description("Sets the editor's value to the minimum allowed value.")]
        public ICommand MinimizeCommand { get; private set; }

        [Description("Sets the editor's value to the maximum allowed value.")]
        public ICommand MaximizeCommand { get; private set; }

        [Description("Gets or sets a spin editor's value. This is a dependency property."), Category("Common Properties")]
        public decimal Value
        {
            get => 
                (decimal) base.GetValue(ValueProperty);
            set => 
                base.SetValue(ValueProperty, value);
        }

        public bool AllowRoundOutOfRangeValue
        {
            get => 
                (bool) base.GetValue(AllowRoundOutOfRangeValueProperty);
            set => 
                base.SetValue(AllowRoundOutOfRangeValueProperty, value);
        }

        [Description("Gets or sets the editor's minimum value. This is a dependency property."), Category("Behavior")]
        public decimal? MinValue
        {
            get => 
                (decimal?) base.GetValue(MinValueProperty);
            set => 
                base.SetValue(MinValueProperty, value);
        }

        [Description("Gets or sets the editor's maximum value. This is a dependency property."), Category("Behavior")]
        public decimal? MaxValue
        {
            get => 
                (decimal?) base.GetValue(MaxValueProperty);
            set => 
                base.SetValue(MaxValueProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets a value by which the editor's value changes each time the editor is spun. This is a dependency property.")]
        public decimal Increment
        {
            get => 
                (decimal) base.GetValue(IncrementProperty);
            set => 
                base.SetValue(IncrementProperty, value);
        }

        [Description("Gets or sets whether the editor's value is a float. This is a dependency property."), Category("Behavior")]
        public bool IsFloatValue
        {
            get => 
                (bool) base.GetValue(IsFloatValueProperty);
            set => 
                base.SetValue(IsFloatValueProperty, value);
        }

        protected internal override MaskType DefaultMaskType =>
            MaskType.Numeric;

        protected SpinEditStrategy EditStrategy =>
            base.EditStrategy as SpinEditStrategy;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SpinEdit.<>c <>9 = new SpinEdit.<>c();
            public static Func<object, bool> <>9__15_1;
            public static Func<object, bool> <>9__15_3;

            internal void <.cctor>b__6_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SpinEdit) d).AllowRoundOutOfRangeValueChanged((bool) e.NewValue);
            }

            internal bool <SetCommands>b__15_1(object parameter) => 
                true;

            internal bool <SetCommands>b__15_3(object parameter) => 
                true;
        }
    }
}

