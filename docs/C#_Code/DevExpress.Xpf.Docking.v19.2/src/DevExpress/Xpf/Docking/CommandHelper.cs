namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    internal static class CommandHelper
    {
        internal static bool CanExecuteCommand(ICommandSource commandSource)
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

        internal static void ExecuteCommand(ICommandSource commandSource)
        {
            ICommand command = commandSource.Command;
            RoutedCommand command2 = command as RoutedCommand;
            if (command != null)
            {
                if ((command2 != null) && command2.CanExecute(commandSource.CommandParameter, commandSource.CommandTarget))
                {
                    command2.Execute(commandSource.CommandParameter, commandSource.CommandTarget);
                }
                else if (command.CanExecute(commandSource.CommandParameter))
                {
                    command.Execute(commandSource.CommandParameter);
                }
            }
        }
    }
}

