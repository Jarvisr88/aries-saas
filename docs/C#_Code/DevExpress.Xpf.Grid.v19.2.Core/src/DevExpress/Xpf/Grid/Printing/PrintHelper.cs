namespace DevExpress.Xpf.Grid.Printing
{
    using DevExpress.Data.Utils;
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Printing;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class PrintHelper
    {
        private static readonly string ClipboardExportManagerTypeName = "DevExpress.Xpf.Printing.ClipboardExportManager`2";
        private static readonly string PrintHelperTypeName = "DevExpress.Xpf.Printing.PrintHelper";
        private static Type printHelperType;
        private static Type clipboardExportManagerType;

        static PrintHelper()
        {
            Assembly assembly = AssemblyHelper.LoadDXAssembly("DevExpress.Xpf.Printing.v19.2");
            if (assembly == null)
            {
                assembly = Helpers.LoadWithPartialName("DevExpress.Xpf.Printing.v19.2, Version=19.2.9.0");
            }
            if (assembly != null)
            {
                printHelperType = assembly.GetType(PrintHelperTypeName);
                clipboardExportManagerType = assembly.GetType(ClipboardExportManagerTypeName);
            }
        }

        private static void CheckIsPrintingAvailable()
        {
            if (!IsPrintingAvailable)
            {
                throw new Exception("DevExpress.Xpf.Printing.v19.2 dll is not available.");
            }
        }

        public static object ClipboardExportManagerInstance(Type typeColumn, Type typeRow, object manager)
        {
            Type[] typeArguments = new Type[] { typeColumn, typeRow };
            object[] args = new object[] { manager };
            return Activator.CreateInstance(clipboardExportManagerType.MakeGenericType(typeArguments), args);
        }

        public static void ExportToCsv(IPrintableControl printableControl, Stream stream)
        {
            object[] paramValues = new object[] { printableControl, stream };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToCsv(IPrintableControl printableControl, string filePath)
        {
            object[] paramValues = new object[] { printableControl, filePath };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToCsv(IPrintableControl printableControl, Stream stream, CsvExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, stream, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToCsv(IPrintableControl printableControl, string filePath, CsvExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, filePath, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToDocx(IPrintableControl printableControl, Stream stream)
        {
            object[] paramValues = new object[] { printableControl, stream };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToDocx(IPrintableControl printableControl, string filePath)
        {
            object[] paramValues = new object[] { printableControl, filePath };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToDocx(IPrintableControl printableControl, Stream stream, DocxExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, stream, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToDocx(IPrintableControl printableControl, string filePath, DocxExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, filePath, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToHtml(IPrintableControl printableControl, Stream stream)
        {
            object[] paramValues = new object[] { printableControl, stream };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToHtml(IPrintableControl printableControl, string filePath)
        {
            object[] paramValues = new object[] { printableControl, filePath };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToHtml(IPrintableControl printableControl, Stream stream, HtmlExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, stream, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToHtml(IPrintableControl printableControl, string filePath, HtmlExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, filePath, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToImage(IPrintableControl printableControl, Stream stream)
        {
            object[] paramValues = new object[] { printableControl, stream };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToImage(IPrintableControl printableControl, string filePath)
        {
            object[] paramValues = new object[] { printableControl, filePath };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToImage(IPrintableControl printableControl, Stream stream, ImageExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, stream, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToImage(IPrintableControl printableControl, string filePath, ImageExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, filePath, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToMht(IPrintableControl printableControl, Stream stream)
        {
            object[] paramValues = new object[] { printableControl, stream };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToMht(IPrintableControl printableControl, string filePath)
        {
            object[] paramValues = new object[] { printableControl, filePath };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToMht(IPrintableControl printableControl, Stream stream, MhtExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, stream, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToMht(IPrintableControl printableControl, string filePath, MhtExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, filePath, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToPdf(IPrintableControl printableControl, Stream stream)
        {
            object[] paramValues = new object[] { printableControl, stream };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToPdf(IPrintableControl printableControl, string filePath)
        {
            object[] paramValues = new object[] { printableControl, filePath };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToPdf(IPrintableControl printableControl, Stream stream, PdfExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, stream, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToPdf(IPrintableControl printableControl, string filePath, PdfExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, filePath, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToRtf(IPrintableControl printableControl, Stream stream)
        {
            object[] paramValues = new object[] { printableControl, stream };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToRtf(IPrintableControl printableControl, string filePath)
        {
            object[] paramValues = new object[] { printableControl, filePath };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToRtf(IPrintableControl printableControl, Stream stream, RtfExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, stream, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToRtf(IPrintableControl printableControl, string filePath, RtfExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, filePath, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToText(IPrintableControl printableControl, Stream stream)
        {
            object[] paramValues = new object[] { printableControl, stream };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToText(IPrintableControl printableControl, string filePath)
        {
            object[] paramValues = new object[] { printableControl, filePath };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToText(IPrintableControl printableControl, Stream stream, TextExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, stream, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToText(IPrintableControl printableControl, string filePath, TextExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, filePath, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToXls(IPrintableControl printableControl, Stream stream)
        {
            object[] paramValues = new object[] { printableControl, stream };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToXls(IPrintableControl printableControl, string filePath)
        {
            object[] paramValues = new object[] { printableControl, filePath };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToXls(IPrintableControl printableControl, Stream stream, XlsExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, stream, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToXls(IPrintableControl printableControl, string filePath, XlsExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, filePath, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToXlsx(IPrintableControl printableControl, Stream stream)
        {
            object[] paramValues = new object[] { printableControl, stream };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToXlsx(IPrintableControl printableControl, string filePath)
        {
            object[] paramValues = new object[] { printableControl, filePath };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToXlsx(IPrintableControl printableControl, Stream stream, XlsxExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, stream, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToXlsx(IPrintableControl printableControl, string filePath, XlsxExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, filePath, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToXps(IPrintableControl printableControl, Stream stream)
        {
            object[] paramValues = new object[] { printableControl, stream };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToXps(IPrintableControl printableControl, string filePath)
        {
            object[] paramValues = new object[] { printableControl, filePath };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToXps(IPrintableControl printableControl, Stream stream, XpsExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, stream, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ExportToXps(IPrintableControl printableControl, string filePath, XpsExportOptions options)
        {
            object[] paramValues = new object[] { printableControl, filePath, options };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        private static object InvokeMember(MethodBase prototypeMethod, params object[] paramValues)
        {
            CheckIsPrintingAvailable();
            Func<ParameterInfo, Type> selector = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<ParameterInfo, Type> local1 = <>c.<>9__8_0;
                selector = <>c.<>9__8_0 = p => p.ParameterType;
            }
            Type[] types = prototypeMethod.GetParameters().Select<ParameterInfo, Type>(selector).ToArray<Type>();
            return printHelperType.GetMethod(prototypeMethod.Name, types).Invoke(null, paramValues);
        }

        public static void Print(IPrintableControl printableControl)
        {
            object[] paramValues = new object[] { printableControl };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void PrintDirect(IPrintableControl printableControl)
        {
            object[] paramValues = new object[] { printableControl };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        [Obsolete("This method is now obsolete. Use the DevExpress.Xpf.Grid.Printing.PrintHelper.PrintDirect(string printerName) method instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public static void PrintDirect(IPrintableControl printableControl, PrintQueue queue)
        {
            object[] paramValues = new object[] { printableControl, queue };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void PrintDirect(IPrintableControl printableControl, string printerName)
        {
            object[] paramValues = new object[] { printableControl, printerName };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ShowPrintPreview(FrameworkElement owner, IPrintableControl printableControl)
        {
            object[] paramValues = new object[] { owner, printableControl };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ShowPrintPreview(FrameworkElement owner, IPrintableControl printableControl, string documentName)
        {
            object[] paramValues = new object[] { owner, printableControl, documentName };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ShowPrintPreview(FrameworkElement owner, IPrintableControl printableControl, string documentName, string title)
        {
            object[] paramValues = new object[] { owner, printableControl, documentName, title };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ShowPrintPreviewDialog(Window owner, IPrintableControl printableControl)
        {
            object[] paramValues = new object[] { owner, printableControl };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ShowPrintPreviewDialog(Window owner, IPrintableControl printableControl, string documentName)
        {
            object[] paramValues = new object[] { owner, printableControl, documentName };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ShowPrintPreviewDialog(Window owner, IPrintableControl printableControl, string documentName, string title)
        {
            object[] paramValues = new object[] { owner, printableControl, documentName, title };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ShowRibbonPrintPreview(FrameworkElement owner, IPrintableControl printableControl)
        {
            object[] paramValues = new object[] { owner, printableControl };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ShowRibbonPrintPreview(FrameworkElement owner, IPrintableControl printableControl, string documentName)
        {
            object[] paramValues = new object[] { owner, printableControl, documentName };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ShowRibbonPrintPreview(FrameworkElement owner, IPrintableControl printableControl, string documentName, string title)
        {
            object[] paramValues = new object[] { owner, printableControl, documentName, title };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ShowRibbonPrintPreviewDialog(Window owner, IPrintableControl printableControl)
        {
            object[] paramValues = new object[] { owner, printableControl };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ShowRibbonPrintPreviewDialog(Window owner, IPrintableControl printableControl, string documentName)
        {
            object[] paramValues = new object[] { owner, printableControl, documentName };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static void ShowRibbonPrintPreviewDialog(Window owner, IPrintableControl printableControl, string documentName, string title)
        {
            object[] paramValues = new object[] { owner, printableControl, documentName, title };
            InvokeMember(MethodBase.GetCurrentMethod(), paramValues);
        }

        public static bool IsPrintingAvailable =>
            printHelperType != null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PrintHelper.<>c <>9 = new PrintHelper.<>c();
            public static Func<ParameterInfo, Type> <>9__8_0;

            internal Type <InvokeMember>b__8_0(ParameterInfo p) => 
                p.ParameterType;
        }
    }
}

