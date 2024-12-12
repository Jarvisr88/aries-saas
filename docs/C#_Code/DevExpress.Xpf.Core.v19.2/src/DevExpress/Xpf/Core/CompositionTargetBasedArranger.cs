namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Media;
    using System.Windows.Threading;

    internal class CompositionTargetBasedArranger : WindowArrangerBase
    {
        protected internal CompositionTargetBasedArranger(WindowArrangerContainer parent, SplashScreenLocation childLocation, SplashScreenArrangeMode arrangeMode) : base(parent, childLocation, arrangeMode)
        {
        }

        private void OnChildRendering(object sender, EventArgs e)
        {
            if (base.IsReleased)
            {
                CompositionTarget.Rendering -= new EventHandler(this.OnChildRendering);
            }
            else if (((Dispatcher) sender).Thread.ManagedThreadId == base.Child.ManagedThreadId)
            {
                base.UpdateChildLocation();
            }
        }

        private void OnParentRendering(object sender, EventArgs e)
        {
            if (base.IsReleased)
            {
                CompositionTarget.Rendering -= new EventHandler(this.OnParentRendering);
            }
            else if (((Dispatcher) sender).Thread.ManagedThreadId == base.Parent.ManagedThreadId)
            {
                base.UpdateParentLocation();
            }
        }

        protected override void SubscribeChildEventsOverride()
        {
            CompositionTarget.Rendering += new EventHandler(this.OnChildRendering);
        }

        protected override void SubscribeParentEventsOverride()
        {
            if (base.childLocation != SplashScreenLocation.CenterScreen)
            {
                CompositionTarget.Rendering += new EventHandler(this.OnParentRendering);
            }
        }
    }
}

