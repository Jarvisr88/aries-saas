namespace DevExpress.XtraPrinting.Native
{
    using System;

    public static class ExportOptionsPropertiesNames
    {
        public static class Base
        {
            public const string ActionAfterExport = "ActionAfterExport";
            public const string DefaultDirectory = "DefaultDirectory";
            public const string DefaultFileName = "DefaultFileName";
            public const string SaveMode = "SaveMode";
            public const string ShowOptionsBeforeExport = "ShowOptionsBeforeExport";
        }

        public static class Docx
        {
            public const string ExportMode = "ExportMode";
            public const string TableLayout = "TableLayout";
            public const string KeepRowHeight = "KeepRowHeight";
            public const string ExportWatermarks = "ExportWatermarks";
        }

        public static class Html
        {
            public const string CharacterSet = "CharacterSet";
            public const string Title = "Title";
            public const string RemoveSecondarySymbols = "RemoveSecondarySymbols";
            public const string EmbedImagesInHTML = "EmbedImagesInHTML";
            public const string ExportMode = "ExportMode";
            public const string PageBorderWidth = "PageBorderWidth";
            public const string PageBorderColor = "PageBorderColor";
            public const string TableLayout = "TableLayout";
            public const string ExportWatermarks = "ExportWatermarks";
        }

        public static class Image
        {
            public const string ExportMode = "ExportMode";
            public const string PageBorderWidth = "PageBorderWidth";
            public const string PageBorderColor = "PageBorderColor";
            public const string Resolution = "Resolution";
            public const string Format = "Format";
        }

        public static class NativeFormat
        {
            public const string Compressed = "Compressed";
        }

        public static class PageByPage
        {
            public const string PageRange = "PageRange";
        }

        public static class Pdf
        {
            public const string ExportEditingFieldsToAcroForms = "ExportEditingFieldsToAcroForms";
            public const string PdfACompatibility = "PdfACompatibility";
            public const string ShowPrintDialogOnOpen = "ShowPrintDialogOnOpen";
            public const string NeverEmbeddedFonts = "NeverEmbeddedFonts";
            public const string ConvertImagesToJpeg = "ConvertImagesToJpeg";
            public const string ImageQuality = "ImageQuality";
            public const string PasswordSecurityOptions = "PasswordSecurityOptions";
            public const string SignatureOptions = "SignatureOptions";

            public static class DocumentOptions
            {
                public const string Author = "Author";
                public const string Application = "Application";
                public const string Title = "Title";
                public const string Subject = "Subject";
                public const string Keywords = "Keywords";
            }
        }

        public static class Rtf
        {
            public const string ExportMode = "ExportMode";
            public const string ExportWatermarks = "ExportWatermarks";
        }

        public static class Text
        {
            public const string Separator = "Separator";
            public const string Encoding = "Encoding";
            public const string QuoteStringsWithSeparators = "QuoteStringsWithSeparators";
            public const string TextExportMode = "TextExportMode";
        }

        public static class Xls
        {
            public const string ExportMode = "ExportMode";
            public const string ShowGridLines = "ShowGridLines";
            public const string UseNativeFormat = "UseNativeFormat";
            public const string ExportHyperlinks = "ExportHyperlinks";
            public const string RawDataMode = "RawDataMode";
            public const string SheetName = "SheetName";
        }

        public static class Xps
        {
            public const string Compression = "Compression";

            public static class DocumentOptions
            {
                public const string Creator = "Creator";
                public const string Category = "Category";
                public const string Title = "Title";
                public const string Subject = "Subject";
                public const string Keywords = "Keywords";
                public const string Version = "Version";
                public const string Description = "Description";
            }
        }
    }
}

