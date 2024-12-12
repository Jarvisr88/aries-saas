namespace DevExpress.Data.Access
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.ComponentModel;

    public class UnboundPropertyDescriptor : PropertyDescriptor
    {
        private DataColumnInfo info;
        private UnboundColumnInfo unboundInfo;
        private DataControllerBase controller;
        private IDataControllerData data;
        private ExpressionEvaluator evaluator;
        private bool evaluatorCreated;
        private Type dataType;
        private Exception evaluatorCreateException;
        private int inEvaluatorGet;

        protected internal UnboundPropertyDescriptor(DataControllerBase controller, UnboundColumnInfo unboundInfo);
        public override bool CanResetValue(object component);
        protected object Convert(object toConvertValue);
        private void CreateEvaluator();
        protected virtual object GetEvaluatorValue(int row);
        public sealed override object GetValue(object component);
        public object GetValueFromRowNumber(int rowNumber);
        public static bool IsErrorValue(object value);
        public override void ResetValue(object component);
        internal void SetInfo(DataColumnInfo info);
        public override void SetValue(object component, object value);
        public override bool ShouldSerializeValue(object component);

        protected ExpressionEvaluator Evaluator { get; }

        protected DataControllerBase Controller { get; }

        public UnboundColumnInfo UnboundInfo { get; }

        public IDataControllerData Data { get; }

        public DataColumnInfo Info { get; }

        private bool RequireValueConversion { get; }

        public override bool IsBrowsable { get; }

        public override bool IsReadOnly { get; }

        public override string Category { get; }

        public override Type PropertyType { get; }

        public override Type ComponentType { get; }
    }
}

