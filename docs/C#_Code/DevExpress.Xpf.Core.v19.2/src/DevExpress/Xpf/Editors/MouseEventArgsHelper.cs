namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    internal class MouseEventArgsHelper
    {
        public MouseEventArgsHelper()
        {
        }

        public MouseEventArgsHelper(MouseEventArgs args)
        {
            this.Args = args;
        }

        public Point GetMousePosition(FrameworkElement element) => 
            (this.Args != null) ? this.Args.GetPosition(element) : new Point(0.0, 0.0);

        private MouseEventArgs Args { get; set; }
    }
}

