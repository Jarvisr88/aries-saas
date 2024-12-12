namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;

    public abstract class SerializationControllerCommand : ICommand
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

        static SerializationControllerCommand()
        {
            SaveLayout = new SerializationControllerCommandLink("SaveLayout", new SaveLayoutCommand());
            RestoreLayout = new SerializationControllerCommandLink("RestoreLayout", new RestoreLayoutCommand());
        }

        internal SerializationControllerCommand()
        {
        }

        internal static void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ISerializationController serializationController = GetSerializationController(DockLayoutManager.GetDockLayoutManager(sender as DependencyObject));
            SerializationControllerCommand command = ((SerializationControllerCommandLink) e.Command).Command;
            e.CanExecute = ((serializationController != null) && (e.Parameter != null)) && command.CanExecuteCore(e.Parameter);
        }

        protected abstract bool CanExecuteCore(object path);
        protected abstract void ExecuteCore(ISerializationController controller, object path);
        internal static void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ISerializationController serializationController = GetSerializationController(DockLayoutManager.GetDockLayoutManager(sender as DependencyObject));
            if ((serializationController != null) && (e.Parameter != null))
            {
                ((SerializationControllerCommandLink) e.Command).Command.ExecuteCore(serializationController, e.Parameter);
            }
        }

        private static ISerializationController GetSerializationController(DockLayoutManager container) => 
            container?.SerializationController;

        protected void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChangedCore != null)
            {
                this.CanExecuteChangedCore(this, EventArgs.Empty);
            }
        }

        bool ICommand.CanExecute(object parameter) => 
            (this.Controller != null) ? this.CanExecuteCore(this.Path) : false;

        void ICommand.Execute(object parameter)
        {
            if (this.Controller != null)
            {
                this.ExecuteCore(this.Controller, this.Path);
            }
        }

        protected internal ISerializationController Controller { get; set; }

        protected internal object Path { get; set; }

        public static RoutedCommand SaveLayout { get; private set; }

        public static RoutedCommand RestoreLayout { get; private set; }

        private class SerializationControllerCommandLink : RoutedCommand
        {
            public SerializationControllerCommandLink(string name, SerializationControllerCommand command) : base(name, typeof(SerializationControllerCommand))
            {
                this.Command = command;
            }

            public SerializationControllerCommand Command { get; private set; }
        }
    }
}

