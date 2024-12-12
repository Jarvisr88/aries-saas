namespace DevExpress.Printing.Native.PrintEditor
{
    using System;

    [Flags]
    public enum PrinterType
    {
        Printer = 0,
        Fax = 1,
        Network = 2,
        Default = 4,
        Offline = 8
    }
}

