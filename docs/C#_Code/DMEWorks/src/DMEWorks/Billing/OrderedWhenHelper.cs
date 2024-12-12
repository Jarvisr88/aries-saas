namespace DMEWorks.Billing
{
    using System;

    public class OrderedWhenHelper
    {
        public const string OneTime = "One time";
        public const string Daily = "Daily";
        public const string Weekly = "Weekly";
        public const string Monthly = "Monthly";
        public const string Quarterly = "Quarterly";
        public const string SemiAnnually = "Semi-Annually";
        public const string Annually = "Annually";

        private OrderedWhenHelper()
        {
        }

        public static OrderedWhenEnum ToEnum(string value)
        {
            StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
            return (!ordinalIgnoreCase.Equals(value, "One time") ? (!ordinalIgnoreCase.Equals(value, "Daily") ? (!ordinalIgnoreCase.Equals(value, "Weekly") ? (!ordinalIgnoreCase.Equals(value, "Monthly") ? (!ordinalIgnoreCase.Equals(value, "Quarterly") ? (!ordinalIgnoreCase.Equals(value, "Semi-Annually") ? (!ordinalIgnoreCase.Equals(value, "Annually") ? ((OrderedWhenEnum) 0) : OrderedWhenEnum.Annually) : OrderedWhenEnum.SemiAnnually) : OrderedWhenEnum.Quarterly) : OrderedWhenEnum.Monthly) : OrderedWhenEnum.Weekly) : OrderedWhenEnum.Daily) : OrderedWhenEnum.OneTime);
        }

        public static string ToString(OrderedWhenEnum value)
        {
            string str;
            switch (value)
            {
                case OrderedWhenEnum.OneTime:
                    str = "One time";
                    break;

                case OrderedWhenEnum.Daily:
                    str = "Daily";
                    break;

                case OrderedWhenEnum.Weekly:
                    str = "Weekly";
                    break;

                case OrderedWhenEnum.Monthly:
                    str = "Monthly";
                    break;

                case OrderedWhenEnum.Quarterly:
                    str = "Quarterly";
                    break;

                case OrderedWhenEnum.SemiAnnually:
                    str = "Semi-Annually";
                    break;

                case OrderedWhenEnum.Annually:
                    str = "Annually";
                    break;

                default:
                    str = string.Empty;
                    break;
            }
            return str;
        }
    }
}

