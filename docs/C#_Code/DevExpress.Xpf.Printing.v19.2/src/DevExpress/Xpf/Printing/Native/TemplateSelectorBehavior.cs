namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TemplateSelectorBehavior : Behavior<ContentControl>
    {
        public static readonly DependencyProperty TrueValueTemplateProperty;
        public static readonly DependencyProperty FalseValueTemplateProperty;
        public static readonly DependencyProperty ConditionProperty;

        static TemplateSelectorBehavior()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(TemplateSelectorBehavior), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<TemplateSelectorBehavior> registrator1 = DependencyPropertyRegistrator<TemplateSelectorBehavior>.New().Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<TemplateSelectorBehavior, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TemplateSelectorBehavior.get_TrueValueTemplate)), parameters), out TrueValueTemplateProperty, null, d => d.SetActualTemplate(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TemplateSelectorBehavior), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<TemplateSelectorBehavior> registrator2 = registrator1.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<TemplateSelectorBehavior, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TemplateSelectorBehavior.get_FalseValueTemplate)), expressionArray2), out FalseValueTemplateProperty, null, d => d.SetActualTemplate(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TemplateSelectorBehavior), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator2.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<TemplateSelectorBehavior, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TemplateSelectorBehavior.get_Condition)), expressionArray3), out ConditionProperty, true, d => d.SetActualTemplate(), frameworkOptions);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.SetActualTemplate();
        }

        private void SetActualTemplate()
        {
            if (base.IsAttached)
            {
                base.AssociatedObject.ContentTemplate = this.Condition ? this.TrueValueTemplate : this.FalseValueTemplate;
            }
        }

        public DataTemplate TrueValueTemplate
        {
            get => 
                (DataTemplate) base.GetValue(TrueValueTemplateProperty);
            set => 
                base.SetValue(TrueValueTemplateProperty, value);
        }

        public DataTemplate FalseValueTemplate
        {
            get => 
                (DataTemplate) base.GetValue(FalseValueTemplateProperty);
            set => 
                base.SetValue(FalseValueTemplateProperty, value);
        }

        public bool Condition
        {
            get => 
                (bool) base.GetValue(ConditionProperty);
            set => 
                base.SetValue(ConditionProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TemplateSelectorBehavior.<>c <>9 = new TemplateSelectorBehavior.<>c();

            internal void <.cctor>b__12_0(TemplateSelectorBehavior d)
            {
                d.SetActualTemplate();
            }

            internal void <.cctor>b__12_1(TemplateSelectorBehavior d)
            {
                d.SetActualTemplate();
            }

            internal void <.cctor>b__12_2(TemplateSelectorBehavior d)
            {
                d.SetActualTemplate();
            }
        }
    }
}

