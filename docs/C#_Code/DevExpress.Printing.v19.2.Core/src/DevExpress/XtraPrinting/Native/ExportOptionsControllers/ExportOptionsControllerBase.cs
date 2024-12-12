namespace DevExpress.XtraPrinting.Native.ExportOptionsControllers
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.Lines;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public abstract class ExportOptionsControllerBase
    {
        private static ExportOptionsControllerBase[] controllers;

        static ExportOptionsControllerBase();
        protected ExportOptionsControllerBase();
        protected static void AddControllerToList(List<ExportOptionKind> hiddenOptions, ExportOptionsBase options, List<BaseLineController> list, string propertyName, Type lineType, ExportOptionKind optionKind);
        protected static void AddControllerToList(List<ExportOptionKind> hiddenOptions, List<BaseLineController> list, PropertyDescriptorCollection properties, string propertyName, object instance, Type lineType, ExportOptionKind optionKind);
        protected static void AddEmptySpaceToList(List<BaseLineController> list);
        protected static void AddPageRangeLineControllerToList(List<ExportOptionKind> hiddenOptions, ExportOptionsBase options, List<BaseLineController> list, ExportOptionKind optionKind);
        protected static void AddSeparatorToList(List<BaseLineController> list);
        protected abstract void CollectLineControllers(ExportOptionsBase options, List<ExportOptionKind> hiddenOptions, List<BaseLineController> list);
        public static ExportOptionsControllerBase GetControllerByOptions(ExportOptionsBase options);
        public abstract string[] GetExportedFileNames(PrintingSystemBase ps, ExportOptionsBase options, string fileName);
        public virtual ILine[] GetExportLines(ExportOptionsBase options, LineFactoryBase lineFactory, AvailableExportModes availableExportModes, List<ExportOptionKind> hiddenOptions);
        protected PropertyDescriptor GetExportModePropertyDescriptor(ExportOptionsBase options);
        protected virtual Type GetExportModeType();
        public virtual string GetFileExtension(ExportOptionsBase options);
        public virtual int GetFilterIndex(ExportOptionsBase options);
        internal BaseLineController[] GetLineControllers(ExportOptionsBase options, List<ExportOptionKind> hiddenOptions);
        public virtual bool ValidateInputFileName(ExportOptionsBase options);

        protected abstract Type ExportOptionsType { get; }

        public abstract PreviewStringId CaptionStringId { get; }

        public string Filter { get; }

        protected abstract string[] LocalizerStrings { get; }

        public abstract string[] FileExtensions { get; }

        protected virtual string ExportModePropertyName { get; }
    }
}

