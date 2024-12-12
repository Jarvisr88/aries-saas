namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [TemplatePart(Name="PART_Caption", Type=typeof(ControlItemCaptionPresenter)), TemplatePart(Name="PART_Control", Type=typeof(ControlItemControlPresenter))]
    public class ControlItem : psvHeaderedContentControl
    {
        static ControlItem()
        {
            new DependencyPropertyRegistrator<ControlItem>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public ControlItem()
        {
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
        }

        protected override void OnActualSizeChanged(Size value)
        {
            base.OnActualSizeChanged(value);
            if (!base.IsDisposing && (this.Item != null))
            {
                this.Item.LayoutSize = value;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartBorder = base.GetTemplateChild("PART_Border") as FrameworkElement;
            if ((this.PartCaption != null) && !LayoutItemsHelper.IsTemplateChild<ControlItem>(this.PartCaption, this))
            {
                this.PartCaption.Dispose();
            }
            this.PartCaption = base.GetTemplateChild("PART_Caption") as ControlItemCaptionPresenter;
            if (this.PartCaption != null)
            {
                this.PartCaption.EnsureOwner(this);
            }
            if ((this.PartControl != null) && !LayoutItemsHelper.IsTemplateChild<ControlItem>(this.PartControl, this))
            {
                this.PartControl.Dispose();
            }
            this.PartControl = base.GetTemplateChild("PART_Control") as ControlItemControlPresenter;
            if (this.PartControl != null)
            {
                this.PartControl.EnsureOwner(this);
            }
        }

        protected override void OnDispose()
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
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

        public ControlItemCaptionPresenter PartCaption { get; private set; }

        public ControlItemControlPresenter PartControl { get; private set; }

        protected FrameworkElement PartBorder { get; private set; }

        protected LayoutControlItem Item =>
            base.LayoutItem as LayoutControlItem;
    }
}

