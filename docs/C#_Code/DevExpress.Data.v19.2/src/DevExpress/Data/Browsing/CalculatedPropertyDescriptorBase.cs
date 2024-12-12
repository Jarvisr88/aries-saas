namespace DevExpress.Data.Browsing
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Services.Internal;
    using DevExpress.XtraReports.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class CalculatedPropertyDescriptorBase : PropertyDescriptor, IContainerComponent
    {
        private readonly Type propertyType;
        private readonly string displayName;
        private readonly string expression;
        private readonly EvaluatorContextDescriptor contextDescriptor;
        private ExpressionEvaluator evaluator;
        private bool isEvaluated;

        public CalculatedPropertyDescriptorBase(ICalculatedField calculatedField);
        public CalculatedPropertyDescriptorBase(ICalculatedField calculatedField, DataContext dataContext);
        protected CalculatedPropertyDescriptorBase(ICalculatedField calculatedField, EvaluatorContextDescriptor descriptor);
        public CalculatedPropertyDescriptorBase(ICalculatedField calculatedField, IEnumerable<IParameter> parameters);
        public CalculatedPropertyDescriptorBase(ICalculatedField calculatedField, IEnumerable<IParameter> parameters, DataContext dataContext);
        public override bool CanResetValue(object component);
        private static object ConvertToType(object value, Type type);
        protected virtual ExpressionEvaluator CreateExpressionEvaluator(EvaluatorContextDescriptor contextDescriptor, CriteriaOperator criteriaOperator);
        public override object GetValue(object component);
        public override void ResetValue(object component);
        public override void SetValue(object component, object value);
        public override bool ShouldSerializeValue(object component);

        protected ICalculatedField CalculatedField { get; private set; }

        public override Type PropertyType { get; }

        public override TypeConverter Converter { get; }

        public override Type ComponentType { get; }

        public override string DisplayName { get; }

        protected ExpressionEvaluator Evaluator { get; }

        public override bool IsReadOnly { get; }

        object IContainerComponent.Component { get; }
    }
}

