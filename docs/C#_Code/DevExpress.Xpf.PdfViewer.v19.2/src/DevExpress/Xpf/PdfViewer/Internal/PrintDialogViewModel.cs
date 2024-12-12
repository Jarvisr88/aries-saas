namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Pdf.Drawing;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.PdfViewer;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interop;

    public class PrintDialogViewModel : ViewModelBase
    {
        private static ReflectionHelper reflectionHelper = new ReflectionHelper();
        private bool hasValidationError;

        public PrintDialogViewModel(PdfPrintDialogViewModel printDialogViewModel)
        {
            this.PreferencesCommand = DelegateCommandFactory.Create(new Action(this.OnPreferencesClick));
            this.FileSelectCommand = DelegateCommandFactory.Create(new Action(this.OnFileSelectClick));
            this.PdfViewModel = printDialogViewModel;
        }

        private void OnFileSelectClick()
        {
            SaveFileDialogService service = new SaveFileDialogService {
                DefaultExt = PdfViewerLocalizer.GetString(PdfViewerStringId.PrintFileExtension),
                Filter = PdfViewerLocalizer.GetString(PdfViewerStringId.PrintFileFilter),
                OverwritePrompt = false
            };
            if (service.ShowDialog(null, null))
            {
                this.PdfViewModel.PrintFileName = service.GetFullFileName();
            }
        }

        private void OnHasValidationErrorChanged()
        {
            this.PdfViewModel.EnableToPrint = !this.HasValidationError;
        }

        private void OnPreferencesClick()
        {
            try
            {
                Func<HwndSource, IntPtr> selector = <>c.<>9__23_0;
                if (<>c.<>9__23_0 == null)
                {
                    Func<HwndSource, IntPtr> local1 = <>c.<>9__23_0;
                    selector = <>c.<>9__23_0 = x => x.Handle;
                }
                IntPtr windowHandle = PresentationSource.CurrentSources.OfType<HwndSource>().Select<HwndSource, IntPtr>(selector).LastOrDefault<IntPtr>();
                this.PdfViewModel.Do<PdfPrintDialogViewModel>(x => x.ShowPreferences(windowHandle));
            }
            catch
            {
            }
        }

        public ICommand PreferencesCommand { get; private set; }

        public ICommand FileSelectCommand { get; private set; }

        public ICommand PrintCommand { get; private set; }

        public PdfPrintDialogViewModel PdfViewModel { get; private set; }

        public bool HasValidationError
        {
            get => 
                this.hasValidationError;
            set => 
                base.SetProperty<bool>(ref this.hasValidationError, value, System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintDialogViewModel)), (MethodInfo) methodof(PrintDialogViewModel.get_HasValidationError)), new ParameterExpression[0]), new Action(this.OnHasValidationErrorChanged));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PrintDialogViewModel.<>c <>9 = new PrintDialogViewModel.<>c();
            public static Func<HwndSource, IntPtr> <>9__23_0;

            internal IntPtr <OnPreferencesClick>b__23_0(HwndSource x) => 
                x.Handle;
        }
    }
}

