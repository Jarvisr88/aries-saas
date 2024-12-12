namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public static class CustomUIFilterValueExtension
    {
        public static bool IsKnownValue(this ICustomUIFilterValue value) => 
            (value != null) ? ((value.Value is KnownValues) || (CustomUIFilterSequence.Match(value.FilterType) || CustomUIFilterAggregate.Match(value.FilterType))) : false;

        internal static void SetValue(this ICustomUIFilter filter, params object[] values)
        {
            ((CustomUIFilter) filter).GetService<ICustomUIFilterValuesFactory>().Do<ICustomUIFilterValuesFactory>(delegate (ICustomUIFilterValuesFactory factory) {
                ICustomUIFilterValue value2 = factory.Create(filter.GetID(), values);
                ((CustomUIFilter) filter).SetValueCore(value2, true);
            });
        }
    }
}

