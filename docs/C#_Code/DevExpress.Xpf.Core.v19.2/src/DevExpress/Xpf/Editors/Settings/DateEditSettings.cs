namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class DateEditSettings : PopupBaseEditSettings
    {
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;
        public static readonly DependencyProperty ShowWeekNumbersProperty;

        static DateEditSettings()
        {
            Type forType = typeof(DateEditSettings);
            MinValueProperty = DependencyPropertyManager.Register("MinValue", typeof(DateTime?), typeof(DateEditSettings), new PropertyMetadata(null, new PropertyChangedCallback(DateEditSettings.OnMinValuePropertyChanged)));
            MaxValueProperty = DependencyPropertyManager.Register("MaxValue", typeof(DateTime?), typeof(DateEditSettings), new PropertyMetadata(null, new PropertyChangedCallback(DateEditSettings.OnMaxValuePropertyChanged)));
            ShowWeekNumbersProperty = DependencyPropertyManager.Register("ShowWeekNumbers", typeof(bool), typeof(DateEditSettings), new PropertyMetadata(false));
            BaseEditSettings.DisplayFormatProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata("d"));
            TextEditSettings.MaskTypeProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(MaskType.DateTime));
            TextEditSettings.MaskProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata("d"));
            BaseEditSettings.AllowNullInputProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(true));
            PopupBaseEditSettings.ShowSizeGripProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(false));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            DateEdit de = edit as DateEdit;
            if (de != null)
            {
                base.SetValueFromSettings(ShowWeekNumbersProperty, () => de.ShowWeekNumbers = this.ShowWeekNumbers);
                base.SetValueFromSettings(MinValueProperty, () => de.MinValue = this.MinValue);
                base.SetValueFromSettings(MaxValueProperty, () => de.MaxValue = this.MaxValue);
                base.SetValueFromSettings(BaseEditSettings.NullValueProperty, () => de.NullValue = this.NullValue);
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.SetDefaultDisplayFormat();
        }

        protected virtual void OnMaxValueChanged()
        {
        }

        protected static void OnMaxValuePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DateEditSettings) obj).OnMaxValueChanged();
        }

        protected virtual void OnMinValueChanged()
        {
        }

        protected static void OnMinValuePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DateEditSettings) obj).OnMinValueChanged();
        }

        protected virtual void OnNullValueChanged()
        {
        }

        protected static void OnNullValuePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DateEditSettings) obj).OnNullValueChanged();
        }

        private void SetDefaultDisplayFormat()
        {
            if (this.HasDefaultValue(BaseEditSettings.DisplayFormatProperty))
            {
                base.DisplayFormat = "d";
            }
        }

        private void SetDefaultMask()
        {
            if (this.HasDefaultValue(TextEditSettings.MaskProperty))
            {
                base.Mask = "d";
            }
        }

        [Description("Gets or sets whether to display week numbers in the drop-down window.This is a dependency property."), Category("Behavior")]
        public bool ShowWeekNumbers
        {
            get => 
                (bool) base.GetValue(ShowWeekNumbersProperty);
            set => 
                base.SetValue(ShowWeekNumbersProperty, value);
        }

        [Description("Gets or sets the editor's minimum value. This is a dependency property."), Category("Behavior")]
        public DateTime? MinValue
        {
            get => 
                (DateTime?) base.GetValue(MinValueProperty);
            set => 
                base.SetValue(MinValueProperty, value);
        }

        [Description("Gets or sets the editor's maximum value. This is a dependency property."), Category("Behavior")]
        public DateTime? MaxValue
        {
            get => 
                (DateTime?) base.GetValue(MaxValueProperty);
            set => 
                base.SetValue(MaxValueProperty, value);
        }
    }
}

