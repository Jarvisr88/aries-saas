namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing.Themes;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Interop;
    using System.Windows.Threading;

    public class PrintProgressController : PrintController
    {
        private int pageNumber = 1;
        private readonly StandardPrintController standardController = new StandardPrintController();
        private readonly CancellationTokenSource cancelationTokenSource = new CancellationTokenSource();
        private Window owner;
        private System.Windows.Point center;

        [CompilerGenerated, DebuggerHidden]
        private void <>n__0(PrintDocument document, PrintPageEventArgs e)
        {
            base.OnEndPage(document, e);
        }

        [CompilerGenerated, DebuggerHidden]
        private void <>n__1(PrintDocument document, PrintEventArgs e)
        {
            base.OnEndPrint(document, e);
        }

        private void CenterSplashScreen(ProgressWindow window)
        {
            if (this.owner == null)
            {
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            else
            {
                window.WindowStartupLocation = WindowStartupLocation.Manual;
                double left = Math.Max((double) (this.center.X - (window.Width / 2.0)), (double) 0.0);
                double top = Math.Max((double) (this.center.Y - (window.Height / 2.0)), (double) 0.0);
                window.Left = left;
                window.Top = top;
                Trace.WriteLine($"status position: {window.Left}, {window.Top}");
                window.Dispatcher.BeginInvoke(delegate {
                    window.Left = left;
                    window.Top = top;
                }, DispatcherPriority.Input, new object[0]);
            }
        }

        private void DoWithTryCatch(Action tryAction, Action finalyAction = null)
        {
            try
            {
                tryAction();
            }
            catch
            {
                this.cancelationTokenSource.Cancel();
                if (DXSplashScreen.IsActive)
                {
                    DXSplashScreen.Close();
                }
                throw;
            }
            finally
            {
                Action<Action> action = <>c.<>9__16_0;
                if (<>c.<>9__16_0 == null)
                {
                    Action<Action> local2 = <>c.<>9__16_0;
                    action = <>c.<>9__16_0 = x => x();
                }
                finalyAction.Do<Action>(action);
            }
        }

        private System.Windows.Point GetOwnerCenter() => 
            ScreenHelper.GetScreenPoint(this.owner, new System.Windows.Point(this.owner.Width / 2.0, this.owner.Height / 2.0));

        private void InitializeOwner()
        {
            HwndSource input = PresentationSource.CurrentSources.OfType<HwndSource>().LastOrDefault<HwndSource>();
            Func<HwndSource, Window> evaluator = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<HwndSource, Window> local1 = <>c.<>9__9_0;
                evaluator = <>c.<>9__9_0 = x => x.RootVisual as Window;
            }
            this.owner = input.With<HwndSource, Window>(evaluator);
            if (this.owner != null)
            {
                this.center = this.GetOwnerCenter();
            }
        }

        public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
        {
            this.DoWithTryCatch(() => this.standardController.OnEndPage(document, e), delegate {
                this.pageNumber++;
                if (this.cancelationTokenSource.IsCancellationRequested)
                {
                    e.Cancel = true;
                }
                this.<>n__0(document, e);
            });
        }

        public override void OnEndPrint(PrintDocument document, PrintEventArgs e)
        {
            this.DoWithTryCatch(() => this.standardController.OnEndPrint(document, e), delegate {
                if (this.cancelationTokenSource.IsCancellationRequested)
                {
                    e.Cancel = true;
                }
                else
                {
                    this.cancelationTokenSource.Cancel();
                }
                this.SetOwnerEnabled(true);
                this.owner = null;
                if (DXSplashScreen.IsActive)
                {
                    DXSplashScreen.Close();
                }
                this.<>n__1(document, e);
            });
        }

        public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
        {
            base.OnStartPage(document, e);
            DispatcherHelper.DoEvents(DispatcherPriority.ApplicationIdle, 1);
            if (!this.cancelationTokenSource.IsCancellationRequested && DXSplashScreen.IsActive)
            {
                DXSplashScreen.CallSplashScreenMethod<ProgressWindow>(delegate (ProgressWindow x) {
                    x.ProgressViewModel.Progress = this.pageNumber;
                });
            }
            Graphics graphics = null;
            this.DoWithTryCatch(delegate {
                graphics = this.standardController.OnStartPage(document, e);
            }, delegate {
                if (this.cancelationTokenSource.IsCancellationRequested)
                {
                    e.Cancel = true;
                }
            });
            return graphics;
        }

        public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
        {
            <>c__DisplayClass6_0 class_;
            base.OnStartPrint(document, e);
            if (!document.PrinterSettings.IsValid)
            {
                throw new InvalidPrinterException(document.PrinterSettings);
            }
            this.DoWithTryCatch(delegate {
                this.standardController.OnStartPrint(document, e);
                if (SystemInformation.UserInteractive)
                {
                    Action<ProgressWindow> <>9__1;
                    this.InitializeOwner();
                    this.SetOwnerEnabled(false);
                    DXSplashScreen.Show<ProgressWindow>(WindowStartupLocation.CenterScreen, null, SplashScreenClosingMode.ManualOnly);
                    Action<ProgressWindow> action = <>9__1;
                    if (<>9__1 == null)
                    {
                        Action<ProgressWindow> local1 = <>9__1;
                        action = <>9__1 = delegate (ProgressWindow x) {
                            if (this.owner != null)
                            {
                                this.CenterSplashScreen(x);
                            }
                            System.Linq.Expressions.Expression[] expressionArray1 = new System.Linq.Expressions.Expression[] { System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(class_, typeof(<>c__DisplayClass6_0)), fieldof(<>c__DisplayClass6_0.document)), System.Linq.Expressions.Expression.Lambda<Action>(System.Linq.Expressions.Expression.Call(System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(this, typeof(PrintProgressController)), fieldof(PrintProgressController.cancelationTokenSource)), (MethodInfo) methodof(CancellationTokenSource.Cancel), new System.Linq.Expressions.Expression[0]), new ParameterExpression[0]) };
                            x.ProgressViewModel = ViewModelSource.Create<PrintProgressViewModel>(System.Linq.Expressions.Expression.Lambda<Func<PrintProgressViewModel>>(System.Linq.Expressions.Expression.New((ConstructorInfo) methodof(PrintProgressViewModel..ctor), (IEnumerable<System.Linq.Expressions.Expression>) expressionArray1), new ParameterExpression[0]));
                        };
                    }
                    DXSplashScreen.CallSplashScreenMethod<ProgressWindow>(action);
                }
            }, delegate {
                if (this.cancelationTokenSource.IsCancellationRequested)
                {
                    e.Cancel = true;
                }
            });
        }

        private void SetOwnerEnabled(bool enabled)
        {
            this.owner.Do<Window>(x => x.IsEnabled = enabled);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PrintProgressController.<>c <>9 = new PrintProgressController.<>c();
            public static Func<HwndSource, Window> <>9__9_0;
            public static Action<Action> <>9__16_0;

            internal void <DoWithTryCatch>b__16_0(Action x)
            {
                x();
            }

            internal Window <InitializeOwner>b__9_0(HwndSource x) => 
                x.RootVisual as Window;
        }

        [POCOViewModel]
        public class PrintProgressViewModel : BindableBase
        {
            private Action onCancel;
            private PrintDocument document;

            protected internal PrintProgressViewModel(PrintDocument document, Action onCancel)
            {
                this.onCancel = onCancel;
                this.document = document;
                this.Progress = 0;
            }

            public void Cancel()
            {
                this.onCancel();
            }

            protected void OnProgressChanged()
            {
                this.ProgressStatus = string.Format(PreviewStringId.SB_PageOfPages.GetString(), this.Progress, this.document.DocumentName);
            }

            public virtual int Progress { get; set; }

            public virtual string ProgressStatus { get; protected set; }
        }

        private class ProgressWindow : ThemedWindow, ISplashScreen
        {
            public static DependencyProperty ProgressViewModelProperty;

            static ProgressWindow()
            {
                ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PrintProgressController.ProgressWindow), "d");
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                FrameworkPropertyMetadataOptions? frameworkOptions = null;
                DependencyPropertyRegistrator<PrintProgressController.ProgressWindow>.New().Register<PrintProgressController.PrintProgressViewModel>(System.Linq.Expressions.Expression.Lambda<Func<PrintProgressController.ProgressWindow, PrintProgressController.PrintProgressViewModel>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PrintProgressController.ProgressWindow.get_ProgressViewModel)), parameters), out ProgressViewModelProperty, null, d => d.Content = d.ProgressViewModel, frameworkOptions);
            }

            public ProgressWindow()
            {
                base.MinWidth = 300.0;
                base.MinHeight = 50.0;
                base.Dispatcher.Invoke(delegate {
                    NewDocumentViewerThemeKeyExtension resourceKey = new NewDocumentViewerThemeKeyExtension();
                    resourceKey.ResourceKey = NewDocumentViewerThemeKeys.PrintStatusDialogTemplate;
                    base.ContentTemplate = new FrameworkElement().TryFindResource(resourceKey) as DataTemplate;
                });
                base.Title = PreviewStringId.Msg_Caption.GetString();
                base.WindowStyle = WindowStyle.ToolWindow;
                base.SizeToContent = SizeToContent.WidthAndHeight;
                base.Topmost = true;
                base.ShowActivated = false;
                base.ResizeMode = ResizeMode.NoResize;
                base.Loaded += new RoutedEventHandler(this.OnLoaded);
            }

            void ISplashScreen.CloseSplashScreen()
            {
                base.Close();
            }

            void ISplashScreen.Progress(double value)
            {
            }

            void ISplashScreen.SetProgressState(bool isIndeterminate)
            {
            }

            private void OnLoaded(object sender, RoutedEventArgs e)
            {
                base.UpdateLayout();
                base.Dispatcher.Invoke(delegate {
                    FrameworkElement element = LayoutHelper.FindElementByName(this, "PART_CloseButton");
                    if (element != null)
                    {
                        element.Name = "close_hiddenButton";
                        element.Visibility = Visibility.Collapsed;
                    }
                });
            }

            public PrintProgressController.PrintProgressViewModel ProgressViewModel
            {
                get => 
                    (PrintProgressController.PrintProgressViewModel) base.GetValue(ProgressViewModelProperty);
                set => 
                    base.SetValue(ProgressViewModelProperty, value);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly PrintProgressController.ProgressWindow.<>c <>9 = new PrintProgressController.ProgressWindow.<>c();

                internal void <.cctor>b__4_0(PrintProgressController.ProgressWindow d)
                {
                    d.Content = d.ProgressViewModel;
                }
            }
        }
    }
}

