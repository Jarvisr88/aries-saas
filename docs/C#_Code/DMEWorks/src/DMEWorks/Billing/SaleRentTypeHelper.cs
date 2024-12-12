namespace DMEWorks.Billing
{
    using System;

    public class SaleRentTypeHelper
    {
        public const string OneTimeSale = "One Time Sale";
        public const string ReoccurringSale = "Re-occurring Sale";
        public const string MedicareOxygenRental = "Medicare Oxygen Rental";
        public const string MonthlyRental = "Monthly Rental";
        public const string OneTimeRental = "One Time Rental";
        public const string RentToPurchase = "Rent to Purchase";
        public const string CappedRental = "Capped Rental";
        public const string ParentalCappedRental = "Parental Capped Rental";

        private SaleRentTypeHelper()
        {
        }

        public static bool IsRental(SaleRentTypeEnum value) => 
            (value == SaleRentTypeEnum.OneTimeRental) || ((value == SaleRentTypeEnum.MonthlyRental) || ((value == SaleRentTypeEnum.CappedRental) || ((value == SaleRentTypeEnum.MedicareOxygenRental) || ((value == SaleRentTypeEnum.ParentalCappedRental) || (value == SaleRentTypeEnum.RentToPurchase)))));

        public static bool IsSale(SaleRentTypeEnum value) => 
            (value == SaleRentTypeEnum.OneTimeSale) || (value == SaleRentTypeEnum.ReoccurringSale);

        public static SaleRentTypeEnum ToEnum(string value)
        {
            StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
            return (!ordinalIgnoreCase.Equals(value, "One Time Sale") ? (!ordinalIgnoreCase.Equals(value, "Re-occurring Sale") ? (!ordinalIgnoreCase.Equals(value, "Medicare Oxygen Rental") ? (!ordinalIgnoreCase.Equals(value, "Monthly Rental") ? (!ordinalIgnoreCase.Equals(value, "One Time Rental") ? (!ordinalIgnoreCase.Equals(value, "Rent to Purchase") ? (!ordinalIgnoreCase.Equals(value, "Capped Rental") ? (!ordinalIgnoreCase.Equals(value, "Parental Capped Rental") ? ((SaleRentTypeEnum) 0) : SaleRentTypeEnum.ParentalCappedRental) : SaleRentTypeEnum.CappedRental) : SaleRentTypeEnum.RentToPurchase) : SaleRentTypeEnum.OneTimeRental) : SaleRentTypeEnum.MonthlyRental) : SaleRentTypeEnum.MedicareOxygenRental) : SaleRentTypeEnum.ReoccurringSale) : SaleRentTypeEnum.OneTimeSale);
        }

        public static string ToString(SaleRentTypeEnum value)
        {
            string str;
            switch (value)
            {
                case SaleRentTypeEnum.OneTimeSale:
                    str = "One Time Sale";
                    break;

                case SaleRentTypeEnum.ReoccurringSale:
                    str = "Re-occurring Sale";
                    break;

                case SaleRentTypeEnum.MedicareOxygenRental:
                    str = "Medicare Oxygen Rental";
                    break;

                case SaleRentTypeEnum.MonthlyRental:
                    str = "Monthly Rental";
                    break;

                case SaleRentTypeEnum.RentToPurchase:
                    str = "Rent to Purchase";
                    break;

                case SaleRentTypeEnum.CappedRental:
                    str = "Capped Rental";
                    break;

                case SaleRentTypeEnum.ParentalCappedRental:
                    str = "Parental Capped Rental";
                    break;

                default:
                    str = string.Empty;
                    break;
            }
            return str;
        }
    }
}

