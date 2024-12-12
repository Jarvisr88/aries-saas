namespace DevExpress.Export.Xl
{
    using System;

    internal abstract class FunctionConverterWeekdayBase : FunctionConverter
    {
        protected FunctionConverterWeekdayBase()
        {
        }

        protected int GetWeekdayReturnType()
        {
            switch (base.Culture.DateTimeFormat.FirstDayOfWeek)
            {
                case DayOfWeek.Monday:
                    return 2;

                case DayOfWeek.Tuesday:
                    return 12;

                case DayOfWeek.Wednesday:
                    return 13;

                case DayOfWeek.Thursday:
                    return 14;

                case DayOfWeek.Friday:
                    return 15;

                case DayOfWeek.Saturday:
                    return 0x10;
            }
            return 1;
        }
    }
}

