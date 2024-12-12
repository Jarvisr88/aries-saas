namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class GroupPaneContentPresenter : BasePanePresenter<GroupPane, LayoutGroup>
    {
        public static readonly DependencyProperty GroupBorderStyleProperty;

        static GroupPaneContentPresenter()
        {
            new DependencyPropertyRegistrator<GroupPaneContentPresenter>().Register<DevExpress.Xpf.Docking.GroupBorderStyle>("GroupBorderStyle", ref GroupBorderStyleProperty, DevExpress.Xpf.Docking.GroupBorderStyle.NoBorder, (dObj, e) => ((GroupPaneContentPresenter) dObj).OnStylePropertyChanged(), null);
        }

        protected override bool CanSelectTemplate(LayoutGroup group) => 
            group.ActualGroupTemplateSelector != null;

        protected override LayoutGroup ConvertToLogicalItem(object content) => 
            (LayoutItemData.ConvertToBaseLayoutItem(content) as LayoutGroup) ?? base.ConvertToLogicalItem(content);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((this.PartLayoutItemsControl != null) && !LayoutItemsHelper.IsTemplateChild<GroupPaneContentPresenter>(this.PartLayoutItemsControl, this))
            {
                this.PartLayoutItemsControl.Dispose();
            }
            this.PartLayoutItemsControl = LayoutItemsHelper.GetTemplateChild<psvItemsControl>(this);
            if ((this.PartGroupContentControl != null) && !LayoutItemsHelper.IsTemplateChild<GroupPaneContentPresenter>(this.PartGroupContentControl, this))
            {
                this.PartGroupContentControl.Dispose();
            }
            this.PartGroupContentControl = LayoutItemsHelper.GetTemplateChild<BaseGroupContentControl>(this);
            if (this.PartGroupContentControl != null)
            {
                this.PartGroupContentControl.Loaded += new RoutedEventHandler(this.PartGroupContentControl_Loaded);
            }
        }

        protected override void OnDispose()
        {
            if (this.PartLayoutItemsControl != null)
            {
                this.PartLayoutItemsControl.Dispose();
                this.PartLayoutItemsControl = null;
            }
            if (this.PartGroupContentControl != null)
            {
                this.PartGroupContentControl.Dispose();
                this.PartGroupContentControl = null;
            }
            base.OnDispose();
        }

        private void PartGroupContentControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((BaseGroupContentControl) sender).Loaded -= new RoutedEventHandler(this.PartGroupContentControl_Loaded);
            if (!base.IsDisposing)
            {
                this.PartLayoutItemsControl = LayoutItemsHelper.GetTemplateChild<psvItemsControl>(this);
            }
        }

        protected override DataTemplate SelectTemplateCore(LayoutGroup group) => 
            group.ActualGroupTemplateSelector.SelectTemplate(group, this);

        public DevExpress.Xpf.Docking.GroupBorderStyle GroupBorderStyle
        {
            get => 
                (DevExpress.Xpf.Docking.GroupBorderStyle) base.GetValue(GroupBorderStyleProperty);
            set => 
                base.SetValue(GroupBorderStyleProperty, value);
        }

        protected psvItemsControl PartLayoutItemsControl { get; private set; }

        protected BaseGroupContentControl PartGroupContentControl { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupPaneContentPresenter.<>c <>9 = new GroupPaneContentPresenter.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((GroupPaneContentPresenter) dObj).OnStylePropertyChanged();
            }
        }
    }
}

