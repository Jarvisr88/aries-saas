namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class MouseRenderEventArgs : RenderEventArgs
    {
        public MouseRenderEventArgs(IFrameworkRenderElementContext source, EventArgs originalEventArgs, RenderEvents renderEvent, Point position, MouseButtonState leftButtonState, MouseButtonState rightButtonState);
        protected internal override void InvokeEventHandler(IFrameworkRenderElementContext target);

        public Point Position { get; private set; }

        public MouseButtonState LeftButtonState { get; private set; }

        public MouseButtonState RightButtonState { get; private set; }
    }
}

