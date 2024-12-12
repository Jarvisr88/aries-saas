namespace DMEWorks.Billing
{
    using System;

    public class BillItemOnHelper
    {
        public const string DayOfDelivery = "Day of Delivery";
        public const string LastDayOfTheMonth = "Last day of the Month";
        public const string LastDayOfThePeriod = "Last day of the Period";
        public const string DayOfPickup = "Day of Pick-up";

        private BillItemOnHelper()
        {
        }

        public static BillItemOnEnum ToEnum(string value)
        {
            StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
            return (!ordinalIgnoreCase.Equals(value, "Day of Delivery") ? (!ordinalIgnoreCase.Equals(value, "Last day of the Period") ? ((BillItemOnEnum) 0) : BillItemOnEnum.LastDayOfThePeriod) : BillItemOnEnum.DayOfDelivery);
        }

        public static string ToString(BillItemOnEnum value)
        {
            BillItemOnEnum enum2 = value;
            return ((enum2 == BillItemOnEnum.DayOfDelivery) ? "Day of Delivery" : ((enum2 == BillItemOnEnum.LastDayOfThePeriod) ? "Last day of the Period" : string.Empty));
        }
    }
}

