namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Input;

    public abstract class LayoutControllerCommand : ICommand
    {
        private event EventHandler CanExecuteChangedCore;

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                this.CanExecuteChangedCore += value;
            }
            remove
            {
                this.CanExecuteChangedCore -= value;
            }
        }

        static LayoutControllerCommand()
        {
            ShowCaption = new LayoutControllerCommandLink("ShowCaption", new ShowCaptionCommand());
            ShowControl = new LayoutControllerCommandLink("ShowControl", new ShowControlCommand());
            ShowCaptionImageBeforeText = new LayoutControllerCommandLink("ShowCaptionImageBeforeText", new CaptionImageBeforeTextCommand());
            ShowCaptionImageAfterText = new LayoutControllerCommandLink("ShowCaptionImageAfterText", new CaptionImageAfterTextCommand());
            ShowCaptionOnLeft = new LayoutControllerCommandLink("ShowCaptionOnLeft", new CaptionLocationLeftCommand());
            ShowCaptionOnRight = new LayoutControllerCommandLink("ShowCaptionOnRight", new CaptionLocationRightCommand());
            ShowCaptionAtTop = new LayoutControllerCommandLink("ShowCaptionAtTop", new CaptionLocationTopCommand());
            ShowCaptionAtBottom = new LayoutControllerCommandLink("ShowCaptionAtBottom", new CaptionLocationBottomCommand());
        }

        internal LayoutControllerCommand()
        {
        }

        internal static void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ILayoutController layoutController = LayoutControllerHelper.GetLayoutController(sender);
            BaseLayoutItem[] parameter = e.Parameter as BaseLayoutItem[];
            if (e.Parameter is BaseLayoutItem)
            {
                parameter = new BaseLayoutItem[] { (BaseLayoutItem) e.Parameter };
            }
            LayoutControllerCommand command = ((LayoutControllerCommandLink) e.Command).Command;
            e.CanExecute = ((layoutController != null) && (parameter != null)) && command.CanExecuteCore(layoutController, parameter);
        }

        protected abstract bool CanExecuteCore(ILayoutController controller, BaseLayoutItem[] items);
        protected abstract void ExecuteCore(ILayoutController controller, BaseLayoutItem[] items);
        internal static void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ILayoutController layoutController = LayoutControllerHelper.GetLayoutController(sender);
            BaseLayoutItem[] parameter = e.Parameter as BaseLayoutItem[];
            if (e.Parameter is BaseLayoutItem)
            {
                parameter = new BaseLayoutItem[] { (BaseLayoutItem) e.Parameter };
            }
            if ((layoutController != null) && (parameter != null))
            {
                ((LayoutControllerCommandLink) e.Command).Command.ExecuteCore(layoutController, parameter);
            }
        }

        protected void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChangedCore != null)
            {
                this.CanExecuteChangedCore(this, EventArgs.Empty);
            }
        }

        bool ICommand.CanExecute(object parameter) => 
            (this.Items != null) ? this.CanExecuteCore(this.Controller, this.Items) : false;

        void ICommand.Execute(object parameter)
        {
            this.ExecuteCore(this.Controller, this.Items);
        }

        protected internal ILayoutController Controller { get; set; }

        protected internal BaseLayoutItem[] Items { get; set; }

        public static RoutedCommand ShowCaption { get; private set; }

        public static RoutedCommand ShowControl { get; private set; }

        public static RoutedCommand ShowCaptionImageBeforeText { get; private set; }

        public static RoutedCommand ShowCaptionImageAfterText { get; private set; }

        public static RoutedCommand ShowCaptionOnLeft { get; private set; }

        public static RoutedCommand ShowCaptionOnRight { get; private set; }

        public static RoutedCommand ShowCaptionAtTop { get; private set; }

        public static RoutedCommand ShowCaptionAtBottom { get; private set; }

        private class LayoutControllerCommandLink : RoutedCommand
        {
            public LayoutControllerCommandLink(string name, LayoutControllerCommand command) : base(name, typeof(LayoutControllerCommand))
            {
                this.Command = command;
            }

            public LayoutControllerCommand Command { get; private set; }
        }
    }
}

