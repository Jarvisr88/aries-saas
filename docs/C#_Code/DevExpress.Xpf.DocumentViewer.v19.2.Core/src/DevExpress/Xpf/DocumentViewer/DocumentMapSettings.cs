namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;

    public class DocumentMapSettings : FrameworkElement
    {
        public static readonly DependencyProperty WrapLongLinesProperty;
        public static readonly DependencyProperty HideAfterUseProperty;
        public static readonly DependencyProperty ActualHideAfterUseProperty;
        private static readonly DependencyPropertyKey ActualHideAfterUsePropertyKey;
        private DocumentViewerControl owner;

        public event EventHandler Invalidate;

        static DocumentMapSettings()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentMapSettings), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            bool? defaultValue = null;
            WrapLongLinesProperty = DependencyPropertyRegistrator.Register<DocumentMapSettings, bool?>(System.Linq.Expressions.Expression.Lambda<Func<DocumentMapSettings, bool?>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentMapSettings.get_WrapLongLines)), parameters), defaultValue, (settings, value, newValue) => settings.RaiseInvalidate());
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentMapSettings), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            defaultValue = null;
            HideAfterUseProperty = DependencyPropertyRegistrator.Register<DocumentMapSettings, bool?>(System.Linq.Expressions.Expression.Lambda<Func<DocumentMapSettings, bool?>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentMapSettings.get_HideAfterUse)), expressionArray2), defaultValue, (settings, value, newValue) => settings.RaiseInvalidate());
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentMapSettings), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            ActualHideAfterUsePropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<DocumentMapSettings, bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentMapSettings, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentMapSettings.get_ActualHideAfterUse)), expressionArray3), false, null);
            ActualHideAfterUseProperty = ActualHideAfterUsePropertyKey.DependencyProperty;
        }

        protected internal virtual void Initialize(DocumentViewerControl owner)
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
        }

        public bool ActualHideAfterUse
        {
            get => 
                (bool) base.GetValue(ActualHideAfterUseProperty);
            internal set => 
                base.SetValue(ActualHideAfterUsePropertyKey, value);
        }

        public bool? WrapLongLines
        {
            get => 
                (bool?) base.GetValue(WrapLongLinesProperty);
            set => 
                base.SetValue(WrapLongLinesProperty, value);
        }

        public bool? HideAfterUse
        {
            get => 
                (bool?) base.GetValue(HideAfterUseProperty);
            set => 
                base.SetValue(HideAfterUseProperty, value);
        }

        protected DocumentViewerControl Owner =>
            this.owner;

        public IEnumerable<object> Source { get; internal set; }

        public ICommand GoToCommand { get; internal set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentMapSettings.<>c <>9 = new DocumentMapSettings.<>c();

            internal void <.cctor>b__4_0(DocumentMapSettings settings, bool? value, bool? newValue)
            {
                settings.RaiseInvalidate();
            }

            internal void <.cctor>b__4_1(DocumentMapSettings settings, bool? value, bool? newValue)
            {
                settings.RaiseInvalidate();
            }
        }
    }
}

