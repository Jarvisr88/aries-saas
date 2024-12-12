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

    public class CheckedComboBoxStyleSettings : BaseComboBoxStyleSettings
    {
        public static readonly DependencyProperty ShowSelectAllItemProperty;
        public static readonly DependencyProperty SelectAllItemTextProperty;

        static CheckedComboBoxStyleSettings()
        {
            Type ownerType = typeof(CheckedComboBoxStyleSettings);
            ShowSelectAllItemProperty = DependencyProperty.Register("ShowSelectAllItem", typeof(bool), ownerType, new PropertyMetadata(true));
            SelectAllItemTextProperty = DependencyProperty.Register("SelectAllItemText", typeof(string), ownerType, new PropertyMetadata(null));
        }

        protected internal override bool GetClosePopupOnMouseUp(LookUpEditBase editor) => 
            false;

        protected internal override IEnumerable<CustomItem> GetCustomItems(LookUpEditBase editor)
        {
            SelectAllItem item = new SelectAllItem();
            item.DisplayText = this.SelectAllItemText;
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
            resourceKey.ResourceKey = EditorListBoxThemeKeys.CheckBoxItemStyle;
            resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(cb);
            return (Style) cb.FindResource(resourceKey);
        }

        public override PopupFooterButtons GetPopupFooterButtons(PopupBaseEdit editor) => 
            PopupFooterButtons.OkCancel;

        protected internal override SelectionEventMode GetSelectionEventMode(ISelectorEdit ce) => 
            SelectionEventMode.MouseUp;

        protected internal override SelectionMode GetSelectionMode(LookUpEditBase editor) => 
            SelectionMode.Multiple;

        protected internal override bool GetShowSizeGrip(PopupBaseEdit editor) => 
            true;

        protected override bool ShowCustomItemInternal(LookUpEditBase editor) => 
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

        protected internal override bool ShouldFocusPopup =>
            true;
    }
}

