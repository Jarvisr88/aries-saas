namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public class ScrollData : FrameworkElement
    {
        private const double AnimationSpeed = 0.8;
        private readonly Slider slider;
        private Storyboard animation;
        private DoubleAnimation verticalOffsetAnimation;

        public ScrollData()
        {
            Slider slider1 = new Slider();
            slider1.SmallChange = 1E-10;
            slider1.Minimum = double.MinValue;
            slider1.Maximum = double.MaxValue;
            this.slider = slider1;
            this.slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.OnVerticalOffsetChanged);
        }

        public void AnimateScrollToVerticalOffset(double offset, Action preCompleted, Action onCompleted, Func<double, double> ensureStep, ScrollDataAnimationEase animationEase)
        {
            if (this.ScrollOwner != null)
            {
                this.StopCurrentAnimatedScroll();
                this.slider.Value = this.ScrollOwner.VerticalOffset;
                this.EnsureStep = ensureStep;
                this.PreCompleted = preCompleted;
                this.OnCompleted = onCompleted;
                this.To = offset;
                this.animation = new Storyboard();
                double num = Math.Abs((double) (offset - this.ScrollOwner.VerticalOffset));
                if (num.IsZero())
                {
                    Action<Action> action = <>c.<>9__30_0;
                    if (<>c.<>9__30_0 == null)
                    {
                        Action<Action> local1 = <>c.<>9__30_0;
                        action = <>c.<>9__30_0 = x => x();
                    }
                    this.PreCompleted.Do<Action>(action);
                    this.ScrollOwner.ScrollToVerticalOffset(offset);
                }
                else
                {
                    DoubleAnimation animation1 = new DoubleAnimation();
                    animation1.From = new double?(this.ScrollOwner.VerticalOffset);
                    animation1.To = new double?(offset);
                    animation1.Duration = TimeSpan.FromMilliseconds(num / 0.8);
                    this.verticalOffsetAnimation = animation1;
                    if (animationEase != ScrollDataAnimationEase.BeginAnimation)
                    {
                        if (animationEase != ScrollDataAnimationEase.Linear)
                        {
                        }
                    }
                    else
                    {
                        PowerEase ease1 = new PowerEase();
                        ease1.EasingMode = EasingMode.EaseOut;
                        ease1.Power = 2.0;
                        this.verticalOffsetAnimation.EasingFunction = ease1;
                    }
                    this.animation.Children.Add(this.verticalOffsetAnimation);
                    Storyboard.SetTarget(this.animation, this.slider);
                    Storyboard.SetTargetProperty(this.verticalOffsetAnimation, new PropertyPath("Value", new object[0]));
                    this.StopAnimation = false;
                    this.animation.Begin();
                }
            }
        }

        private void OnVerticalOffsetChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!this.StopAnimation)
            {
                Action<DXScrollViewer> action = <>c.<>9__29_0;
                if (<>c.<>9__29_0 == null)
                {
                    Action<DXScrollViewer> local1 = <>c.<>9__29_0;
                    action = <>c.<>9__29_0 = x => x.IsIntermediate = true;
                }
                this.ScrollOwner.Do<DXScrollViewer>(action);
                if (e.NewValue.AreClose(this.To))
                {
                    Action<Action> action2 = <>c.<>9__29_1;
                    if (<>c.<>9__29_1 == null)
                    {
                        Action<Action> local2 = <>c.<>9__29_1;
                        action2 = <>c.<>9__29_1 = x => x();
                    }
                    this.PreCompleted.Do<Action>(action2);
                }
                this.ScrollOwner.Do<DXScrollViewer>(x => x.ScrollToVerticalOffset((this.EnsureStep != null) ? this.EnsureStep(e.NewValue) : e.NewValue));
                if (e.NewValue.AreClose(this.To))
                {
                    Action<Action> action3 = <>c.<>9__29_3;
                    if (<>c.<>9__29_3 == null)
                    {
                        Action<Action> local3 = <>c.<>9__29_3;
                        action3 = <>c.<>9__29_3 = x => x();
                    }
                    this.OnCompleted.Do<Action>(action3);
                }
            }
        }

        public void StopCurrentAnimatedScroll()
        {
            if (this.animation != null)
            {
                this.StopAnimation = true;
                this.animation.Stop();
            }
        }

        private bool StopAnimation { get; set; }

        private double To { get; set; }

        private Action PreCompleted { get; set; }

        private Action OnCompleted { get; set; }

        private Func<double, double> EnsureStep { get; set; }

        public DXScrollViewer ScrollOwner { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.Editors.ScrollData.<>c <>9 = new DevExpress.Xpf.Editors.ScrollData.<>c();
            public static Action<DXScrollViewer> <>9__29_0;
            public static Action<Action> <>9__29_1;
            public static Action<Action> <>9__29_3;
            public static Action<Action> <>9__30_0;

            internal void <AnimateScrollToVerticalOffset>b__30_0(Action x)
            {
                x();
            }

            internal void <OnVerticalOffsetChanged>b__29_0(DXScrollViewer x)
            {
                x.IsIntermediate = true;
            }

            internal void <OnVerticalOffsetChanged>b__29_1(Action x)
            {
                x();
            }

            internal void <OnVerticalOffsetChanged>b__29_3(Action x)
            {
                x();
            }
        }
    }
}

