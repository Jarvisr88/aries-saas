namespace DevExpress.Mvvm.UI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class CriteriaOperatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            CriteriaOperator criteria = CriteriaOperator.Parse(this.Expression, new object[0]);
            return new ExpressionEvaluator(TypeDescriptor.GetProperties(value), criteria).Evaluate(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public string Expression { get; set; }
    }
}

