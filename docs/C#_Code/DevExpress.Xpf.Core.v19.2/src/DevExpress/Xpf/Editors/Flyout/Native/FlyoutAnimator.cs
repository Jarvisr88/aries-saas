namespace DevExpress.Xpf.Editors.Flyout.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class FlyoutAnimator : FlyoutAnimatorBase
    {
        private const string TranslateName = "FlyoutTranslateName";

        private Storyboard CreateStoryboard(params Timeline[] timelines)
        {
            Storyboard storyboard = new Storyboard();
            timelines.Do<Timeline[]>(delegate (Timeline[] x) {
                Action<Timeline> <>9__1;
                Action<Timeline> action = <>9__1;
                if (<>9__1 == null)
                {
                    Action<Timeline> local1 = <>9__1;
                    action = <>9__1 = timeline => storyboard.Children.Add(timeline);
                }
                Array.ForEach<Timeline>(x, action);
            });
            return storyboard;
        }

        public override Storyboard GetCloseAnimation(FlyoutBase flyout)
        {
            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.From = 1.0;
            animation1.To = 0.0;
            animation1.Duration = this.GetDuration(flyout);
            ExponentialEase ease1 = new ExponentialEase();
            ease1.EasingMode = EasingMode.EaseOut;
            ease1.Exponent = 4.0;
            animation1.EasingFunction = ease1;
            Timeline element = animation1;
            Timeline[] timelines = new Timeline[] { element };
            Storyboard storyboard = this.CreateStoryboard(timelines);
            Storyboard.SetTarget(element, flyout.RenderGrid);
            Storyboard.SetTargetProperty(element, new PropertyPath(UIElement.OpacityProperty));
            return storyboard;
        }

        public virtual Duration GetDuration(FlyoutBase flyout) => 
            flyout.AnimationDuration;

        public override Storyboard GetMoveAnimation(FlyoutBase flyout, Point from, Point to)
        {
            if (this.GetMoveTransform(flyout) == null)
            {
                return null;
            }
            Timeline element = this.GetMoveAnimation(from.X, to.X, this.GetDuration(flyout));
            Timeline timeline2 = this.GetMoveAnimation(from.Y, to.Y, this.GetDuration(flyout));
            Timeline[] timelines = new Timeline[] { element, timeline2 };
            Storyboard storyboard = this.CreateStoryboard(timelines);
            Storyboard.SetTargetProperty(element, new PropertyPath(TranslateTransform.XProperty));
            Storyboard.SetTargetProperty(timeline2, new PropertyPath(TranslateTransform.YProperty));
            Storyboard.SetTargetName(element, "FlyoutTranslateName");
            Storyboard.SetTargetName(timeline2, "FlyoutTranslateName");
            return storyboard;
        }

        private Timeline GetMoveAnimation(double from, double to, Duration duration)
        {
            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.From = new double?(from);
            animation1.To = new double?(to);
            animation1.Duration = duration;
            ExponentialEase ease1 = new ExponentialEase();
            ease1.EasingMode = EasingMode.EaseOut;
            ease1.Exponent = 4.0;
            animation1.EasingFunction = ease1;
            return animation1;
        }

        private TranslateTransform GetMoveTransform(FlyoutBase flyout)
        {
            if (flyout.RenderGrid == null)
            {
                return null;
            }
            TranslateTransform renderTransform = flyout.RenderGrid.RenderTransform as TranslateTransform;
            if (renderTransform == null)
            {
                renderTransform = new TranslateTransform();
                INameScope nameScope = NameScope.GetNameScope(flyout);
                if (nameScope == null)
                {
                    nameScope = new NameScope();
                    NameScope.SetNameScope(flyout, nameScope);
                }
                if (nameScope.FindName("FlyoutTranslateName") != null)
                {
                    nameScope.UnregisterName("FlyoutTranslateName");
                }
                flyout.RegisterName("FlyoutTranslateName", renderTransform);
                flyout.RenderGrid.RenderTransform = renderTransform;
            }
            return renderTransform;
        }

        public override Storyboard GetOpenAnimation(FlyoutBase flyout, Point offset)
        {
            if (this.GetMoveTransform(flyout) == null)
            {
                return null;
            }
            Storyboard storyboard = this.GetMoveAnimation(flyout, offset, new Point(0.0, 0.0));
            if (storyboard == null)
            {
                return null;
            }
            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.From = 0.0;
            animation1.To = 1.0;
            animation1.Duration = this.GetDuration(flyout);
            ExponentialEase ease1 = new ExponentialEase();
            ease1.EasingMode = EasingMode.EaseOut;
            ease1.Exponent = 4.0;
            animation1.EasingFunction = ease1;
            Timeline timeline = animation1;
            storyboard.Children.Add(timeline);
            Storyboard.SetTarget(timeline, flyout.RenderGrid);
            Storyboard.SetTargetProperty(timeline, new PropertyPath(UIElement.OpacityProperty));
            return storyboard;
        }
    }
}

