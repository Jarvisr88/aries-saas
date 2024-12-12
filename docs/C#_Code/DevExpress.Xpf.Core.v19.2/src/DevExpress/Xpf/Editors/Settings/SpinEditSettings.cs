namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class SpinEditSettings : ButtonEditSettings
    {
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;
        public static readonly DependencyProperty IncrementProperty;
        public static readonly DependencyProperty IsFloatValueProperty;
        public static readonly DependencyProperty AllowRoundOutOfRangeValueProperty;

        static SpinEditSettings()
        {
            Type ownerType = typeof(SpinEditSettings);
            AllowRoundOutOfRangeValueProperty = DependencyPropertyManager.Register("AllowRoundOutOfRangeValue", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None));
            MinValueProperty = DependencyPropertyManager.Register("MinValue", typeof(decimal?), ownerType, new FrameworkPropertyMetadata(null));
            MaxValueProperty = DependencyPropertyManager.Register("MaxValue", typeof(decimal?), ownerType, new FrameworkPropertyMetadata(null));
            IncrementProperty = DependencyPropertyManager.Register("Increment", typeof(decimal), ownerType, new FrameworkPropertyMetadata(1M));
            IsFloatValueProperty = DependencyPropertyManager.Register("IsFloatValue", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            TextEditSettings.MaskTypeProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(MaskType.Numeric));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            SpinEdit se = edit as SpinEdit;
            if (se != null)
            {
                base.SetValueFromSettings(AllowRoundOutOfRangeValueProperty, () => se.AllowRoundOutOfRangeValue = this.AllowRoundOutOfRangeValue);
                base.SetValueFromSettings(MinValueProperty, () => se.MinValue = this.MinValue);
                base.SetValueFromSettings(MaxValueProperty, () => se.MaxValue = this.MaxValue);
                base.SetValueFromSettings(IncrementProperty, () => se.Increment = this.Increment);
                base.SetValueFromSettings(IsFloatValueProperty, () => se.IsFloatValue = this.IsFloatValue);
            }
        }

        protected internal override ButtonInfoBase CreateDefaultButtonInfo()
        {
            SpinButtonInfo info1 = new SpinButtonInfo();
            info1.IsDefaultButton = true;
            return info1;
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

        [Category("Behavior"), Description("Gets or sets the editor's maximum value. This is a dependency property.")]
        public decimal? MaxValue
        {
            get => 
                (decimal?) base.GetValue(MaxValueProperty);
            set => 
                base.SetValue(MaxValueProperty, value);
        }

        [Description("Gets or sets a value by which the editor's value changes each time the editor is spun.This is a dependency property."), Category("Behavior")]
        public decimal Increment
        {
            get => 
                (decimal) base.GetValue(IncrementProperty);
            set => 
                base.SetValue(IncrementProperty, value);
        }

        [Description("Gets or sets whether the editor's value is a float.This is a dependency property."), Category("Behavior")]
        public bool IsFloatValue
        {
            get => 
                (bool) base.GetValue(IsFloatValueProperty);
            set => 
                base.SetValue(IsFloatValueProperty, value);
        }
    }
}

