namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class PdfAttachmentsViewerCommands
    {
        static PdfAttachmentsViewerCommands()
        {
            Type ownerType = typeof(PdfAttachmentsViewerCommands);
            OpenAttachmentCommand = new RoutedCommand("OpenAttachment", ownerType);
            CommandManager.RegisterClassCommandBinding(typeof(TableView), new CommandBinding(OpenAttachmentCommand, (d, e) => ExecuteOpenAttachmentCommand((TableView) d, e), (d, e) => CanExecuteOpenAttachmentCommand((TableView) d, e)));
            SaveAttachmentCommand = new RoutedCommand("SaveAttachment", ownerType);
            CommandManager.RegisterClassCommandBinding(typeof(TableView), new CommandBinding(SaveAttachmentCommand, (d, e) => ExecuteSaveAttachmentCommand((TableView) d, e), (d, e) => CanExecuteSaveAttachmentCommand((TableView) d, e)));
        }

        private static void CanExecuteOpenAttachmentCommand(TableView d, CanExecuteRoutedEventArgs e)
        {
            Predicate<DependencyObject> predicate = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Predicate<DependencyObject> local1 = <>c.<>9__9_0;
                predicate = <>c.<>9__9_0 = o => o is PdfAttachmentsViewerControl;
            }
            PdfAttachmentsViewerControl input = (PdfAttachmentsViewerControl) LayoutHelper.FindLayoutOrVisualParentObject(d, predicate, false, null);
            PdfAttachmentsViewerSettings evaluator = input.With<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings>(<>c.<>9__9_1 ??= x => x.ActualSettings);
            if (<>c.<>9__9_2 == null)
            {
                PdfAttachmentsViewerSettings local3 = input.With<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings>(<>c.<>9__9_1 ??= x => x.ActualSettings);
                evaluator = (PdfAttachmentsViewerSettings) (<>c.<>9__9_2 = x => x.OpenAttachmentCommand);
            }
            e.CanExecute = ((PdfAttachmentsViewerSettings) <>c.<>9__9_2).With<PdfAttachmentsViewerSettings, ICommand>(evaluator).If<ICommand>(x => x.CanExecute(e.Parameter)).ReturnSuccess<ICommand>();
            e.Handled = true;
        }

        private static void CanExecuteSaveAttachmentCommand(TableView d, CanExecuteRoutedEventArgs e)
        {
            Predicate<DependencyObject> predicate = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Predicate<DependencyObject> local1 = <>c.<>9__11_0;
                predicate = <>c.<>9__11_0 = o => o is PdfAttachmentsViewerControl;
            }
            PdfAttachmentsViewerControl input = (PdfAttachmentsViewerControl) LayoutHelper.FindLayoutOrVisualParentObject(d, predicate, false, null);
            PdfAttachmentsViewerSettings evaluator = input.With<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings>(<>c.<>9__11_1 ??= x => x.ActualSettings);
            if (<>c.<>9__11_2 == null)
            {
                PdfAttachmentsViewerSettings local3 = input.With<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings>(<>c.<>9__11_1 ??= x => x.ActualSettings);
                evaluator = (PdfAttachmentsViewerSettings) (<>c.<>9__11_2 = x => x.SaveAttachmentCommand);
            }
            e.CanExecute = ((PdfAttachmentsViewerSettings) <>c.<>9__11_2).With<PdfAttachmentsViewerSettings, ICommand>(evaluator).If<ICommand>(x => x.CanExecute(e.Parameter)).ReturnSuccess<ICommand>();
            e.Handled = true;
        }

        private static void ExecuteOpenAttachmentCommand(TableView d, ExecutedRoutedEventArgs e)
        {
            Predicate<DependencyObject> predicate = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Predicate<DependencyObject> local1 = <>c.<>9__10_0;
                predicate = <>c.<>9__10_0 = o => o is PdfAttachmentsViewerControl;
            }
            Func<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings> evaluator = <>c.<>9__10_1;
            if (<>c.<>9__10_1 == null)
            {
                Func<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings> local2 = <>c.<>9__10_1;
                evaluator = <>c.<>9__10_1 = x => x.ActualSettings;
            }
            ((PdfAttachmentsViewerControl) LayoutHelper.FindLayoutOrVisualParentObject(d, predicate, false, null)).With<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings>(evaluator).Do<PdfAttachmentsViewerSettings>(x => x.OpenAttachmentCommand.Execute(e.Parameter));
            e.Handled = true;
        }

        private static void ExecuteSaveAttachmentCommand(TableView d, ExecutedRoutedEventArgs e)
        {
            Predicate<DependencyObject> predicate = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Predicate<DependencyObject> local1 = <>c.<>9__12_0;
                predicate = <>c.<>9__12_0 = o => o is PdfAttachmentsViewerControl;
            }
            Func<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings> evaluator = <>c.<>9__12_1;
            if (<>c.<>9__12_1 == null)
            {
                Func<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings> local2 = <>c.<>9__12_1;
                evaluator = <>c.<>9__12_1 = x => x.ActualSettings;
            }
            ((PdfAttachmentsViewerControl) LayoutHelper.FindLayoutOrVisualParentObject(d, predicate, false, null)).With<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings>(evaluator).Do<PdfAttachmentsViewerSettings>(x => x.SaveAttachmentCommand.Execute(e.Parameter));
            e.Handled = true;
        }

        public static ICommand OpenAttachmentCommand { get; private set; }

        public static ICommand SaveAttachmentCommand { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfAttachmentsViewerCommands.<>c <>9 = new PdfAttachmentsViewerCommands.<>c();
            public static Predicate<DependencyObject> <>9__9_0;
            public static Func<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings> <>9__9_1;
            public static Func<PdfAttachmentsViewerSettings, ICommand> <>9__9_2;
            public static Predicate<DependencyObject> <>9__10_0;
            public static Func<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings> <>9__10_1;
            public static Predicate<DependencyObject> <>9__11_0;
            public static Func<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings> <>9__11_1;
            public static Func<PdfAttachmentsViewerSettings, ICommand> <>9__11_2;
            public static Predicate<DependencyObject> <>9__12_0;
            public static Func<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings> <>9__12_1;

            internal void <.cctor>b__8_0(object d, ExecutedRoutedEventArgs e)
            {
                PdfAttachmentsViewerCommands.ExecuteOpenAttachmentCommand((TableView) d, e);
            }

            internal void <.cctor>b__8_1(object d, CanExecuteRoutedEventArgs e)
            {
                PdfAttachmentsViewerCommands.CanExecuteOpenAttachmentCommand((TableView) d, e);
            }

            internal void <.cctor>b__8_2(object d, ExecutedRoutedEventArgs e)
            {
                PdfAttachmentsViewerCommands.ExecuteSaveAttachmentCommand((TableView) d, e);
            }

            internal void <.cctor>b__8_3(object d, CanExecuteRoutedEventArgs e)
            {
                PdfAttachmentsViewerCommands.CanExecuteSaveAttachmentCommand((TableView) d, e);
            }

            internal bool <CanExecuteOpenAttachmentCommand>b__9_0(DependencyObject o) => 
                o is PdfAttachmentsViewerControl;

            internal PdfAttachmentsViewerSettings <CanExecuteOpenAttachmentCommand>b__9_1(PdfAttachmentsViewerControl x) => 
                x.ActualSettings;

            internal ICommand <CanExecuteOpenAttachmentCommand>b__9_2(PdfAttachmentsViewerSettings x) => 
                x.OpenAttachmentCommand;

            internal bool <CanExecuteSaveAttachmentCommand>b__11_0(DependencyObject o) => 
                o is PdfAttachmentsViewerControl;

            internal PdfAttachmentsViewerSettings <CanExecuteSaveAttachmentCommand>b__11_1(PdfAttachmentsViewerControl x) => 
                x.ActualSettings;

            internal ICommand <CanExecuteSaveAttachmentCommand>b__11_2(PdfAttachmentsViewerSettings x) => 
                x.SaveAttachmentCommand;

            internal bool <ExecuteOpenAttachmentCommand>b__10_0(DependencyObject o) => 
                o is PdfAttachmentsViewerControl;

            internal PdfAttachmentsViewerSettings <ExecuteOpenAttachmentCommand>b__10_1(PdfAttachmentsViewerControl x) => 
                x.ActualSettings;

            internal bool <ExecuteSaveAttachmentCommand>b__12_0(DependencyObject o) => 
                o is PdfAttachmentsViewerControl;

            internal PdfAttachmentsViewerSettings <ExecuteSaveAttachmentCommand>b__12_1(PdfAttachmentsViewerControl x) => 
                x.ActualSettings;
        }
    }
}

