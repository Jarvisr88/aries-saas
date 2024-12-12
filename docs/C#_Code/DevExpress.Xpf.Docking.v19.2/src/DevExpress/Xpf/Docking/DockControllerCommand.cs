namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;

    public abstract class DockControllerCommand : ICommand
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

        static DockControllerCommand()
        {
            Activate = new DockControllerCommandLink("Activate", new ActivateCommand());
            Close = new DockControllerCommandLink("Close", new CloseCommand());
            Dock = new DockControllerCommandLink("Dock", new DockCommand());
            Float = new DockControllerCommandLink("Float", new FloatCommand());
            Hide = new DockControllerCommandLink("Hide", new HideCommand());
            Restore = new DockControllerCommandLink("Restore", new RestoreCommand());
            CloseActive = new DockControllerCommandLink("CloseActive", new CloseActiveCommand());
        }

        internal DockControllerCommand()
        {
        }

        internal static void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            IDockController dockController = GetDockController(DockLayoutManager.GetDockLayoutManager(sender as DependencyObject));
            BaseLayoutItem parameter = e.Parameter as BaseLayoutItem;
            DockControllerCommand command = ((DockControllerCommandLink) e.Command).Command;
            e.CanExecute = ((dockController != null) && (parameter != null)) && command.CanExecuteCore(parameter);
        }

        protected abstract bool CanExecuteCore(BaseLayoutItem item);
        protected abstract void ExecuteCore(IDockController controller, BaseLayoutItem item);
        internal static void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IDockController dockController = GetDockController(DockLayoutManager.GetDockLayoutManager(sender as DependencyObject));
            BaseLayoutItem parameter = e.Parameter as BaseLayoutItem;
            if ((dockController != null) && (parameter != null))
            {
                ((DockControllerCommandLink) e.Command).Command.ExecuteCore(dockController, parameter);
            }
        }

        private static IDockController GetDockController(DockLayoutManager container) => 
            container?.DockController;

        protected void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChangedCore != null)
            {
                this.CanExecuteChangedCore(this, EventArgs.Empty);
            }
        }

        bool ICommand.CanExecute(object parameter) => 
            (this.Controller != null) && ((this.Item != null) && this.CanExecuteCore(this.Item));

        void ICommand.Execute(object parameter)
        {
            if (this.Controller != null)
            {
                this.ExecuteCore(this.Controller, this.Item);
            }
        }

        protected internal IDockController Controller { get; set; }

        protected internal BaseLayoutItem Item { get; set; }

        public static RoutedCommand Activate { get; private set; }

        public static RoutedCommand Close { get; private set; }

        public static RoutedCommand Dock { get; private set; }

        public static RoutedCommand Float { get; private set; }

        public static RoutedCommand Hide { get; private set; }

        public static RoutedCommand Restore { get; private set; }

        internal static RoutedCommand CloseActive { get; private set; }

        private class DockControllerCommandLink : RoutedCommand
        {
            public DockControllerCommandLink(string name, DockControllerCommand command) : base(name, typeof(DockControllerCommand))
            {
                this.Command = command;
            }

            public DockControllerCommand Command { get; private set; }
        }
    }
}

