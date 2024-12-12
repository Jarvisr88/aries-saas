namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class AutoSuggestEditSettingsExtension : PopupBaseSettingsExtension
    {
        public AutoSuggestEditSettingsExtension()
        {
            this.AcceptValueOnPopupContentSelectionChanged = (bool) AutoSuggestEditSettings.AcceptValueOnPopupContentSelectionChangedProperty.DefaultMetadata.DefaultValue;
            this.ImmediatePopup = (bool) AutoSuggestEditSettings.ImmediatePopupProperty.DefaultMetadata.DefaultValue;
        }

        protected override void Assign(PopupBaseEditSettings settings)
        {
            base.Assign(settings);
            AutoSuggestEditSettings settings2 = (AutoSuggestEditSettings) settings;
            settings2.AcceptValueOnPopupContentSelectionChanged = this.AcceptValueOnPopupContentSelectionChanged;
            settings2.ImmediatePopup = this.ImmediatePopup;
            settings2.ItemsSource = this.ItemsSource;
            settings2.ItemTemplate = this.ItemTemplate;
            settings2.ItemsPanel = this.ItemsPanel;
            settings2.ItemContainerStyle = this.ItemContainerStyle;
            settings2.DisplayMember = this.DisplayMember;
            settings2.TextMember = this.TextMember;
            settings2.ItemContainerStyleSelector = this.ItemContainerStyleSelector;
            settings2.ItemStringFormat = this.ItemStringFormat;
        }

        protected override PopupBaseEditSettings CreatePopupBaseEditSettings() => 
            new AutoSuggestEditSettings();

        public bool AcceptValueOnPopupContentSelectionChanged { get; set; }

        public bool ImmediatePopup { get; set; }

        public object ItemsSource { get; set; }

        public DataTemplate ItemTemplate { get; set; }

        public ItemsPanelTemplate ItemsPanel { get; set; }

        public DataTemplateSelector ItemTemplateSelector { get; set; }

        public Style ItemContainerStyle { get; set; }

        public string DisplayMember { get; set; }

        public string TextMember { get; set; }

        public StyleSelector ItemContainerStyleSelector { get; set; }

        public string ItemStringFormat { get; set; }
    }
}

