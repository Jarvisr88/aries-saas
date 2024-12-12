namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Reflection;

    public interface ITextLayoutTable
    {
        int Count { get; }

        string this[int index] { get; set; }
    }
}

