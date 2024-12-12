namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Windows;
    using System.Windows.Input;

    internal class KeyboardEventSubscriber : ViewEventSubscriber<IInputElement>
    {
        public KeyboardEventSubscriber(IInputElement rootUIElement, BaseView view) : base(rootUIElement, view)
        {
        }

        private void RootUIElementKeyDown(object sender, KeyEventArgs e)
        {
            if (base.View != null)
            {
                bool flag = base.View.Adapter.DragService.OperationType == DevExpress.Xpf.Layout.Core.OperationType.Regular;
                if (e.IsDown)
                {
                    base.View.OnKeyDown(e.Key);
                }
                if (e.IsUp)
                {
                    base.View.OnKeyUp(e.Key);
                }
                if (!flag && (e.Key == Key.Tab))
                {
                    e.Handled = true;
                }
            }
        }

        protected override void Subscribe(IInputElement element)
        {
            element.PreviewKeyDown += new KeyEventHandler(this.RootUIElementKeyDown);
            element.PreviewKeyUp += new KeyEventHandler(this.RootUIElementKeyDown);
        }

        protected override void UnSubscribe(IInputElement element)
        {
            element.PreviewKeyDown -= new KeyEventHandler(this.RootUIElementKeyDown);
            element.PreviewKeyUp -= new KeyEventHandler(this.RootUIElementKeyDown);
        }
    }
}

