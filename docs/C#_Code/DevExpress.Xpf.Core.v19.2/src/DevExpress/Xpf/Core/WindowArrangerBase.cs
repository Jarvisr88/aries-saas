namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Media;

    internal abstract class WindowArrangerBase : WindowRelationInfo
    {
        protected Rect lastChildPos = Rect.Empty;
        protected Rect lastParentPos = Rect.Empty;
        protected Rect nextParentPos = Rect.Empty;
        protected SplashScreenLocation childLocation;
        private SplashScreenArrangeMode arrangeMode;

        protected internal WindowArrangerBase(WindowArrangerContainer parent, SplashScreenLocation childLocation, SplashScreenArrangeMode arrangeMode)
        {
            this.childLocation = childLocation;
            this.arrangeMode = arrangeMode;
            base.AttachParent(parent);
        }

        protected override void ChildAttachedOverride()
        {
            base.Child.Window.WindowStartupLocation = WindowStartupLocation.Manual;
        }

        protected override void CompleteInitializationOverride()
        {
            if (base.Child.Window.Dispatcher.CheckAccess())
            {
                switch (this.childLocation)
                {
                    case SplashScreenLocation.CenterContainer:
                        this.nextParentPos = ((WindowArrangerContainer) base.Parent).ControlStartupPosition;
                        return;

                    case SplashScreenLocation.CenterWindow:
                        this.nextParentPos = ((WindowArrangerContainer) base.Parent).WindowStartupPosition;
                        return;

                    case SplashScreenLocation.CenterScreen:
                    {
                        Screen screen = Screen.FromHandle(base.Parent.Handle);
                        this.nextParentPos = new Rect(new Point((double) screen.WorkingArea.X, (double) screen.WorkingArea.Y), new Size((double) screen.WorkingArea.Width, (double) screen.WorkingArea.Height));
                        break;
                    }
                    default:
                        return;
                }
            }
        }

        private static Rect GetDpiBasedBounds(Rect bounds, Visual visual)
        {
            PresentationSource source = PresentationSource.FromVisual(visual);
            return ((source == null) ? bounds : new Rect(bounds.X / source.CompositionTarget.TransformToDevice.M11, bounds.Y / source.CompositionTarget.TransformToDevice.M22, bounds.Width / source.CompositionTarget.TransformToDevice.M11, bounds.Height / source.CompositionTarget.TransformToDevice.M22));
        }

        private static bool IsZero(double value) => 
            (value == 0.0) || double.IsNaN(value);

        protected void UpdateChildLocation()
        {
            if (!this.SkipArrange && ((base.Child != null) && (base.Child.IsInitialized && !this.IsArrangeValid)))
            {
                if ((this.arrangeMode == SplashScreenArrangeMode.ArrangeOnStartupOnly) && ((this.lastParentPos != Rect.Empty) && (this.lastParentPos != this.nextParentPos)))
                {
                    this.arrangeMode = SplashScreenArrangeMode.Skip;
                }
                else
                {
                    Rect nextParentPos = this.nextParentPos;
                    Window visual = base.Child.Window;
                    if (!IsZero(visual.ActualWidth) && !IsZero(visual.ActualHeight))
                    {
                        if (this.childLocation == SplashScreenLocation.CenterScreen)
                        {
                            nextParentPos = GetDpiBasedBounds(nextParentPos, visual);
                        }
                        Point point = new Point(nextParentPos.X + ((nextParentPos.Width - visual.ActualWidth) * 0.5), nextParentPos.Y + ((nextParentPos.Height - visual.ActualHeight) * 0.5));
                        visual.Left = Math.Round(point.X);
                        visual.Top = Math.Round(point.Y);
                        this.lastChildPos = new Rect(visual.Left, visual.Top, visual.Width, visual.Height);
                        this.lastParentPos = nextParentPos;
                    }
                }
            }
        }

        protected void UpdateParentLocation()
        {
            if (base.Parent == null)
            {
                this.nextParentPos = Rect.Empty;
            }
            else
            {
                switch (this.childLocation)
                {
                    case SplashScreenLocation.CenterContainer:
                        this.nextParentPos = base.ActualIsParentClosed ? Rect.Empty : ((WindowArrangerContainer) base.Parent).GetControlRect();
                        return;

                    case SplashScreenLocation.CenterWindow:
                        this.nextParentPos = base.ActualIsParentClosed ? Rect.Empty : ((WindowArrangerContainer) base.Parent).GetWindowRect();
                        return;

                    case SplashScreenLocation.CenterScreen:
                    {
                        Screen screen = Screen.FromHandle(base.Parent.Handle);
                        this.nextParentPos = new Rect(new Point((double) screen.WorkingArea.X, (double) screen.WorkingArea.Y), new Size((double) screen.WorkingArea.Width, (double) screen.WorkingArea.Height));
                        return;
                    }
                }
            }
        }

        protected FrameworkElement ParentContainer =>
            base.Parent.WindowObject as FrameworkElement;

        protected bool SkipArrange =>
            base.IsReleased || (this.arrangeMode == SplashScreenArrangeMode.Skip);

        protected bool IsArrangeValid =>
            (this.nextParentPos == this.lastParentPos) && ((this.lastChildPos.Width == base.Child.Window.ActualWidth) && (this.lastChildPos.Height == base.Child.Window.ActualHeight));
    }
}

