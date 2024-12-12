namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class PdfOutlinesViewerCommands : DocumentMapCommands
    {
        static PdfOutlinesViewerCommands()
        {
            PrintCommand = new RoutedCommand("PrintCommand", typeof(PdfOutlinesViewerCommands));
            CommandManager.RegisterClassCommandBinding(typeof(TreeListView), new CommandBinding(PrintCommand, (d, e) => ExecutePrintCommand((TreeListView) d, e), (d, e) => CanExecutePrintCommand((TreeListView) d, e)));
            PrintSectionCommand = new RoutedCommand("PrintSectionCommand", typeof(PdfOutlinesViewerCommands));
            CommandManager.RegisterClassCommandBinding(typeof(TreeListView), new CommandBinding(PrintSectionCommand, (d, e) => ExecutePrintSectionCommand((TreeListView) d, e), (d, e) => CanExecutePrintSectionCommand((TreeListView) d, e)));
        }

        private static void CanExecutePrintCommand(TreeListView d, CanExecuteRoutedEventArgs e)
        {
            Predicate<DependencyObject> predicate = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Predicate<DependencyObject> local1 = <>c.<>9__9_0;
                predicate = <>c.<>9__9_0 = o => o is DocumentMapControl;
            }
            DocumentMapControl input = (DocumentMapControl) LayoutHelper.FindLayoutOrVisualParentObject(d, predicate, false, null);
            e.CanExecute = input.With<DocumentMapControl, PdfOutlinesViewerSettings>((<>c.<>9__9_1 ??= x => (x.ActualSettings as PdfOutlinesViewerSettings))).If<PdfOutlinesViewerSettings>(x => x.PrintCommand.CanExecute(e.Parameter)).ReturnSuccess<PdfOutlinesViewerSettings>();
            e.Handled = true;
        }

        private static void CanExecutePrintSectionCommand(TreeListView d, CanExecuteRoutedEventArgs e)
        {
            Predicate<DependencyObject> predicate = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Predicate<DependencyObject> local1 = <>c.<>9__11_0;
                predicate = <>c.<>9__11_0 = o => o is DocumentMapControl;
            }
            DocumentMapControl input = (DocumentMapControl) LayoutHelper.FindLayoutOrVisualParentObject(d, predicate, false, null);
            e.CanExecute = input.With<DocumentMapControl, PdfOutlinesViewerSettings>((<>c.<>9__11_1 ??= x => (x.ActualSettings as PdfOutlinesViewerSettings))).If<PdfOutlinesViewerSettings>(x => x.PrintSectionCommand.CanExecute(e.Parameter)).ReturnSuccess<PdfOutlinesViewerSettings>();
            e.Handled = true;
        }

        private static void ExecutePrintCommand(TreeListView d, ExecutedRoutedEventArgs e)
        {
            Predicate<DependencyObject> predicate = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Predicate<DependencyObject> local1 = <>c.<>9__10_0;
                predicate = <>c.<>9__10_0 = o => o is DocumentMapControl;
            }
            Func<DocumentMapControl, PdfOutlinesViewerSettings> evaluator = <>c.<>9__10_1;
            if (<>c.<>9__10_1 == null)
            {
                Func<DocumentMapControl, PdfOutlinesViewerSettings> local2 = <>c.<>9__10_1;
                evaluator = <>c.<>9__10_1 = x => x.ActualSettings as PdfOutlinesViewerSettings;
            }
            ((DocumentMapControl) LayoutHelper.FindLayoutOrVisualParentObject(d, predicate, false, null)).With<DocumentMapControl, PdfOutlinesViewerSettings>(evaluator).Do<PdfOutlinesViewerSettings>(x => x.PrintCommand.Execute(e.Parameter));
            e.Handled = true;
        }

        private static void ExecutePrintSectionCommand(TreeListView d, ExecutedRoutedEventArgs e)
        {
            Predicate<DependencyObject> predicate = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Predicate<DependencyObject> local1 = <>c.<>9__12_0;
                predicate = <>c.<>9__12_0 = o => o is DocumentMapControl;
            }
            Func<DocumentMapControl, PdfOutlinesViewerSettings> evaluator = <>c.<>9__12_1;
            if (<>c.<>9__12_1 == null)
            {
                Func<DocumentMapControl, PdfOutlinesViewerSettings> local2 = <>c.<>9__12_1;
                evaluator = <>c.<>9__12_1 = x => x.ActualSettings as PdfOutlinesViewerSettings;
            }
            ((DocumentMapControl) LayoutHelper.FindLayoutOrVisualParentObject(d, predicate, false, null)).With<DocumentMapControl, PdfOutlinesViewerSettings>(evaluator).Do<PdfOutlinesViewerSettings>(x => x.PrintSectionCommand.Execute(e.Parameter));
            e.Handled = true;
        }

        public static ICommand PrintCommand { get; private set; }

        public static ICommand PrintSectionCommand { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfOutlinesViewerCommands.<>c <>9 = new PdfOutlinesViewerCommands.<>c();
            public static Predicate<DependencyObject> <>9__9_0;
            public static Func<DocumentMapControl, PdfOutlinesViewerSettings> <>9__9_1;
            public static Predicate<DependencyObject> <>9__10_0;
            public static Func<DocumentMapControl, PdfOutlinesViewerSettings> <>9__10_1;
            public static Predicate<DependencyObject> <>9__11_0;
            public static Func<DocumentMapControl, PdfOutlinesViewerSettings> <>9__11_1;
            public static Predicate<DependencyObject> <>9__12_0;
            public static Func<DocumentMapControl, PdfOutlinesViewerSettings> <>9__12_1;

            internal void <.cctor>b__8_0(object d, ExecutedRoutedEventArgs e)
            {
                PdfOutlinesViewerCommands.ExecutePrintCommand((TreeListView) d, e);
            }

            internal void <.cctor>b__8_1(object d, CanExecuteRoutedEventArgs e)
            {
                PdfOutlinesViewerCommands.CanExecutePrintCommand((TreeListView) d, e);
            }

            internal void <.cctor>b__8_2(object d, ExecutedRoutedEventArgs e)
            {
                PdfOutlinesViewerCommands.ExecutePrintSectionCommand((TreeListView) d, e);
            }

            internal void <.cctor>b__8_3(object d, CanExecuteRoutedEventArgs e)
            {
                PdfOutlinesViewerCommands.CanExecutePrintSectionCommand((TreeListView) d, e);
            }

            internal bool <CanExecutePrintCommand>b__9_0(DependencyObject o) => 
                o is DocumentMapControl;

            internal PdfOutlinesViewerSettings <CanExecutePrintCommand>b__9_1(DocumentMapControl x) => 
                x.ActualSettings as PdfOutlinesViewerSettings;

            internal bool <CanExecutePrintSectionCommand>b__11_0(DependencyObject o) => 
                o is DocumentMapControl;

            internal PdfOutlinesViewerSettings <CanExecutePrintSectionCommand>b__11_1(DocumentMapControl x) => 
                x.ActualSettings as PdfOutlinesViewerSettings;

            internal bool <ExecutePrintCommand>b__10_0(DependencyObject o) => 
                o is DocumentMapControl;

            internal PdfOutlinesViewerSettings <ExecutePrintCommand>b__10_1(DocumentMapControl x) => 
                x.ActualSettings as PdfOutlinesViewerSettings;

            internal bool <ExecutePrintSectionCommand>b__12_0(DependencyObject o) => 
                o is DocumentMapControl;

            internal PdfOutlinesViewerSettings <ExecutePrintSectionCommand>b__12_1(DocumentMapControl x) => 
                x.ActualSettings as PdfOutlinesViewerSettings;
        }
    }
}

