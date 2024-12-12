namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars.Internal;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class DocumentMapControl : Control, IVisualOwner, IInputElement, ILogicalOwner
    {
        private const string GridName = "PART_GridControl";
        private static readonly DependencyPropertyKey ActualSourcePropertyKey;
        public static readonly DependencyProperty ActualSourceProperty;
        public static readonly DependencyProperty TreeViewStyleProperty;
        public static readonly DependencyProperty SettingsProperty;
        private static readonly DependencyPropertyKey ActualSettingsPropertyKey;
        public static readonly DependencyProperty ActualSettingsProperty;
        public static readonly DependencyProperty WrapLongLinesProperty;
        public static readonly DependencyProperty HideAfterUseProperty;
        private static readonly DependencyPropertyKey HighlightedItemPropertyKey;
        public static readonly DependencyProperty HighlightedItemProperty;
        private GridControl grid;

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

        static DocumentMapControl()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentMapControl), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            ActualSourcePropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<DocumentMapControl, IEnumerable<object>>(System.Linq.Expressions.Expression.Lambda<Func<DocumentMapControl, IEnumerable<object>>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentMapControl.get_ActualSource)), parameters), null, (d, oldValue, newValue) => d.ActualSourceChanged(oldValue, newValue));
            ActualSourceProperty = ActualSourcePropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentMapControl), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            TreeViewStyleProperty = DependencyPropertyRegistrator.Register<DocumentMapControl, Style>(System.Linq.Expressions.Expression.Lambda<Func<DocumentMapControl, Style>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentMapControl.get_TreeViewStyle)), expressionArray2), null);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentMapControl), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            SettingsProperty = DependencyPropertyRegistrator.Register<DocumentMapControl, DocumentMapSettings>(System.Linq.Expressions.Expression.Lambda<Func<DocumentMapControl, DocumentMapSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentMapControl.get_Settings)), expressionArray3), null, (control, oldValue, newValue) => control.SettingsChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentMapControl), "owner");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            ActualSettingsPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<DocumentMapControl, DocumentMapSettings>(System.Linq.Expressions.Expression.Lambda<Func<DocumentMapControl, DocumentMapSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentMapControl.get_ActualSettings)), expressionArray4), null, (control, oldValue, newValue) => control.ActualSettingsChanged(oldValue, newValue));
            ActualSettingsProperty = ActualSettingsPropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentMapControl), "owner");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            WrapLongLinesProperty = DependencyPropertyRegistrator.Register<DocumentMapControl, bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentMapControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentMapControl.get_WrapLongLines)), expressionArray5), false);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentMapControl), "owner");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            HideAfterUseProperty = DependencyPropertyRegistrator.Register<DocumentMapControl, bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentMapControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentMapControl.get_HideAfterUse)), expressionArray6), false, (control, value, newValue) => control.HideAfterUseChanged(value, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentMapControl), "owner");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            HighlightedItemPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<DocumentMapControl, object>(System.Linq.Expressions.Expression.Lambda<Func<DocumentMapControl, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentMapControl.get_HighlightedItem)), expressionArray7), null, (control, value, newValue) => control.HighlightedItemChanged(value, newValue));
            HighlightedItemProperty = HighlightedItemPropertyKey.DependencyProperty;
        }

        public DocumentMapControl()
        {
            this.SetDefaultStyleKey(typeof(DocumentMapControl));
            this.VCContainer = new VisualChildrenContainer(this, this);
            this.LCContainer = new LogicalChildrenContainer(this);
            this.ActualSettings = this.CreateDefaultMapSettings();
        }

        protected virtual void ActualSettingsChanged(DocumentMapSettings oldValue, DocumentMapSettings newValue)
        {
            if (oldValue != null)
            {
                this.LCContainer.RemoveLogicalChild(oldValue);
                this.VCContainer.RemoveChild(oldValue);
            }
            oldValue.Do<DocumentMapSettings>(delegate (DocumentMapSettings x) {
                x.Invalidate -= new EventHandler(this.InvalidateSettings);
            });
            newValue.Do<DocumentMapSettings>(delegate (DocumentMapSettings x) {
                x.Invalidate += new EventHandler(this.InvalidateSettings);
            });
            if ((newValue != null) && (newValue.GetVisualParent() == null))
            {
                this.VCContainer.AddChild(newValue);
                this.LCContainer.AddLogicalChild(newValue);
            }
            this.AssignSource(newValue);
        }

        protected void ActualSourceChanged(IEnumerable<object> oldValue, IEnumerable<object> newValue)
        {
        }

        private void AssignSource(DocumentMapSettings newValue)
        {
            base.ClearValue(WrapLongLinesProperty);
            base.ClearValue(HideAfterUseProperty);
            if (newValue == null)
            {
                this.ActualSource = new ObservableCollection<object>();
            }
            else
            {
                newValue.UpdateProperties();
                this.ActualSource = newValue.Source;
                bool? wrapLongLines = newValue.WrapLongLines;
                this.WrapLongLines = (wrapLongLines != null) ? wrapLongLines.GetValueOrDefault() : this.WrapLongLines;
                wrapLongLines = newValue.HideAfterUse;
                this.HideAfterUse = (wrapLongLines != null) ? wrapLongLines.GetValueOrDefault() : this.HideAfterUse;
            }
        }

        protected virtual DocumentMapSettings CalcActualSettings(DocumentMapSettings settings) => 
            settings ?? this.CreateDefaultMapSettings();

        protected virtual DocumentMapSettings CreateDefaultMapSettings() => 
            new DocumentMapSettings();

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

        protected override Visual GetVisualChild(int index) => 
            (index < base.VisualChildrenCount) ? base.GetVisualChild(index) : this.VCContainer.GetVisualChild(index - base.VisualChildrenCount);

        protected virtual void HideAfterUseChanged(bool oldValue, bool newValue)
        {
            this.ActualSettings.ActualHideAfterUse = newValue;
        }

        protected virtual void HighlightedItemChanged(object oldValue, object newValue)
        {
        }

        private void InvalidateSettings(object sender, EventArgs eventArgs)
        {
            this.ActualSettings.Do<DocumentMapSettings>(new Action<DocumentMapSettings>(this.AssignSource));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.grid = base.GetTemplateChild("PART_GridControl") as GridControl;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            Action<GridControl> action = <>c.<>9__52_0;
            if (<>c.<>9__52_0 == null)
            {
                Action<GridControl> local1 = <>c.<>9__52_0;
                action = <>c.<>9__52_0 = x => x.Focus();
            }
            this.grid.Do<GridControl>(action);
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            if (this.ActualSource == null)
            {
                base.SetValue(ActualSourcePropertyKey, new ObservableCollection<IDocumentMapItem>());
            }
        }

        protected virtual void SettingsChanged(DocumentMapSettings oldValue, DocumentMapSettings newValue)
        {
            this.ActualSettings = this.CalcActualSettings(newValue);
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

        public bool WrapLongLines
        {
            get => 
                (bool) base.GetValue(WrapLongLinesProperty);
            set => 
                base.SetValue(WrapLongLinesProperty, value);
        }

        public DocumentMapSettings ActualSettings
        {
            get => 
                (DocumentMapSettings) base.GetValue(ActualSettingsProperty);
            private set => 
                base.SetValue(ActualSettingsPropertyKey, value);
        }

        public DocumentMapSettings Settings
        {
            get => 
                (DocumentMapSettings) base.GetValue(SettingsProperty);
            set => 
                base.SetValue(SettingsProperty, value);
        }

        public Style TreeViewStyle
        {
            get => 
                (Style) base.GetValue(TreeViewStyleProperty);
            set => 
                base.SetValue(TreeViewStyleProperty, value);
        }

        public IEnumerable<object> ActualSource
        {
            get => 
                (IEnumerable<object>) base.GetValue(ActualSourceProperty);
            private set => 
                base.SetValue(ActualSourcePropertyKey, value);
        }

        private VisualChildrenContainer VCContainer { get; set; }

        private LogicalChildrenContainer LCContainer { get; set; }

        protected override int VisualChildrenCount =>
            base.VisualChildrenCount + this.VCContainer.VisualChildrenCount;

        protected override IEnumerator LogicalChildren
        {
            get
            {
                IEnumerator[] args = new IEnumerator[] { this.VCContainer.GetEnumerator(), base.LogicalChildren, this.LCContainer.GetEnumerator() };
                return new MergedEnumerator(args);
            }
        }

        bool ILogicalOwner.IsLoaded =>
            base.IsLoaded;

        double ILogicalOwner.ActualWidth =>
            base.ActualWidth;

        double ILogicalOwner.ActualHeight =>
            base.ActualHeight;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentMapControl.<>c <>9 = new DocumentMapControl.<>c();
            public static Action<GridControl> <>9__52_0;

            internal void <.cctor>b__11_0(DocumentMapControl d, IEnumerable<object> oldValue, IEnumerable<object> newValue)
            {
                d.ActualSourceChanged(oldValue, newValue);
            }

            internal void <.cctor>b__11_1(DocumentMapControl control, DocumentMapSettings oldValue, DocumentMapSettings newValue)
            {
                control.SettingsChanged(oldValue, newValue);
            }

            internal void <.cctor>b__11_2(DocumentMapControl control, DocumentMapSettings oldValue, DocumentMapSettings newValue)
            {
                control.ActualSettingsChanged(oldValue, newValue);
            }

            internal void <.cctor>b__11_3(DocumentMapControl control, bool value, bool newValue)
            {
                control.HideAfterUseChanged(value, newValue);
            }

            internal void <.cctor>b__11_4(DocumentMapControl control, object value, object newValue)
            {
                control.HighlightedItemChanged(value, newValue);
            }

            internal void <OnGotFocus>b__52_0(GridControl x)
            {
                x.Focus();
            }
        }
    }
}

