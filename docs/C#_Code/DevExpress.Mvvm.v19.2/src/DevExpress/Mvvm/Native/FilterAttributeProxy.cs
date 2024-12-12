namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=false)]
    public abstract class FilterAttributeProxy : Attribute, IAttributeProxy
    {
        public const string FilterAttributeName = "DevExpress.Utils.Filtering.FilterAttribute";
        private const string FilterRangeAttributeName = "DevExpress.Utils.Filtering.FilterRangeAttribute";
        private const string FilterDateTimeRangeAttributeName = "DevExpress.Utils.Filtering.FilterDateTimeRangeAttribute";
        private const string FilterLookupAttributeName = "DevExpress.Utils.Filtering.FilterLookupAttribute";
        private const string FilterBooleanChoiceAttributeName = "DevExpress.Utils.Filtering.FilterBooleanChoiceAttribute";
        private const string FilterEnumChoiceAttributeName = "DevExpress.Utils.Filtering.FilterEnumChoiceAttribute";
        private static RangeAttributeCtor rangeInitializer;
        private static DateTimeRangeAttributeEmptyCtor dateTimeRangeEmptyInitializer;
        private static DateTimeRangeAttributeCtor dateTimeRangeInitializer;
        private static LookupAttributeCtor lookupInitializer;
        private static BooleanChoiceAttributeCtor booleanChoiceInitializer;
        private static BooleanChoiceWithDefaultValueAttributeCtor booleanChoiceWithDefaultValueInitializer;
        private static EnumChoiceAttributeEmptyCtor enumChoiceEmptyInitializer;
        private static EnumChoiceAttributeCtor enumChoiceInitializer;

        protected FilterAttributeProxy()
        {
        }

        protected abstract Attribute CreateRealAttribute();
        Attribute IAttributeProxy.CreateRealAttribute() => 
            this.CreateRealAttribute();

        private static BooleanChoiceAttributeCtor GetBooleanChoiceInitializer() => 
            Expression.Lambda<BooleanChoiceAttributeCtor>(Expression.New(DynamicAssemblyHelper.DataAssembly.GetType("DevExpress.Utils.Filtering.FilterBooleanChoiceAttribute").GetConstructor(new Type[0])), new ParameterExpression[0]).Compile();

        private static BooleanChoiceWithDefaultValueAttributeCtor GetBooleanChoiceWithDefaultValueInitializer()
        {
            ParameterExpression expression = Expression.Parameter(typeof(bool), "defaultValue");
            Type[] types = new Type[] { typeof(bool) };
            Expression[] arguments = new Expression[] { expression };
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            return Expression.Lambda<BooleanChoiceWithDefaultValueAttributeCtor>(Expression.New(DynamicAssemblyHelper.DataAssembly.GetType("DevExpress.Utils.Filtering.FilterBooleanChoiceAttribute").GetConstructor(types), arguments), parameters).Compile();
        }

        private static DateTimeRangeAttributeEmptyCtor GetDateTimeRangeEmptyInitializer() => 
            Expression.Lambda<DateTimeRangeAttributeEmptyCtor>(Expression.New(DynamicAssemblyHelper.DataAssembly.GetType("DevExpress.Utils.Filtering.FilterDateTimeRangeAttribute").GetConstructor(new Type[0])), new ParameterExpression[0]).Compile();

        private static DateTimeRangeAttributeCtor GetDateTimeRangeInitializer()
        {
            ParameterExpression expression = Expression.Parameter(typeof(string), "minOrMinMember");
            ParameterExpression expression2 = Expression.Parameter(typeof(string), "maxOrMaxMember");
            Type[] types = new Type[] { typeof(string), typeof(string) };
            Expression[] arguments = new Expression[] { expression, expression2 };
            ParameterExpression[] parameters = new ParameterExpression[] { expression, expression2 };
            return Expression.Lambda<DateTimeRangeAttributeCtor>(Expression.New(DynamicAssemblyHelper.DataAssembly.GetType("DevExpress.Utils.Filtering.FilterDateTimeRangeAttribute").GetConstructor(types), arguments), parameters).Compile();
        }

        private static EnumChoiceAttributeEmptyCtor GetEnumChoiceEmptyInitializer() => 
            Expression.Lambda<EnumChoiceAttributeEmptyCtor>(Expression.New(DynamicAssemblyHelper.DataAssembly.GetType("DevExpress.Utils.Filtering.FilterEnumChoiceAttribute").GetConstructor(new Type[0])), new ParameterExpression[0]).Compile();

        private static EnumChoiceAttributeCtor GetEnumChoiceInitializer()
        {
            ParameterExpression expression = Expression.Parameter(typeof(bool), "useFlags");
            Type[] types = new Type[] { typeof(bool) };
            Expression[] arguments = new Expression[] { expression };
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            return Expression.Lambda<EnumChoiceAttributeCtor>(Expression.New(DynamicAssemblyHelper.DataAssembly.GetType("DevExpress.Utils.Filtering.FilterEnumChoiceAttribute").GetConstructor(types), arguments), parameters).Compile();
        }

        private static LookupAttributeCtor GetLookupInitializer()
        {
            ParameterExpression expression = Expression.Parameter(typeof(object), "dataSourceOrDataSourceMember");
            ParameterExpression expression2 = Expression.Parameter(typeof(int), "top");
            ParameterExpression expression3 = Expression.Parameter(typeof(int), "maxCount");
            Type[] types = new Type[] { typeof(object), typeof(int), typeof(int) };
            Expression[] arguments = new Expression[] { expression, expression2, expression3 };
            ParameterExpression[] parameters = new ParameterExpression[] { expression, expression2, expression3 };
            return Expression.Lambda<LookupAttributeCtor>(Expression.New(DynamicAssemblyHelper.DataAssembly.GetType("DevExpress.Utils.Filtering.FilterLookupAttribute").GetConstructor(types), arguments), parameters).Compile();
        }

        private static RangeAttributeCtor GetRangeInitializer()
        {
            ParameterExpression expression = Expression.Parameter(typeof(object), "minOrMinMember");
            ParameterExpression expression2 = Expression.Parameter(typeof(object), "maxOrMaxMember");
            ParameterExpression expression3 = Expression.Parameter(typeof(object), "avgOrAvgMember");
            Type[] types = new Type[] { typeof(object), typeof(object), typeof(object) };
            Expression[] arguments = new Expression[] { expression, expression2, expression3 };
            ParameterExpression[] parameters = new ParameterExpression[] { expression, expression2, expression3 };
            return Expression.Lambda<RangeAttributeCtor>(Expression.New(DynamicAssemblyHelper.DataAssembly.GetType("DevExpress.Utils.Filtering.FilterRangeAttribute").GetConstructor(types), arguments), parameters).Compile();
        }

        protected void SetProperty<T>(Attribute attr, Expression<Func<T>> property, T value)
        {
            TypeDescriptor.GetProperties(attr)[BindableBase.GetPropertyName<T>(property)].SetValue(attr, value);
        }

        protected void SetProperty<T>(Attribute attr, string property, T value)
        {
            TypeDescriptor.GetProperties(attr)[property].SetValue(attr, value);
        }

        protected static RangeAttributeCtor RangeInitializer =>
            rangeInitializer ??= GetRangeInitializer();

        protected static DateTimeRangeAttributeEmptyCtor DateTimeRangeEmptyInitializer =>
            dateTimeRangeEmptyInitializer ??= GetDateTimeRangeEmptyInitializer();

        protected static DateTimeRangeAttributeCtor DateTimeRangeInitializer =>
            dateTimeRangeInitializer ??= GetDateTimeRangeInitializer();

        protected static LookupAttributeCtor LookupInitializer =>
            lookupInitializer ??= GetLookupInitializer();

        protected static BooleanChoiceAttributeCtor BooleanChoiceInitializer =>
            booleanChoiceInitializer ??= GetBooleanChoiceInitializer();

        protected static BooleanChoiceWithDefaultValueAttributeCtor BooleanChoiceWithDefaultValueInitializer =>
            booleanChoiceWithDefaultValueInitializer ??= GetBooleanChoiceWithDefaultValueInitializer();

        protected static EnumChoiceAttributeEmptyCtor EnumChoiceEmptyInitializer =>
            enumChoiceEmptyInitializer ??= GetEnumChoiceEmptyInitializer();

        protected static EnumChoiceAttributeCtor EnumChoiceInitializer =>
            enumChoiceInitializer ??= GetEnumChoiceInitializer();

        protected delegate Attribute BooleanChoiceAttributeCtor();

        protected delegate Attribute BooleanChoiceWithDefaultValueAttributeCtor(bool defaultValue);

        protected delegate Attribute DateTimeRangeAttributeCtor(string minOrMinMember, string maxOrMaxMember);

        protected delegate Attribute DateTimeRangeAttributeEmptyCtor();

        protected delegate Attribute EnumChoiceAttributeCtor(bool useFlags);

        protected delegate Attribute EnumChoiceAttributeEmptyCtor();

        protected delegate Attribute LookupAttributeCtor(object dataSourceOrDataSourceMember, int top, int maxCount);

        protected delegate Attribute RangeAttributeCtor(object minOrMinMember, object maxOrMaxMember, object avgOrAvgMember);
    }
}

