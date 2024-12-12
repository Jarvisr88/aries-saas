namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class CommandWrapper : ICommand
    {
        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        public CommandWrapper(Func<ICommand> getCommand)
        {
            this.GetCommand = getCommand;
        }

        public bool CanExecute(object parameter)
        {
            Func<bool> fallback = <>c.<>9__8_1;
            if (<>c.<>9__8_1 == null)
            {
                Func<bool> local1 = <>c.<>9__8_1;
                fallback = <>c.<>9__8_1 = () => false;
            }
            return this.GetCommand().Return<ICommand, bool>(x => x.CanExecute(parameter), fallback);
        }

        public void Execute(object parameter)
        {
            this.GetCommand().Do<ICommand>(x => x.Execute(parameter));
        }

        private Func<ICommand> GetCommand { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CommandWrapper.<>c <>9 = new CommandWrapper.<>c();
            public static Func<bool> <>9__8_1;

            internal bool <CanExecute>b__8_1() => 
                false;
        }
    }
}

