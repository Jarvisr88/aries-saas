namespace ActiproSoftware.WinUICore.Commands
{
    using ActiproSoftware.WinUICore.Input;
    using System;
    using System.Windows.Forms;

    public class KeyBinding
    {
        private ModifierKeys #aTb;
        private Keys #1Nc;

        public KeyBinding(ModifierKeys modifiers, Keys key)
        {
            this.#aTb = modifiers;
            this.#1Nc = key;
        }

        public ModifierKeys Modifiers =>
            this.#aTb;

        public Keys Key =>
            this.#1Nc;
    }
}

