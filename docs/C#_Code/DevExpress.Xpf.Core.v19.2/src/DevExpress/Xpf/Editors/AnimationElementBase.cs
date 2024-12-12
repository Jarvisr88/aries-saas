namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public abstract class AnimationElementBase : Canvas
    {
        private Storyboard storyboard;
        public static readonly DependencyProperty AccelerateRatioProperty;

        static AnimationElementBase()
        {
            Type ownerType = typeof(AnimationElementBase);
            AccelerateRatioProperty = DependencyPropertyManager.Register("AccelerateRatio", typeof(double), ownerType, new PropertyMetadata((d, e) => ((AnimationElementBase) d).AccelerateRatioChanged((double) e.NewValue)));
        }

        public AnimationElementBase()
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
            this.IsVisibleChangedHelper = new EventToEventHelper<bool>();
            base.IsVisibleChanged += (d, e) => this.IsVisibleChangedHelper.RaiseTargetChanged((bool) e.NewValue);
            this.IsVisibleChangedHelper.TargetChanged += (d, e) => this.OnIsVisibleChanged(e.Value);
            FrameworkElementHelper.SetIsClipped(this, true);
        }

        private void AccelerateRatioChanged(double ratio)
        {
            this.StartAnimation();
        }

        protected abstract DoubleAnimation CreateAnimation();
        protected Duration GetAnimationDuration(double lenght)
        {
            double num = (this.AccelerateRatio <= 0.0) ? 1.0 : this.AccelerateRatio;
            return new Duration(TimeSpan.FromSeconds((lenght / 100.0) / num));
        }

        private void OnIsVisibleChanged(bool isVisible)
        {
            if (isVisible)
            {
                this.StartAnimation();
            }
            else
            {
                this.StopAnimation();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.StartAnimation();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.StartAnimation();
        }

        protected void StartAnimation()
        {
            this.StopAnimation();
            if (base.Children.Count != 0)
            {
                this.VerifyStoryboard();
                FrameworkElement content = (FrameworkElement) base.Children[0];
                this.UpdateContent(content);
                SetTop(content, 0.0);
                DoubleAnimation element = this.CreateAnimation();
                Storyboard.SetTarget(element, content);
                this.storyboard.Children.Add(element);
                this.storyboard.Begin();
            }
        }

        protected void StopAnimation()
        {
            Action<Storyboard> action = <>c.<>9__16_0;
            if (<>c.<>9__16_0 == null)
            {
                Action<Storyboard> local1 = <>c.<>9__16_0;
                action = <>c.<>9__16_0 = x => x.Stop();
            }
            this.storyboard.Do<Storyboard>(action);
            Action<Storyboard> action2 = <>c.<>9__16_1;
            if (<>c.<>9__16_1 == null)
            {
                Action<Storyboard> local2 = <>c.<>9__16_1;
                action2 = <>c.<>9__16_1 = x => x.Children.Clear();
            }
            this.storyboard.Do<Storyboard>(action2);
        }

        protected abstract void UpdateContent(FrameworkElement content);
        private void VerifyStoryboard()
        {
            this.storyboard ??= new Storyboard();
        }

        private ITargetChangedHelper<bool> IsVisibleChangedHelper { get; set; }

        public double AccelerateRatio
        {
            get => 
                (double) base.GetValue(AccelerateRatioProperty);
            set => 
                base.SetValue(AccelerateRatioProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AnimationElementBase.<>c <>9 = new AnimationElementBase.<>c();
            public static Action<Storyboard> <>9__16_0;
            public static Action<Storyboard> <>9__16_1;

            internal void <.cctor>b__6_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((AnimationElementBase) d).AccelerateRatioChanged((double) e.NewValue);
            }

            internal void <StopAnimation>b__16_0(Storyboard x)
            {
                x.Stop();
            }

            internal void <StopAnimation>b__16_1(Storyboard x)
            {
                x.Children.Clear();
            }
        }
    }
}

