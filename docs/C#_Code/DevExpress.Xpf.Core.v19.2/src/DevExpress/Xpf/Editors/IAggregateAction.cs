namespace DevExpress.Xpf.Editors
{
    using System;

    public interface IAggregateAction : IAction
    {
        bool CanAggregate(IAction action);
    }
}

