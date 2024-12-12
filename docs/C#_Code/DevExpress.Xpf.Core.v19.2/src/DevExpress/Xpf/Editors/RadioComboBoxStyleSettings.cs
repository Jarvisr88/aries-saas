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

    public class RadioComboBoxStyleSettings : BaseComboBoxStyleSettings
    {
        public static readonly DependencyProperty ShowEmptyItemProperty;
        public static readonly DependencyProperty EmptyItemTextProperty;

        static RadioComboBoxStyleSettings()
        {
            Type ownerType = typeof(RadioComboBoxStyleSettings);
            ShowEmptyItemProperty = DependencyProperty.Register("ShowEmptyItem", typeof(bool), ownerType, new PropertyMetadata(false));
            EmptyItemTextProperty = DependencyProperty.Register("EmptyItemText", typeof(string), ownerType, new PropertyMetadata(null));
        }

        protected internal override IEnumerable<CustomItem> GetCustomItems(LookUpEditBase editor)
        {
            RadioEmptyItem item = new RadioEmptyItem();
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
            resourceKey.ResourceKey = EditorListBoxThemeKeys.RadioButtonItemStyle;
            resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(cb);
            return (Style) cb.FindResource(resourceKey);
        }

        public override PopupFooterButtons GetPopupFooterButtons(PopupBaseEdit editor) => 
            PopupFooterButtons.OkCancel;

        protected internal override SelectionEventMode GetSelectionEventMode(ISelectorEdit ce) => 
            SelectionEventMode.MouseUp;

        protected internal override SelectionMode GetSelectionMode(LookUpEditBase editor) => 
            SelectionMode.Single;

        protected internal override bool GetShowSizeGrip(PopupBaseEdit editor) => 
            true;

        protected internal override bool ShowCustomItem(LookUpEditBase editor) => 
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

