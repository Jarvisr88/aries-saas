namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class CriteriaOperatorConverterExtension : MarkupExtension
    {
        public CriteriaOperatorConverterExtension()
        {
        }

        public CriteriaOperatorConverterExtension(string expression)
        {
            this.Expression = expression;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            CriteriaOperatorConverter converter1 = new CriteriaOperatorConverter();
            converter1.Expression = this.Expression;
            return converter1;
        }

        public string Expression { get; set; }
    }
}

