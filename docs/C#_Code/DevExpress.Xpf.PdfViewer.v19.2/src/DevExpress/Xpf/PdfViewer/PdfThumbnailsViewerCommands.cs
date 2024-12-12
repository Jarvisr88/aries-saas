namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class PdfThumbnailsViewerCommands
    {
        static PdfThumbnailsViewerCommands()
        {
            Type ownerType = typeof(PdfThumbnailsViewerCommands);
            PrintPagesCommand = new RoutedCommand("PrintPages", ownerType);
            CommandManager.RegisterClassCommandBinding(typeof(PdfThumbnailsViewerControl), new CommandBinding(PrintPagesCommand, (d, e) => ExecutePrintPagesCommand((PdfThumbnailsViewerControl) d, e), (d, e) => CanExecutePrintPagesCommand((PdfThumbnailsViewerControl) d, e)));
            ZoomCommand = new RoutedCommand("Zoom", ownerType);
            CommandManager.RegisterClassCommandBinding(typeof(PdfThumbnailsViewerControl), new CommandBinding(ZoomCommand, (d, e) => ExecuteZoomCommand((PdfThumbnailsViewerControl) d, e), (d, e) => CanExecuteZoomCommand((PdfThumbnailsViewerControl) d, e)));
        }

        private static void CanExecutePrintPagesCommand(PdfThumbnailsViewerControl thumbnailsViewer, CanExecuteRoutedEventArgs e)
        {
            Func<PdfThumbnailsViewerControl, ICommand> evaluator = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<PdfThumbnailsViewerControl, ICommand> local1 = <>c.<>9__9_0;
                evaluator = <>c.<>9__9_0 = x => x.PrintPagesCommand;
            }
            e.CanExecute = thumbnailsViewer.With<PdfThumbnailsViewerControl, ICommand>(evaluator).If<ICommand>(x => x.CanExecute(e.Parameter)).ReturnSuccess<ICommand>();
            e.Handled = true;
        }

        private static void CanExecuteZoomCommand(PdfThumbnailsViewerControl thumbnailsViewer, CanExecuteRoutedEventArgs e)
        {
            Func<PdfThumbnailsViewerControl, ICommand> evaluator = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Func<PdfThumbnailsViewerControl, ICommand> local1 = <>c.<>9__11_0;
                evaluator = <>c.<>9__11_0 = x => x.SetZoomFactorCommand;
            }
            e.CanExecute = thumbnailsViewer.With<PdfThumbnailsViewerControl, ICommand>(evaluator).If<ICommand>(x => x.CanExecute(e.Parameter)).ReturnSuccess<ICommand>();
            e.Handled = true;
        }

        private static void ExecutePrintPagesCommand(PdfThumbnailsViewerControl thumbnailsViewer, ExecutedRoutedEventArgs e)
        {
            thumbnailsViewer.Do<PdfThumbnailsViewerControl>(x => x.PrintPagesCommand.Execute(e.Parameter));
            e.Handled = true;
        }

        private static void ExecuteZoomCommand(PdfThumbnailsViewerControl thumbnailsViewer, ExecutedRoutedEventArgs e)
        {
            thumbnailsViewer.Do<PdfThumbnailsViewerControl>(x => x.SetZoomFactorCommand.Execute(e.Parameter));
            e.Handled = true;
        }

        public static ICommand PrintPagesCommand { get; private set; }

        public static ICommand ZoomCommand { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfThumbnailsViewerCommands.<>c <>9 = new PdfThumbnailsViewerCommands.<>c();
            public static Func<PdfThumbnailsViewerControl, ICommand> <>9__9_0;
            public static Func<PdfThumbnailsViewerControl, ICommand> <>9__11_0;

            internal void <.cctor>b__8_0(object d, ExecutedRoutedEventArgs e)
            {
                PdfThumbnailsViewerCommands.ExecutePrintPagesCommand((PdfThumbnailsViewerControl) d, e);
            }

            internal void <.cctor>b__8_1(object d, CanExecuteRoutedEventArgs e)
            {
                PdfThumbnailsViewerCommands.CanExecutePrintPagesCommand((PdfThumbnailsViewerControl) d, e);
            }

            internal void <.cctor>b__8_2(object d, ExecutedRoutedEventArgs e)
            {
                PdfThumbnailsViewerCommands.ExecuteZoomCommand((PdfThumbnailsViewerControl) d, e);
            }

            internal void <.cctor>b__8_3(object d, CanExecuteRoutedEventArgs e)
            {
                PdfThumbnailsViewerCommands.CanExecuteZoomCommand((PdfThumbnailsViewerControl) d, e);
            }

            internal ICommand <CanExecutePrintPagesCommand>b__9_0(PdfThumbnailsViewerControl x) => 
                x.PrintPagesCommand;

            internal ICommand <CanExecuteZoomCommand>b__11_0(PdfThumbnailsViewerControl x) => 
                x.SetZoomFactorCommand;
        }
    }
}

