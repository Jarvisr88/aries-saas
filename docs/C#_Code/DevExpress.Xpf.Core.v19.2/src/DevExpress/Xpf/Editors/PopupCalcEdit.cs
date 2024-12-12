namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free)]
    public class PopupCalcEdit : PopupBaseEdit
    {
        public static readonly DependencyProperty IsPopupAutoWidthProperty;
        public static readonly DependencyProperty PrecisionProperty;
        public static readonly DependencyProperty ValueProperty;
        public static readonly RoutedEvent CustomErrorTextEvent;
        private PopupCalcEditCalculator calculator;
        private decimal prevCalculatorMemory;

        public event CalculatorCustomErrorTextEventHandler CustomErrorText
        {
            add
            {
                base.AddHandler(CustomErrorTextEvent, value);
            }
            remove
            {
                base.RemoveHandler(CustomErrorTextEvent, value);
            }
        }

        static PopupCalcEdit()
        {
            Type ownerType = typeof(PopupCalcEdit);
            IsPopupAutoWidthProperty = DependencyPropertyManager.Register("IsPopupAutoWidth", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, new PropertyChangedCallback(PopupCalcEdit.OnIsPopupAutoWidthChanged)));
            PrecisionProperty = DependencyPropertyManager.Register("Precision", typeof(int), ownerType, new FrameworkPropertyMetadata(6, new PropertyChangedCallback(PopupCalcEdit.OnPrecisionChanged)), new ValidateValueCallback(PopupCalcEdit.PropertyValueValidatePrecision));
            ValueProperty = DependencyPropertyManager.Register("Value", typeof(decimal), ownerType, new FrameworkPropertyMetadata(0M, new PropertyChangedCallback(PopupCalcEdit.OnDecimalValueChanged), new CoerceValueCallback(PopupCalcEdit.CoerceDecimalValueProperty)));
            CustomErrorTextEvent = DevExpress.Xpf.Editors.Calculator.CustomErrorTextEvent.AddOwner(ownerType);
            Control.HorizontalContentAlignmentProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(HorizontalAlignment.Right));
            TextEdit.MaskTypeProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(MaskType.Numeric));
        }

        public PopupCalcEdit()
        {
            this.SetDefaultStyleKey(typeof(PopupCalcEdit));
        }

        protected override void AcceptPopupValue()
        {
            base.AcceptPopupValue();
            this.EditStrategy.AcceptCalculatorValue();
        }

        protected virtual object CoerceDecimalValue(decimal value) => 
            this.EditStrategy.CoerceDecimalValue(value);

        private static object CoerceDecimalValueProperty(DependencyObject d, object value) => 
            ((PopupCalcEdit) d).CoerceDecimalValue((decimal) value);

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new PopupCalcEditPropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new CalcEditStrategy(this);

        protected override bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers) => 
            base.IsClosePopupWithAcceptGesture(key, modifiers) || ((key == Key.Return) && ModifierKeysHelper.IsCtrlPressed(modifiers));

        protected override bool IsClosePopupWithCancelGesture(Key key, ModifierKeys modifiers) => 
            ((key != Key.Escape) || (!ModifierKeysHelper.NoModifiers(modifiers) || (base.EditMode != EditMode.Standalone))) ? base.IsClosePopupWithCancelGesture(key, modifiers) : false;

        protected virtual void OnCalculatorCustomErrorText(object sender, CalculatorCustomErrorTextEventArgs e)
        {
            base.RaiseEvent(e);
        }

        protected virtual void OnDecimalValueChanged(decimal oldValue, decimal newValue)
        {
            this.EditStrategy.SyncWithValue(ValueProperty, oldValue, newValue);
        }

        private static void OnDecimalValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PopupCalcEdit) obj).OnDecimalValueChanged((decimal) e.OldValue, (decimal) e.NewValue);
        }

        protected override void OnEditModeChanged(EditMode oldValue, EditMode newValue)
        {
            base.OnEditModeChanged(oldValue, newValue);
            if (newValue == EditMode.InplaceInactive)
            {
                this.prevCalculatorMemory = 0M;
            }
        }

        protected virtual void OnIsPopupAutoWidthChanged()
        {
        }

        private static void OnIsPopupAutoWidthChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PopupCalcEdit) obj).OnIsPopupAutoWidthChanged();
        }

        protected override void OnPopupClosed()
        {
            base.OnPopupClosed();
            this.EditStrategy.UpdateDisplayText();
            this.Calculator = null;
        }

        protected override void OnPopupOpened()
        {
            base.OnPopupOpened();
        }

        protected virtual void OnPrecisionChanged(int newValue)
        {
        }

        private static void OnPrecisionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PopupCalcEdit) obj).OnPrecisionChanged((int) e.NewValue);
        }

        private static bool PropertyValueValidatePrecision(object value)
        {
            int num = (int) value;
            return ((num >= 0) && (num <= 0x1c));
        }

        protected internal override Type StyleSettingsType =>
            typeof(PopupCalcEditStyleSettings);

        [Description("Gets or sets whether the popup calculator's width is automatically calculated. This is a dependency property.")]
        public bool IsPopupAutoWidth
        {
            get => 
                (bool) base.GetValue(IsPopupAutoWidthProperty);
            set => 
                base.SetValue(IsPopupAutoWidthProperty, value);
        }

        [Description("Gets or sets the maximum number of digits displayed to the right of the decimal point. This is a dependency property.")]
        public int Precision
        {
            get => 
                (int) base.GetValue(PrecisionProperty);
            set => 
                base.SetValue(PrecisionProperty, value);
        }

        [Description("Gets or sets the editor's decimal value. This is a dependency property.")]
        public decimal Value
        {
            get => 
                (decimal) base.GetValue(ValueProperty);
            set => 
                base.SetValue(ValueProperty, value);
        }

        protected internal override MaskType DefaultMaskType =>
            MaskType.Numeric;

        protected CalcEditStrategy EditStrategy
        {
            get => 
                base.EditStrategy as CalcEditStrategy;
            set => 
                base.EditStrategy = value;
        }

        internal PopupCalcEditCalculator Calculator
        {
            get => 
                this.calculator;
            set
            {
                if (!ReferenceEquals(this.calculator, value))
                {
                    PopupCalcEditCalculator oldCalculator = this.calculator;
                    if (this.calculator != null)
                    {
                        this.calculator.CustomErrorText -= new CalculatorCustomErrorTextEventHandler(this.OnCalculatorCustomErrorText);
                        this.prevCalculatorMemory = this.calculator.Memory;
                    }
                    this.calculator = value;
                    if (this.calculator != null)
                    {
                        this.calculator.CustomErrorText += new CalculatorCustomErrorTextEventHandler(this.OnCalculatorCustomErrorText);
                        this.calculator.InitMemory(this.prevCalculatorMemory);
                    }
                    this.EditStrategy.CalculatorAssigned(oldCalculator);
                }
            }
        }

        public override FrameworkElement PopupElement =>
            this.Calculator;

        protected internal override bool ShouldApplyPopupSize =>
            !this.IsPopupAutoWidth;
    }
}

