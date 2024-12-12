namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class RangeBaseEditSettings : BaseEditSettings
    {
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty MinimumProperty;
        public static readonly DependencyProperty MaximumProperty;
        public static readonly DependencyProperty SmallStepProperty;
        public static readonly DependencyProperty LargeStepProperty;

        static RangeBaseEditSettings()
        {
            Type ownerType = typeof(RangeBaseEditSettings);
            SmallStepProperty = DependencyPropertyManager.Register("SmallStep", typeof(double), ownerType, new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            LargeStepProperty = DependencyPropertyManager.Register("LargeStep", typeof(double), ownerType, new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            OrientationProperty = DependencyPropertyManager.Register("Orientation", typeof(System.Windows.Controls.Orientation), ownerType, new FrameworkPropertyMetadata(System.Windows.Controls.Orientation.Horizontal, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            MinimumProperty = DependencyPropertyManager.Register("Minimum", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            MaximumProperty = DependencyPropertyManager.Register("Maximum", typeof(double), ownerType, new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            RangeBaseEdit editor = edit as RangeBaseEdit;
            if (editor != null)
            {
                base.SetValueFromSettings(SmallStepProperty, () => editor.SmallStep = this.SmallStep);
                base.SetValueFromSettings(LargeStepProperty, () => editor.LargeStep = this.LargeStep);
                base.SetValueFromSettings(OrientationProperty, () => editor.Orientation = this.Orientation);
                base.SetValueFromSettings(MinimumProperty, () => editor.Minimum = this.Minimum);
                base.SetValueFromSettings(MaximumProperty, () => editor.Maximum = this.Maximum);
            }
        }

        [Category("Behavior")]
        public double SmallStep
        {
            get => 
                (double) base.GetValue(SmallStepProperty);
            set => 
                base.SetValue(SmallStepProperty, value);
        }

        [Category("Behavior")]
        public double LargeStep
        {
            get => 
                (double) base.GetValue(LargeStepProperty);
            set => 
                base.SetValue(LargeStepProperty, value);
        }

        [Category("Behavior")]
        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        [Category("Behavior")]
        public double Minimum
        {
            get => 
                (double) base.GetValue(MinimumProperty);
            set => 
                base.SetValue(MinimumProperty, value);
        }

        [Category("Behavior")]
        public double Maximum
        {
            get => 
                (double) base.GetValue(MaximumProperty);
            set => 
                base.SetValue(MaximumProperty, value);
        }
    }
}

