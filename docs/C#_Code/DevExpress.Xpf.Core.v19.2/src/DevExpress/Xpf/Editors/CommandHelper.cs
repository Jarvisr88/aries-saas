namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    internal class CommandHelper
    {
        internal static bool CanExecuteCommandSource(ICommandSource commandSource)
        {
            ICommand command = commandSource.Command;
            if (command == null)
            {
                return false;
            }
            object commandParameter = commandSource.CommandParameter;
            IInputElement commandTarget = commandSource.CommandTarget;
            RoutedCommand command2 = command as RoutedCommand;
            if (command2 == null)
            {
                return command.CanExecute(commandParameter);
            }
            commandTarget ??= (commandSource as IInputElement);
            return command2.CanExecute(commandParameter, commandTarget);
        }
    }
}

