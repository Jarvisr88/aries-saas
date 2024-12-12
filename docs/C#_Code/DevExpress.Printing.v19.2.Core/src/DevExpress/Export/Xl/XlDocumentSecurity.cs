namespace DevExpress.Export.Xl
{
    using System;

    [Flags]
    public enum XlDocumentSecurity
    {
        None = 0,
        ReadonlyRecommended = 2,
        ReadonlyEnforced = 4,
        Locked = 8
    }
}

