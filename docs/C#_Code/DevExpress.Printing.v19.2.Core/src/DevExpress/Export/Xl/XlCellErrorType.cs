namespace DevExpress.Export.Xl
{
    using System;

    public enum XlCellErrorType
    {
        Null = 0,
        DivisionByZero = 7,
        Value = 15,
        Reference = 0x17,
        Name = 0x1d,
        Number = 0x24,
        NotAvailable = 0x2a
    }
}

