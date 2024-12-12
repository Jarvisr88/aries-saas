namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ComboBoxSettingsExtension : PopupBaseSettingsExtension
    {
        public ComboBoxSettingsExtension()
        {
            base.HorizontalContentAlignment = EditSettingsHorizontalAlignment.Left;
            this.DisplayMember = this.ValueMember = string.Empty;
            this.ItemTemplate = null;
            this.ItemTemplateSelector = null;
            this.ItemsPanel = LookUpEditSettingsBase.ItemsPanelProperty.DefaultMetadata.DefaultValue as ItemsPanelTemplate;
            base.PopupMaxHeight = SystemParameters.PrimaryScreenHeight / 3.0;
            base.PopupMinHeight = 35.0;
            this.ApplyItemTemplateToSelectedItem = false;
            this.AutoComplete = (bool) LookUpEditBase.AutoCompleteProperty.DefaultMetadata.DefaultValue;
            this.IsCaseSensitiveSearch = (bool) LookUpEditBase.IsCaseSensitiveSearchProperty.DefaultMetadata.DefaultValue;
            this.ImmediatePopup = (bool) LookUpEditBase.ImmediatePopupProperty.DefaultMetadata.DefaultValue;
            this.AllowItemHighlighting = (bool) LookUpEditBase.AllowItemHighlightingProperty.DefaultMetadata.DefaultValue;
            this.SeparatorString = (string) LookUpEditBase.SeparatorStringProperty.DefaultMetadata.DefaultValue;
            this.AllowCollectionView = false;
            this.FilterCondition = null;
        }

        protected override void Assign(PopupBaseEditSettings settings)
        {
            base.Assign(settings);
            LookUpEditSettingsBase base2 = settings as LookUpEditSettingsBase;
            if (base2 != null)
            {
                base2.AddNewButtonPlacement = this.AddNewButtonPlacement;
                base2.FindButtonPlacement = this.FindButtonPlacement;
            }
        }

        protected override PopupBaseEditSettings CreatePopupBaseEditSettings()
        {
            ComboBoxEditSettings settings1 = new ComboBoxEditSettings();
            settings1.ItemsSource = this.ItemsSource;
            settings1.DisplayMember = this.DisplayMember;
            settings1.ValueMember = this.ValueMember;
            settings1.IsTextEditable = base.IsTextEditable;
            settings1.ItemTemplate = this.ItemTemplate;
            settings1.ItemTemplateSelector = this.ItemTemplateSelector;
            settings1.ItemsPanel = this.ItemsPanel;
            settings1.ApplyItemTemplateToSelectedItem = this.ApplyItemTemplateToSelectedItem;
            settings1.StyleSettings = this.StyleSettings;
            settings1.AutoComplete = this.AutoComplete;
            settings1.IsCaseSensitiveSearch = this.IsCaseSensitiveSearch;
            settings1.ImmediatePopup = this.ImmediatePopup;
            settings1.AllowItemHighlighting = this.AllowItemHighlighting;
            settings1.IncrementalFiltering = this.IncrementalFiltering;
            settings1.SeparatorString = this.SeparatorString;
            settings1.FindMode = this.FindMode;
            settings1.FilterCondition = this.FilterCondition;
            settings1.AllowCollectionView = this.AllowCollectionView;
            return settings1;
        }

        public string SeparatorString { get; set; }

        public IEnumerable ItemsSource { get; set; }

        public string DisplayMember { get; set; }

        public string ValueMember { get; set; }

        public DataTemplate ItemTemplate { get; set; }

        public DataTemplateSelector ItemTemplateSelector { get; set; }

        public ItemsPanelTemplate ItemsPanel { get; set; }

        public bool ApplyItemTemplateToSelectedItem { get; set; }

        public bool AutoComplete { get; set; }

        public bool IsCaseSensitiveSearch { get; set; }

        public bool ImmediatePopup { get; set; }

        public bool AllowItemHighlighting { get; set; }

        public bool? IncrementalFiltering { get; set; }

        public bool AllowCollectionView { get; set; }

        public EditorPlacement? AddNewButtonPlacement { get; set; }

        public EditorPlacement? FindButtonPlacement { get; set; }

        public DevExpress.Xpf.Editors.FindMode? FindMode { get; set; }

        public DevExpress.Data.Filtering.FilterCondition? FilterCondition { get; set; }

        public BaseComboBoxStyleSettings StyleSettings { get; set; }
    }
}

