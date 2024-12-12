namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class MinimizedFloatGroupsItemsControl : ItemsControl
    {
        static MinimizedFloatGroupsItemsControl()
        {
            DependencyPropertyRegistrator<MinimizedFloatGroupsItemsControl>.New().OverrideDefaultStyleKey();
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new MinimizedFloatGroupsItem();

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is MinimizedFloatGroupsItem;
    }
}

