namespace DevExpress.Export.Xl
{
    using DevExpress.Office.Crypto;
    using System;
    using System.Globalization;
    using System.IO;

    public interface IXlExport
    {
        IXlCell BeginCell();
        IXlColumn BeginColumn();
        IXlDocument BeginExport(Stream stream);
        IXlDocument BeginExport(Stream stream, EncryptionOptions encryptionOptions);
        IXlGroup BeginGroup();
        IXlPicture BeginPicture();
        IXlRow BeginRow();
        IXlSheet BeginSheet();
        void EndCell();
        void EndColumn();
        void EndExport();
        void EndGroup();
        void EndPicture();
        void EndRow();
        void EndSheet();
        void SkipCells(int count);
        void SkipColumns(int count);
        void SkipRows(int count);

        int CurrentRowIndex { get; }

        int CurrentColumnIndex { get; }

        int CurrentOutlineLevel { get; }

        CultureInfo CurrentCulture { get; }

        IXlFormulaEngine FormulaEngine { get; }

        IXlFormulaParser FormulaParser { get; }

        IXlDocumentOptions DocumentOptions { get; }

        XlDocumentProperties DocumentProperties { get; }

        IXlDocument CurrentDocument { get; }

        IXlSheet CurrentSheet { get; }
    }
}

