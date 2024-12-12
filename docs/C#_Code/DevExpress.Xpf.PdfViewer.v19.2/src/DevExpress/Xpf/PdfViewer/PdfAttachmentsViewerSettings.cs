namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;

    public class PdfAttachmentsViewerSettings : FrameworkElement
    {
        public static readonly DependencyProperty HideAttachmentsViewerProperty;
        public static readonly DependencyProperty AttachmentsViewerStateProperty;
        public static readonly DependencyProperty AttachmentsViewerInitialStateProperty;
        public static readonly DependencyProperty AttachmentsViewerStyleProperty;
        public static readonly DependencyProperty AttachmentsViewerPanelStyleProperty;
        public static readonly DependencyProperty HideAfterUseProperty;
        public static readonly DependencyProperty ActualHideAfterUseProperty;
        private static readonly DependencyPropertyKey ActualHideAfterUsePropertyKey;
        private PdfViewerControl owner;

        public event EventHandler Invalidate;

        static PdfAttachmentsViewerSettings()
        {
            Type ownerType = typeof(PdfAttachmentsViewerSettings);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfAttachmentsViewerSettings), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            HideAttachmentsViewerProperty = DependencyPropertyRegistrator.Register<PdfAttachmentsViewerSettings, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfAttachmentsViewerSettings, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfAttachmentsViewerSettings.get_HideAttachmentsViewer)), parameters), false, (settings, value, newValue) => settings.HideAttachmentsViewerChanged(value, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfAttachmentsViewerSettings), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            AttachmentsViewerStateProperty = DependencyPropertyRegistrator.Register<PdfAttachmentsViewerSettings, PdfAttachmentsViewerState>(System.Linq.Expressions.Expression.Lambda<Func<PdfAttachmentsViewerSettings, PdfAttachmentsViewerState>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfAttachmentsViewerSettings.get_AttachmentsViewerState)), expressionArray2), PdfAttachmentsViewerState.Collapsed, (settings, value, newValue) => settings.AttachmentsViewerStateChanged(value, newValue));
            AttachmentsViewerPanelStyleProperty = DependencyProperty.Register("AttachmentsViewerPanelStyle", typeof(PdfAttachmentsViewerPanelStyle), ownerType, new FrameworkPropertyMetadata(PdfAttachmentsViewerPanelStyle.Tab, (d, e) => ((PdfAttachmentsViewerSettings) d).AttachmentsViewerStyleChanged((PdfAttachmentsViewerPanelStyle) e.OldValue, (PdfAttachmentsViewerPanelStyle) e.NewValue)));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfAttachmentsViewerSettings), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            PdfAttachmentsViewerState? defaultValue = null;
            AttachmentsViewerInitialStateProperty = DependencyPropertyRegistrator.Register<PdfAttachmentsViewerSettings, PdfAttachmentsViewerState?>(System.Linq.Expressions.Expression.Lambda<Func<PdfAttachmentsViewerSettings, PdfAttachmentsViewerState?>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfAttachmentsViewerSettings.get_AttachmentsViewerInitialState)), expressionArray3), defaultValue, (settings, value, newValue) => settings.AttachmentsViewerInitialStateChanged(value, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfAttachmentsViewerSettings), "owner");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            AttachmentsViewerStyleProperty = DependencyPropertyRegistrator.Register<PdfAttachmentsViewerSettings, Style>(System.Linq.Expressions.Expression.Lambda<Func<PdfAttachmentsViewerSettings, Style>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfAttachmentsViewerSettings.get_AttachmentsViewerStyle)), expressionArray4), null);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfAttachmentsViewerSettings), "owner");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            bool? nullable2 = null;
            HideAfterUseProperty = DependencyPropertyRegistrator.Register<PdfAttachmentsViewerSettings, bool?>(System.Linq.Expressions.Expression.Lambda<Func<PdfAttachmentsViewerSettings, bool?>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfAttachmentsViewerSettings.get_HideAfterUse)), expressionArray5), nullable2, (settings, value, newValue) => settings.RaiseInvalidate());
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfAttachmentsViewerSettings), "owner");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            ActualHideAfterUsePropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<PdfAttachmentsViewerSettings, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfAttachmentsViewerSettings, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfAttachmentsViewerSettings.get_ActualHideAfterUse)), expressionArray6), false, null);
            ActualHideAfterUseProperty = ActualHideAfterUsePropertyKey.DependencyProperty;
        }

        protected virtual void AttachmentsViewerInitialStateChanged(PdfAttachmentsViewerState? oldValue, PdfAttachmentsViewerState? newValue)
        {
        }

        protected virtual void AttachmentsViewerStateChanged(PdfAttachmentsViewerState oldValue, PdfAttachmentsViewerState newValue)
        {
            if (newValue == PdfAttachmentsViewerState.Visible)
            {
                IPdfDocument input = this.Owner.Document;
                Func<IPdfDocument, ObservableCollection<PdfFileAttachmentListItem>> evaluator = <>c.<>9__56_0;
                if (<>c.<>9__56_0 == null)
                {
                    Func<IPdfDocument, ObservableCollection<PdfFileAttachmentListItem>> local1 = <>c.<>9__56_0;
                    evaluator = <>c.<>9__56_0 = x => new ObservableCollection<PdfFileAttachmentListItem>(x.CreateAttachments());
                }
                this.Source = new ReadOnlyObservableCollection<PdfFileAttachmentListItem>(input.Return<IPdfDocument, ObservableCollection<PdfFileAttachmentListItem>>(evaluator, <>c.<>9__56_1 ??= () => new ObservableCollection<PdfFileAttachmentListItem>()));
                this.RaiseInvalidate();
            }
        }

        protected virtual void AttachmentsViewerStyleChanged(PdfAttachmentsViewerPanelStyle oldValue, PdfAttachmentsViewerPanelStyle newValue)
        {
            this.Owner.Do<PdfViewerControl>(x => x.NavigationPanelsLayout = (NavigationPanelsLayout) newValue);
        }

        protected virtual void HideAttachmentsViewerChanged(bool oldValue, bool newValue)
        {
            this.Owner.Do<PdfViewerControl>(x => x.UpdateHasAttachments(newValue));
        }

        protected internal virtual void Initialize(PdfViewerControl owner)
        {
            this.owner = owner;
        }

        protected internal void RaiseInvalidate()
        {
            this.Invalidate.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        protected internal virtual void Release()
        {
            this.owner = null;
        }

        protected virtual void UpdateAttachmentsViewerCurrentState()
        {
            PdfAttachmentsViewerState? attachmentsViewerInitialState = this.AttachmentsViewerInitialState;
            this.AttachmentsViewerState = (attachmentsViewerInitialState != null) ? attachmentsViewerInitialState.GetValueOrDefault() : this.AttachmentsViewerState;
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
            if (this.HideAfterUse != null)
            {
                this.ActualHideAfterUse = this.HideAfterUse.Value;
            }
            this.OpenAttachmentCommand = ((PdfCommandProvider) this.Owner.ActualCommandProvider).OpenAttachmentCommand;
            this.SaveAttachmentCommand = ((PdfCommandProvider) this.Owner.ActualCommandProvider).SaveAttachmentCommand;
            IPdfDocument input = this.Owner.Document;
            Func<IPdfDocument, ObservableCollection<PdfFileAttachmentListItem>> evaluator = <>c.<>9__53_0;
            if (<>c.<>9__53_0 == null)
            {
                Func<IPdfDocument, ObservableCollection<PdfFileAttachmentListItem>> local1 = <>c.<>9__53_0;
                evaluator = <>c.<>9__53_0 = x => new ObservableCollection<PdfFileAttachmentListItem>(x.CreateAttachments());
            }
            this.Source = new ReadOnlyObservableCollection<PdfFileAttachmentListItem>(input.Return<IPdfDocument, ObservableCollection<PdfFileAttachmentListItem>>(evaluator, <>c.<>9__53_1 ??= () => new ObservableCollection<PdfFileAttachmentListItem>()));
            if (this.AttachmentsViewerInitialState != null)
            {
                this.UpdateAttachmentsViewerCurrentState();
            }
        }

        public PdfAttachmentsViewerState AttachmentsViewerState
        {
            get => 
                (PdfAttachmentsViewerState) base.GetValue(AttachmentsViewerStateProperty);
            set => 
                base.SetValue(AttachmentsViewerStateProperty, value);
        }

        public PdfAttachmentsViewerState? AttachmentsViewerInitialState
        {
            get => 
                (PdfAttachmentsViewerState?) base.GetValue(AttachmentsViewerInitialStateProperty);
            set => 
                base.SetValue(AttachmentsViewerInitialStateProperty, value);
        }

        public Style AttachmentsViewerStyle
        {
            get => 
                (Style) base.GetValue(AttachmentsViewerStyleProperty);
            set => 
                base.SetValue(AttachmentsViewerStyleProperty, value);
        }

        [Obsolete("Use the PdfViewerControl.NavigationPanelsStyle property instead.")]
        public PdfAttachmentsViewerPanelStyle AttachmentsViewerPanelStyle
        {
            get => 
                (PdfAttachmentsViewerPanelStyle) base.GetValue(AttachmentsViewerPanelStyleProperty);
            set => 
                base.SetValue(AttachmentsViewerPanelStyleProperty, value);
        }

        public bool HideAttachmentsViewer
        {
            get => 
                (bool) base.GetValue(HideAttachmentsViewerProperty);
            set => 
                base.SetValue(HideAttachmentsViewerProperty, value);
        }

        public bool ActualHideAfterUse
        {
            get => 
                (bool) base.GetValue(ActualHideAfterUseProperty);
            internal set => 
                base.SetValue(ActualHideAfterUsePropertyKey, value);
        }

        public bool? HideAfterUse
        {
            get => 
                (bool?) base.GetValue(HideAfterUseProperty);
            set => 
                base.SetValue(HideAfterUseProperty, value);
        }

        protected PdfViewerControl Owner =>
            this.owner;

        public IEnumerable<object> Source { get; internal set; }

        public ICommand OpenAttachmentCommand { get; internal set; }

        public ICommand SaveAttachmentCommand { get; internal set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfAttachmentsViewerSettings.<>c <>9 = new PdfAttachmentsViewerSettings.<>c();
            public static Func<IPdfDocument, ObservableCollection<PdfFileAttachmentListItem>> <>9__53_0;
            public static Func<ObservableCollection<PdfFileAttachmentListItem>> <>9__53_1;
            public static Func<IPdfDocument, ObservableCollection<PdfFileAttachmentListItem>> <>9__56_0;
            public static Func<ObservableCollection<PdfFileAttachmentListItem>> <>9__56_1;

            internal void <.cctor>b__8_0(PdfAttachmentsViewerSettings settings, bool value, bool newValue)
            {
                settings.HideAttachmentsViewerChanged(value, newValue);
            }

            internal void <.cctor>b__8_1(PdfAttachmentsViewerSettings settings, PdfAttachmentsViewerState value, PdfAttachmentsViewerState newValue)
            {
                settings.AttachmentsViewerStateChanged(value, newValue);
            }

            internal void <.cctor>b__8_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PdfAttachmentsViewerSettings) d).AttachmentsViewerStyleChanged((PdfAttachmentsViewerPanelStyle) e.OldValue, (PdfAttachmentsViewerPanelStyle) e.NewValue);
            }

            internal void <.cctor>b__8_3(PdfAttachmentsViewerSettings settings, PdfAttachmentsViewerState? value, PdfAttachmentsViewerState? newValue)
            {
                settings.AttachmentsViewerInitialStateChanged(value, newValue);
            }

            internal void <.cctor>b__8_4(PdfAttachmentsViewerSettings settings, bool? value, bool? newValue)
            {
                settings.RaiseInvalidate();
            }

            internal ObservableCollection<PdfFileAttachmentListItem> <AttachmentsViewerStateChanged>b__56_0(IPdfDocument x) => 
                new ObservableCollection<PdfFileAttachmentListItem>(x.CreateAttachments());

            internal ObservableCollection<PdfFileAttachmentListItem> <AttachmentsViewerStateChanged>b__56_1() => 
                new ObservableCollection<PdfFileAttachmentListItem>();

            internal ObservableCollection<PdfFileAttachmentListItem> <UpdatePropertiesInternal>b__53_0(IPdfDocument x) => 
                new ObservableCollection<PdfFileAttachmentListItem>(x.CreateAttachments());

            internal ObservableCollection<PdfFileAttachmentListItem> <UpdatePropertiesInternal>b__53_1() => 
                new ObservableCollection<PdfFileAttachmentListItem>();
        }
    }
}

