namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class BadgeShaderWorker : BadgeWorkerBase
    {
        private BadgeShaderEffect effect;
        private FrameworkElement badgeContainer;
        private DispatcherOperation badgeContainerResult;

        public BadgeShaderWorker(FrameworkElement target) : base(target)
        {
        }

        protected override bool CalculateIsVisibleOverride() => 
            true;

        protected override void Destroy()
        {
            this.badgeContainerResult.Abort();
            this.badgeContainer.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
            base.Destroy();
        }

        private void DoLayout()
        {
            if (base.IsVisible)
            {
                this.badgeContainerResult.Abort();
                this.badgeContainer.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
                this.badgeContainer.Width = base.FrameworkElement.ActualWidth;
                this.badgeContainer.Height = base.FrameworkElement.ActualHeight;
                this.badgeContainer.Measure(SizeHelper.Infinite);
                Rect rect = base.BadgeControl.CalcArrangeRect(new Size(base.FrameworkElement.ActualWidth, base.FrameworkElement.ActualHeight));
                this.effect.UpdatePaddings(-Math.Min(0.0, rect.Left), -Math.Min(0.0, rect.Top), Math.Max((double) 0.0, (double) (rect.Right - base.FrameworkElement.ActualWidth)), Math.Max((double) 0.0, (double) (rect.Bottom - base.FrameworkElement.ActualHeight)));
                this.badgeContainer.Arrange(new Rect(0.0, 0.0, base.FrameworkElement.ActualWidth, base.FrameworkElement.ActualHeight));
            }
        }

        protected override void HideOverride()
        {
            base.FrameworkElement.Effect = null;
            this.effect = null;
            if (this.badgeContainer != null)
            {
                base.BadgeControl = null;
                this.badgeContainer = null;
            }
        }

        private object LayoutCallback(object arg)
        {
            this.DoLayout();
            return null;
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            this.DoLayout();
        }

        protected override void OnTargetSizeChanged(object sender, SizeChangedEventArgs e)
        {
            base.OnTargetSizeChanged(sender, e);
            this.DoLayout();
        }

        protected override void ShowOverride()
        {
            if (this.effect == null)
            {
                this.effect = new BadgeShaderEffect();
                base.FrameworkElement.Effect = this.effect;
                if (!base.FrameworkElement.UseLayoutRounding)
                {
                    base.FrameworkElement.UseLayoutRounding = true;
                    base.FrameworkElement.SnapsToDevicePixels = true;
                }
            }
            base.BadgeControl = base.Badge.CreateControl();
            NonLogicalDecorator decorator = new NonLogicalDecorator();
            Border border1 = new Border();
            border1.Background = Brushes.Transparent;
            border1.Child = decorator;
            this.badgeContainer = border1;
            decorator.Child = base.BadgeControl;
            VisualBrush brush1 = new VisualBrush(this.badgeContainer);
            brush1.AutoLayoutContent = false;
            VisualBrush brush = brush1;
            this.effect.Badge = brush;
            this.badgeContainer.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
            this.badgeContainerResult = this.badgeContainer.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(this.LayoutCallback), this.badgeContainer);
        }
    }
}

