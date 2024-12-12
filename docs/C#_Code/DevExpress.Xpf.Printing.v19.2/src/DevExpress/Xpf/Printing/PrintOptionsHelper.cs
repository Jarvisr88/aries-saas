namespace DevExpress.Xpf.Printing
{
    using DevExpress.Printing.Native;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    internal class PrintOptionsHelper
    {
        public static PSPrintDocument CreatePrintDocument(PrintingSystemBase printingSystem, PrinterSettings printerSettings)
        {
            string pageRange = new PageScope(0, printingSystem.PageCount).PageRange;
            PrintController printController = printingSystem.ShowPrintStatusDialog ? ((PrintController) new PrintProgressController()) : ((PrintController) new StandardPrintController());
            PrintingSystemBase base1 = printingSystem;
            if (<>c.<>9__3_0 == null)
            {
                PrintingSystemBase local1 = printingSystem;
                base1 = (PrintingSystemBase) (<>c.<>9__3_0 = () => true);
            }
            PSPrintDocument document1 = new PSPrintDocument((PrintingSystemBase) <>c.<>9__3_0, (Color) printerSettings, printController, (PrinterSettings) printingSystem.Graph.PageBackColor, (Func<bool>) base1);
            document1.PageRange = pageRange;
            return document1;
        }

        public static PrinterSettings CreatePrinterSettings()
        {
            PageSettings settings = (PageSettings) PageSettingsHelper.DefaultPageSettings.Clone();
            if (settings.PrinterSettings != null)
            {
                settings.PrinterSettings = (PrinterSettings) settings.PrinterSettings.Clone();
                PageSettings pageSettings = (PageSettings) PageSettingsHelper.DefaultPageSettings.Clone();
                pageSettings.PrinterSettings = settings.PrinterSettings;
                PageSettingsHelper.SetDefaultPageSettings(settings.PrinterSettings, pageSettings);
            }
            PrinterSettings printerSettings = settings.PrinterSettings;
            printerSettings.MinimumPage = 1;
            printerSettings.MaximumPage = 0;
            printerSettings.FromPage = 0;
            printerSettings.ToPage = 0;
            return printerSettings;
        }

        public static PaperSize GetAppropriatePaperSize(PrintingSystemBase printingSystem, PrinterSettings.PaperSizeCollection paperSizes)
        {
            PageData data = printingSystem.PageSettings.Data;
            PaperSize paperSize = new PaperSize("Custom Paper Size", data.Size.Width, data.Size.Height);
            return (PageSizeInfo.GetAppropriatePaperSize(paperSizes, paperSize) ?? paperSize);
        }

        public static void UpdatePaperSources(PrinterSettings printerSettings, Action<IEnumerable<string>> onGetPaperSources, Action<string> onGetActualPaperSource)
        {
            PaperSource source = null;
            IEnumerable<string> sources = Enumerable.Empty<string>();
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task<PrinterSettings.PaperSourceCollection> paperSourcesAsync = DevExpress.Printing.Native.PrintHelper.GetPaperSourcesAsync(printerSettings);
            paperSourcesAsync.ContinueWith(delegate (Task<PrinterSettings.PaperSourceCollection> t) {
                Func<PaperSource, string> selector = <>c.<>9__4_1;
                if (<>c.<>9__4_1 == null)
                {
                    Func<PaperSource, string> local1 = <>c.<>9__4_1;
                    selector = <>c.<>9__4_1 = x => x.SourceName;
                }
                Func<string, string> keySelector = <>c.<>9__4_2;
                if (<>c.<>9__4_2 == null)
                {
                    Func<string, string> local2 = <>c.<>9__4_2;
                    keySelector = <>c.<>9__4_2 = x => x;
                }
                Func<string, bool> predicate = <>c.<>9__4_3;
                if (<>c.<>9__4_3 == null)
                {
                    Func<string, bool> local3 = <>c.<>9__4_3;
                    predicate = <>c.<>9__4_3 = x => x != string.Empty;
                }
                sources = t.Result.Cast<PaperSource>().Select<PaperSource, string>(selector).OrderBy<string, string>(keySelector).Where<string>(predicate).ToList<string>();
                onGetPaperSources(sources);
            }, scheduler);
            Task task2 = Task.Factory.StartNew(delegate {
                source = null;
                try
                {
                    source = printerSettings.DefaultPageSettings.PaperSource;
                }
                catch (Exception exception)
                {
                    Tracer.TraceError("DXperience.Reporting", exception);
                    Exception printerException = DevExpress.Printing.Native.PrintHelper.GetPrinterException(exception);
                }
            });
            Task[] tasks = new Task[] { task2, paperSourcesAsync };
            Task task3 = new TaskFactory(scheduler).ContinueWhenAll(tasks, delegate (Task[] x) {
                string str = null;
                if ((source != null) && !string.IsNullOrEmpty(source.SourceName))
                {
                    str = sources.Contains<string>(source.SourceName) ? source.SourceName : null;
                }
                onGetActualPaperSource(str);
            });
        }

        public static string ValidatePageRange(string pageRange, int maxPages)
        {
            if (string.IsNullOrEmpty(pageRange))
            {
                return PreviewStringId.Msg_IncorrectPageRange.GetString();
            }
            int[] indices = PageRangeParser.GetIndices(pageRange, maxPages);
            return (((indices.Length == 0) || ((indices.Length == 1) && (indices[0] == -1))) ? PreviewStringId.Msg_IncorrectPageRange.GetString() : null);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PrintOptionsHelper.<>c <>9 = new PrintOptionsHelper.<>c();
            public static Func<bool> <>9__3_0;
            public static Func<PaperSource, string> <>9__4_1;
            public static Func<string, string> <>9__4_2;
            public static Func<string, bool> <>9__4_3;

            internal bool <CreatePrintDocument>b__3_0() => 
                true;

            internal string <UpdatePaperSources>b__4_1(PaperSource x) => 
                x.SourceName;

            internal string <UpdatePaperSources>b__4_2(string x) => 
                x;

            internal bool <UpdatePaperSources>b__4_3(string x) => 
                x != string.Empty;
        }
    }
}

