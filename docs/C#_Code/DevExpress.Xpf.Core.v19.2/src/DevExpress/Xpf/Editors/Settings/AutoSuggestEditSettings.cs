namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class AutoSuggestEditSettings : PopupBaseEditSettings
    {
        public static readonly DependencyProperty AcceptValueOnPopupContentSelectionChangedProperty;
        public static readonly DependencyProperty ImmediatePopupProperty;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        public static readonly DependencyProperty ItemsPanelProperty;
        public static readonly DependencyProperty ItemContainerStyleProperty;
        public static readonly DependencyProperty ItemContainerStyleSelectorProperty;
        public static readonly DependencyProperty ItemStringFormatProperty;
        public static readonly DependencyProperty DisplayMemberProperty;
        public static readonly DependencyProperty TextMemberProperty;

        static AutoSuggestEditSettings()
        {
            Type forType = typeof(AutoSuggestEditSettings);
            PopupBaseEditSettings.PopupMaxHeightProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(SystemParameters.PrimaryScreenHeight / 3.0));
            TextEditSettings.ValidateOnTextInputProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(false));
            AcceptValueOnPopupContentSelectionChangedProperty = DependencyProperty.Register("AcceptValueOnPopupContentSelectionChanged", typeof(bool), forType, new PropertyMetadata(false));
            ImmediatePopupProperty = DependencyProperty.Register("ImmediatePopup", typeof(bool), forType, new PropertyMetadata(false));
            ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(object), forType, new PropertyMetadata(null));
            ItemTemplateProperty = ItemsControl.ItemTemplateProperty.AddOwner(forType, new FrameworkPropertyMetadata(null));
            ItemsPanelProperty = ItemsControl.ItemsPanelProperty.AddOwner(forType, new FrameworkPropertyMetadata(ItemsControl.ItemsPanelProperty.DefaultMetadata.DefaultValue));
            ItemTemplateSelectorProperty = ItemsControl.ItemTemplateSelectorProperty.AddOwner(forType, new FrameworkPropertyMetadata(null));
            ItemContainerStyleProperty = ItemsControl.ItemContainerStyleProperty.AddOwner(forType);
            ItemContainerStyleSelectorProperty = ItemsControl.ItemContainerStyleSelectorProperty.AddOwner(forType);
            ItemStringFormatProperty = ItemsControl.ItemStringFormatProperty.AddOwner(forType);
            DisplayMemberProperty = DependencyProperty.Register("DisplayMember", typeof(string), forType, new PropertyMetadata(null));
            TextMemberProperty = DependencyProperty.Register("TextMember", typeof(string), forType);
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            AutoSuggestEdit editor = edit as AutoSuggestEdit;
            if (editor != null)
            {
                base.SetValueFromSettings(AcceptValueOnPopupContentSelectionChangedProperty, () => editor.AcceptValueOnPopupContentSelectionChanged = this.AcceptValueOnPopupContentSelectionChanged);
                base.SetValueFromSettings(ImmediatePopupProperty, () => editor.ImmediatePopup = this.ImmediatePopup);
                base.SetValueFromSettings(ItemsSourceProperty, () => editor.ItemsSource = this.ItemsSource);
                base.SetValueFromSettings(ItemTemplateProperty, () => editor.ItemTemplate = this.ItemTemplate, () => this.ClearEditorPropertyIfNeeded(editor, AutoSuggestEdit.ItemTemplateProperty, ItemTemplateProperty));
                base.SetValueFromSettings(ItemsPanelProperty, () => editor.ItemsPanel = this.ItemsPanel, () => this.ClearEditorPropertyIfNeeded(editor, AutoSuggestEdit.ItemsPanelProperty, ItemsPanelProperty));
                base.SetValueFromSettings(ItemTemplateSelectorProperty, () => editor.ItemTemplateSelector = this.ItemTemplateSelector);
                base.SetValueFromSettings(ItemContainerStyleProperty, () => editor.ItemContainerStyle = this.ItemContainerStyle);
                base.SetValueFromSettings(ItemContainerStyleSelectorProperty, () => editor.ItemContainerStyleSelector = this.ItemContainerStyleSelector);
                base.SetValueFromSettings(ItemStringFormatProperty, () => editor.ItemStringFormat = this.ItemStringFormat);
                base.SetValueFromSettings(DisplayMemberProperty, () => editor.DisplayMember = this.DisplayMember);
                base.SetValueFromSettings(TextMemberProperty, () => editor.TextMember = this.TextMember);
            }
        }

        public bool AcceptValueOnPopupContentSelectionChanged
        {
            get => 
                (bool) base.GetValue(AcceptValueOnPopupContentSelectionChangedProperty);
            set => 
                base.SetValue(AcceptValueOnPopupContentSelectionChangedProperty, value);
        }

        public bool ImmediatePopup
        {
            get => 
                (bool) base.GetValue(ImmediatePopupProperty);
            set => 
                base.SetValue(ImmediatePopupProperty, value);
        }

        public object ItemsSource
        {
            get => 
                base.GetValue(ItemsSourceProperty);
            set => 
                base.SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemTemplateProperty);
            set => 
                base.SetValue(ItemTemplateProperty, value);
        }

        public ItemsPanelTemplate ItemsPanel
        {
            get => 
                (ItemsPanelTemplate) base.GetValue(ItemsPanelProperty);
            set => 
                base.SetValue(ItemsPanelProperty, value);
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ItemTemplateSelectorProperty);
            set => 
                base.SetValue(ItemTemplateSelectorProperty, value);
        }

        public Style ItemContainerStyle
        {
            get => 
                (Style) base.GetValue(ItemContainerStyleProperty);
            set => 
                base.SetValue(ItemContainerStyleProperty, value);
        }

        public StyleSelector ItemContainerStyleSelector
        {
            get => 
                (StyleSelector) base.GetValue(ItemContainerStyleSelectorProperty);
            set => 
                base.SetValue(ItemContainerStyleSelectorProperty, value);
        }

        public string ItemStringFormat
        {
            get => 
                (string) base.GetValue(ItemStringFormatProperty);
            set => 
                base.SetValue(ItemStringFormatProperty, value);
        }

        public string DisplayMember
        {
            get => 
                (string) base.GetValue(DisplayMemberProperty);
            set => 
                base.SetValue(DisplayMemberProperty, value);
        }

        public string TextMember
        {
            get => 
                (string) base.GetValue(TextMemberProperty);
            set => 
                base.SetValue(TextMemberProperty, value);
        }

        protected internal override bool RestrictPopupHeight =>
            true;
    }
}

