namespace ActiproSoftware.WinUICore.Commands
{
    using System;

    public class CommandEventArgs : EventArgs
    {
        private ActiproSoftware.WinUICore.Commands.Command #POd;

        public CommandEventArgs(ActiproSoftware.WinUICore.Commands.Command command)
        {
            this.#POd = command;
        }

        public ActiproSoftware.WinUICore.Commands.Command Command =>
            this.#POd;
    }
}

