namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class GroupContentControl : BaseGroupContentControl
    {
        static GroupContentControl()
        {
            new DependencyPropertyRegistrator<GroupContentControl>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        protected override void UpdateVisualState()
        {
            base.UpdateVisualState();
            VisualStateManager.GoToState(this, this.IsCaptionVisible ? "CaptionVisible" : "CaptionHidden", false);
        }

        protected virtual bool IsCaptionVisible =>
            (base.LayoutItem != null) && (base.LayoutItem.IsCaptionVisible && (base.LayoutItem.HasCaption || base.LayoutItem.HasCaptionTemplate));
    }
}

