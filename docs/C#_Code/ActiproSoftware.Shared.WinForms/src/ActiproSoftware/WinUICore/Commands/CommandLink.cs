namespace ActiproSoftware.WinUICore.Commands
{
    using ActiproSoftware.WinUICore.Input;
    using System;
    using System.Windows.Forms;

    public class CommandLink
    {
        private ActiproSoftware.WinUICore.Commands.Command #POd;
        private bool #L0d;
        private bool #W0d;
        private ActiproSoftware.WinUICore.Commands.KeyBinding #3ue;
        private ActiproSoftware.WinUICore.Commands.MouseBinding #4ue;

        internal bool #Oxe(Keys #t0f, ModifierKeys #u0f) => 
            (this.#3ue != null) && ((#t0f == this.#3ue.Key) && (#u0f == this.#3ue.Modifiers));

        public CommandLink() : this(null, (ActiproSoftware.WinUICore.Commands.KeyBinding) null, (ActiproSoftware.WinUICore.Commands.MouseBinding) null)
        {
        }

        public CommandLink(ActiproSoftware.WinUICore.Commands.Command command) : this(command, (ActiproSoftware.WinUICore.Commands.KeyBinding) null, (ActiproSoftware.WinUICore.Commands.MouseBinding) null)
        {
        }

        public CommandLink(ActiproSoftware.WinUICore.Commands.Command command, ActiproSoftware.WinUICore.Commands.KeyBinding keyBinding) : this(command, keyBinding, null)
        {
        }

        public CommandLink(ActiproSoftware.WinUICore.Commands.Command command, ActiproSoftware.WinUICore.Commands.KeyBinding keyBinding, ActiproSoftware.WinUICore.Commands.MouseBinding mouseBinding)
        {
            this.#L0d = true;
            this.#POd = command;
            this.#3ue = keyBinding;
            this.#4ue = mouseBinding;
        }

        public CommandLink(ActiproSoftware.WinUICore.Commands.Command command, Keys key, ModifierKeys modifiers) : this(command, new ActiproSoftware.WinUICore.Commands.KeyBinding(modifiers, key), null)
        {
        }

        public bool Checked
        {
            get => 
                this.#W0d;
            set => 
                this.#W0d = value;
        }

        public ActiproSoftware.WinUICore.Commands.Command Command
        {
            get => 
                this.#POd;
            set => 
                this.#POd = value;
        }

        public bool Enabled
        {
            get => 
                this.#L0d;
            set => 
                this.#L0d = value;
        }

        public ActiproSoftware.WinUICore.Commands.KeyBinding KeyBinding
        {
            get => 
                this.#3ue;
            set => 
                this.#3ue = value;
        }

        public ActiproSoftware.WinUICore.Commands.MouseBinding MouseBinding
        {
            get => 
                this.#4ue;
            set => 
                this.#4ue = value;
        }
    }
}

