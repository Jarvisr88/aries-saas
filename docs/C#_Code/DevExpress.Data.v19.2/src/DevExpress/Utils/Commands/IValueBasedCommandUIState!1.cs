namespace DevExpress.Utils.Commands
{
    using System;

    public interface IValueBasedCommandUIState<T> : ICommandUIState
    {
        T Value { get; set; }
    }
}

