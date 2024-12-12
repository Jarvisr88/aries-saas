namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;

    public class ToggleSwitchEdit : BaseEdit, ICommandSource, ITextExportSettings, IExportSettings
    {
        public static readonly DependencyProperty IsCheckedProperty;
        public static readonly DependencyProperty IsThreeStateProperty;
        public static readonly DependencyProperty CheckedStateContentProperty;
        public static readonly DependencyProperty UncheckedStateContentProperty;
        public static readonly DependencyProperty EnableAnimationProperty;
        public static readonly DependencyProperty ContentPlacementProperty;
        public static readonly DependencyProperty ToggleSwitchWidthProperty;
        public static readonly DependencyProperty ToggleSwitchHeightProperty;
        public static readonly DependencyProperty CheckedStateContentTemplateProperty;
        public static readonly DependencyProperty UncheckedStateContentTemplateProperty;
        public static readonly DependencyProperty ClickModeProperty;
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty CommandParameterProperty;
        public static readonly DependencyProperty CommandTargetProperty;
        public static readonly DependencyProperty AnimationModeProperty;
        public static readonly DependencyProperty SwitchThumbTemplateProperty;
        public static readonly DependencyProperty SwitchBorderTemplateProperty;
        public static readonly RoutedEvent CheckedEvent;
        public static readonly RoutedEvent UncheckedEvent;
        public static readonly RoutedEvent IndeterminateEvent;

        public event RoutedEventHandler Checked
        {
            add
            {
                base.AddHandler(CheckedEvent, value);
            }
            remove
            {
                base.RemoveHandler(CheckedEvent, value);
            }
        }

        public event RoutedEventHandler Indeterminate
        {
            add
            {
                base.AddHandler(IndeterminateEvent, value);
            }
            remove
            {
                base.RemoveHandler(IndeterminateEvent, value);
            }
        }

        public event RoutedEventHandler Unchecked
        {
            add
            {
                base.AddHandler(UncheckedEvent, value);
            }
            remove
            {
                base.RemoveHandler(UncheckedEvent, value);
            }
        }

        static ToggleSwitchEdit()
        {
            Type forType = typeof(ToggleSwitchEdit);
            BaseEdit.EditValueProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, null, true, UpdateSourceTrigger.PropertyChanged));
            CheckedStateContentProperty = DependencyProperty.Register("CheckedStateContent", typeof(object), forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            UncheckedStateContentProperty = DependencyProperty.Register("UncheckedStateContent", typeof(object), forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            EnableAnimationProperty = DependencyProperty.Register("EnableAnimation", typeof(bool), forType, new FrameworkPropertyMetadata(true));
            ContentPlacementProperty = DependencyProperty.Register("ContentPlacement", typeof(ToggleSwitchContentPlacement), forType, new FrameworkPropertyMetadata(ToggleSwitchContentPlacement.Near, FrameworkPropertyMetadataOptions.AffectsMeasure));
            ToggleSwitchWidthProperty = DependencyProperty.Register("ToggleSwitchWidth", typeof(double), forType, new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
            ToggleSwitchHeightProperty = DependencyProperty.Register("ToggleSwitchHeight", typeof(double), forType, new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
            CheckedStateContentTemplateProperty = DependencyProperty.Register("CheckedStateContentTemplate", typeof(DataTemplate), forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            UncheckedStateContentTemplateProperty = DependencyProperty.Register("UncheckedStateContentTemplate", typeof(DataTemplate), forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            CommandProperty = DependencyPropertyManager.Register("Command", typeof(ICommand), forType);
            CommandParameterProperty = DependencyPropertyManager.Register("CommandParameter", typeof(object), forType);
            CommandTargetProperty = DependencyPropertyManager.Register("CommandTarget", typeof(IInputElement), forType, new FrameworkPropertyMetadata(null));
            IsCheckedProperty = ToggleButton.IsCheckedProperty.AddOwner(forType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) => ((ToggleSwitchEdit) d).OnIsCheckedChanged((bool?) e.OldValue, (bool?) e.NewValue), (d, e) => ((ToggleSwitchEdit) d).CoerceIsChecked(e), true, UpdateSourceTrigger.PropertyChanged));
            IsThreeStateProperty = ToggleButton.IsThreeStateProperty.AddOwner(forType);
            ClickModeProperty = ButtonBase.ClickModeProperty.AddOwner(forType);
            CheckedEvent = EventManager.RegisterRoutedEvent("Checked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), forType);
            UncheckedEvent = EventManager.RegisterRoutedEvent("Unchecked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), forType);
            IndeterminateEvent = EventManager.RegisterRoutedEvent("Indeterminate", RoutingStrategy.Bubble, typeof(RoutedEventHandler), forType);
            AnimationModeProperty = DependencyProperty.Register("AnimationMode", typeof(ToggleSwitchAnimationMode), forType, new FrameworkPropertyMetadata(ToggleSwitchAnimationMode.Always));
            SwitchThumbTemplateProperty = DependencyProperty.Register("SwitchThumbTemplate", typeof(DataTemplate), forType);
            SwitchBorderTemplateProperty = DependencyProperty.Register("SwitchBorderTemplate", typeof(DataTemplate), forType);
        }

        public ToggleSwitchEdit()
        {
            this.SetDefaultStyleKey(typeof(ToggleSwitchEdit));
        }

        protected virtual object CoerceIsChecked(object isChecked) => 
            this.EditStrategy.CoerceValue(IsCheckedProperty, isChecked);

        protected override EditStrategyBase CreateEditStrategy() => 
            new ToggleSwitchEditStrategy(this);

        protected virtual string GetCheckText() => 
            this.EditStrategy.GetCheckText(base.EditValue);

        protected virtual TextDecorationCollection GetPrintTextDecorations() => 
            ExportSettingDefaultValue.TextDecorations;

        protected virtual bool? GetXlsExportNativeFormatInternal() => 
            ExportSettingDefaultValue.XlsExportNativeFormat;

        protected internal void OnChecked(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }

        protected internal void OnIndeterminate(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }

        protected virtual void OnIsCheckedChanged(bool? oldValue, bool? newValue)
        {
            this.EditStrategy.OnIsCheckedChanged(oldValue, newValue);
        }

        protected virtual void OnIsThreeStateChanged()
        {
            this.EditStrategy.OnIsThreeStateChanged();
        }

        protected internal void OnUnchecked(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }

        protected override void SubscribeEditEventsCore()
        {
            base.SubscribeEditEventsCore();
            ToggleSwitch editCore = base.EditCore as ToggleSwitch;
            if (editCore != null)
            {
                editCore.Checked += new RoutedEventHandler(this.ToggleSwitchCheckedChanged);
                editCore.Unchecked += new RoutedEventHandler(this.ToggleSwitchCheckedChanged);
                editCore.Indeterminate += new RoutedEventHandler(this.ToggleSwitchCheckedChanged);
            }
        }

        private void ToggleSwitchCheckedChanged(object sender, RoutedEventArgs e)
        {
            this.EditStrategy.ToggleSwitchToggled();
        }

        protected override void UnsubscribeEditEventsCore()
        {
            base.UnsubscribeEditEventsCore();
            ToggleSwitch editCore = base.EditCore as ToggleSwitch;
            if (editCore != null)
            {
                editCore.Checked -= new RoutedEventHandler(this.ToggleSwitchCheckedChanged);
                editCore.Unchecked -= new RoutedEventHandler(this.ToggleSwitchCheckedChanged);
                editCore.Indeterminate -= new RoutedEventHandler(this.ToggleSwitchCheckedChanged);
            }
        }

        private ToggleSwitchEditStrategy EditStrategy =>
            (ToggleSwitchEditStrategy) base.EditStrategy;

        protected internal EditBoxWrapper EditBox { get; private set; }

        public DataTemplate SwitchThumbTemplate
        {
            get => 
                (DataTemplate) base.GetValue(SwitchThumbTemplateProperty);
            set => 
                base.SetValue(SwitchThumbTemplateProperty, value);
        }

        public DataTemplate SwitchBorderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(SwitchBorderTemplateProperty);
            set => 
                base.SetValue(SwitchBorderTemplateProperty, value);
        }

        public ToggleSwitchAnimationMode AnimationMode
        {
            get => 
                (ToggleSwitchAnimationMode) base.GetValue(AnimationModeProperty);
            set => 
                base.SetValue(AnimationModeProperty, value);
        }

        public ICommand Command
        {
            get => 
                (ICommand) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => 
                base.GetValue(CommandParameterProperty);
            set => 
                base.SetValue(CommandParameterProperty, value);
        }

        public IInputElement CommandTarget
        {
            get => 
                (IInputElement) base.GetValue(CheckEdit.CommandTargetProperty);
            set => 
                base.SetValue(CheckEdit.CommandTargetProperty, value);
        }

        public System.Windows.Controls.ClickMode ClickMode
        {
            get => 
                (System.Windows.Controls.ClickMode) base.GetValue(ClickModeProperty);
            set => 
                base.SetValue(ClickModeProperty, value);
        }

        public bool? IsChecked
        {
            get => 
                (bool?) base.GetValue(IsCheckedProperty);
            set => 
                base.SetValue(IsCheckedProperty, value);
        }

        public bool IsThreeState
        {
            get => 
                (bool) base.GetValue(IsThreeStateProperty);
            set => 
                base.SetValue(IsThreeStateProperty, value);
        }

        public DataTemplate CheckedStateContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CheckedStateContentTemplateProperty);
            set => 
                base.SetValue(CheckedStateContentTemplateProperty, value);
        }

        public DataTemplate UncheckedStateContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(UncheckedStateContentTemplateProperty);
            set => 
                base.SetValue(UncheckedStateContentTemplateProperty, value);
        }

        public double ToggleSwitchWidth
        {
            get => 
                (double) base.GetValue(ToggleSwitchWidthProperty);
            set => 
                base.SetValue(ToggleSwitchWidthProperty, value);
        }

        public double ToggleSwitchHeight
        {
            get => 
                (double) base.GetValue(ToggleSwitchHeightProperty);
            set => 
                base.SetValue(ToggleSwitchHeightProperty, value);
        }

        public ToggleSwitchContentPlacement ContentPlacement
        {
            get => 
                (ToggleSwitchContentPlacement) base.GetValue(ContentPlacementProperty);
            set => 
                base.SetValue(ContentPlacementProperty, value);
        }

        public object CheckedStateContent
        {
            get => 
                base.GetValue(CheckedStateContentProperty);
            set => 
                base.SetValue(CheckedStateContentProperty, value);
        }

        public object UncheckedStateContent
        {
            get => 
                base.GetValue(UncheckedStateContentProperty);
            set => 
                base.SetValue(UncheckedStateContentProperty, value);
        }

        public bool EnableAnimation
        {
            get => 
                (bool) base.GetValue(EnableAnimationProperty);
            set => 
                base.SetValue(EnableAnimationProperty, value);
        }

        HorizontalAlignment ITextExportSettings.HorizontalAlignment =>
            HorizontalAlignment.Left;

        VerticalAlignment ITextExportSettings.VerticalAlignment =>
            (base.VerticalContentAlignment != VerticalAlignment.Stretch) ? base.VerticalContentAlignment : ExportSettingDefaultValue.VerticalAlignment;

        string ITextExportSettings.Text =>
            this.GetCheckText();

        object ITextExportSettings.TextValue =>
            this.GetCheckText();

        string ITextExportSettings.TextValueFormatString =>
            base.DisplayFormatString;

        TextWrapping ITextExportSettings.TextWrapping =>
            TextWrapping.NoWrap;

        bool ITextExportSettings.NoTextExport =>
            ExportSettingDefaultValue.NoTextExport;

        bool? ITextExportSettings.XlsExportNativeFormat =>
            this.GetXlsExportNativeFormatInternal();

        TextTrimming ITextExportSettings.TextTrimming =>
            TextTrimming.CharacterEllipsis;

        string ITextExportSettings.XlsxFormatString =>
            ExportSettingDefaultValue.XlsxFormatString;

        TextDecorationCollection ITextExportSettings.TextDecorations =>
            this.GetPrintTextDecorations();

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

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ToggleSwitchEdit.<>c <>9 = new ToggleSwitchEdit.<>c();

            internal void <.cctor>b__20_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ToggleSwitchEdit) d).OnIsCheckedChanged((bool?) e.OldValue, (bool?) e.NewValue);
            }

            internal object <.cctor>b__20_1(DependencyObject d, object e) => 
                ((ToggleSwitchEdit) d).CoerceIsChecked(e);
        }
    }
}

