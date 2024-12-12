namespace DevExpress.Export.Xl
{
    using System;

    internal static class XlDynamicFilterCalculator
    {
        public static void CalculateValues(XlDynamicFilter filter, DateTime today)
        {
            switch (filter.DynamicFilterType)
            {
                case XlDynamicFilterType.Tomorrow:
                    filter.Value = today.AddDays(1.0);
                    filter.MaxValue = today.AddDays(2.0);
                    return;

                case XlDynamicFilterType.Today:
                    filter.Value = today;
                    filter.MaxValue = today.AddDays(1.0);
                    return;

                case XlDynamicFilterType.Yesterday:
                    filter.Value = today.AddDays(-1.0);
                    filter.MaxValue = today;
                    return;

                case XlDynamicFilterType.NextWeek:
                    filter.Value = GetStartOfWeek(today).AddDays(7.0);
                    filter.MaxValue = GetStartOfWeek(today).AddDays(14.0);
                    return;

                case XlDynamicFilterType.ThisWeek:
                    filter.Value = GetStartOfWeek(today);
                    filter.MaxValue = GetStartOfWeek(today).AddDays(7.0);
                    return;

                case XlDynamicFilterType.LastWeek:
                    filter.Value = GetStartOfWeek(today).AddDays(-7.0);
                    filter.MaxValue = GetStartOfWeek(today);
                    return;

                case XlDynamicFilterType.NextMonth:
                    filter.Value = GetStartOfMonth(today).AddMonths(1);
                    filter.MaxValue = GetStartOfMonth(today).AddMonths(2);
                    return;

                case XlDynamicFilterType.ThisMonth:
                    filter.Value = GetStartOfMonth(today);
                    filter.MaxValue = GetStartOfMonth(today).AddMonths(1);
                    return;

                case XlDynamicFilterType.LastMonth:
                    filter.Value = GetStartOfMonth(today).AddMonths(-1);
                    filter.MaxValue = GetStartOfMonth(today);
                    return;

                case XlDynamicFilterType.NextQuarter:
                    filter.Value = GetStartOfQuarter(today).AddMonths(3);
                    filter.MaxValue = GetStartOfQuarter(today).AddMonths(6);
                    return;

                case XlDynamicFilterType.ThisQuarter:
                    filter.Value = GetStartOfQuarter(today);
                    filter.MaxValue = GetStartOfQuarter(today).AddMonths(3);
                    return;

                case XlDynamicFilterType.LastQuarter:
                    filter.Value = GetStartOfQuarter(today).AddMonths(-3);
                    filter.MaxValue = GetStartOfQuarter(today);
                    return;

                case XlDynamicFilterType.NextYear:
                    filter.Value = new DateTime(today.Year + 1, 1, 1);
                    filter.MaxValue = new DateTime(today.Year + 2, 1, 1);
                    return;

                case XlDynamicFilterType.ThisYear:
                    filter.Value = new DateTime(today.Year, 1, 1);
                    filter.MaxValue = new DateTime(today.Year + 1, 1, 1);
                    return;

                case XlDynamicFilterType.LastYear:
                    filter.Value = new DateTime(today.Year - 1, 1, 1);
                    filter.MaxValue = new DateTime(today.Year, 1, 1);
                    return;

                case XlDynamicFilterType.YearToDate:
                    filter.Value = new DateTime(today.Year, 1, 1);
                    filter.MaxValue = today.AddDays(1.0);
                    return;
            }
        }

        private static DateTime GetStartOfMonth(DateTime today) => 
            new DateTime(today.Year, today.Month, 1);

        private static DateTime GetStartOfQuarter(DateTime today) => 
            new DateTime(today.Year, (((today.Month - 1) / 3) * 3) + 1, 1);

        private static DateTime GetStartOfWeek(DateTime today) => 
            today.AddDays((double) -today.DayOfWeek);
    }
}

