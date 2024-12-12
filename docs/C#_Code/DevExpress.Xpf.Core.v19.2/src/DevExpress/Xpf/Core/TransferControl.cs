namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    public class TransferControl : System.Windows.Controls.ContentControl
    {
        public static readonly DependencyProperty CurrentContentProperty;
        public static readonly DependencyProperty PreviousContentProperty;
        public static readonly DependencyProperty ControlTemplateProperty;
        public static readonly DependencyProperty PreviousControlTemplateProperty;
        public static readonly DependencyProperty SpeedRatioProperty;
        public static readonly RoutedEvent ContentChangedEvent;
        private DispatcherTimer timer = new DispatcherTimer();
        private bool raisingResetEvent;

        public event RoutedEventHandler ContentChanged
        {
            add
            {
                base.AddHandler(ContentChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ContentChangedEvent, value);
            }
        }

        static TransferControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(TransferControl), new FrameworkPropertyMetadata(typeof(TransferControl)));
            CurrentContentProperty = DependencyProperty.Register("CurrentContent", typeof(object), typeof(TransferControl), new FrameworkPropertyMetadata(null));
            PreviousContentProperty = DependencyProperty.Register("PreviousContent", typeof(object), typeof(TransferControl), new FrameworkPropertyMetadata(null));
            ControlTemplateProperty = DependencyProperty.Register("ControlTemplate", typeof(System.Windows.Controls.ControlTemplate), typeof(TransferControl), new FrameworkPropertyMetadata(null));
            PreviousControlTemplateProperty = DependencyProperty.Register("PreviousControlTemplate", typeof(System.Windows.Controls.ControlTemplate), typeof(TransferControl), new FrameworkPropertyMetadata(null));
            SpeedRatioProperty = DependencyProperty.Register("SpeedRatio", typeof(double), typeof(TransferControl), new FrameworkPropertyMetadata(1.0));
            ContentChangedEvent = EventManager.RegisterRoutedEvent("ContentChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(TransferControl));
            DataObjectBase.NeedsResetEventProperty.OverrideMetadata(typeof(TransferControl), new PropertyMetadata(true));
        }

        public TransferControl()
        {
            this.timer.Tick += new EventHandler(this.RenewAnimation);
            this.timer.IsEnabled = false;
            base.AddHandler(DataObjectBase.ResetEvent, new RoutedEventHandler(this.OnReset));
        }

        protected virtual void EndTimer()
        {
            this.timer.IsEnabled = false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ContentControl = (FrameworkElement) base.GetTemplateChild("PART_ContentPresenter");
            this.PreviousContentControl = (FrameworkElement) base.GetTemplateChild("PART_PreviousContentPresenter");
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            this.OnContentChangedCore(oldContent, newContent);
        }

        protected virtual void OnContentChangedCore(object oldContent, object newContent)
        {
            if (this.AnimationInProgress)
            {
                this.PendingContent = newContent;
            }
            else
            {
                this.CurrentContent = newContent;
                this.PreviousContent = oldContent;
                this.TryRaiseContentChanged(newContent);
            }
        }

        protected internal virtual void OnCurrentContentChanged(TransferContentControl control)
        {
        }

        protected internal virtual void OnPrevContentChanged(TransferContentControl control)
        {
        }

        private void OnReset(object sender, RoutedEventArgs e)
        {
            if (!this.raisingResetEvent)
            {
                this.raisingResetEvent = true;
                try
                {
                    this.RenewAnimationCore();
                    this.timer.IsEnabled = false;
                    RoutedEventArgs args1 = new RoutedEventArgs();
                    args1.RoutedEvent = DataObjectBase.ResetEvent;
                    base.RaiseEvent(args1);
                }
                finally
                {
                    this.raisingResetEvent = false;
                }
            }
        }

        protected virtual void RaiseContentChanged()
        {
            if (this.ContentControl != null)
            {
                this.ContentControl.RaiseEvent(new RoutedEventArgs(ContentChangedEvent, this));
            }
            if (this.PreviousContentControl != null)
            {
                this.PreviousContentControl.RaiseEvent(new RoutedEventArgs(ContentChangedEvent, this));
            }
            this.StartTimer();
        }

        private void RenewAnimation(object sender, EventArgs e)
        {
            this.RenewAnimationCore();
        }

        protected void RenewAnimationCore()
        {
            this.EndTimer();
            if (this.PendingContent != null)
            {
                if (this.CurrentContent != this.PendingContent)
                {
                    this.OnContentChangedCore(this.CurrentContent, this.PendingContent);
                }
                this.PendingContent = null;
            }
            this.ResetPreviousContent();
        }

        protected virtual void ResetPreviousContent()
        {
            this.PreviousContent = null;
        }

        protected virtual void StartTimer()
        {
            if (this.SkipLongAnimations)
            {
                this.timer.Interval = TimeSpan.FromMilliseconds(1000.0 / this.SpeedRatio);
                this.timer.IsEnabled = true;
            }
        }

        protected virtual void TryRaiseContentChanged(object newContent)
        {
            if ((this.PreviousContent != null) && (this.CurrentContent != null))
            {
                this.RaiseContentChanged();
            }
        }

        protected bool AnimationInProgress =>
            this.timer.IsEnabled;

        public object PreviousContent
        {
            get => 
                base.GetValue(PreviousContentProperty);
            set => 
                base.SetValue(PreviousContentProperty, value);
        }

        public object CurrentContent
        {
            get => 
                base.GetValue(CurrentContentProperty);
            set => 
                base.SetValue(CurrentContentProperty, value);
        }

        public System.Windows.Controls.ControlTemplate ControlTemplate
        {
            get => 
                (System.Windows.Controls.ControlTemplate) base.GetValue(ControlTemplateProperty);
            set => 
                base.SetValue(ControlTemplateProperty, value);
        }

        public System.Windows.Controls.ControlTemplate PreviousControlTemplate
        {
            get => 
                (System.Windows.Controls.ControlTemplate) base.GetValue(PreviousControlTemplateProperty);
            set => 
                base.SetValue(PreviousControlTemplateProperty, value);
        }

        public double SpeedRatio
        {
            get => 
                (double) base.GetValue(SpeedRatioProperty);
            set => 
                base.SetValue(SpeedRatioProperty, value);
        }

        protected virtual bool SkipLongAnimations =>
            true;

        protected FrameworkElement ContentControl { get; set; }

        protected FrameworkElement PreviousContentControl { get; set; }

        protected object PendingContent { get; set; }
    }
}

