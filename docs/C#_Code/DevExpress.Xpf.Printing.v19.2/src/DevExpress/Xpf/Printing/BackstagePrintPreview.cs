namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing.PreviewControl;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class BackstagePrintPreview : Control<BackstagePrintPreview>
    {
        private DocumentPreviewControl preview;
        public static readonly DependencyPropertyKey ActualPreviewPropertyKey;
        public static readonly DependencyProperty ActualPreviewProperty;
        public static readonly DependencyProperty DocumentSourceProperty;
        public static readonly DependencyProperty PrintOptionsProperty;
        public static readonly DependencyPropertyKey PrintOptionsPropertyKey;
        public static readonly DependencyProperty PrintOptionsTemplateProperty;
        public static readonly DependencyProperty ShowPreviewProperty;
        public static readonly DependencyProperty ShowPrintSettingsProperty;
        public static readonly DependencyProperty CustomSettingsProperty;
        public static readonly DependencyProperty CustomSettingsTemplateProperty;
        public static readonly DependencyProperty StatusBarTemplateProperty;
        public static readonly DependencyProperty EnableContinuousScrollingProperty;
        public static readonly DependencyProperty ZoomFactorProperty;
        public static readonly DependencyProperty ZoomModeProperty;

        static BackstagePrintPreview()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(FrameworkElement), "d");
            System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BackstagePrintPreview), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<BackstagePrintPreview> registrator1 = DependencyPropertyRegistrator<BackstagePrintPreview>.New().RegisterAttachedReadOnly<FrameworkElement, BackstagePrintPreview>(System.Linq.Expressions.Expression.Lambda<Func<FrameworkElement, BackstagePrintPreview>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(BackstagePrintPreview.GetActualPreview), arguments), parameters), out ActualPreviewPropertyKey, out ActualPreviewProperty, null, 0x20).Register<object>(System.Linq.Expressions.Expression.Lambda<Func<BackstagePrintPreview, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BackstagePrintPreview.get_DocumentSource)), expressionArray3), out DocumentSourceProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BackstagePrintPreview), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<BackstagePrintPreview> registrator2 = registrator1.RegisterReadOnly<DevExpress.Xpf.Printing.PrintOptions>(System.Linq.Expressions.Expression.Lambda<Func<BackstagePrintPreview, DevExpress.Xpf.Printing.PrintOptions>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BackstagePrintPreview.get_PrintOptions)), expressionArray4), out PrintOptionsPropertyKey, out PrintOptionsProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BackstagePrintPreview), "d");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<BackstagePrintPreview> registrator3 = registrator2.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<BackstagePrintPreview, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BackstagePrintPreview.get_PrintOptionsTemplate)), expressionArray5), out PrintOptionsTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BackstagePrintPreview), "d");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<BackstagePrintPreview> registrator4 = registrator3.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<BackstagePrintPreview, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BackstagePrintPreview.get_ShowPreview)), expressionArray6), out ShowPreviewProperty, true, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BackstagePrintPreview), "d");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<BackstagePrintPreview> registrator5 = registrator4.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<BackstagePrintPreview, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BackstagePrintPreview.get_ShowPrintSettings)), expressionArray7), out ShowPrintSettingsProperty, true, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BackstagePrintPreview), "d");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<BackstagePrintPreview> registrator6 = registrator5.Register<object>(System.Linq.Expressions.Expression.Lambda<Func<BackstagePrintPreview, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BackstagePrintPreview.get_CustomSettings)), expressionArray8), out CustomSettingsProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BackstagePrintPreview), "d");
            ParameterExpression[] expressionArray9 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<BackstagePrintPreview> registrator7 = registrator6.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<BackstagePrintPreview, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BackstagePrintPreview.get_CustomSettingsTemplate)), expressionArray9), out CustomSettingsTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BackstagePrintPreview), "d");
            ParameterExpression[] expressionArray10 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<BackstagePrintPreview> registrator8 = registrator7.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<BackstagePrintPreview, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BackstagePrintPreview.get_EnableContinuousScrolling)), expressionArray10), out EnableContinuousScrollingProperty, false, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BackstagePrintPreview), "d");
            ParameterExpression[] expressionArray11 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<BackstagePrintPreview> registrator9 = registrator8.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<BackstagePrintPreview, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BackstagePrintPreview.get_StatusBarTemplate)), expressionArray11), out StatusBarTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BackstagePrintPreview), "d");
            ParameterExpression[] expressionArray12 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<BackstagePrintPreview> registrator10 = registrator9.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<BackstagePrintPreview, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BackstagePrintPreview.get_ZoomFactor)), expressionArray12), out ZoomFactorProperty, 1.0, d => d.OnZoomChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BackstagePrintPreview), "d");
            ParameterExpression[] expressionArray13 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator10.Register<DevExpress.Xpf.DocumentViewer.ZoomMode>(System.Linq.Expressions.Expression.Lambda<Func<BackstagePrintPreview, DevExpress.Xpf.DocumentViewer.ZoomMode>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BackstagePrintPreview.get_ZoomMode)), expressionArray13), out ZoomModeProperty, DevExpress.Xpf.DocumentViewer.ZoomMode.PageLevel, d => d.OnZoomChanged(), frameworkOptions);
        }

        public BackstagePrintPreview()
        {
            SetActualPreview(this, this);
            this.PrintCommand = DelegateCommandFactory.Create(new Action(this.Print), new Func<bool>(this.CanPrint));
        }

        internal void AttachPreview(DocumentPreviewControl preview)
        {
            Guard.ArgumentNotNull(preview, "preview");
            this.preview.Do<DocumentPreviewControl>(delegate (DocumentPreviewControl x) {
                x.DocumentChanged -= new RoutedEventHandler(this.OnDocumentChanged);
                x.DocumentLoaded -= new RoutedEventHandler(this.OnDocumentLoaded);
            });
            preview.DocumentChanged += new RoutedEventHandler(this.OnDocumentChanged);
            preview.DocumentLoaded += new RoutedEventHandler(this.OnDocumentLoaded);
            Func<IDocumentViewModel, bool> evaluator = <>c.<>9__57_1;
            if (<>c.<>9__57_1 == null)
            {
                Func<IDocumentViewModel, bool> local1 = <>c.<>9__57_1;
                evaluator = <>c.<>9__57_1 = x => x.IsLoaded;
            }
            if (preview.Document.Return<IDocumentViewModel, bool>(evaluator, <>c.<>9__57_2 ??= () => false))
            {
                this.EnsureDocumentModel();
            }
        }

        private bool CanPrint()
        {
            Func<DevExpress.Xpf.Printing.PrintOptions, bool> evaluator = <>c.<>9__65_0;
            if (<>c.<>9__65_0 == null)
            {
                Func<DevExpress.Xpf.Printing.PrintOptions, bool> local1 = <>c.<>9__65_0;
                evaluator = <>c.<>9__65_0 = x => x.IsValid;
            }
            return this.PrintOptions.Return<DevExpress.Xpf.Printing.PrintOptions, bool>(evaluator, (<>c.<>9__65_1 ??= () => false));
        }

        private DevExpress.Xpf.Printing.PrintOptions CreatePrintOptionsInternal() => 
            ViewModelSource.Create<DevExpress.Xpf.Printing.PrintOptions>(this.GetPrintOptionsConstructorExpression());

        private void EnsureDocumentModel()
        {
            this.PrintOptions = this.CreatePrintOptionsInternal();
            this.PrintOptions.SetDocumentModel(this.preview.Document);
        }

        public static BackstagePrintPreview GetActualPreview(FrameworkElement d) => 
            (BackstagePrintPreview) d.GetValue(ActualPreviewProperty);

        protected virtual Expression<Func<DevExpress.Xpf.Printing.PrintOptions>> GetPrintOptionsConstructorExpression() => 
            System.Linq.Expressions.Expression.Lambda<Func<DevExpress.Xpf.Printing.PrintOptions>>(System.Linq.Expressions.Expression.New(typeof(DevExpress.Xpf.Printing.PrintOptions)), new ParameterExpression[0]);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.preview = base.GetTemplateChild("PART_Preview") as DocumentPreviewControl;
            this.AttachPreview(this.preview);
        }

        private void OnDocumentChanged(object sender, RoutedEventArgs e)
        {
            this.PrintOptions.Do<DevExpress.Xpf.Printing.PrintOptions>(options => options.SetDocumentModel(this.preview.Document));
        }

        private void OnDocumentLoaded(object sender, RoutedEventArgs e)
        {
            this.PrintOptions.Do<DevExpress.Xpf.Printing.PrintOptions>(options => options.SetDocumentModel(this.preview.Document));
        }

        private void OnZoomChanged()
        {
        }

        private void Print()
        {
            this.PrintOptions.SavePrinterSettings();
            new DocumentPublishEngine(this.preview.Document.PrintingSystem).Print(this.PrintOptions.PrintDocument);
        }

        private static void SetActualPreview(FrameworkElement d, BackstagePrintPreview control)
        {
            d.SetValue(ActualPreviewPropertyKey, control);
        }

        public double ZoomFactor
        {
            get => 
                (double) base.GetValue(ZoomFactorProperty);
            set => 
                base.SetValue(ZoomFactorProperty, value);
        }

        public DevExpress.Xpf.DocumentViewer.ZoomMode ZoomMode
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.ZoomMode) base.GetValue(ZoomModeProperty);
            set => 
                base.SetValue(ZoomModeProperty, value);
        }

        public object DocumentSource
        {
            get => 
                base.GetValue(DocumentSourceProperty);
            set => 
                base.SetValue(DocumentSourceProperty, value);
        }

        public DevExpress.Xpf.Printing.PrintOptions PrintOptions
        {
            get => 
                (DevExpress.Xpf.Printing.PrintOptions) base.GetValue(PrintOptionsProperty);
            private set => 
                base.SetValue(PrintOptionsPropertyKey, value);
        }

        public DataTemplate PrintOptionsTemplate
        {
            get => 
                (DataTemplate) base.GetValue(PrintOptionsTemplateProperty);
            set => 
                base.SetValue(PrintOptionsTemplateProperty, value);
        }

        public bool ShowPreview
        {
            get => 
                (bool) base.GetValue(ShowPreviewProperty);
            set => 
                base.SetValue(ShowPreviewProperty, value);
        }

        public object CustomSettings
        {
            get => 
                base.GetValue(CustomSettingsProperty);
            set => 
                base.SetValue(CustomSettingsProperty, value);
        }

        public DataTemplate CustomSettingsTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CustomSettingsProperty);
            set => 
                base.SetValue(CustomSettingsProperty, value);
        }

        public bool ShowPrintSettings
        {
            get => 
                (bool) base.GetValue(ShowPrintSettingsProperty);
            set => 
                base.SetValue(ShowPrintSettingsProperty, value);
        }

        public DataTemplate StatusBarTemplate
        {
            get => 
                (DataTemplate) base.GetValue(StatusBarTemplateProperty);
            set => 
                base.SetValue(StatusBarTemplateProperty, value);
        }

        public bool EnableContinuousScrolling
        {
            get => 
                (bool) base.GetValue(EnableContinuousScrollingProperty);
            set => 
                base.SetValue(EnableContinuousScrollingProperty, value);
        }

        public ICommand PrintCommand { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BackstagePrintPreview.<>c <>9 = new BackstagePrintPreview.<>c();
            public static Func<IDocumentViewModel, bool> <>9__57_1;
            public static Func<bool> <>9__57_2;
            public static Func<PrintOptions, bool> <>9__65_0;
            public static Func<bool> <>9__65_1;

            internal void <.cctor>b__54_0(BackstagePrintPreview d)
            {
                d.OnZoomChanged();
            }

            internal void <.cctor>b__54_1(BackstagePrintPreview d)
            {
                d.OnZoomChanged();
            }

            internal bool <AttachPreview>b__57_1(IDocumentViewModel x) => 
                x.IsLoaded;

            internal bool <AttachPreview>b__57_2() => 
                false;

            internal bool <CanPrint>b__65_0(PrintOptions x) => 
                x.IsValid;

            internal bool <CanPrint>b__65_1() => 
                false;
        }
    }
}

