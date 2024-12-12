namespace DevExpress.Data.Controls
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public interface IControlRowSource
    {
        event ControlRowSourceChangedEventHandler Changed;

        IEnumerable GetRows(ControlRows rows);

        object RowSource { get; }
    }
}

