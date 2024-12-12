namespace DevExpress.Xpf.Editors.RangeControl
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class EditRangeToFilterCriteriaConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (value as EditRange).With<EditRange, CriteriaOperator>(range => (parameter as string).WithString<CriteriaOperator>(argumentMember => CriteriaOperator.And(new BinaryOperator(argumentMember, range.Start, BinaryOperatorType.GreaterOrEqual), new BinaryOperator(argumentMember, range.End, BinaryOperatorType.LessOrEqual))));

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

