namespace DevExpress.Data.TreeList
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class TreeListUnboundPropertyDescriptor : PropertyDescriptor
    {
        private UnboundColumnInfo unboundInfo;
        private ExpressionEvaluator evaluator;
        private Type dataType;
        private Exception evaluatorCreateException;

        protected internal TreeListUnboundPropertyDescriptor(TreeListDataControllerBase controller, UnboundColumnInfo unboundInfo);
        public override bool CanResetValue(object component);
        protected object Convert(object value);
        protected virtual ExpressionEvaluator CreateEvaluator();
        protected virtual object GetEvaluatorValue(TreeListNodeBase node);
        public override object GetValue(object component);
        public override void ResetValue(object component);
        public override void SetValue(object component, object value);
        public override bool ShouldSerializeValue(object component);

        public override bool IsBrowsable { get; }

        protected ExpressionEvaluator Evaluator { get; }

        protected TreeListDataControllerBase Controller { get; private set; }

        public UnboundColumnInfo UnboundInfo { get; }

        public override bool IsReadOnly { get; }

        public override string Category { get; }

        public override Type PropertyType { get; }

        public override Type ComponentType { get; }

        private bool RequireValueConversion { get; }
    }
}

