namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ListBoxEditSettingsExtension : BaseSettingsExtension
    {
        public ListBoxEditSettingsExtension()
        {
            this.DisplayMember = this.ValueMember = string.Empty;
            this.ItemTemplate = null;
            this.ItemTemplateSelector = null;
            this.ItemsPanel = (ItemsPanelTemplate) ListBoxEditSettings.ItemsPanelProperty.DefaultMetadata.DefaultValue;
            this.SelectionMode = (System.Windows.Controls.SelectionMode) ListBoxEditSettings.SelectionModeProperty.DefaultMetadata.DefaultValue;
            this.AllowCollectionView = false;
        }

        protected override BaseEditSettings CreateEditSettings()
        {
            ListBoxEditSettings settings1 = new ListBoxEditSettings();
            settings1.ItemsSource = this.ItemsSource;
            settings1.StyleSettings = this.StyleSettings;
            settings1.SelectionMode = this.SelectionMode;
            settings1.DisplayMember = this.DisplayMember;
            settings1.ValueMember = this.ValueMember;
            settings1.ItemTemplate = this.ItemTemplate;
            settings1.ItemTemplateSelector = this.ItemTemplateSelector;
            settings1.ItemsPanel = this.ItemsPanel;
            settings1.AllowCollectionView = this.AllowCollectionView;
            return settings1;
        }

        public IEnumerable ItemsSource { get; set; }

        public ListBoxEditStyleSettings StyleSettings { get; set; }

        public string DisplayMember { get; set; }

        public string ValueMember { get; set; }

        public DataTemplate ItemTemplate { get; set; }

        public DataTemplateSelector ItemTemplateSelector { get; set; }

        public ItemsPanelTemplate ItemsPanel { get; set; }

        public System.Windows.Controls.SelectionMode SelectionMode { get; set; }

        public bool AllowCollectionView { get; set; }
    }
}

