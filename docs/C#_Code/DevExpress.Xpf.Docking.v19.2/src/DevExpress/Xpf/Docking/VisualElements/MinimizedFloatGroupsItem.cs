namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class MinimizedFloatGroupsItem : psvContentControl
    {
        public static readonly DependencyProperty ActualLayoutItemProperty;

        static MinimizedFloatGroupsItem()
        {
            DependencyPropertyRegistrator<MinimizedFloatGroupsItem> registrator = new DependencyPropertyRegistrator<MinimizedFloatGroupsItem>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<BaseLayoutItem>("ActualLayoutItem", ref ActualLayoutItemProperty, null, null, null);
        }

        public MinimizedFloatGroupsItem()
        {
            ContentControlHelper.SetContentIsNotLogical(this, true);
        }

        protected override void OnLayoutItemChanged(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            base.OnLayoutItemChanged(item, oldItem);
            FloatGroup floatGroup = item as FloatGroup;
            if (floatGroup != null)
            {
                this.ActualLayoutItem = floatGroup.GetActualLayoutItem();
            }
        }

        public BaseLayoutItem ActualLayoutItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(ActualLayoutItemProperty);
            set => 
                base.SetValue(ActualLayoutItemProperty, value);
        }
    }
}

