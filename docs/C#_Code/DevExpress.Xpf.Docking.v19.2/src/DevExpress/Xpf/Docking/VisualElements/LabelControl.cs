namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LabelControl : psvHeaderedContentControl
    {
        static LabelControl()
        {
            new DependencyPropertyRegistrator<LabelControl>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public LabelControl()
        {
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
        }

        protected override void OnActualSizeChanged(Size value)
        {
            base.OnActualSizeChanged(value);
            if (!base.IsDisposing)
            {
                BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem(this);
                if (layoutItem != null)
                {
                    layoutItem.LayoutSize = value;
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((this.PartCaption != null) && !LayoutItemsHelper.IsTemplateChild<LabelControl>(this.PartCaption, this))
            {
                this.PartCaption.Dispose();
            }
            if ((this.PartControl != null) && !LayoutItemsHelper.IsTemplateChild<LabelControl>(this.PartControl, this))
            {
                this.PartControl.Dispose();
            }
            this.PartBorder = base.GetTemplateChild("PART_Border") as UIElement;
            this.PartCaption = base.GetTemplateChild("PART_Caption") as LabelControlCaptionPresenter;
            this.PartControl = base.GetTemplateChild("PART_Content") as LabelControlContentPresenter;
            if (this.PartControl != null)
            {
                this.PartControl.EnsureOwner(this);
            }
        }

        protected override void OnDispose()
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
            base.ClearValue(DockLayoutManager.DockLayoutManagerProperty);
            base.ClearValue(DockLayoutManager.LayoutItemProperty);
            if (this.PartCaption != null)
            {
                this.PartCaption.Dispose();
                this.PartCaption = null;
            }
            if (this.PartControl != null)
            {
                this.PartControl.Dispose();
                this.PartControl = null;
            }
            base.OnDispose();
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            this.UpdateDesiredSize(base.DesiredSize);
        }

        private void UpdateDesiredSize(Size desiredSize)
        {
            if ((this.Item != null) && !this.Item.HasDesiredSize)
            {
                if (this.PartBorder != null)
                {
                    desiredSize = this.PartBorder.DesiredSize;
                }
                this.Item.DesiredSizeInternal = desiredSize;
            }
        }

        protected UIElement PartBorder { get; private set; }

        protected LabelControlCaptionPresenter PartCaption { get; private set; }

        protected LabelControlContentPresenter PartControl { get; private set; }

        protected LabelItem Item =>
            base.LayoutItem as LabelItem;
    }
}

