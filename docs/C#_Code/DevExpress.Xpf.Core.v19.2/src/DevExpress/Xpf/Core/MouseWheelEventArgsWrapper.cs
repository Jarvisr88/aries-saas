namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Input;

    internal class MouseWheelEventArgsWrapper : MouseWheelEventArgsEx
    {
        public readonly MouseWheelEventArgs Args;

        public MouseWheelEventArgsWrapper(MouseWheelEventArgs args, int deltaX) : base(args.MouseDevice, args.Timestamp, deltaX, 0)
        {
            this.Args = args;
            base.RoutedEvent = args.RoutedEvent;
        }

        public bool Handled
        {
            get => 
                this.Args.Handled;
            set => 
                this.Args.Handled = value;
        }
    }
}

