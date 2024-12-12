namespace DMEWorks.Billing
{
    using System;

    public class BilledWhenHelper
    {
        public const string OneTime = "One time";
        public const string Daily = "Daily";
        public const string Weekly = "Weekly";
        public const string Monthly = "Monthly";
        public const string CalendarMonthly = "Calendar Monthly";
        public const string Quarterly = "Quarterly";
        public const string SemiAnnually = "Semi-Annually";
        public const string Annually = "Annually";
        public const string Custom = "Custom";

        private BilledWhenHelper()
        {
        }

        public static BilledWhenEnum ToEnum(string value)
        {
            StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
            return (!ordinalIgnoreCase.Equals(value, "One time") ? (!ordinalIgnoreCase.Equals(value, "Daily") ? (!ordinalIgnoreCase.Equals(value, "Weekly") ? (!ordinalIgnoreCase.Equals(value, "Monthly") ? (!ordinalIgnoreCase.Equals(value, "Calendar Monthly") ? (!ordinalIgnoreCase.Equals(value, "Quarterly") ? (!ordinalIgnoreCase.Equals(value, "Semi-Annually") ? (!ordinalIgnoreCase.Equals(value, "Annually") ? (!ordinalIgnoreCase.Equals(value, "Custom") ? ((BilledWhenEnum) 0) : BilledWhenEnum.Custom) : BilledWhenEnum.Annually) : BilledWhenEnum.SemiAnnually) : BilledWhenEnum.Quarterly) : BilledWhenEnum.CalendarMonthly) : BilledWhenEnum.Monthly) : BilledWhenEnum.Weekly) : BilledWhenEnum.Daily) : BilledWhenEnum.OneTime);
        }

        public static string ToString(BilledWhenEnum value)
        {
            string str;
            switch (value)
            {
                case BilledWhenEnum.OneTime:
                    str = "One time";
                    break;

                case BilledWhenEnum.Daily:
                    str = "Daily";
                    break;

                case BilledWhenEnum.Weekly:
                    str = "Weekly";
                    break;

                case BilledWhenEnum.Monthly:
                    str = "Monthly";
                    break;

                case BilledWhenEnum.Quarterly:
                    str = "Quarterly";
                    break;

                case BilledWhenEnum.SemiAnnually:
                    str = "Semi-Annually";
                    break;

                case BilledWhenEnum.Annually:
                    str = "Annually";
                    break;

                case BilledWhenEnum.Custom:
                    str = "Custom";
                    break;

                default:
                    str = string.Empty;
                    break;
            }
            return str;
        }
    }
}

