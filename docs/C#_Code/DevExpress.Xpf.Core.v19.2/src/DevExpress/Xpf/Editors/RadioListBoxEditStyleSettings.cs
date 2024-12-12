namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Themes;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public class RadioListBoxEditStyleSettings : BaseListBoxEditStyleSettings
    {
        public static readonly DependencyProperty ShowEmptyItemProperty;
        public static readonly DependencyProperty EmptyItemTextProperty;

        static RadioListBoxEditStyleSettings()
        {
            Type ownerType = typeof(RadioListBoxEditStyleSettings);
            ShowEmptyItemProperty = DependencyProperty.Register("ShowEmptyItem", typeof(bool), ownerType, new PropertyMetadata(false));
            EmptyItemTextProperty = DependencyProperty.Register("EmptyItemText", typeof(string), ownerType, new PropertyMetadata(null));
        }

        protected internal override IEnumerable<CustomItem> GetCustomItems(ListBoxEdit editor)
        {
            RadioEmptyItem item = new RadioEmptyItem();
            item.DisplayText = this.EmptyItemText;
            List<CustomItem> list1 = new List<CustomItem>();
            list1.Add(item);
            return list1;
        }

        protected internal override Style GetItemContainerStyle(ListBoxEdit control)
        {
            if (this.IsPropertySet(BaseListBoxEditStyleSettings.ItemContainerStyleProperty))
            {
                return base.ItemContainerStyle;
            }
            EditorListBoxThemeKeyExtension resourceKey = new EditorListBoxThemeKeyExtension();
            resourceKey.ResourceKey = EditorListBoxThemeKeys.RadioButtonItemStyle;
            resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(control);
            return (Style) control.FindResource(resourceKey);
        }

        protected override SelectionMode GetSelectionMode(ListBoxEdit editor) => 
            SelectionMode.Single;

        protected internal override bool ShowCustomItem(ListBoxEdit editor) => 
            this.ShowEmptyItem;

        public string EmptyItemText
        {
            get => 
                (string) base.GetValue(EmptyItemTextProperty);
            set => 
                base.SetValue(EmptyItemTextProperty, value);
        }

        public bool ShowEmptyItem
        {
            get => 
                (bool) base.GetValue(ShowEmptyItemProperty);
            set => 
                base.SetValue(ShowEmptyItemProperty, value);
        }
    }
}

