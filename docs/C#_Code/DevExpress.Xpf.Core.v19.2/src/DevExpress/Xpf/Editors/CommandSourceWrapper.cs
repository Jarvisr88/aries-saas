namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    internal class CommandSourceWrapper : ICommandSource
    {
        public CommandSourceWrapper(ICommand command, object commandParameter, IInputElement commandTarget)
        {
            this.Command = command;
            this.CommandParameter = commandParameter;
            this.CommandTarget = commandTarget;
        }

        public ICommand Command { get; private set; }

        public object CommandParameter { get; private set; }

        public IInputElement CommandTarget { get; private set; }
    }
}

