namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    public abstract class FixedItem : BaseLayoutItem, IUIElement
    {
        static FixedItem()
        {
            DependencyPropertyRegistrator<FixedItem> registrator = new DependencyPropertyRegistrator<FixedItem>();
            registrator.OverrideMetadata<bool>(BaseLayoutItem.AllowFloatProperty, false, null, null);
            registrator.OverrideMetadata<bool>(BaseLayoutItem.AllowDockProperty, false, null, null);
            registrator.OverrideMetadata<bool>(BaseLayoutItem.AllowCloseProperty, false, null, null);
        }

        internal override UIChildren CreateUIChildren() => 
            new EmptyUIChildrenCollection();
    }
}

