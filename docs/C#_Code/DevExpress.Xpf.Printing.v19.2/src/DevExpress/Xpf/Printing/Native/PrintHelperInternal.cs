namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.Themes;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public static class PrintHelperInternal
    {
        private static T FindResource<T>(NewDocumentViewerThemeKeys key) where T: class
        {
            NewDocumentViewerThemeKeyExtension resourceKey = new NewDocumentViewerThemeKeyExtension();
            resourceKey.ResourceKey = key;
            return ((new FrameworkElement().TryFindResource(resourceKey) ?? null) as T);
        }

        private static IEnumerable<UICommand> GetDialogCommands(ICommand okCommand)
        {
            UICommand command1 = new UICommand();
            command1.Id = MessageBoxResult.OK;
            command1.Caption = PrintingLocalizer.GetString(PrintingStringId.OK);
            command1.IsCancel = false;
            command1.IsDefault = true;
            command1.Command = okCommand;
            UICommand command = command1;
            UICommand command3 = new UICommand();
            command3.Id = MessageBoxResult.Cancel;
            command3.Caption = PrintingLocalizer.GetString(PrintingStringId.Cancel);
            command3.IsCancel = true;
            command3.IsDefault = false;
            UICommand command2 = command3;
            return new UICommand[] { command, command2 };
        }

        public static bool? Print(PrintingSystemBase printingSystem, Window owner)
        {
            Guard.ArgumentNotNull(printingSystem, "printingSystem");
            if (printingSystem.PageCount == 0)
            {
                throw new InvalidOperationException("A document must contain at least one page to be printed.");
            }
            PrintOptionsViewModel printOptions = PrintOptionsViewModel.Create(printingSystem);
            DelegateCommand okCommand = DelegateCommandFactory.Create(delegate {
                new DocumentPublishEngine(printingSystem).Print(printOptions);
            }, () => printOptions.IsValid);
            DXDialogWindow window1 = new DXDialogWindow(PrintingLocalizer.GetString(PrintingStringId.Print), GetDialogCommands(okCommand));
            window1.Owner = owner;
            DXDialogWindow window = window1;
            window.Style = FindResource<Style>(NewDocumentViewerThemeKeys.ExportOptionsDialogStyle);
            window.ContentTemplate = FindResource<DataTemplate>(NewDocumentViewerThemeKeys.PrintDialogTemplate);
            window.WindowStartupLocation = (owner != null) ? WindowStartupLocation.CenterOwner : WindowStartupLocation.CenterScreen;
            window.Content = printOptions;
            UICommand command1 = window.ShowDialogWindow();
            if (<>c.<>9__0_2 == null)
            {
                UICommand local1 = window.ShowDialogWindow();
                command1 = (UICommand) (<>c.<>9__0_2 = x => new bool?(!x.IsCancel));
            }
            return ((UICommand) <>c.<>9__0_2).Return<UICommand, bool?>(((Func<UICommand, bool?>) command1), (<>c.<>9__0_3 ??= ((Func<bool?>) (() => null))));
        }

        public static void PrintDirect(PrintingSystemBase printingSystem, string printerName = null)
        {
            Guard.ArgumentNotNull(printingSystem, "printingSystem");
            if (printingSystem.PageCount == 0)
            {
                throw new InvalidOperationException("A document must contain at least one page to be printed.");
            }
            new DocumentPublishEngine(printingSystem).PrintDirect(printerName);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PrintHelperInternal.<>c <>9 = new PrintHelperInternal.<>c();
            public static Func<UICommand, bool?> <>9__0_2;
            public static Func<bool?> <>9__0_3;

            internal bool? <Print>b__0_2(UICommand x) => 
                new bool?(!x.IsCancel);

            internal bool? <Print>b__0_3() => 
                null;
        }
    }
}

