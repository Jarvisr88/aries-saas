namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Themes;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public class CheckedListBoxEditStyleSettings : BaseListBoxEditStyleSettings
    {
        public static readonly DependencyProperty ShowSelectAllItemProperty;
        public static readonly DependencyProperty SelectAllItemTextProperty;

        static CheckedListBoxEditStyleSettings()
        {
            Type ownerType = typeof(CheckedListBoxEditStyleSettings);
            ShowSelectAllItemProperty = DependencyProperty.Register("ShowSelectAllItem", typeof(bool), ownerType, new PropertyMetadata(true));
            SelectAllItemTextProperty = DependencyProperty.Register("SelectAllItemText", typeof(string), ownerType, new PropertyMetadata(null));
        }

        protected internal override IEnumerable<CustomItem> GetCustomItems(ListBoxEdit editor)
        {
            List<CustomItem> list = new List<CustomItem>();
            SelectAllItem item = new SelectAllItem();
            item.DisplayText = this.SelectAllItemText;
            list.Add(item);
            return list;
        }

        protected internal override Style GetItemContainerStyle(ListBoxEdit control)
        {
            if (this.IsPropertySet(BaseListBoxEditStyleSettings.ItemContainerStyleProperty))
            {
                return base.ItemContainerStyle;
            }
            EditorListBoxThemeKeyExtension resourceKey = new EditorListBoxThemeKeyExtension();
            resourceKey.ResourceKey = EditorListBoxThemeKeys.CheckBoxItemStyle;
            resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(control);
            return (Style) control.FindResource(resourceKey);
        }

        protected override SelectionMode GetSelectionMode(ListBoxEdit editor) => 
            SelectionMode.Multiple;

        protected override bool ShowCustomItemInternal(ListBoxEdit editor) => 
            this.ShowSelectAllItem;

        public string SelectAllItemText
        {
            get => 
                (string) base.GetValue(SelectAllItemTextProperty);
            set => 
                base.SetValue(SelectAllItemTextProperty, value);
        }

        public bool ShowSelectAllItem
        {
            get => 
                (bool) base.GetValue(ShowSelectAllItemProperty);
            set => 
                base.SetValue(ShowSelectAllItemProperty, value);
        }
    }
}

