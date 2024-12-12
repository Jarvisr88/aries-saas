namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Editors.Themes;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class BaseListBoxEditStyleSettings : BaseItemsControlStyleSettings<ListBoxEdit>, ISelectorEditStyleSettings
    {
        public static readonly DependencyProperty SelectionEventModeProperty;
        public static readonly DependencyProperty ItemContainerStyleProperty;

        static BaseListBoxEditStyleSettings()
        {
            Type ownerType = typeof(BaseListBoxEditStyleSettings);
            ItemContainerStyleProperty = DependencyProperty.Register("ItemContainerStyle", typeof(Style), ownerType, new PropertyMetadata(null));
            SelectionEventModeProperty = DependencyProperty.Register("SelectionEventMode", typeof(DevExpress.Xpf.Editors.Popups.SelectionEventMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.Popups.SelectionEventMode.MouseDown));
        }

        protected BaseListBoxEditStyleSettings()
        {
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            ListBoxEdit edit = editor as ListBoxEdit;
            if (edit != null)
            {
                edit.SelectionMode = this.GetSelectionMode(edit);
                edit.Settings.SelectionMode = this.GetSelectionMode(edit);
                if (edit.ListBoxCore != null)
                {
                    edit.ListBoxCore.ItemContainerStyle = this.GetItemContainerStyle(edit);
                }
            }
        }

        Style ISelectorEditStyleSettings.GetItemContainerStyle(ISelectorEdit editor) => 
            this.GetItemContainerStyle((ListBoxEdit) editor);

        protected internal override Style GetItemContainerStyle(ListBoxEdit editor)
        {
            if (editor.ItemContainerStyle != null)
            {
                return editor.ItemContainerStyle;
            }
            EditorListBoxThemeKeyExtension resourceKey = new EditorListBoxThemeKeyExtension();
            resourceKey.ResourceKey = EditorListBoxThemeKeys.DefaultItemStyle;
            resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(editor);
            return (Style) editor.FindResource(resourceKey);
        }

        protected internal override DevExpress.Xpf.Editors.Popups.SelectionEventMode GetSelectionEventMode(ISelectorEdit ce) => 
            this.SelectionEventMode;

        protected virtual SelectionMode GetSelectionMode(ListBoxEdit editor) => 
            editor.SelectionMode;

        protected internal override bool ShowCustomItem(ListBoxEdit editor)
        {
            bool? showCustomItems = editor.ShowCustomItems;
            return ((showCustomItems != null) ? showCustomItems.GetValueOrDefault() : this.ShowCustomItemInternal(editor));
        }

        protected virtual bool ShowCustomItemInternal(ListBoxEdit editor) => 
            false;

        public Style ItemContainerStyle
        {
            get => 
                (Style) base.GetValue(ItemContainerStyleProperty);
            set => 
                base.SetValue(ItemContainerStyleProperty, value);
        }

        public DevExpress.Xpf.Editors.Popups.SelectionEventMode SelectionEventMode
        {
            get => 
                (DevExpress.Xpf.Editors.Popups.SelectionEventMode) base.GetValue(SelectionEventModeProperty);
            set => 
                base.SetValue(SelectionEventModeProperty, value);
        }
    }
}

