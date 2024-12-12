namespace ActiproSoftware.WinUICore.Commands
{
    using System;

    public interface ICommandTarget
    {
        bool RaiseCommand(Command command);

        CommandLinkCollection CommandLinks { get; }

        ICommandTarget ForwardCommandsTo { get; }
    }
}

