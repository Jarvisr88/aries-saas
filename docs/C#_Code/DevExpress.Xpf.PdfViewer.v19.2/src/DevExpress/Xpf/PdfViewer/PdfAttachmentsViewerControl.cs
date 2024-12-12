namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars.Internal;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class PdfAttachmentsViewerControl : Control, ILogicalOwner, IInputElement, IVisualOwner
    {
        private static readonly DependencyPropertyKey ActualSourcePropertyKey;
        public static readonly DependencyProperty ActualSourceProperty;
        public static readonly DependencyProperty SettingsProperty;
        private static readonly DependencyPropertyKey ActualSettingsPropertyKey;
        public static readonly DependencyProperty ActualSettingsProperty;
        public static readonly DependencyProperty HideAfterUseProperty;
        private static readonly DependencyPropertyKey HighlightedItemPropertyKey;
        public static readonly DependencyProperty HighlightedItemProperty;

        event RoutedEventHandler ILogicalOwner.Loaded
        {
            add
            {
                base.Loaded += value;
            }
            remove
            {
                base.Loaded -= value;
            }
        }

        static PdfAttachmentsViewerControl()
        {
            Type type = typeof(PdfAttachmentsViewerControl);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfAttachmentsViewerControl), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            ActualSourcePropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<PdfAttachmentsViewerControl, IEnumerable<object>>(System.Linq.Expressions.Expression.Lambda<Func<PdfAttachmentsViewerControl, IEnumerable<object>>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfAttachmentsViewerControl.get_ActualSource)), parameters), null, (d, oldValue, newValue) => d.ActualSourceChanged(oldValue, newValue));
            ActualSourceProperty = ActualSourcePropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfAttachmentsViewerControl), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            ActualSettingsPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings>(System.Linq.Expressions.Expression.Lambda<Func<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfAttachmentsViewerControl.get_ActualSettings)), expressionArray2), null, (d, oldValue, newValue) => d.ActualSettingsChanged(oldValue, newValue));
            ActualSettingsProperty = ActualSettingsPropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfAttachmentsViewerControl), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            SettingsProperty = DependencyPropertyRegistrator.Register<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings>(System.Linq.Expressions.Expression.Lambda<Func<PdfAttachmentsViewerControl, PdfAttachmentsViewerSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfAttachmentsViewerControl.get_Settings)), expressionArray3), null, (d, oldValue, newValue) => d.SettingsChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfAttachmentsViewerControl), "owner");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            HideAfterUseProperty = DependencyPropertyRegistrator.Register<PdfAttachmentsViewerControl, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfAttachmentsViewerControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfAttachmentsViewerControl.get_HideAfterUse)), expressionArray4), false, (control, value, newValue) => control.HideAfterUseChanged(value, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfAttachmentsViewerControl), "owner");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            HighlightedItemPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<PdfAttachmentsViewerControl, object>(System.Linq.Expressions.Expression.Lambda<Func<PdfAttachmentsViewerControl, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfAttachmentsViewerControl.get_HighlightedItem)), expressionArray5), null, (control, value, newValue) => control.HighlightedItemChanged(value, newValue));
            HighlightedItemProperty = HighlightedItemPropertyKey.DependencyProperty;
        }

        public PdfAttachmentsViewerControl()
        {
            base.DefaultStyleKey = typeof(PdfAttachmentsViewerControl);
            this.VCContainer = new VisualChildrenContainer(this, this);
            this.LCContainer = new LogicalChildrenContainer(this);
        }

        protected virtual void ActualSettingsChanged(PdfAttachmentsViewerSettings oldValue, PdfAttachmentsViewerSettings newValue)
        {
            if (oldValue != null)
            {
                this.LCContainer.RemoveLogicalChild(oldValue);
                this.VCContainer.RemoveChild(oldValue);
            }
            oldValue.Do<PdfAttachmentsViewerSettings>(delegate (PdfAttachmentsViewerSettings x) {
                x.Invalidate -= new EventHandler(this.InvalidateSettings);
            });
            newValue.Do<PdfAttachmentsViewerSettings>(delegate (PdfAttachmentsViewerSettings x) {
                x.Invalidate += new EventHandler(this.InvalidateSettings);
            });
            if ((newValue != null) && (newValue.GetVisualParent() == null))
            {
                this.VCContainer.AddChild(newValue);
                this.LCContainer.AddLogicalChild(newValue);
            }
            this.AssignSource(newValue);
        }

        protected virtual void ActualSourceChanged(IEnumerable<object> oldValue, IEnumerable<object> newValue)
        {
        }

        private void AssignSource(PdfAttachmentsViewerSettings newValue)
        {
            base.ClearValue(HideAfterUseProperty);
            if (newValue == null)
            {
                this.ActualSource = new ObservableCollection<object>();
            }
            else
            {
                newValue.UpdateProperties();
                this.ActualSource = newValue.Source;
                bool? hideAfterUse = newValue.HideAfterUse;
                this.HideAfterUse = (hideAfterUse != null) ? hideAfterUse.GetValueOrDefault() : this.HideAfterUse;
            }
        }

        protected virtual PdfAttachmentsViewerSettings CalcActualSettings(PdfAttachmentsViewerSettings settings) => 
            settings ?? this.CreateAttachmentsViewerSettings();

        protected virtual PdfAttachmentsViewerSettings CreateAttachmentsViewerSettings() => 
            new PdfAttachmentsViewerSettings();

        void ILogicalOwner.AddChild(object child)
        {
            base.AddLogicalChild(child);
        }

        void ILogicalOwner.RemoveChild(object child)
        {
            base.RemoveLogicalChild(child);
        }

        void IVisualOwner.AddChild(Visual child)
        {
            base.AddVisualChild(child);
        }

        void IVisualOwner.RemoveChild(Visual child)
        {
            base.RemoveVisualChild(child);
        }

        protected virtual void HideAfterUseChanged(bool oldValue, bool newValue)
        {
            this.ActualSettings.ActualHideAfterUse = newValue;
        }

        protected virtual void HighlightedItemChanged(object oldValue, object newValue)
        {
        }

        private void InvalidateSettings(object sender, EventArgs eventArgs)
        {
            this.ActualSettings.Do<PdfAttachmentsViewerSettings>(new Action<PdfAttachmentsViewerSettings>(this.AssignSource));
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            if (this.ActualSource == null)
            {
                base.SetValue(ActualSourcePropertyKey, new ObservableCollection<PdfFileAttachmentListItem>());
            }
        }

        protected virtual void SettingsChanged(PdfAttachmentsViewerSettings oldValue, PdfAttachmentsViewerSettings newValue)
        {
            this.ActualSettings = this.CalcActualSettings(newValue);
        }

        public IEnumerable<object> ActualSource
        {
            get => 
                (IEnumerable<object>) base.GetValue(ActualSourceProperty);
            private set => 
                base.SetValue(ActualSourcePropertyKey, value);
        }

        public PdfAttachmentsViewerSettings ActualSettings
        {
            get => 
                (PdfAttachmentsViewerSettings) base.GetValue(ActualSettingsProperty);
            private set => 
                base.SetValue(ActualSettingsPropertyKey, value);
        }

        public PdfAttachmentsViewerSettings Settings
        {
            get => 
                (PdfAttachmentsViewerSettings) base.GetValue(SettingsProperty);
            set => 
                base.SetValue(SettingsProperty, value);
        }

        public object HighlightedItem
        {
            get => 
                base.GetValue(HighlightedItemProperty);
            protected internal set => 
                base.SetValue(HighlightedItemPropertyKey, value);
        }

        public bool HideAfterUse
        {
            get => 
                (bool) base.GetValue(HideAfterUseProperty);
            set => 
                base.SetValue(HideAfterUseProperty, value);
        }

        private VisualChildrenContainer VCContainer { get; set; }

        private LogicalChildrenContainer LCContainer { get; set; }

        bool ILogicalOwner.IsLoaded =>
            base.IsLoaded;

        double ILogicalOwner.ActualWidth =>
            base.ActualWidth;

        double ILogicalOwner.ActualHeight =>
            base.ActualHeight;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfAttachmentsViewerControl.<>c <>9 = new PdfAttachmentsViewerControl.<>c();

            internal void <.cctor>b__8_0(PdfAttachmentsViewerControl d, IEnumerable<object> oldValue, IEnumerable<object> newValue)
            {
                d.ActualSourceChanged(oldValue, newValue);
            }

            internal void <.cctor>b__8_1(PdfAttachmentsViewerControl d, PdfAttachmentsViewerSettings oldValue, PdfAttachmentsViewerSettings newValue)
            {
                d.ActualSettingsChanged(oldValue, newValue);
            }

            internal void <.cctor>b__8_2(PdfAttachmentsViewerControl d, PdfAttachmentsViewerSettings oldValue, PdfAttachmentsViewerSettings newValue)
            {
                d.SettingsChanged(oldValue, newValue);
            }

            internal void <.cctor>b__8_3(PdfAttachmentsViewerControl control, bool value, bool newValue)
            {
                control.HideAfterUseChanged(value, newValue);
            }

            internal void <.cctor>b__8_4(PdfAttachmentsViewerControl control, object value, object newValue)
            {
                control.HighlightedItemChanged(value, newValue);
            }
        }
    }
}

