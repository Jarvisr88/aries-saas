namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public class ListBoxEditStyleSettings : BaseListBoxEditStyleSettings
    {
        public static readonly DependencyProperty ShowEmptyItemProperty;
        public static readonly DependencyProperty EmptyItemTextProperty;

        static ListBoxEditStyleSettings()
        {
            Type ownerType = typeof(ListBoxEditStyleSettings);
            ShowEmptyItemProperty = DependencyProperty.Register("ShowEmptyItem", typeof(bool), ownerType, new PropertyMetadata(false));
            EmptyItemTextProperty = DependencyProperty.Register("EmptyItemText", typeof(string), ownerType, new PropertyMetadata(null));
        }

        protected internal override IEnumerable<CustomItem> GetCustomItems(ListBoxEdit editor)
        {
            List<CustomItem> list = new List<CustomItem>(base.GetCustomItems(editor));
            if (this.GetSelectionMode(editor) == SelectionMode.Single)
            {
                EmptyItem item = new EmptyItem();
                item.DisplayText = this.EmptyItemText;
                list.Add(item);
            }
            return list;
        }

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

