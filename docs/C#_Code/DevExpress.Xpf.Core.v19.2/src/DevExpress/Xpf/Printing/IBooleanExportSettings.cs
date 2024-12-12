namespace DevExpress.Xpf.Printing
{
    using System;

    public interface IBooleanExportSettings : IExportSettings
    {
        bool? BooleanValue { get; }

        string CheckText { get; }
    }
}

