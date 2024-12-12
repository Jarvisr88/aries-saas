namespace DevExpress.Data.Helpers
{
    using System;

    public interface IColumnsServerActions
    {
        bool AllowAction(string fieldName, ColumnServerActionType action);
    }
}

