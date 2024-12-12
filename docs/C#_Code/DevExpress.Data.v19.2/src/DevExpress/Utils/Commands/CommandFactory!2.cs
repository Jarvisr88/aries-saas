namespace DevExpress.Utils.Commands
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate TCommand CommandFactory<TCommand, TOwner>(TOwner owner) where TCommand: Command where TOwner: class;
}

