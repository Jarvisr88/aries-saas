namespace DevExpress.Data.Filtering
{
    using System;

    internal class CustomAggregatesHelper
    {
        public static ICustomAggregate GetCustomAggregate(string name);
        public static ICustomAggregateConvertibleToExpression GetCustomAggregateConvertibleToExpression(string name);
        public static ICustomAggregateFormattable GetCustomAggregateFormattable(string name);
        public static bool IsValidCustomAggregateArgumentsCount(string customAggregateName, int argumentsCount);
        public static void ThrowAggregateNotFound(string name);
        public static void ThrowCustomAggregateNotFormattable(string name);
        public static void ValidateCustomAggregateName(string name);
    }
}

