namespace DevExpress.XtraPrinting.Native
{
    using System;

    public interface IDocumentProxy
    {
        void AddPage(PSPage page);

        string InfoString { get; }

        bool SmartXDivision { get; }

        bool SmartYDivision { get; }

        int PageCount { get; }

        bool RightToLeftLayout { get; }
    }
}

