namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class PdfOutlinesViewerSettings : DocumentMapSettings
    {
        public static readonly DependencyProperty HideOutlinesViewerProperty;
        public static readonly DependencyProperty OutlinesViewerStateProperty;
        public static readonly DependencyProperty OutlinesViewerInitialStateProperty;
        public static readonly DependencyProperty OutlinesViewerStyleProperty;
        public static readonly DependencyProperty OutlinesViewerPanelStyleProperty;
        public static readonly DependencyProperty ApplyOutlinesForegroundProperty;

        static PdfOutlinesViewerSettings()
        {
            Type ownerType = typeof(PdfOutlinesViewerSettings);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfOutlinesViewerSettings), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            HideOutlinesViewerProperty = DependencyPropertyRegistrator.Register<PdfOutlinesViewerSettings, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfOutlinesViewerSettings, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfOutlinesViewerSettings.get_HideOutlinesViewer)), parameters), false, (control, value, newValue) => control.HideOutlinesViewerChanged(value, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfOutlinesViewerSettings), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            OutlinesViewerStateProperty = DependencyPropertyRegistrator.Register<PdfOutlinesViewerSettings, PdfOutlinesViewerState>(System.Linq.Expressions.Expression.Lambda<Func<PdfOutlinesViewerSettings, PdfOutlinesViewerState>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfOutlinesViewerSettings.get_OutlinesViewerState)), expressionArray2), PdfOutlinesViewerState.Collapsed, (settings, value, newValue) => settings.OutlinesViewerStateChanged(value, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfOutlinesViewerSettings), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            PdfOutlinesViewerState? defaultValue = null;
            OutlinesViewerInitialStateProperty = DependencyPropertyRegistrator.Register<PdfOutlinesViewerSettings, PdfOutlinesViewerState?>(System.Linq.Expressions.Expression.Lambda<Func<PdfOutlinesViewerSettings, PdfOutlinesViewerState?>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfOutlinesViewerSettings.get_OutlinesViewerInitialState)), expressionArray3), defaultValue, (control, value, newValue) => control.OutlinesViewerInitialStateChanged(value, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfOutlinesViewerSettings), "owner");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            ApplyOutlinesForegroundProperty = DependencyPropertyRegistrator.Register<PdfOutlinesViewerSettings, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfOutlinesViewerSettings, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfOutlinesViewerSettings.get_ApplyOutlinesForeground)), expressionArray4), false, (control, value, newValue) => control.ApplyOutlinesForegroundChanged(value, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfOutlinesViewerSettings), "owner");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            OutlinesViewerStyleProperty = DependencyPropertyRegistrator.Register<PdfOutlinesViewerSettings, Style>(System.Linq.Expressions.Expression.Lambda<Func<PdfOutlinesViewerSettings, Style>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfOutlinesViewerSettings.get_OutlinesViewerStyle)), expressionArray5), null);
            OutlinesViewerPanelStyleProperty = DependencyProperty.Register("OutlinesViewerPanelStyle", typeof(PdfOutlinesViewerPanelStyle), ownerType, new FrameworkPropertyMetadata(PdfOutlinesViewerPanelStyle.Tab, (d, e) => ((PdfOutlinesViewerSettings) d).OutlinesViewerStyleChanged((PdfOutlinesViewerPanelStyle) e.OldValue, (PdfOutlinesViewerPanelStyle) e.NewValue)));
        }

        protected virtual void ApplyOutlinesForegroundChanged(bool oldValue, bool newValue)
        {
            base.RaiseInvalidate();
        }

        protected virtual bool CanPrint(object parameter, bool isRange)
        {
            Func<PdfViewerControl, IPdfDocument> evaluator = <>c.<>9__41_0;
            if (<>c.<>9__41_0 == null)
            {
                Func<PdfViewerControl, IPdfDocument> local1 = <>c.<>9__41_0;
                evaluator = <>c.<>9__41_0 = x => x.Document;
            }
            Func<IPdfDocument, bool> func2 = <>c.<>9__41_1;
            if (<>c.<>9__41_1 == null)
            {
                Func<IPdfDocument, bool> local2 = <>c.<>9__41_1;
                func2 = <>c.<>9__41_1 = x => x.IsLoaded;
            }
            if (!this.Owner.With<PdfViewerControl, IPdfDocument>(evaluator).If<IPdfDocument>(func2).ReturnSuccess<IPdfDocument>())
            {
                return false;
            }
            Func<IEnumerable, List<PdfOutlineTreeListItem>> func3 = <>c.<>9__41_2;
            if (<>c.<>9__41_2 == null)
            {
                Func<IEnumerable, List<PdfOutlineTreeListItem>> local3 = <>c.<>9__41_2;
                func3 = <>c.<>9__41_2 = x => x.Cast<PdfOutlineTreeListItem>().ToList<PdfOutlineTreeListItem>();
            }
            List<PdfOutlineTreeListItem> selectedItems = (parameter as IEnumerable).With<IEnumerable, List<PdfOutlineTreeListItem>>(func3);
            return this.Owner.Document.CanPrintPages(selectedItems, isRange);
        }

        protected virtual void HideOutlinesViewerChanged(bool oldValue, bool newValue)
        {
            this.Owner.Do<PdfViewerControl>(x => x.UpdateHasOutlines(newValue));
        }

        protected virtual void OutlinesViewerInitialStateChanged(PdfOutlinesViewerState? oldValue, PdfOutlinesViewerState? newValue)
        {
        }

        protected virtual void OutlinesViewerStateChanged(PdfOutlinesViewerState oldValue, PdfOutlinesViewerState newValue)
        {
        }

        protected virtual void OutlinesViewerStyleChanged(PdfOutlinesViewerPanelStyle oldValue, PdfOutlinesViewerPanelStyle newValue)
        {
            this.Owner.Do<PdfViewerControl>(x => x.NavigationPanelsLayout = (NavigationPanelsLayout) newValue);
        }

        protected virtual void Print(object parameter, bool isRange)
        {
            Func<IEnumerable, List<PdfOutlineTreeListItem>> evaluator = <>c.<>9__42_0;
            if (<>c.<>9__42_0 == null)
            {
                Func<IEnumerable, List<PdfOutlineTreeListItem>> local1 = <>c.<>9__42_0;
                evaluator = <>c.<>9__42_0 = x => x.Cast<PdfOutlineTreeListItem>().ToList<PdfOutlineTreeListItem>();
            }
            List<PdfOutlineTreeListItem> selectedItems = (parameter as IEnumerable).With<IEnumerable, List<PdfOutlineTreeListItem>>(evaluator);
            if (selectedItems != null)
            {
                this.Owner.Print(this.Owner.Document.CalcPrintPages(selectedItems, isRange));
            }
        }

        protected virtual void UpdateOutlinesViewerCurrentState()
        {
            PdfOutlinesViewerState? outlinesViewerInitialState = this.OutlinesViewerInitialState;
            this.OutlinesViewerState = (outlinesViewerInitialState != null) ? outlinesViewerInitialState.GetValueOrDefault() : this.OutlinesViewerState;
        }

        protected override void UpdatePropertiesInternal()
        {
            base.UpdatePropertiesInternal();
            Func<PdfViewerControl, IPdfDocument> evaluator = <>c.<>9__39_0;
            if (<>c.<>9__39_0 == null)
            {
                Func<PdfViewerControl, IPdfDocument> local1 = <>c.<>9__39_0;
                evaluator = <>c.<>9__39_0 = x => x.Document;
            }
            Func<IPdfDocument, bool> func2 = <>c.<>9__39_1;
            if (<>c.<>9__39_1 == null)
            {
                Func<IPdfDocument, bool> local2 = <>c.<>9__39_1;
                func2 = <>c.<>9__39_1 = x => x.IsLoaded;
            }
            if (!this.Owner.With<PdfViewerControl, IPdfDocument>(evaluator).If<IPdfDocument>(func2).ReturnSuccess<IPdfDocument>())
            {
                base.Source = null;
                base.GoToCommand = null;
                this.PrintCommand = null;
                this.PrintSectionCommand = null;
            }
            else
            {
                IPdfDocument input = this.Owner.Document;
                Func<IPdfDocument, ObservableCollection<PdfOutlineTreeListItem>> func3 = <>c.<>9__39_2;
                if (<>c.<>9__39_2 == null)
                {
                    Func<IPdfDocument, ObservableCollection<PdfOutlineTreeListItem>> local3 = <>c.<>9__39_2;
                    func3 = <>c.<>9__39_2 = x => new ObservableCollection<PdfOutlineTreeListItem>(x.CreateOutlines());
                }
                this.Source = new ReadOnlyObservableCollection<PdfOutlineTreeListItem>(input.Return<IPdfDocument, ObservableCollection<PdfOutlineTreeListItem>>(func3, <>c.<>9__39_3 ??= () => new ObservableCollection<PdfOutlineTreeListItem>()));
                base.Source.ForEach<object>(x => ((PdfOutlineTreeListItem) x).UseForeColor = this.ApplyOutlinesForeground);
                base.GoToCommand = this.Owner.ActualCommandProvider.NavigateCommand;
                this.PrintCommand = DelegateCommandFactory.Create<object>(parameter => this.Print(parameter, false), parameter => this.CanPrint(parameter, false));
                this.PrintSectionCommand = DelegateCommandFactory.Create<object>(parameter => this.Print(parameter, true), parameter => this.CanPrint(parameter, true));
                if (this.OutlinesViewerInitialState != null)
                {
                    this.UpdateOutlinesViewerCurrentState();
                }
            }
        }

        [Obsolete("Use the PdfViewerControl.NavigationPanelsStyle property instead.")]
        public PdfOutlinesViewerPanelStyle OutlinesViewerPanelStyle
        {
            get => 
                (PdfOutlinesViewerPanelStyle) base.GetValue(OutlinesViewerPanelStyleProperty);
            set => 
                base.SetValue(OutlinesViewerPanelStyleProperty, value);
        }

        public bool ApplyOutlinesForeground
        {
            get => 
                (bool) base.GetValue(ApplyOutlinesForegroundProperty);
            set => 
                base.SetValue(ApplyOutlinesForegroundProperty, value);
        }

        public PdfOutlinesViewerState OutlinesViewerState
        {
            get => 
                (PdfOutlinesViewerState) base.GetValue(OutlinesViewerStateProperty);
            set => 
                base.SetValue(OutlinesViewerStateProperty, value);
        }

        public PdfOutlinesViewerState? OutlinesViewerInitialState
        {
            get => 
                (PdfOutlinesViewerState?) base.GetValue(OutlinesViewerInitialStateProperty);
            set => 
                base.SetValue(OutlinesViewerInitialStateProperty, value);
        }

        public Style OutlinesViewerStyle
        {
            get => 
                (Style) base.GetValue(OutlinesViewerStyleProperty);
            set => 
                base.SetValue(OutlinesViewerStyleProperty, value);
        }

        public bool HideOutlinesViewer
        {
            get => 
                (bool) base.GetValue(HideOutlinesViewerProperty);
            set => 
                base.SetValue(HideOutlinesViewerProperty, value);
        }

        public ICommand PrintCommand { get; protected set; }

        public ICommand PrintSectionCommand { get; protected set; }

        private PdfViewerControl Owner =>
            base.Owner as PdfViewerControl;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfOutlinesViewerSettings.<>c <>9 = new PdfOutlinesViewerSettings.<>c();
            public static Func<PdfViewerControl, IPdfDocument> <>9__39_0;
            public static Func<IPdfDocument, bool> <>9__39_1;
            public static Func<IPdfDocument, ObservableCollection<PdfOutlineTreeListItem>> <>9__39_2;
            public static Func<ObservableCollection<PdfOutlineTreeListItem>> <>9__39_3;
            public static Func<PdfViewerControl, IPdfDocument> <>9__41_0;
            public static Func<IPdfDocument, bool> <>9__41_1;
            public static Func<IEnumerable, List<PdfOutlineTreeListItem>> <>9__41_2;
            public static Func<IEnumerable, List<PdfOutlineTreeListItem>> <>9__42_0;

            internal void <.cctor>b__6_0(PdfOutlinesViewerSettings control, bool value, bool newValue)
            {
                control.HideOutlinesViewerChanged(value, newValue);
            }

            internal void <.cctor>b__6_1(PdfOutlinesViewerSettings settings, PdfOutlinesViewerState value, PdfOutlinesViewerState newValue)
            {
                settings.OutlinesViewerStateChanged(value, newValue);
            }

            internal void <.cctor>b__6_2(PdfOutlinesViewerSettings control, PdfOutlinesViewerState? value, PdfOutlinesViewerState? newValue)
            {
                control.OutlinesViewerInitialStateChanged(value, newValue);
            }

            internal void <.cctor>b__6_3(PdfOutlinesViewerSettings control, bool value, bool newValue)
            {
                control.ApplyOutlinesForegroundChanged(value, newValue);
            }

            internal void <.cctor>b__6_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PdfOutlinesViewerSettings) d).OutlinesViewerStyleChanged((PdfOutlinesViewerPanelStyle) e.OldValue, (PdfOutlinesViewerPanelStyle) e.NewValue);
            }

            internal IPdfDocument <CanPrint>b__41_0(PdfViewerControl x) => 
                x.Document;

            internal bool <CanPrint>b__41_1(IPdfDocument x) => 
                x.IsLoaded;

            internal List<PdfOutlineTreeListItem> <CanPrint>b__41_2(IEnumerable x) => 
                x.Cast<PdfOutlineTreeListItem>().ToList<PdfOutlineTreeListItem>();

            internal List<PdfOutlineTreeListItem> <Print>b__42_0(IEnumerable x) => 
                x.Cast<PdfOutlineTreeListItem>().ToList<PdfOutlineTreeListItem>();

            internal IPdfDocument <UpdatePropertiesInternal>b__39_0(PdfViewerControl x) => 
                x.Document;

            internal bool <UpdatePropertiesInternal>b__39_1(IPdfDocument x) => 
                x.IsLoaded;

            internal ObservableCollection<PdfOutlineTreeListItem> <UpdatePropertiesInternal>b__39_2(IPdfDocument x) => 
                new ObservableCollection<PdfOutlineTreeListItem>(x.CreateOutlines());

            internal ObservableCollection<PdfOutlineTreeListItem> <UpdatePropertiesInternal>b__39_3() => 
                new ObservableCollection<PdfOutlineTreeListItem>();
        }
    }
}

