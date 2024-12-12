namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class BaseGroupContentControl : psvContentControl
    {
        static BaseGroupContentControl()
        {
            new DependencyPropertyRegistrator<BaseGroupContentControl>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public BaseGroupContentControl()
        {
            ContentControlHelper.SetContentIsNotLogical(this, true);
        }

        private void item_VisualChanged(object sender, EventArgs e)
        {
            this.UpdateVisual();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (base.LayoutItem != null)
            {
                this.UpdateVisual();
            }
        }

        protected override void Subscribe(BaseLayoutItem item)
        {
            base.Subscribe(item);
            if (item != null)
            {
                item.VisualChanged += new EventHandler(this.item_VisualChanged);
            }
        }

        protected override void Unsubscribe(BaseLayoutItem item)
        {
            base.Unsubscribe(item);
            if (item != null)
            {
                item.VisualChanged -= new EventHandler(this.item_VisualChanged);
            }
        }

        protected virtual void UpdateCaptionBackground()
        {
        }

        private void UpdateVisual()
        {
            this.UpdateVisualState();
            this.UpdateCaptionBackground();
        }

        protected virtual void UpdateVisualState()
        {
        }

        public DevExpress.Xpf.Docking.LayoutGroup LayoutGroup =>
            base.LayoutItem as DevExpress.Xpf.Docking.LayoutGroup;
    }
}

