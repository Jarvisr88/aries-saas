namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;

    public sealed class PredefinedFilter : Freezable, IPredefinedFilter
    {
        public static readonly DependencyProperty NameProperty;
        public static readonly DependencyProperty FilterProperty;

        static PredefinedFilter()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PredefinedFilter), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<PredefinedFilter> registrator1 = DependencyPropertyRegistrator<PredefinedFilter>.New().Register<CriteriaOperator>(System.Linq.Expressions.Expression.Lambda<Func<PredefinedFilter, CriteriaOperator>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PredefinedFilter.get_Filter)), parameters), out FilterProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PredefinedFilter), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator1.Register<string>(System.Linq.Expressions.Expression.Lambda<Func<PredefinedFilter, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PredefinedFilter.get_Name)), expressionArray2), out NameProperty, null, frameworkOptions);
        }

        protected override Freezable CreateInstanceCore() => 
            new PredefinedFilter();

        public string Name
        {
            get => 
                (string) base.GetValue(NameProperty);
            set => 
                base.SetValue(NameProperty, value);
        }

        [TypeConverter(typeof(CriteriaOperatorConverter))]
        public CriteriaOperator Filter
        {
            get => 
                (CriteriaOperator) base.GetValue(FilterProperty);
            set => 
                base.SetValue(FilterProperty, value);
        }
    }
}

