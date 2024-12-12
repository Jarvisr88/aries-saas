namespace DevExpress.XtraReports.Expressions
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(BasicExpressionBindingConverter))]
    public class BasicExpressionBinding
    {
        private string propertyName;
        private string expression;

        public BasicExpressionBinding()
        {
            this.propertyName = string.Empty;
            this.expression = string.Empty;
        }

        public BasicExpressionBinding(string propertyName, string expression)
        {
            this.propertyName = string.Empty;
            this.expression = string.Empty;
            this.PropertyName = propertyName;
            this.Expression = expression;
        }

        protected virtual void OnExpressionChanged()
        {
        }

        [DefaultValue(""), XtraSerializableProperty]
        public string PropertyName
        {
            get => 
                this.propertyName ?? string.Empty;
            set => 
                this.propertyName = value;
        }

        [XtraSerializableProperty, DefaultValue("")]
        public string Expression
        {
            get => 
                this.expression ?? string.Empty;
            set
            {
                if (this.expression != value)
                {
                    this.expression = value;
                    this.OnExpressionChanged();
                }
            }
        }

        internal virtual DevExpress.Data.Filtering.CriteriaOperator CriteriaOperator =>
            DevExpress.Data.Filtering.CriteriaOperator.Parse(this.expression, new object[0]);
    }
}

