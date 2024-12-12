namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Threading;

    [TargetType(typeof(Control))]
    public class FocusBehavior : EventTriggerBase<Control>
    {
        public static readonly TimeSpan DefaultFocusDelay = TimeSpan.FromMilliseconds(0.0);
        public static readonly DependencyProperty FocusDelayProperty = DependencyProperty.Register("FocusDelay", typeof(TimeSpan?), typeof(FocusBehavior), new PropertyMetadata(null));
        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register("PropertyName", typeof(string), typeof(FocusBehavior), new PropertyMetadata(string.Empty));
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty PropertyValueProperty;
        private bool lockPropertyValueChanged;

        static FocusBehavior()
        {
            PropertyValueProperty = DependencyProperty.Register("PropertyValue", typeof(object), typeof(FocusBehavior), new PropertyMetadata(null, (d, e) => ((FocusBehavior) d).OnPropertyValueChanged()));
        }

        private void AssociatedObjectFocus()
        {
            if (base.AssociatedObject.Focusable && base.AssociatedObject.IsTabStop)
            {
                base.AssociatedObject.Focus();
            }
            else
            {
                Func<Control, bool> predicate = <>c.<>9__15_0;
                if (<>c.<>9__15_0 == null)
                {
                    Func<Control, bool> local1 = <>c.<>9__15_0;
                    predicate = <>c.<>9__15_0 = x => x.Focusable && x.IsTabStop;
                }
                Action<Control> action = <>c.<>9__15_1;
                if (<>c.<>9__15_1 == null)
                {
                    Action<Control> local2 = <>c.<>9__15_1;
                    action = <>c.<>9__15_1 = x => x.Focus();
                }
                LayoutTreeHelper.GetVisualChildren(base.AssociatedObject).OfType<Control>().Where<Control>(predicate).FirstOrDefault<Control>().Do<Control>(action);
            }
        }

        private void DoFocus()
        {
            if (base.IsAttached)
            {
                TimeSpan focusDelay = this.GetFocusDelay();
                if (focusDelay == TimeSpan.FromMilliseconds(0.0))
                {
                    this.AssociatedObjectFocus();
                }
                else
                {
                    DispatcherTimer timer1 = new DispatcherTimer();
                    timer1.Interval = focusDelay;
                    DispatcherTimer timer = timer1;
                    timer.Tick += new EventHandler(this.OnTimerTick);
                    timer.Start();
                }
            }
        }

        internal TimeSpan GetFocusDelay()
        {
            TimeSpan? focusDelay;
            if (base.EventName == "Loaded")
            {
                focusDelay = this.FocusDelay;
                return ((focusDelay != null) ? focusDelay.GetValueOrDefault() : DefaultFocusDelay);
            }
            focusDelay = this.FocusDelay;
            return ((focusDelay != null) ? focusDelay.GetValueOrDefault() : TimeSpan.FromMilliseconds(0.0));
        }

        protected override void OnEvent(object sender, object eventArgs)
        {
            base.OnEvent(sender, eventArgs);
            if (string.IsNullOrEmpty(this.PropertyName))
            {
                this.DoFocus();
            }
        }

        private void OnPropertyValueChanged()
        {
            if (!this.lockPropertyValueChanged)
            {
                this.DoFocus();
            }
        }

        protected override void OnSourceChanged(object oldSource, object newSource)
        {
            base.OnSourceChanged(oldSource, newSource);
            this.lockPropertyValueChanged = true;
            base.ClearValue(PropertyValueProperty);
            this.lockPropertyValueChanged = false;
            if (!string.IsNullOrEmpty(this.PropertyName) && (newSource != null))
            {
                this.lockPropertyValueChanged = true;
                Binding binding = new Binding {
                    Path = new PropertyPath(this.PropertyName, new object[0]),
                    Source = newSource,
                    Mode = BindingMode.OneWay
                };
                BindingOperations.SetBinding(this, PropertyValueProperty, binding);
                this.lockPropertyValueChanged = false;
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer) sender;
            timer.Tick -= new EventHandler(this.OnTimerTick);
            timer.Stop();
            this.AssociatedObjectFocus();
        }

        public TimeSpan? FocusDelay
        {
            get => 
                (TimeSpan?) base.GetValue(FocusDelayProperty);
            set => 
                base.SetValue(FocusDelayProperty, value);
        }

        public string PropertyName
        {
            get => 
                (string) base.GetValue(PropertyNameProperty);
            set => 
                base.SetValue(PropertyNameProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FocusBehavior.<>c <>9 = new FocusBehavior.<>c();
            public static Func<Control, bool> <>9__15_0;
            public static Action<Control> <>9__15_1;

            internal void <.cctor>b__19_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FocusBehavior) d).OnPropertyValueChanged();
            }

            internal bool <AssociatedObjectFocus>b__15_0(Control x) => 
                x.Focusable && x.IsTabStop;

            internal void <AssociatedObjectFocus>b__15_1(Control x)
            {
                x.Focus();
            }
        }
    }
}

