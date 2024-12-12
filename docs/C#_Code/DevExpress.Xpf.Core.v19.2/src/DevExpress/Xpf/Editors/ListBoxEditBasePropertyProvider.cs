namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class ListBoxEditBasePropertyProvider : ActualPropertyProvider, ISelectorEditPropertyProvider
    {
        public static readonly DependencyProperty DisplayMemberPathProperty;
        public static readonly DependencyProperty SelectionViewModelProperty;
        public static readonly DependencyProperty ShowWaitIndicatorProperty;
        public static readonly DependencyProperty HasItemTemplateProperty;

        static ListBoxEditBasePropertyProvider()
        {
            Type ownerType = typeof(ListBoxEditBasePropertyProvider);
            SelectionViewModelProperty = DependencyPropertyManager.Register("SelectionViewModel", typeof(DevExpress.Xpf.Editors.Popups.SelectionViewModel), ownerType, new FrameworkPropertyMetadata(null));
            DisplayMemberPathProperty = DependencyPropertyManager.Register("DisplayMemberPath", typeof(string), ownerType);
            ShowWaitIndicatorProperty = DependencyPropertyManager.Register("ShowWaitIndicator", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            HasItemTemplateProperty = DependencyPropertyManager.Register("HasItemTemplate", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
        }

        public ListBoxEditBasePropertyProvider(BaseEdit editor) : base(editor)
        {
            this.SelectionViewModel = new DevExpress.Xpf.Editors.Popups.SelectionViewModel(() => this.Editor.ListBoxCore);
        }

        public Style GetItemContainerStyle()
        {
            Style itemContainerStyle = this.Editor.ItemContainerStyle;
            Style style3 = itemContainerStyle;
            if (itemContainerStyle == null)
            {
                Style local1 = itemContainerStyle;
                Style style2 = this.StyleSettings.ItemContainerStyle;
                style3 = style2;
                if (style2 == null)
                {
                    Style local2 = style2;
                    style3 = this.StyleSettings.GetItemContainerStyle(this.Editor);
                }
            }
            return style3;
        }

        public bool ShowCustomItems()
        {
            bool? showCustomItems = this.Editor.ShowCustomItems;
            return ((showCustomItems != null) ? showCustomItems.GetValueOrDefault() : this.StyleSettings.ShowCustomItem(this.Editor));
        }

        public bool ShowWaitIndicator
        {
            get => 
                (bool) base.GetValue(ShowWaitIndicatorProperty);
            set => 
                base.SetValue(ShowWaitIndicatorProperty, value);
        }

        public bool HasItemTemplate
        {
            get => 
                (bool) base.GetValue(HasItemTemplateProperty);
            set => 
                base.SetValue(HasItemTemplateProperty, value);
        }

        public DevExpress.Xpf.Editors.Popups.SelectionEventMode SelectionEventMode =>
            this.StyleSettings.GetSelectionEventMode(this.Editor);

        public string DisplayMemberPath
        {
            get => 
                (string) base.GetValue(DisplayMemberPathProperty);
            set => 
                base.SetValue(DisplayMemberPathProperty, value);
        }

        public DevExpress.Xpf.Editors.Popups.SelectionViewModel SelectionViewModel
        {
            get => 
                (DevExpress.Xpf.Editors.Popups.SelectionViewModel) base.GetValue(SelectionViewModelProperty);
            set => 
                base.SetValue(SelectionViewModelProperty, value);
        }

        private BaseListBoxEditStyleSettings StyleSettings =>
            (BaseListBoxEditStyleSettings) base.StyleSettings;

        private ListBoxEdit Editor =>
            (ListBoxEdit) base.Editor;
    }
}

