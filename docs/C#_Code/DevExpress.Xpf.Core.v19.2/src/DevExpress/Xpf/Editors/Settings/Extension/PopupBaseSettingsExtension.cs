namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public class PopupBaseSettingsExtension : ButtonSettingsExtension
    {
        public PopupBaseSettingsExtension()
        {
            this.ShowPopupIfReadOnly = (bool) PopupBaseEditSettings.ShowPopupIfReadOnlyProperty.DefaultMetadata.DefaultValue;
            this.PopupWidth = (double) PopupBaseEditSettings.PopupWidthProperty.DefaultMetadata.DefaultValue;
            this.PopupHeight = (double) PopupBaseEditSettings.PopupHeightProperty.DefaultMetadata.DefaultValue;
            this.PopupMinWidth = (double) PopupBaseEditSettings.PopupMinWidthProperty.DefaultMetadata.DefaultValue;
            this.PopupMinHeight = (double) PopupBaseEditSettings.PopupMinHeightProperty.DefaultMetadata.DefaultValue;
            this.PopupMaxWidth = (double) PopupBaseEditSettings.PopupMaxWidthProperty.DefaultMetadata.DefaultValue;
            this.PopupMaxHeight = (double) PopupBaseEditSettings.PopupMaxHeightProperty.DefaultMetadata.DefaultValue;
            this.IsSharedPopupSize = true;
            this.PopupFooterButtons = (DevExpress.Xpf.Editors.PopupFooterButtons?) PopupBaseEditSettings.PopupFooterButtonsProperty.DefaultMetadata.DefaultValue;
            this.ShowSizeGrip = (bool?) PopupBaseEditSettings.ShowSizeGripProperty.DefaultMetadata.DefaultValue;
            this.PopupTopAreaTemplate = (ControlTemplate) PopupBaseEditSettings.PopupTopAreaTemplateProperty.DefaultMetadata.DefaultValue;
            this.PopupBottomAreaTemplate = (ControlTemplate) PopupBaseEditSettings.PopupBottomAreaTemplateProperty.DefaultMetadata.DefaultValue;
        }

        protected virtual void Assign(PopupBaseEditSettings settings)
        {
            settings.ShowPopupIfReadOnly = this.ShowPopupIfReadOnly;
            settings.PopupWidth = this.PopupWidth;
            settings.PopupHeight = this.PopupHeight;
            settings.PopupMinWidth = this.PopupMinWidth;
            settings.PopupMinHeight = this.PopupMinHeight;
            settings.PopupMaxWidth = this.PopupMaxWidth;
            settings.PopupMaxHeight = this.PopupMaxHeight;
            settings.PopupFooterButtons = this.PopupFooterButtons;
            settings.ShowSizeGrip = this.ShowSizeGrip;
            settings.IsSharedPopupSize = this.IsSharedPopupSize;
            settings.PopupTopAreaTemplate = this.PopupTopAreaTemplate;
            settings.PopupBottomAreaTemplate = this.PopupBottomAreaTemplate;
        }

        protected sealed override ButtonEditSettings CreateButtonEditSettings()
        {
            PopupBaseEditSettings settings = this.CreatePopupBaseEditSettings();
            this.Assign(settings);
            return settings;
        }

        protected virtual PopupBaseEditSettings CreatePopupBaseEditSettings() => 
            new PopupBaseEditSettings();

        public bool ShowPopupIfReadOnly { get; set; }

        public double PopupWidth { get; set; }

        public double PopupHeight { get; set; }

        public double PopupMinWidth { get; set; }

        public double PopupMinHeight { get; set; }

        public double PopupMaxWidth { get; set; }

        public double PopupMaxHeight { get; set; }

        public bool IsSharedPopupSize { get; set; }

        public DevExpress.Xpf.Editors.PopupFooterButtons? PopupFooterButtons { get; set; }

        public bool? ShowSizeGrip { get; set; }

        public ControlTemplate PopupTopAreaTemplate { get; set; }

        public ControlTemplate PopupBottomAreaTemplate { get; set; }
    }
}

