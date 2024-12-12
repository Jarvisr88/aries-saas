namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class CheckSettingsExtension : BaseSettingsExtension
    {
        public CheckSettingsExtension()
        {
            this.ClickMode = (System.Windows.Controls.ClickMode) CheckEditSettings.ClickModeProperty.GetMetadata(typeof(CheckEditSettings)).DefaultValue;
            this.IsThreeState = (bool) CheckEditSettings.IsThreeStateProperty.GetMetadata(typeof(CheckEditSettings)).DefaultValue;
            this.Content = CheckEditSettings.ContentProperty.GetMetadata(typeof(CheckEditSettings)).DefaultValue;
            this.ContentTemplate = (DataTemplate) CheckEditSettings.ContentTemplateProperty.GetMetadata(typeof(CheckEditSettings)).DefaultValue;
            this.ContentTemplateSelector = (DataTemplateSelector) CheckEditSettings.ContentTemplateSelectorProperty.GetMetadata(typeof(CheckEditSettings)).DefaultValue;
        }

        protected override BaseEditSettings CreateEditSettings()
        {
            CheckEditSettings settings1 = new CheckEditSettings();
            settings1.ClickMode = this.ClickMode;
            settings1.IsThreeState = this.IsThreeState;
            settings1.Content = this.Content;
            settings1.ContentTemplate = this.ContentTemplate;
            settings1.ContentTemplateSelector = this.ContentTemplateSelector;
            return settings1;
        }

        public System.Windows.Controls.ClickMode ClickMode { get; set; }

        public bool IsThreeState { get; set; }

        public object Content { get; set; }

        public DataTemplate ContentTemplate { get; set; }

        public DataTemplateSelector ContentTemplateSelector { get; set; }
    }
}

