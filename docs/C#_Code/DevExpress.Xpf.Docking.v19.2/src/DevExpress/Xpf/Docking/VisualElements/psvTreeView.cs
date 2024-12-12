namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class psvTreeView : TreeView
    {
        public static readonly DependencyProperty LayoutGroupTemplateProperty;
        public static readonly DependencyProperty LayoutItemTemplateProperty;

        static psvTreeView()
        {
            DependencyPropertyRegistrator<psvTreeView> registrator = new DependencyPropertyRegistrator<psvTreeView>();
            registrator.Register<DataTemplate>("LayoutGroupTemplate", ref LayoutGroupTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("LayoutItemTemplate", ref LayoutItemTemplateProperty, null, null, null);
        }

        public psvTreeView()
        {
            base.ItemTemplateSelector = new psvTreeViewItemDataTemplateSelector(this);
        }

        public DataTemplate LayoutGroupTemplate
        {
            get => 
                (DataTemplate) base.GetValue(LayoutGroupTemplateProperty);
            set => 
                base.SetValue(LayoutGroupTemplateProperty, value);
        }

        public DataTemplate LayoutItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(LayoutItemTemplateProperty);
            set => 
                base.SetValue(LayoutItemTemplateProperty, value);
        }

        private class psvTreeViewItemDataTemplateSelector : DataTemplateSelector
        {
            private psvTreeView Owner;

            public psvTreeViewItemDataTemplateSelector(psvTreeView owner)
            {
                this.Owner = owner;
            }

            public override DataTemplate SelectTemplate(object item, DependencyObject container) => 
                (item is LayoutGroup) ? this.Owner.LayoutGroupTemplate : this.Owner.LayoutItemTemplate;
        }
    }
}

