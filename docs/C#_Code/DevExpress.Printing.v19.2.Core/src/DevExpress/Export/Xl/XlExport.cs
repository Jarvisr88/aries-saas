namespace DevExpress.Export.Xl
{
    using DevExpress.XtraExport.Csv;
    using DevExpress.XtraExport.Xls;
    using DevExpress.XtraExport.Xlsx;
    using System;

    public static class XlExport
    {
        public static IXlExporter CreateExporter(XlDocumentFormat documentFormat) => 
            CreateExporter(documentFormat, null);

        public static IXlExporter CreateExporter(XlDocumentFormat documentFormat, IXlFormulaParser formulaParser) => 
            (documentFormat != XlDocumentFormat.Xlsx) ? ((documentFormat != XlDocumentFormat.Xls) ? ((IXlExporter) new CsvDataAwareExporter()) : ((IXlExporter) new XlsDataAwareExporter(formulaParser))) : ((IXlExporter) new XlsxDataAwareExporter(formulaParser));
    }
}

