namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class FieldItem
    {
        private static FieldItem CreateColumnBase(object caption, DataTemplateSelector selector)
        {
            FieldItem item1 = new FieldItem();
            item1.Caption = caption;
            item1.CaptionTemplateSelector = selector;
            item1.SelectedCaptionTemplateSelector = selector;
            return item1;
        }

        internal static FieldItem CreateGroup(object caption, DataTemplateSelector selector, IList<FieldItem> children)
        {
            FieldItem item = CreateColumnBase(caption, selector);
            item.Children = children;
            return item;
        }

        internal static FieldItem CreateLeaf(object caption, DataTemplateSelector selector, string fieldName)
        {
            FieldItem item = CreateColumnBase(caption, selector);
            item.FieldName = fieldName;
            return item;
        }

        public string FieldName { get; set; }

        public object Caption { get; set; }

        public DataTemplate CaptionTemplate { get; set; }

        public DataTemplateSelector CaptionTemplateSelector { get; set; }

        public DataTemplate SelectedCaptionTemplate { get; set; }

        public DataTemplateSelector SelectedCaptionTemplateSelector { get; set; }

        public IList<FieldItem> Children { get; set; }
    }
}

