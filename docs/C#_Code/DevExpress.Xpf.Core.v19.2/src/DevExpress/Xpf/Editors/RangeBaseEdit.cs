namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Automation;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    [DXToolboxBrowsable(false)]
    public abstract class RangeBaseEdit : BaseEdit, ITextExportSettings, IExportSettings
    {
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty MaximumProperty;
        public static readonly DependencyProperty MinimumProperty;
        public static readonly DependencyProperty SmallStepProperty;
        public static readonly DependencyProperty LargeStepProperty;

        static RangeBaseEdit()
        {
            Type ownerType = typeof(RangeBaseEdit);
            SmallStepProperty = DependencyPropertyManager.Register("SmallStep", typeof(double), ownerType, new FrameworkPropertyMetadata(1.0));
            LargeStepProperty = DependencyPropertyManager.Register("LargeStep", typeof(double), ownerType, new FrameworkPropertyMetadata(5.0));
            OrientationProperty = DependencyPropertyManager.Register("Orientation", typeof(System.Windows.Controls.Orientation), ownerType, new FrameworkPropertyMetadata(System.Windows.Controls.Orientation.Horizontal, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(RangeBaseEdit.OrientationPropertyChanged)));
            ValueProperty = DependencyPropertyManager.Register("Value", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(RangeBaseEdit.ValuePropertyChanged), new CoerceValueCallback(RangeBaseEdit.CoerceValueProperty)));
            MaximumProperty = DependencyPropertyManager.Register("Maximum", typeof(double), ownerType, new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(RangeBaseEdit.MaximumPropertyChanged)));
            MinimumProperty = DependencyPropertyManager.Register("Minimum", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(RangeBaseEdit.MinimumPropertyChanged)));
        }

        public RangeBaseEdit()
        {
            this.IncrementLargeCommand = DelegateCommandFactory.Create<object>(parameter => this.IncrementLargeInternal(GetTargetFromObject(parameter)), false);
            this.DecrementLargeCommand = DelegateCommandFactory.Create<object>(parameter => this.DecrementLargeInternal(GetTargetFromObject(parameter)), false);
            this.IncrementSmallCommand = DelegateCommandFactory.Create<object>(parameter => this.IncrementSmallInternal(GetTargetFromObject(parameter)), false);
            this.DecrementSmallCommand = DelegateCommandFactory.Create<object>(parameter => this.DecrementSmallInternal(GetTargetFromObject(parameter)), false);
            this.MinimizeCommand = DelegateCommandFactory.Create<object>(parameter => this.MinimizeInternal(GetTargetFromObject(parameter)), false);
            this.MaximizeCommand = DelegateCommandFactory.Create<object>(parameter => this.MaximizeInternal(GetTargetFromObject(parameter)), false);
        }

        protected virtual object CoerceValue(object value) => 
            this.EditStrategy.CoerceValue(ValueProperty, value);

        private static object CoerceValueProperty(DependencyObject d, object value) => 
            ((RangeBaseEdit) d).CoerceValue(value);

        public void Decrement(double value)
        {
            this.EditStrategy.IncrementDecrement(value, false, true);
        }

        public void DecrementLarge()
        {
            this.EditStrategy.DecrementLarge(null);
        }

        private void DecrementLargeInternal(object parameter)
        {
            if (this.BeginInplaceEditing())
            {
                this.EditStrategy.DecrementLarge(parameter);
            }
        }

        public void DecrementSmall()
        {
            this.EditStrategy.DecrementSmall(null);
        }

        private void DecrementSmallInternal(object parameter)
        {
            if (this.BeginInplaceEditing())
            {
                this.EditStrategy.DecrementSmall(parameter);
            }
        }

        protected virtual string GetExportText() => 
            base.PropertyProvider.DisplayText;

        protected double GetPrintPosition() => 
            this.EditStrategy.GetNormalValue();

        private static TrackBarIncrementTargetEnum GetTargetFromObject(object parameter)
        {
            TrackBarIncrementTargetEnum enum2;
            if (parameter is string)
            {
                Enum.TryParse<TrackBarIncrementTargetEnum>((string) parameter, out enum2);
            }
            else
            {
                enum2 = (parameter == null) ? TrackBarIncrementTargetEnum.Value : ((TrackBarIncrementTargetEnum) parameter);
            }
            return enum2;
        }

        protected virtual object GetTextValueInternal() => 
            this.EditStrategy.IsNullValue(base.EditValue) ? null : base.EditValue;

        public void Increment(double value)
        {
            this.EditStrategy.IncrementDecrement(value, true, true);
        }

        public void IncrementLarge()
        {
            this.EditStrategy.IncrementLarge(null);
        }

        private void IncrementLargeInternal(object parameter)
        {
            if (this.BeginInplaceEditing())
            {
                this.EditStrategy.IncrementLarge(parameter);
            }
        }

        public void IncrementSmall()
        {
            this.EditStrategy.IncrementSmall(null);
        }

        private void IncrementSmallInternal(object parameter)
        {
            if (this.BeginInplaceEditing())
            {
                this.EditStrategy.IncrementSmall(parameter);
            }
        }

        public void Maximize()
        {
            this.EditStrategy.Maximize(null);
        }

        private void MaximizeInternal(object parameter)
        {
            if (this.BeginInplaceEditing())
            {
                this.EditStrategy.Maximize(parameter);
            }
        }

        protected virtual void MaximumChanged(double value)
        {
            this.EditStrategy.MaximumChanged(value);
        }

        private static void MaximumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RangeBaseEdit) d).MaximumChanged((double) e.NewValue);
        }

        public void Minimize()
        {
            this.EditStrategy.Minimize(null);
        }

        private void MinimizeInternal(object parameter)
        {
            if (this.BeginInplaceEditing())
            {
                this.EditStrategy.Minimize(parameter);
            }
        }

        protected virtual void MinimumChanged(double value)
        {
            this.EditStrategy.MinimumChanged(value);
        }

        private static void MinimumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RangeBaseEdit) d).MinimumChanged((double) e.NewValue);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((this.Panel != null) && base.IsPrintingMode)
            {
                this.Panel.ForceLoaded();
            }
        }

        protected virtual void OrientationChanged(System.Windows.Controls.Orientation orientation)
        {
            this.EditStrategy.OrientationChanged(orientation);
        }

        private static void OrientationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RangeBaseEdit) d).OrientationChanged((System.Windows.Controls.Orientation) e.NewValue);
        }

        protected int ToInt(double value) => 
            (int) Math.Round(value);

        protected virtual void ValuePropertyChanged(double oldValue, double value)
        {
            this.EditStrategy.ValuePropertyChanged(oldValue, value);
            RangeBaseEditAutomationPeer peer = (RangeBaseEditAutomationPeer) UIElementAutomationPeer.FromElement(this);
            if (peer != null)
            {
                peer.RaiseValuePropertyChangedEvent(oldValue, value);
            }
        }

        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RangeBaseEdit) d).ValuePropertyChanged((double) e.OldValue, (double) e.NewValue);
        }

        internal RangeBaseEditStrategyBase EditStrategy =>
            (RangeBaseEditStrategyBase) base.EditStrategy;

        internal RangeEditBasePanel Panel =>
            base.EditCore as RangeEditBasePanel;

        [Description("Increments the editor's value by the RangeBaseEdit.LargeStep property's value.")]
        public ICommand IncrementLargeCommand { get; private set; }

        [Description("Decreases the editor's value by the RangeBaseEdit.LargeStep property's value.")]
        public ICommand DecrementLargeCommand { get; private set; }

        [Description("Increments the editor's value by the RangeBaseEdit.SmallStep property's value.")]
        public ICommand IncrementSmallCommand { get; private set; }

        [Description("Decreases the editor's value by the RangeBaseEdit.SmallStep property's value.")]
        public ICommand DecrementSmallCommand { get; private set; }

        [Description("Sets the editor's value to the minimum allowed value, specified by the RangeBaseEdit.Minimum property.")]
        public ICommand MinimizeCommand { get; private set; }

        [Description("Sets the editor's value to the maximum allowed value, specified by the RangeBaseEdit.Maximum property.")]
        public ICommand MaximizeCommand { get; private set; }

        [Description("Gets or sets a value by which the editor's value is changed when using the DecrementSmall or IncrementSmall method.")]
        public double SmallStep
        {
            get => 
                (double) base.GetValue(SmallStepProperty);
            set => 
                base.SetValue(SmallStepProperty, value);
        }

        [Description("Gets or sets a value by which the editor's value is changed when using the DecrementLarge or IncrementLarge method.")]
        public double LargeStep
        {
            get => 
                (double) base.GetValue(LargeStepProperty);
            set => 
                base.SetValue(LargeStepProperty, value);
        }

        [Description("Gets or sets the editor's orientation.")]
        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        [Description("Gets or sets the editor's value.")]
        public double Value
        {
            get => 
                (double) base.GetValue(ValueProperty);
            set => 
                base.SetValue(ValueProperty, value);
        }

        [Description("Gets or sets the minimum allowed value.")]
        public double Minimum
        {
            get => 
                (double) base.GetValue(MinimumProperty);
            set => 
                base.SetValue(MinimumProperty, value);
        }

        [Description("Gets or sets the maximum allowed value.")]
        public double Maximum
        {
            get => 
                (double) base.GetValue(MaximumProperty);
            set => 
                base.SetValue(MaximumProperty, value);
        }

        HorizontalAlignment ITextExportSettings.HorizontalAlignment =>
            (base.HorizontalContentAlignment != HorizontalAlignment.Stretch) ? base.HorizontalContentAlignment : ExportSettingDefaultValue.HorizontalAlignment;

        VerticalAlignment ITextExportSettings.VerticalAlignment =>
            (base.VerticalContentAlignment != VerticalAlignment.Stretch) ? base.VerticalContentAlignment : ExportSettingDefaultValue.VerticalAlignment;

        string ITextExportSettings.Text =>
            this.GetExportText();

        object ITextExportSettings.TextValue =>
            this.GetTextValueInternal();

        string ITextExportSettings.TextValueFormatString =>
            base.DisplayFormatString;

        FontFamily ITextExportSettings.FontFamily =>
            base.FontFamily;

        FontStyle ITextExportSettings.FontStyle =>
            base.FontStyle;

        FontWeight ITextExportSettings.FontWeight =>
            base.FontWeight;

        double ITextExportSettings.FontSize =>
            base.FontSize;

        Thickness ITextExportSettings.Padding =>
            base.Padding;

        TextWrapping ITextExportSettings.TextWrapping =>
            ExportSettingDefaultValue.TextWrapping;

        bool ITextExportSettings.NoTextExport =>
            ExportSettingDefaultValue.NoTextExport;

        bool? ITextExportSettings.XlsExportNativeFormat =>
            ExportSettingDefaultValue.XlsExportNativeFormat;

        string ITextExportSettings.XlsxFormatString =>
            ExportSettingDefaultValue.XlsxFormatString;

        TextDecorationCollection ITextExportSettings.TextDecorations =>
            ExportSettingDefaultValue.TextDecorations;

        TextTrimming ITextExportSettings.TextTrimming =>
            ExportSettingDefaultValue.TextTrimming;
    }
}

