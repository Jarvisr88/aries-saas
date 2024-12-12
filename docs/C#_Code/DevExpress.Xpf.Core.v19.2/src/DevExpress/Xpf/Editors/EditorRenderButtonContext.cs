namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Windows.Input;

    public class EditorRenderButtonContext : RenderButtonContext
    {
        private int startInterval;
        private readonly Counter counter;
        private const int IntervalDecrementValue = 50;

        public EditorRenderButtonContext(EditorRenderButton factory) : base(factory)
        {
            this.startInterval = 500;
            this.counter = new Counter();
            this.Reset();
        }

        private void ChangeInterval()
        {
            base.Interval = Math.Max(10, base.Interval - 50);
        }

        protected override void OnClick(RenderEventArgs args)
        {
            if (base.ActualButtonKind != ButtonKind.Repeat)
            {
                base.OnClick(args);
            }
            else
            {
                if (this.counter.IsClear)
                {
                    this.Reset();
                }
                base.OnClick(args);
                this.ChangeInterval();
                this.counter.Increment();
            }
        }

        protected override void OnLostMouseCapture()
        {
            base.OnLostMouseCapture();
            if (base.ActualButtonKind == ButtonKind.Repeat)
            {
                this.Reset();
            }
        }

        protected override void OnMouseUp(MouseRenderEventArgs args)
        {
            base.OnMouseUp(args);
            if ((base.ActualButtonKind == ButtonKind.Repeat) && (args.LeftButtonState == MouseButtonState.Pressed))
            {
                this.Reset();
            }
        }

        private void Reset()
        {
            this.counter.Reset();
            base.Interval = this.StartInterval;
        }

        public int StartInterval
        {
            get => 
                this.startInterval;
            set => 
                base.SetProperty<int>(ref this.startInterval, value, FREInvalidateOptions.None);
        }
    }
}

