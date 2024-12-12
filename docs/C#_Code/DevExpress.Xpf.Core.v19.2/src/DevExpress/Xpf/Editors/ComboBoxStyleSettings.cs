namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Editors.Themes;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public class ComboBoxStyleSettings : BaseComboBoxStyleSettings
    {
        public static readonly DependencyProperty ShowEmptyItemProperty;
        public static readonly DependencyProperty EmptyItemTextProperty;

        static ComboBoxStyleSettings()
        {
            Type ownerType = typeof(ComboBoxStyleSettings);
            ShowEmptyItemProperty = DependencyProperty.Register("ShowEmptyItem", typeof(bool), ownerType, new PropertyMetadata(false));
            EmptyItemTextProperty = DependencyProperty.Register("EmptyItemText", typeof(string), ownerType, new PropertyMetadata(null));
        }

        protected internal override IEnumerable<CustomItem> GetCustomItems(LookUpEditBase editor)
        {
            if (!((LookUpEditBasePropertyProvider) editor.PropertyProvider).IsSingleSelection)
            {
                return new List<CustomItem>();
            }
            EmptyItem item = new EmptyItem();
            item.DisplayText = this.EmptyItemText;
            List<CustomItem> list1 = new List<CustomItem>();
            list1.Add(item);
            return list1;
        }

        protected internal override Style GetItemContainerStyle(LookUpEditBase cb)
        {
            if (this.IsPropertySet(BaseComboBoxStyleSettings.ItemContainerStyleProperty))
            {
                return base.ItemContainerStyle;
            }
            EditorListBoxThemeKeyExtension resourceKey = new EditorListBoxThemeKeyExtension();
            resourceKey.ResourceKey = EditorListBoxThemeKeys.DefaultItemStyle;
            resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(cb);
            return (Style) cb.FindResource(resourceKey);
        }

        protected internal override SelectionEventMode GetSelectionEventMode(ISelectorEdit ce) => 
            ((LookUpEditBase) ce).AllowItemHighlighting ? SelectionEventMode.MouseEnter : SelectionEventMode.MouseDown;

        protected internal override SelectionMode GetSelectionMode(LookUpEditBase editor) => 
            SelectionMode.Single;

        protected internal override bool GetShowSizeGrip(PopupBaseEdit editor) => 
            false;

        protected override bool ShowCustomItemInternal(LookUpEditBase editor) => 
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

