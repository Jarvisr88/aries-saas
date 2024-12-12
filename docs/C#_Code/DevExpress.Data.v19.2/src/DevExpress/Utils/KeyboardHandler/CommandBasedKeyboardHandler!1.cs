namespace DevExpress.Utils.KeyboardHandler
{
    using DevExpress.Utils.Commands;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public abstract class CommandBasedKeyboardHandler<T> : DevExpress.Utils.KeyboardHandler.KeyboardHandler
    {
        private Dictionary<long, T> keyHandlerIdTable;

        protected CommandBasedKeyboardHandler();
        public static long CharToInt64KeyData(char value, Keys modifier);
        public abstract Command CreateHandlerCommand(T handlerId);
        protected abstract IKeyHashProvider CreateKeyHashProviderFromContext();
        protected bool ExecuteCommand(Command command, Keys keyData);
        protected virtual void ExecuteCommandCore(Command command, Keys keyData);
        public virtual Command GetKeyHandler(Keys keyData);
        public virtual Command GetKeyHandler(char key, Keys modifier);
        protected internal virtual Command GetKeyHandlerCore(long keyData);
        public virtual T GetKeyHandlerId(long keyData);
        public virtual Keys GetKeys(T handlerId);
        public override bool HandleKey(Keys keyData);
        public override bool HandleKeyPress(char character, Keys modifier);
        public static Keys KeyDataToKeys(long keyData);
        public static long KeysToInt64KeyData(Keys keys);
        public virtual void RegisterKeyHandler(IKeyHashProvider provider, Keys key, Keys modifier, T handlerId);
        protected internal virtual void RegisterKeyHandlerCore(IKeyHashProvider provider, long keyData, T handlerId);
        public virtual void UnregisterKeyHandler(IKeyHashProvider provider, Keys key, Keys modifier);
        protected internal virtual void UnregisterKeyHandlerCore(IKeyHashProvider provider, long keyData);
        protected abstract void ValidateHandlerId(T handlerId);

        public Dictionary<long, T> KeyHandlerIdTable { get; }
    }
}

