namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public class SpinButton : RepeatButton
    {
        public static readonly DependencyProperty StartIntervalProperty;
        protected readonly DevExpress.Xpf.Editors.Helpers.Counter Counter = new DevExpress.Xpf.Editors.Helpers.Counter();
        private int intervalDecrementValue = 50;

        static SpinButton()
        {
            Type ownerType = typeof(SpinButton);
            StartIntervalProperty = DependencyPropertyManager.Register("StartInterval", typeof(int), ownerType, new FrameworkPropertyMetadata(500));
            UIElement.FocusableProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(false));
        }

        public SpinButton()
        {
            this.Reset();
        }

        protected virtual void ChangeInterval()
        {
            base.Interval = Math.Max(10, base.Interval - this.intervalDecrementValue);
        }

        protected override void OnClick()
        {
            if (this.Counter.IsClear)
            {
                this.Reset();
            }
            base.OnClick();
            this.ChangeInterval();
            this.Counter.Increment();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.Key == Key.Space)
            {
                this.Reset();
            }
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);
            this.Reset();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            this.Reset();
        }

        private void Reset()
        {
            this.Counter.Reset();
            this.RestoreInterval();
        }

        private void RestoreInterval()
        {
            base.Interval = this.StartInterval;
        }

        public int StartInterval
        {
            get => 
                (int) base.GetValue(StartIntervalProperty);
            set => 
                base.SetValue(StartIntervalProperty, value);
        }
    }
}

