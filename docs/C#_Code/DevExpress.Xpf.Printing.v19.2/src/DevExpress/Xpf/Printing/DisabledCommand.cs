namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows.Input;

    public class DisabledCommand : ICommand
    {
        private static readonly DisabledCommand instance = new DisabledCommand();

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        public bool CanExecute(object parameter) => 
            false;

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public static DisabledCommand Instance =>
            instance;
    }
}

