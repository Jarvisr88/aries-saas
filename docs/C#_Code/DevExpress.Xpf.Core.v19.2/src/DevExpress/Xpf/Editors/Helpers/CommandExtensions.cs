namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Input;

    public static class CommandExtensions
    {
        private static readonly Func<RoutedCommand, object, IInputElement, bool, bool> ExecuteCoreHandler;

        static CommandExtensions()
        {
            int? parametersCount = null;
            ExecuteCoreHandler = ReflectionHelper.CreateInstanceMethodHandler<Func<RoutedCommand, object, IInputElement, bool, bool>>(null, "ExecuteCore", BindingFlags.NonPublic | BindingFlags.Instance, typeof(RoutedCommand), parametersCount, null, true);
        }

        public static bool CanExecute(this ICommandSource commandSource) => 
            CanExecuteCommandSource(commandSource);

        private static bool CanExecuteCommandSource(ICommandSource commandSource)
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

        [SecurityCritical]
        private static void CriticalExecuteCommandSource(ICommandSource commandSource, bool userInitiated)
        {
            ICommand command = commandSource.Command;
            if (command != null)
            {
                object commandParameter = commandSource.CommandParameter;
                IInputElement commandTarget = commandSource.CommandTarget;
                RoutedCommand command2 = command as RoutedCommand;
                if (command2 == null)
                {
                    if (command.CanExecute(commandParameter))
                    {
                        command.Execute(commandParameter);
                    }
                }
                else
                {
                    commandTarget ??= (commandSource as IInputElement);
                    if (command2.CanExecute(commandParameter, commandTarget))
                    {
                        ExecuteCoreHandler(command2, commandParameter, commandTarget, userInitiated);
                    }
                }
            }
        }

        internal static void ExecuteCommand(ICommand command, object parameter, IInputElement target)
        {
            RoutedCommand command2 = command as RoutedCommand;
            if (command2 == null)
            {
                if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
            else if (command2.CanExecute(parameter, target))
            {
                command2.Execute(parameter, target);
            }
        }

        [SecuritySafeCritical]
        private static void ExecuteCommandSource(ICommandSource commandSource)
        {
            CriticalExecuteCommandSource(commandSource, false);
        }

        public static void TryExecute(this ICommandSource commandSource)
        {
            commandSource.If<ICommandSource>(new Func<ICommandSource, bool>(CommandExtensions.CanExecuteCommandSource)).Do<ICommandSource>(new Action<ICommandSource>(CommandExtensions.ExecuteCommandSource));
        }

        public static void TryExecute(this ICommand command, object parameter)
        {
            command.If<ICommand>(x => x.CanExecute(parameter)).Do<ICommand>(x => x.Execute(parameter));
        }
    }
}

