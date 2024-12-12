namespace DMEWorks.Billing
{
    using System;

    public class Converter
    {
        public static double DeliveredQty2BilledQty(double DeliveryQty, double DeliveredConverter, double BilledConverter)
        {
            if (DeliveredConverter < 1E-09)
            {
                throw new ArgumentOutOfRangeException("DeliveredConverter", "Paramater DeliveryConverter must be greater than zero");
            }
            if (BilledConverter < 1E-09)
            {
                throw new ArgumentOutOfRangeException("BilledConverter", "Paramater BilledConverter must be greater than zero");
            }
            return ((DeliveryQty * DeliveredConverter) / BilledConverter);
        }

        public static double GetAmountMultiplier(DateTime FromDate, DateTime ToDate, DateTime EndDate, SaleRentTypeEnum SaleRentType, OrderedWhenEnum OrderedWhen, BilledWhenEnum BilledWhen)
        {
            double num;
            if (!Enum.IsDefined(typeof(SaleRentTypeEnum), SaleRentType))
            {
                throw new ArgumentOutOfRangeException("SaleRentType");
            }
            switch (SaleRentType)
            {
                case SaleRentTypeEnum.OneTimeSale:
                    num = 1.0;
                    break;

                case SaleRentTypeEnum.ReoccurringSale:
                    num = 1.0;
                    break;

                case SaleRentTypeEnum.MedicareOxygenRental:
                    num = 1.0;
                    break;

                case SaleRentTypeEnum.MonthlyRental:
                    num = GetMultiplier(FromDate, ToDate, OrderedWhen, BilledWhen);
                    break;

                case SaleRentTypeEnum.OneTimeRental:
                    switch (OrderedWhen)
                    {
                        case OrderedWhenEnum.OneTime:
                            num = (EndDate - FromDate).Days + 1;
                            break;

                        case OrderedWhenEnum.Daily:
                            num = (EndDate - FromDate).Days + 1;
                            break;

                        case OrderedWhenEnum.Weekly:
                            num = ((double) ((EndDate - FromDate).Days + 1)) / 7.0;
                            break;

                        case OrderedWhenEnum.Monthly:
                            num = ((double) ((EndDate - FromDate).Days + 1)) / 30.4;
                            break;

                        case OrderedWhenEnum.Quarterly:
                            num = ((double) ((EndDate - FromDate).Days + 1)) / 91.25;
                            break;

                        case OrderedWhenEnum.SemiAnnually:
                            num = ((double) ((EndDate - FromDate).Days + 1)) / 182.5;
                            break;

                        case OrderedWhenEnum.Annually:
                            num = ((double) ((EndDate - FromDate).Days + 1)) / 365.0;
                            break;

                        default:
                            goto TR_0001;
                    }
                    break;

                case SaleRentTypeEnum.RentToPurchase:
                    num = 1.0;
                    break;

                case SaleRentTypeEnum.CappedRental:
                    num = 1.0;
                    break;

                case SaleRentTypeEnum.ParentalCappedRental:
                    num = 1.0;
                    break;

                default:
                    goto TR_0001;
            }
            return num;
        TR_0001:
            string[] textArray1 = new string[] { "Invalid combination: SaleRentType = ", SaleRentTypeHelper.ToString(SaleRentType), ", BilledWhen = ", BilledWhenHelper.ToString(BilledWhen), ", OrderedWhen = ", OrderedWhenHelper.ToString(OrderedWhen) };
            throw new InvalidOperationException(string.Concat(textArray1));
        }

        public static double GetMultiplier(DateTime FromDate, DateTime ToDate, OrderedWhenEnum OrderedWhen, BilledWhenEnum BilledWhen)
        {
            if (!Enum.IsDefined(typeof(OrderedWhenEnum), OrderedWhen))
            {
                throw new ArgumentOutOfRangeException("OrderedWhen");
            }
            if (!Enum.IsDefined(typeof(BilledWhenEnum), BilledWhen))
            {
                throw new ArgumentOutOfRangeException("BilledWhen");
            }
            if (OrderedWhen != OrderedWhenEnum.OneTime)
            {
                double days;
                int num2 = (int) (((OrderedWhenEnum.Annually | OrderedWhenEnum.Daily) * OrderedWhen) + ((OrderedWhenEnum) ((int) BilledWhen)));
                if (num2 > 0x44)
                {
                    if (num2 == 0x4d)
                    {
                        days = 1.0;
                    }
                    else if (num2 == 0x4e)
                    {
                        days = 2.0;
                    }
                    else if (num2 == 0x58)
                    {
                        days = 1.0;
                    }
                    else
                    {
                        goto TR_0004;
                    }
                    return days;
                }
                else
                {
                    switch (num2)
                    {
                        case 0x16:
                            days = 1.0;
                            break;

                        case 0x17:
                            days = 7.0;
                            break;

                        case 0x18:
                            days = (GetNextPeriodStart(FromDate, ToDate, BilledWhen) - FromDate).Days;
                            break;

                        case 0x19:
                            days = (GetNextPeriodStart(FromDate, ToDate, BilledWhen) - FromDate).Days;
                            break;

                        case 0x1a:
                            days = (GetNextPeriodStart(FromDate, ToDate, BilledWhen) - FromDate).Days;
                            break;

                        case 0x1b:
                            days = (GetNextPeriodStart(FromDate, ToDate, BilledWhen) - FromDate).Days;
                            break;

                        case 0x1c:
                            days = (GetNextPeriodStart(FromDate, ToDate, BilledWhen) - FromDate).Days;
                            break;

                        case 0x1d:
                            days = (GetNextPeriodStart(FromDate, ToDate, BilledWhen) - FromDate).Days;
                            break;

                        case 30:
                        case 0x1f:
                        case 0x20:
                        case 0x27:
                        case 40:
                        case 0x29:
                        case 0x2a:
                        case 0x2b:
                            goto TR_0004;

                        case 0x21:
                            days = 1.0;
                            break;

                        case 0x22:
                            days = 4.0;
                            break;

                        case 0x23:
                            days = ((double) (GetNextPeriodStart(FromDate, ToDate, BilledWhen) - FromDate).Days) / 7.0;
                            break;

                        case 0x24:
                            days = 13.0;
                            break;

                        case 0x25:
                            days = 26.0;
                            break;

                        case 0x26:
                            days = 52.0;
                            break;

                        case 0x2c:
                            days = 1.0;
                            break;

                        case 0x2d:
                            days = 1.0;
                            break;

                        case 0x2e:
                            days = 3.0;
                            break;

                        case 0x2f:
                            days = 6.0;
                            break;

                        case 0x30:
                            days = 12.0;
                            break;

                        default:
                            switch (num2)
                            {
                                case 0x42:
                                    days = 1.0;
                                    break;

                                case 0x43:
                                    days = 2.0;
                                    break;

                                case 0x44:
                                    days = 4.0;
                                    break;

                                default:
                                    goto TR_0004;
                            }
                            break;
                    }
                    return days;
                }
            }
            else
            {
                return 1.0;
            }
        TR_0004:
            throw new InvalidOperationException("Invalid combination: BilledWhen = " + BilledWhenHelper.ToString(BilledWhen) + ", OrderedWhen = " + OrderedWhenHelper.ToString(OrderedWhen));
        }

        public static DateTime GetNextPeriodStart(DateTime FromDate, DateTime ToDate, BilledWhenEnum BilledWhen)
        {
            DateTime minValue;
            FromDate = FromDate.Date;
            switch (BilledWhen)
            {
                case BilledWhenEnum.OneTime:
                    minValue = FromDate;
                    break;

                case BilledWhenEnum.Daily:
                    minValue = FromDate.AddDays(1.0);
                    break;

                case BilledWhenEnum.Weekly:
                    minValue = FromDate.AddDays(7.0);
                    break;

                case BilledWhenEnum.Monthly:
                    minValue = FromDate.AddMonths(1);
                    break;

                case BilledWhenEnum.CalendarMonthly:
                    FromDate = FromDate.AddMonths(1);
                    minValue = FromDate.AddDays((double) (1 - FromDate.Day));
                    break;

                case BilledWhenEnum.Quarterly:
                    minValue = FromDate.AddMonths(3);
                    break;

                case BilledWhenEnum.SemiAnnually:
                    minValue = FromDate.AddMonths(6);
                    break;

                case BilledWhenEnum.Annually:
                    minValue = FromDate.AddMonths(12);
                    break;

                case BilledWhenEnum.Custom:
                    minValue = ToDate.Date.AddDays(1.0);
                    break;

                default:
                    minValue = DateTime.MinValue;
                    break;
            }
            return minValue;
        }

        public static DateTime GetPeriodEnd(DateTime FromDate, DateTime ToDate, BilledWhenEnum BilledWhen)
        {
            DateTime minValue;
            FromDate = FromDate.Date;
            switch (BilledWhen)
            {
                case BilledWhenEnum.OneTime:
                    minValue = FromDate;
                    break;

                case BilledWhenEnum.Daily:
                    minValue = FromDate.AddDays(2.0);
                    break;

                case BilledWhenEnum.Weekly:
                    minValue = FromDate.AddDays(14.0);
                    break;

                case BilledWhenEnum.Monthly:
                    minValue = FromDate.AddMonths(2);
                    break;

                case BilledWhenEnum.CalendarMonthly:
                    FromDate = FromDate.AddMonths(2);
                    minValue = FromDate.AddDays((double) (1 - FromDate.Day));
                    break;

                case BilledWhenEnum.Quarterly:
                    minValue = FromDate.AddMonths(6);
                    break;

                case BilledWhenEnum.SemiAnnually:
                    minValue = FromDate.AddMonths(12);
                    break;

                case BilledWhenEnum.Annually:
                    minValue = FromDate.AddMonths(0x18);
                    break;

                case BilledWhenEnum.Custom:
                {
                    int num = (ToDate - FromDate).Days + 1;
                    minValue = FromDate.AddDays((double) ((2 * num) - 1));
                    break;
                }
                default:
                    minValue = DateTime.MinValue;
                    break;
            }
            return minValue;
        }

        public static double OrderedQty2BilledQty(DateTime FromDate, DateTime ToDate, double OrderedQty, OrderedWhenEnum OrderedWhen, BilledWhenEnum BilledWhen, double OrderedConverter, double DeliveredConverter, double BilledConverter) => 
            DeliveredQty2BilledQty(OrderedQty2DeliveredQty(FromDate, ToDate, OrderedQty, OrderedWhen, BilledWhen, OrderedConverter, DeliveredConverter, BilledConverter), DeliveredConverter, BilledConverter);

        public static double OrderedQty2DeliveredQty(DateTime FromDate, DateTime ToDate, double OrderedQty, OrderedWhenEnum OrderedWhen, BilledWhenEnum BilledWhen, double OrderedConverter, double DeliveredConverter, double BilledConverter)
        {
            if (OrderedConverter < 1E-09)
            {
                throw new ArgumentOutOfRangeException("OrderedConverter", "Parameter OrderedConverter must be greater than zero");
            }
            if (DeliveredConverter < 1E-09)
            {
                throw new ArgumentOutOfRangeException("DeliveredConverter", "Parameter DeliveryConverter must be greater than zero");
            }
            double num = GetMultiplier(FromDate, ToDate, OrderedWhen, BilledWhen);
            return Math.Ceiling((double) (((OrderedQty * num) * OrderedConverter) / DeliveredConverter));
        }
    }
}

