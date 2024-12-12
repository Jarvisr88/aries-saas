namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.RangeControl;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;

    public class RangeControlAnimator
    {
        private int frames = 50;
        private DispatcherTimer timer;

        public event EventHandler<AnimationEventArgs> AnimationCompleted;

        public RangeControlAnimator()
        {
            this.AnimationDuration = this.GetDefaultDuration();
        }

        public void AnimateDoubleTapZoom(double fromStart, double fromEnd, double toStart, double toEnd, Action<double, double> predicate)
        {
            this.StopAnimation();
            this.CanProcessAnimation = true;
            int time = 400;
            int ticks = (int) (((double) time) / this.Delay);
            double startIncr = (toStart - fromStart) / ((double) ticks);
            double endIncr = (toEnd - fromEnd) / ((double) ticks);
            double start = fromStart;
            double end = fromEnd;
            this.SetDuration(time);
            this.CreateTimer();
            this.timer.Tick += delegate (object s, EventArgs e) {
                int num = ticks;
                ticks = num - 1;
                start += startIncr;
                end += endIncr;
                predicate(start, end);
                if (ticks == 0)
                {
                    this.StopTimer();
                    this.AnimationDuration = this.GetDefaultDuration();
                    predicate(toStart, toEnd);
                    this.RaiseAnimationCompleted(new AnimationEventArgs(AnimationTypes.Zoom));
                }
            };
            this.IsProcessAnimation = true;
            this.timer.Start();
        }

        internal void AnimateLabel(ContentPresenter label, double labelLeft)
        {
            this.CanProcessAnimation = true;
            Storyboard storyboard = new Storyboard {
                FillBehavior = FillBehavior.Stop,
                Children = { this.CreateAnimation(label, labelLeft) }
            };
            storyboard.Completed += delegate (object s, EventArgs e) {
                this.IsProcessAnimation = true;
                AnimationEventArgs args = new AnimationEventArgs(AnimationTypes.Label);
                args.Target = label;
                this.RaiseAnimationCompleted(args);
            };
            this.IsProcessAnimation = true;
            storyboard.Begin();
        }

        public void AnimateScroll(double from, double to, Action<double> action)
        {
            if (this.IsProcessAnimation)
            {
                this.StopAnimation();
            }
            double num = 250.0;
            double step = (to - from) / (num / this.Delay);
            double offset = from;
            int ticks = (int) (num / this.Delay);
            this.CreateTimer();
            this.timer.Tick += delegate (object s, EventArgs e) {
                int num = ticks;
                ticks = num - 1;
                offset += step;
                action(offset);
                if (ticks == 0)
                {
                    this.StopTimer();
                    this.AnimationDuration = this.GetDefaultDuration();
                    action(to);
                    this.RaiseAnimationCompleted(new AnimationEventArgs(AnimationTypes.Scroll));
                }
            };
            this.IsProcessAnimation = true;
            this.timer.Start();
        }

        public void AnimateSelection(FrameworkElement thumb, FrameworkElement border, double borderPosition, double thumbPosition, DevExpress.Xpf.Editors.RangeControl.RangeControl control)
        {
            this.CanProcessAnimation = true;
            Storyboard storyboard = new Storyboard {
                FillBehavior = FillBehavior.Stop,
                Children = { 
                    this.CreateAnimation(border, borderPosition),
                    this.CreateAnimation(thumb, thumbPosition)
                }
            };
            storyboard.Completed += delegate (object s, EventArgs e) {
                thumb.SetValue(Canvas.LeftProperty, thumbPosition);
                border.SetValue(Canvas.LeftProperty, borderPosition);
                this.RaiseAnimationCompleted(new AnimationEventArgs(AnimationTypes.Selection));
            };
            this.IsProcessAnimation = true;
            storyboard.Begin();
        }

        public void AnimateShader(GrayScaleEffect effect, double left)
        {
            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.Duration = new Duration(TimeSpan.FromSeconds(2.0));
            animation1.To = new double?(left);
            DoubleAnimation element = animation1;
            Storyboard.SetTarget(element, effect);
            Storyboard.SetTargetProperty(element, new PropertyPath(GrayScaleEffect.LeftProperty));
            Storyboard storyboard = new Storyboard {
                Children = { element }
            };
            storyboard.Completed += (s, e) => (effect.Left = left);
            storyboard.Begin();
        }

        private DoubleAnimation CreateAnimation(FrameworkElement element, double position)
        {
            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.Duration = this.AnimationDuration;
            animation1.To = new double?(position.Round(false));
            DoubleAnimation animation = animation1;
            Storyboard.SetTarget(animation, element);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Left)", new object[0]));
            return animation;
        }

        private void CreateTimer()
        {
            DispatcherTimer timer1 = new DispatcherTimer(DispatcherPriority.Render);
            timer1.Interval = TimeSpan.FromMilliseconds(this.Delay);
            this.timer = timer1;
        }

        private Duration GetDefaultDuration() => 
            new Duration(TimeSpan.FromMilliseconds(100.0));

        private void RaiseAnimationCompleted(AnimationEventArgs args)
        {
            if (this.IsProcessAnimation)
            {
                this.AnimationDuration = this.GetDefaultDuration();
                this.AnimationCompleted(this, args);
                this.IsProcessAnimation = false;
            }
        }

        public void ResetDefault()
        {
            this.AnimationDuration = this.GetDefaultDuration();
        }

        private Duration SetDuration(int time)
        {
            Duration duration = new Duration(TimeSpan.FromMilliseconds((double) time));
            this.AnimationDuration = duration;
            return duration;
        }

        public void StopAnimation()
        {
            if (this.IsProcessAnimation)
            {
                this.CanProcessAnimation = false;
                this.StopTimer();
                this.RaiseAnimationCompleted(new AnimationEventArgs(AnimationTypes.Stopped));
            }
        }

        private void StopTimer()
        {
            if (this.timer != null)
            {
                this.timer.Stop();
            }
        }

        public Duration AnimationDuration { get; set; }

        public bool CanAnimate { get; set; }

        public bool IsProcessAnimation { get; set; }

        private bool CanProcessAnimation { get; set; }

        private double Delay =>
            (double) (0x3e8 / this.frames);
    }
}

