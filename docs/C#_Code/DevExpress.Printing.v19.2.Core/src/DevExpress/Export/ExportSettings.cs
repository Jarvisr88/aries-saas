namespace DevExpress.Export
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class ExportSettings
    {
        private static ExportType defaultExportTypeCore = ExportType.DataAware;
        private static readonly DefaultBoolean EncodeCsvExecutableContentDefaultValue = DefaultBoolean.Default;

        static ExportSettings()
        {
            EncodeCsvExecutableContent = EncodeCsvExecutableContentDefaultValue;
        }

        public static ExportType DefaultExportType
        {
            get => 
                (defaultExportTypeCore == ExportType.Default) ? ExportType.DataAware : defaultExportTypeCore;
            set => 
                defaultExportTypeCore = value;
        }

        public static DefaultBoolean EncodeCsvExecutableContent { get; set; }
    }
}

