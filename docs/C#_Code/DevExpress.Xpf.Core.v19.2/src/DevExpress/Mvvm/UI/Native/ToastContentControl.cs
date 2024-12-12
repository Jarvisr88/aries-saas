namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Animation;

    public class ToastContentControl : ContentControl
    {
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty TimerPausedProperty;
        private VisualState vsActivated;
        private VisualState vsDismissed;
        private VisualState vsAppeared;
        private VisualState vsTimedOut;
        private bool isPressed;
        private bool isMouseInBounds;
        private bool stopStateChanges;
        private bool isTimedOut;

        static ToastContentControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ToastContentControl), new FrameworkPropertyMetadata(typeof(ToastContentControl)));
            CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ToastContentControl), new PropertyMetadata(null));
            TimerPausedProperty = DependencyProperty.Register("TimerPaused", typeof(bool), typeof(ToastContentControl), new PropertyMetadata(false, (s, e) => ((ToastContentControl) s).TimerPausedChanged()));
        }

        public ToastContentControl()
        {
            base.MouseDown += delegate (object s, MouseButtonEventArgs e) {
                if (e.ChangedButton == MouseButton.Left)
                {
                    base.CaptureMouse();
                    this.isPressed = true;
                    this.ChangeVisualState(true);
                }
            };
            base.MouseUp += delegate (object s, MouseButtonEventArgs e) {
                if (e.ChangedButton == MouseButton.Left)
                {
                    this.isPressed = false;
                    base.ReleaseMouseCapture();
                    this.ChangeVisualState(true);
                    if (this.isMouseInBounds && (base.IsMouseOver && ((this.Command != null) && this.Command.CanExecute(this))))
                    {
                        this.Command.Execute(this);
                    }
                }
            };
            base.MouseMove += delegate (object s, MouseEventArgs e) {
                this.ChangeVisualState(true);
                this.isMouseInBounds = this.GetIsMouseInBounds(e);
            };
            base.MouseLeave += (s, e) => this.ChangeVisualState(true);
            this.DismissCommand = new DelegateCommand(new Action(this.OnDismiss));
            this.ActivateCommand = new DelegateCommand(new Action(this.OnActivate));
            this.TimeOutCommand = new DelegateCommand(new Action(this.OnTimeOut));
        }

        private void ChangeVisualState(bool useTransitions)
        {
            if (!this.stopStateChanges)
            {
                if (!base.IsEnabled)
                {
                    VisualStateManager.GoToState(this, "Disabled", useTransitions);
                }
                else if (this.isPressed && this.isMouseInBounds)
                {
                    VisualStateManager.GoToState(this, "Pressed", useTransitions);
                }
                else if (this.isMouseInBounds && base.IsMouseOver)
                {
                    VisualStateManager.GoToState(this, "MouseOver", useTransitions);
                }
                else
                {
                    VisualStateManager.GoToState(this, "Normal", useTransitions);
                }
            }
        }

        private bool GetIsMouseInBounds(MouseEventArgs e)
        {
            Rect rect = new Rect(0.0, 0.0, base.ActualWidth, base.ActualHeight);
            return rect.Contains(e.GetPosition(this));
        }

        private void OnActivate()
        {
            this.OnVisualStateChanged(delegate {
                if (!this.isTimedOut)
                {
                    this.Toast.Activate();
                }
            }, this.vsActivated);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.vsActivated = (VisualState) base.GetTemplateChild("Activated");
            this.vsDismissed = (VisualState) base.GetTemplateChild("Dismissed");
            this.vsAppeared = (VisualState) base.GetTemplateChild("Appeared");
            this.vsTimedOut = (VisualState) base.GetTemplateChild("TimedOut");
            Duration timedOutDuration = new Duration(new TimeSpan(0, 0, 0, 1, 0));
            Duration appearedDuration = new Duration(new TimeSpan(0, 0, 0, 0, 100));
            (base.GetTemplateChild("PART_TimedOutAnimation") as DoubleAnimation).Do<DoubleAnimation>(x => x.Duration = timedOutDuration);
            (base.GetTemplateChild("PART_AppearedAnimation1") as DoubleAnimation).Do<DoubleAnimation>(x => x.Duration = appearedDuration);
            (base.GetTemplateChild("PART_AppearedAnimation2") as DoubleAnimation).Do<DoubleAnimation>(x => x.Duration = appearedDuration);
            if (this.vsAppeared != null)
            {
                this.vsAppeared.Storyboard.Completed += (s, e) => this.ChangeVisualState(true);
                VisualStateManager.GoToState(this, this.vsAppeared.Name, true);
            }
        }

        private void OnDismiss()
        {
            this.OnVisualStateChanged(delegate {
                if (!this.isTimedOut)
                {
                    this.Toast.Dismiss();
                }
            }, this.vsDismissed);
        }

        private void OnTimeOut()
        {
            this.isTimedOut = true;
            this.OnVisualStateChanged(() => this.Toast.TimeOut(), this.vsTimedOut);
        }

        private void OnVisualStateChanged(Action doAfterChange, VisualState state)
        {
            if (state == null)
            {
                doAfterChange();
            }
            else
            {
                this.stopStateChanges = true;
                state.Storyboard.Completed += (s, e) => doAfterChange();
                VisualStateManager.GoToState(this, state.Name, true);
            }
        }

        private void TimerPausedChanged()
        {
            if (this.TimerPaused)
            {
                this.Toast.StopTimer();
            }
            else
            {
                this.Toast.ResetTimer();
            }
        }

        public ICommand Command
        {
            get => 
                (ICommand) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        public bool TimerPaused
        {
            get => 
                (bool) base.GetValue(TimerPausedProperty);
            set => 
                base.SetValue(TimerPausedProperty, value);
        }

        public ICommand DismissCommand { get; private set; }

        public ICommand ActivateCommand { get; private set; }

        public ICommand TimeOutCommand { get; private set; }

        internal CustomNotification Toast { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ToastContentControl.<>c <>9 = new ToastContentControl.<>c();

            internal void <.cctor>b__2_0(DependencyObject s, DependencyPropertyChangedEventArgs e)
            {
                ((ToastContentControl) s).TimerPausedChanged();
            }
        }
    }
}

