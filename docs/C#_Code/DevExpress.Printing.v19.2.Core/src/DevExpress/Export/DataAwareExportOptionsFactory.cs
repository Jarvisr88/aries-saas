namespace DevExpress.Export
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;

    public static class DataAwareExportOptionsFactory
    {
        public static IDataAwareExportOptions Create(ExportTarget target)
        {
            if (target == ExportTarget.Xls)
            {
                return new XlsExportOptionsEx();
            }
            if (target == ExportTarget.Xlsx)
            {
                return new XlsxExportOptionsEx();
            }
            if (target != ExportTarget.Csv)
            {
                throw new NotImplementedException();
            }
            return new CsvExportOptionsEx();
        }

        public static IDataAwareExportOptions Create(ExportTarget target, object options)
        {
            if (options == null)
            {
                return Create(target);
            }
            if (target == ExportTarget.Xls)
            {
                return CreateCore<XlsExportOptionsEx>(options, target);
            }
            if (target == ExportTarget.Xlsx)
            {
                return CreateCore<XlsxExportOptionsEx>(options, target);
            }
            if (target != ExportTarget.Csv)
            {
                throw new NotImplementedException();
            }
            return CreateCore<CsvExportOptionsEx>(options, target);
        }

        private static IDataAwareExportOptions CreateCore<T>(object options, ExportTarget target)
        {
            if (options is T)
            {
                return (options as IDataAwareExportOptions);
            }
            IDataAwareExportOptions options2 = Create(target);
            CsvExportOptions options3 = options as CsvExportOptions;
            XlExportOptionsBase base2 = options as XlExportOptionsBase;
            if (base2 != null)
            {
                XlExportOptionsBase base3 = options2 as XlExportOptionsBase;
                if (base3 != null)
                {
                    base3.SheetName = base2.SheetName;
                    base3.ShowGridLines = base2.ShowGridLines;
                    base3.ExportHyperlinks = base2.ExportHyperlinks;
                }
            }
            if (options3 != null)
            {
                options2.CSVEncoding = options3.Encoding;
                options2.CSVSeparator = options3.Separator;
            }
            return options2;
        }

        public static DefaultBoolean GetActualOptionValue(DefaultBoolean currentValue, bool condition) => 
            condition ? DefaultBoolean.False : currentValue;

        public static DefaultBoolean UpdateDefaultBoolean(DefaultBoolean oldValue, bool suggestedValue) => 
            (oldValue == DefaultBoolean.Default) ? (suggestedValue ? DefaultBoolean.True : DefaultBoolean.False) : oldValue;
    }
}

