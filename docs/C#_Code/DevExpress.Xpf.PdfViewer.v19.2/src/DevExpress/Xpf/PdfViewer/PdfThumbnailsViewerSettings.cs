namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf.Drawing;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.PdfViewer.Internal;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public class PdfThumbnailsViewerSettings : FrameworkElement
    {
        public static readonly DependencyProperty HideThumbnailsViewerProperty;
        public static readonly DependencyProperty ThumbnailsViewerStateProperty;
        public static readonly DependencyProperty ThumbnailsViewerInitialStateProperty;
        public static readonly DependencyProperty ThumbnailsViewerStyleProperty;
        private PdfViewerControl owner;

        public event EventHandler Invalidate;

        static PdfThumbnailsViewerSettings()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfThumbnailsViewerSettings), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            HideThumbnailsViewerProperty = DependencyPropertyRegistrator.Register<PdfThumbnailsViewerSettings, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfThumbnailsViewerSettings, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfThumbnailsViewerSettings.get_HideThumbnailsViewer)), parameters), false, (settings, value, newValue) => settings.HideThumbnailsViewerChanged(value, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfThumbnailsViewerSettings), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            ThumbnailsViewerStyleProperty = DependencyPropertyRegistrator.Register<PdfThumbnailsViewerSettings, Style>(System.Linq.Expressions.Expression.Lambda<Func<PdfThumbnailsViewerSettings, Style>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfThumbnailsViewerSettings.get_ThumbnailsViewerStyle)), expressionArray2), null);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfThumbnailsViewerSettings), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            ThumbnailsViewerStateProperty = DependencyPropertyRegistrator.Register<PdfThumbnailsViewerSettings, PdfThumbnailsViewerState>(System.Linq.Expressions.Expression.Lambda<Func<PdfThumbnailsViewerSettings, PdfThumbnailsViewerState>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfThumbnailsViewerSettings.get_ThumbnailsViewerState)), expressionArray3), PdfThumbnailsViewerState.Collapsed);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfThumbnailsViewerSettings), "owner");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            PdfThumbnailsViewerState? defaultValue = null;
            ThumbnailsViewerInitialStateProperty = DependencyPropertyRegistrator.Register<PdfThumbnailsViewerSettings, PdfThumbnailsViewerState?>(System.Linq.Expressions.Expression.Lambda<Func<PdfThumbnailsViewerSettings, PdfThumbnailsViewerState?>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfThumbnailsViewerSettings.get_ThumbnailsViewerInitialState)), expressionArray4), defaultValue);
        }

        protected internal virtual IDocument CreateThumbnailsDocument() => 
            new ThumbnailsDocumentViewModel();

        protected virtual void HideThumbnailsViewerChanged(bool oldValue, bool newValue)
        {
            this.Owner.Do<PdfViewerControl>(x => x.UpdateHasThumbnails(newValue));
        }

        protected internal virtual void Initialize(PdfViewerControl owner)
        {
            this.owner = owner;
            this.UpdateProperties();
        }

        protected internal void RaiseInvalidate()
        {
            this.Invalidate.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        protected internal virtual void Release()
        {
            this.owner = null;
        }

        protected virtual void ThumbnailsViewerInitialStateChanged(PdfThumbnailsViewerState? oldValue, PdfThumbnailsViewerState? newValue)
        {
        }

        protected virtual void ThumbnailsViewerStateChanged(PdfThumbnailsViewerState oldValue, PdfThumbnailsViewerState newValue)
        {
        }

        public virtual void UpdateProperties()
        {
            if (this.owner != null)
            {
                this.UpdatePropertiesInternal();
            }
        }

        protected virtual void UpdatePropertiesInternal()
        {
            if (this.ThumbnailsViewerInitialState != null)
            {
                this.UpdateThumbnailsViewerCurrentState();
            }
        }

        protected virtual void UpdateThumbnailsViewerCurrentState()
        {
            PdfThumbnailsViewerState? thumbnailsViewerInitialState = this.ThumbnailsViewerInitialState;
            this.ThumbnailsViewerState = (thumbnailsViewerInitialState != null) ? thumbnailsViewerInitialState.GetValueOrDefault() : this.ThumbnailsViewerState;
        }

        public PdfThumbnailsViewerState ThumbnailsViewerState
        {
            get => 
                (PdfThumbnailsViewerState) base.GetValue(ThumbnailsViewerStateProperty);
            set => 
                base.SetValue(ThumbnailsViewerStateProperty, value);
        }

        public PdfThumbnailsViewerState? ThumbnailsViewerInitialState
        {
            get => 
                (PdfThumbnailsViewerState?) base.GetValue(ThumbnailsViewerInitialStateProperty);
            set => 
                base.SetValue(ThumbnailsViewerInitialStateProperty, value);
        }

        public Style ThumbnailsViewerStyle
        {
            get => 
                (Style) base.GetValue(ThumbnailsViewerStyleProperty);
            set => 
                base.SetValue(ThumbnailsViewerStyleProperty, value);
        }

        public bool HideThumbnailsViewer
        {
            get => 
                (bool) base.GetValue(HideThumbnailsViewerProperty);
            set => 
                base.SetValue(HideThumbnailsViewerProperty, value);
        }

        protected internal PdfViewerControl Owner =>
            this.owner;

        protected internal IDocument Document
        {
            get
            {
                Func<PdfViewerControl, IPdfDocument> evaluator = <>c.<>9__24_0;
                if (<>c.<>9__24_0 == null)
                {
                    Func<PdfViewerControl, IPdfDocument> local1 = <>c.<>9__24_0;
                    evaluator = <>c.<>9__24_0 = x => x.Document;
                }
                return (IDocument) this.Owner.With<PdfViewerControl, IPdfDocument>(evaluator);
            }
        }

        protected internal PdfViewerBackend ViewerBackend
        {
            get
            {
                Func<PdfViewerControl, PdfViewerBackend> evaluator = <>c.<>9__26_0;
                if (<>c.<>9__26_0 == null)
                {
                    Func<PdfViewerControl, PdfViewerBackend> local1 = <>c.<>9__26_0;
                    evaluator = <>c.<>9__26_0 = x => x.ViewerBackend;
                }
                return this.Owner.With<PdfViewerControl, PdfViewerBackend>(evaluator);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfThumbnailsViewerSettings.<>c <>9 = new PdfThumbnailsViewerSettings.<>c();
            public static Func<PdfViewerControl, IPdfDocument> <>9__24_0;
            public static Func<PdfViewerControl, PdfViewerBackend> <>9__26_0;

            internal void <.cctor>b__4_0(PdfThumbnailsViewerSettings settings, bool value, bool newValue)
            {
                settings.HideThumbnailsViewerChanged(value, newValue);
            }

            internal IPdfDocument <get_Document>b__24_0(PdfViewerControl x) => 
                x.Document;

            internal PdfViewerBackend <get_ViewerBackend>b__26_0(PdfViewerControl x) => 
                x.ViewerBackend;
        }
    }
}

