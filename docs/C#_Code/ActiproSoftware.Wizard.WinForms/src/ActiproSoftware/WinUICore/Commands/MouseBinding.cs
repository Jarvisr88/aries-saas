namespace ActiproSoftware.WinUICore.Commands
{
    using ActiproSoftware.WinUICore.Input;
    using System;

    public class MouseBinding
    {
        private MouseAction #m6b;
        private ModifierKeys #aTb;

        public MouseBinding(ModifierKeys modifiers, MouseAction action)
        {
            this.#aTb = modifiers;
            this.#m6b = action;
        }

        public MouseAction Action =>
            this.#m6b;

        public ModifierKeys Modifiers =>
            this.#aTb;
    }
}

