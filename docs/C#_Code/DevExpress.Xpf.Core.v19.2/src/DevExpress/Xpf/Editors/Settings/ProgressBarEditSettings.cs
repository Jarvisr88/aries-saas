namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class ProgressBarEditSettings : RangeBaseEditSettings
    {
        public static readonly DependencyProperty IsPercentProperty;
        public static readonly DependencyProperty ContentDisplayModeProperty;
        public static readonly DependencyProperty ContentTemplateProperty;

        static ProgressBarEditSettings()
        {
            Type ownerType = typeof(ProgressBarEditSettings);
            IsPercentProperty = DependencyPropertyManager.Register("IsPercent", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ContentDisplayModeProperty = DependencyPropertyManager.Register("ContentDisplayMode", typeof(DevExpress.Xpf.Editors.ContentDisplayMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.ContentDisplayMode.None, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ContentTemplateProperty = DependencyPropertyManager.Register("ContentTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            ProgressBarEdit editor = edit as ProgressBarEdit;
            if (editor != null)
            {
                base.SetValueFromSettings(IsPercentProperty, () => editor.IsPercent = this.IsPercent);
                base.SetValueFromSettings(ContentDisplayModeProperty, () => editor.ContentDisplayMode = this.ContentDisplayMode);
                base.SetValueFromSettings(ContentTemplateProperty, () => editor.ContentTemplate = this.ContentTemplate);
            }
        }

        [Category("Behavior")]
        public bool IsPercent
        {
            get => 
                (bool) base.GetValue(IsPercentProperty);
            set => 
                base.SetValue(IsPercentProperty, value);
        }

        [Category("Behavior")]
        public DevExpress.Xpf.Editors.ContentDisplayMode ContentDisplayMode
        {
            get => 
                (DevExpress.Xpf.Editors.ContentDisplayMode) base.GetValue(ContentDisplayModeProperty);
            set => 
                base.SetValue(ContentDisplayModeProperty, value);
        }

        [Category("Appearance ")]
        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }
    }
}

