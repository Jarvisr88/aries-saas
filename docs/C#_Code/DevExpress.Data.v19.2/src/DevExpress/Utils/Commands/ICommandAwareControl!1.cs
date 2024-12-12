namespace DevExpress.Utils.Commands
{
    using DevExpress.Utils.KeyboardHandler;
    using System;
    using System.Runtime.CompilerServices;

    public interface ICommandAwareControl<TCommandId> where TCommandId: struct
    {
        event EventHandler BeforeDispose;

        event EventHandler UpdateUI;

        void CommitImeContent();
        Command CreateCommand(TCommandId id);
        void Focus();
        bool HandleException(Exception e);

        CommandBasedKeyboardHandler<TCommandId> KeyboardHandler { get; }
    }
}

