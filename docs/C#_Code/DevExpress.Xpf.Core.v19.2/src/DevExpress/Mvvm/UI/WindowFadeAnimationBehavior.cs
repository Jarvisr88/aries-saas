namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media.Animation;

    [TargetType(typeof(System.Windows.Window)), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class WindowFadeAnimationBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty EnableAnimationProperty = DependencyProperty.RegisterAttached("EnableAnimation", typeof(bool), typeof(WindowFadeAnimationBehavior), new PropertyMetadata(false, new PropertyChangedCallback(WindowFadeAnimationBehavior.OnEnableAnimationChanged)));
        public static readonly DependencyProperty WindowProperty;
        public static readonly DependencyProperty FadeInDurationProperty;
        public static readonly DependencyProperty FadeOutDurationProperty;
        private Storyboard fadeInAnimaiton;

        static WindowFadeAnimationBehavior()
        {
            WindowProperty = DependencyProperty.Register("Window", typeof(System.Windows.Window), typeof(WindowFadeAnimationBehavior), new PropertyMetadata(null, (d, e) => ((WindowFadeAnimationBehavior) d).OnWindowChanged((System.Windows.Window) e.OldValue)));
            FadeInDurationProperty = DependencyProperty.Register("FadeInDuration", typeof(TimeSpan), typeof(WindowFadeAnimationBehavior), new PropertyMetadata(TimeSpan.FromSeconds(0.2)));
            FadeOutDurationProperty = DependencyProperty.Register("FadeOutDuration", typeof(TimeSpan), typeof(WindowFadeAnimationBehavior), new PropertyMetadata(TimeSpan.FromSeconds(0.2)));
        }

        private Storyboard CreateStoryboard(System.Windows.Window w, double from, double to, TimeSpan duration)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.From = new double?(from);
            animation1.To = new double?(to);
            animation1.Duration = duration;
            DoubleAnimation element = animation1;
            Storyboard.SetTarget(element, w);
            Storyboard.SetTargetProperty(element, new PropertyPath(UIElement.OpacityProperty));
            element.Freeze();
            storyboard.Children.Add(element);
            return storyboard;
        }

        public static bool GetEnableAnimation(System.Windows.Window obj) => 
            (bool) obj.GetValue(EnableAnimationProperty);

        private void Initialize()
        {
            this.Uninitialize(this.ActualWindow);
            if (this.ActualWindow != null)
            {
                this.ActualWindow.Closing += new CancelEventHandler(this.OnWindowClosing);
                this.ActualWindow.Loaded += new RoutedEventHandler(this.OnWindowLoaded);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.Initialize();
        }

        protected override void OnDetaching()
        {
            this.Uninitialize(this.ActualWindow);
            base.OnDetaching();
        }

        private static void OnEnableAnimationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BehaviorCollection source = Interaction.GetBehaviors(d as System.Windows.Window);
            Func<Behavior, bool> predicate = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<Behavior, bool> local1 = <>c.<>9__3_0;
                predicate = <>c.<>9__3_0 = x => x is WindowFadeAnimationBehavior;
            }
            source.Remove((WindowFadeAnimationBehavior) source.FirstOrDefault<Behavior>(predicate));
            if ((bool) e.NewValue)
            {
                source.Add(new WindowFadeAnimationBehavior());
            }
        }

        private void OnWindowChanged(System.Windows.Window oldValue)
        {
            this.Uninitialize(base.AssociatedObject as System.Windows.Window);
            this.Uninitialize(oldValue);
            this.Initialize();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                if (this.fadeInAnimaiton != null)
                {
                    this.fadeInAnimaiton.Stop();
                    this.fadeInAnimaiton = null;
                }
                System.Windows.Window w = (System.Windows.Window) sender;
                w.Closing -= new CancelEventHandler(this.OnWindowClosing);
                if (this.FadeOutDuration.TotalMilliseconds != 0.0)
                {
                    Storyboard storyboard = this.CreateStoryboard(w, 1.0, 0.0, this.FadeOutDuration);
                    storyboard.Completed += (d, ee) => w.Close();
                    e.Cancel = true;
                    storyboard.Begin();
                }
            }
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Window w = (System.Windows.Window) sender;
            w.Loaded -= new RoutedEventHandler(this.OnWindowLoaded);
            if (this.FadeInDuration.TotalMilliseconds != 0.0)
            {
                this.fadeInAnimaiton = this.CreateStoryboard(w, 0.0, 1.0, this.FadeInDuration);
                this.fadeInAnimaiton.Completed += (d, ee) => (this.fadeInAnimaiton = null);
                this.fadeInAnimaiton.Begin();
            }
        }

        public static void SetEnableAnimation(System.Windows.Window obj, bool value)
        {
            obj.SetValue(EnableAnimationProperty, value);
        }

        private void Uninitialize(System.Windows.Window window)
        {
            if (window != null)
            {
                window.Closing -= new CancelEventHandler(this.OnWindowClosing);
                window.Loaded -= new RoutedEventHandler(this.OnWindowLoaded);
            }
        }

        public System.Windows.Window Window
        {
            get => 
                (System.Windows.Window) base.GetValue(WindowProperty);
            set => 
                base.SetValue(WindowProperty, value);
        }

        public TimeSpan FadeInDuration
        {
            get => 
                (TimeSpan) base.GetValue(FadeInDurationProperty);
            set => 
                base.SetValue(FadeInDurationProperty, value);
        }

        public TimeSpan FadeOutDuration
        {
            get => 
                (TimeSpan) base.GetValue(FadeOutDurationProperty);
            set => 
                base.SetValue(FadeOutDurationProperty, value);
        }

        protected System.Windows.Window ActualWindow =>
            this.Window ?? (base.AssociatedObject as System.Windows.Window);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WindowFadeAnimationBehavior.<>c <>9 = new WindowFadeAnimationBehavior.<>c();
            public static Func<Behavior, bool> <>9__3_0;

            internal void <.cctor>b__28_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((WindowFadeAnimationBehavior) d).OnWindowChanged((Window) e.OldValue);
            }

            internal bool <OnEnableAnimationChanged>b__3_0(Behavior x) => 
                x is WindowFadeAnimationBehavior;
        }
    }
}

