namespace DevExpress.Xpf.Docking.Customization
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;

    public abstract class CustomizationControllerCommand : ICommand
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

        static CustomizationControllerCommand()
        {
            ShowClosedItems = new CustomizationControllerCommandLink("ShowClosedItems", new ShowClosedItemsCommand());
            HideClosedItems = new CustomizationControllerCommandLink("HideClosedItems", new HideClosedItemsCommand());
        }

        internal CustomizationControllerCommand()
        {
        }

        internal static void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ICustomizationController customizationController = GetCustomizationController(DockLayoutManager.GetDockLayoutManager(sender as DependencyObject));
            CustomizationControllerCommand command = ((CustomizationControllerCommandLink) e.Command).Command;
            e.CanExecute = (customizationController != null) && command.CanExecuteCore(customizationController);
        }

        protected abstract bool CanExecuteCore(ICustomizationController controller);
        protected abstract void ExecuteCore(ICustomizationController controller);
        internal static void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ICustomizationController customizationController = GetCustomizationController(DockLayoutManager.GetDockLayoutManager(sender as DependencyObject));
            if ((customizationController != null) && (e.Parameter != null))
            {
                ((CustomizationControllerCommandLink) e.Command).Command.ExecuteCore(customizationController);
            }
        }

        private static ICustomizationController GetCustomizationController(DockLayoutManager container) => 
            container?.CustomizationController;

        protected void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChangedCore != null)
            {
                this.CanExecuteChangedCore(this, EventArgs.Empty);
            }
        }

        bool ICommand.CanExecute(object parameter) => 
            (this.Controller != null) && this.CanExecuteCore(this.Controller);

        void ICommand.Execute(object parameter)
        {
            if (this.Controller != null)
            {
                this.ExecuteCore(this.Controller);
            }
        }

        protected internal ICustomizationController Controller { get; set; }

        public static RoutedCommand ShowClosedItems { get; private set; }

        public static RoutedCommand HideClosedItems { get; private set; }

        private class CustomizationControllerCommandLink : RoutedCommand
        {
            public CustomizationControllerCommandLink(string name, CustomizationControllerCommand command) : base(name, typeof(CustomizationControllerCommand))
            {
                this.Command = command;
            }

            public CustomizationControllerCommand Command { get; private set; }
        }
    }
}

