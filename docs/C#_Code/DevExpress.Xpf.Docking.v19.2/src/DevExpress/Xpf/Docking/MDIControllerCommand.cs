namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Input;

    public abstract class MDIControllerCommand : ICommand
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

        static MDIControllerCommand()
        {
            Maximize = new MDIControllerCommandLink("Maximize", new MaximizeDocumentCommand());
            Minimize = new MDIControllerCommandLink("Minimize", new MinimizeDocumentCommand());
            Restore = new MDIControllerCommandLink("Restore", new RestoreDocumentCommand());
            TileVertical = new MDIControllerCommandLink("TileVertical", new TileVerticalCommand());
            TileHorizontal = new MDIControllerCommandLink("TileHorizontal", new TileHorizontalCommand());
            Cascade = new MDIControllerCommandLink("Cascade", new CascadeCommand());
            ArrangeIcons = new MDIControllerCommandLink("ArrangeIcons", new ArrangeIconsCommand());
            ChangeMDIStyle = new MDIControllerCommandLink("ChangeMDIStyle", new ChangeMDIStyleCommand());
        }

        internal MDIControllerCommand()
        {
        }

        internal static void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            IMDIController mDIController = MDIControllerHelper.GetMDIController(sender);
            BaseLayoutItem[] parameter = e.Parameter as BaseLayoutItem[];
            if (e.Parameter is BaseLayoutItem)
            {
                parameter = new BaseLayoutItem[] { (BaseLayoutItem) e.Parameter };
            }
            MDIControllerCommand command = ((MDIControllerCommandLink) e.Command).Command;
            e.CanExecute = ((mDIController != null) && (parameter != null)) && command.CanExecuteCore(mDIController, parameter);
        }

        protected abstract bool CanExecuteCore(IMDIController controller, BaseLayoutItem[] items);
        protected abstract void ExecuteCore(IMDIController controller, BaseLayoutItem[] items);
        internal static void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IMDIController mDIController = MDIControllerHelper.GetMDIController(sender);
            BaseLayoutItem[] parameter = e.Parameter as BaseLayoutItem[];
            if (e.Parameter is BaseLayoutItem)
            {
                parameter = new BaseLayoutItem[] { (BaseLayoutItem) e.Parameter };
            }
            if ((mDIController != null) && (parameter != null))
            {
                ((MDIControllerCommandLink) e.Command).Command.ExecuteCore(mDIController, parameter);
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

        protected internal IMDIController Controller { get; set; }

        protected internal BaseLayoutItem[] Items { get; set; }

        public static RoutedCommand Maximize { get; private set; }

        public static RoutedCommand Minimize { get; private set; }

        public static RoutedCommand Restore { get; private set; }

        public static RoutedCommand TileVertical { get; private set; }

        public static RoutedCommand TileHorizontal { get; private set; }

        public static RoutedCommand Cascade { get; private set; }

        public static RoutedCommand ArrangeIcons { get; private set; }

        public static RoutedCommand ChangeMDIStyle { get; private set; }

        private class MDIControllerCommandLink : RoutedCommand
        {
            public MDIControllerCommandLink(string name, MDIControllerCommand command) : base(name, typeof(MDIControllerCommand))
            {
                this.Command = command;
            }

            public MDIControllerCommand Command { get; private set; }
        }
    }
}

