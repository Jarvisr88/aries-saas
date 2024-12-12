namespace DMEWorks.Billing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    [StructLayout(LayoutKind.Sequential)]
    public struct Actualizer
    {
        private const string NoError = "";
        public string ArgSaleRentType { get; }
        public string ArgBillItemOn { get; }
        public string ArgOrderedWhen { get; }
        public string ArgBilledWhen { get; }
        public SaleRentTypeEnum ActualSaleRentType { get; }
        public BillItemOnEnum ActualBillItemOn { get; }
        public OrderedWhenEnum ActualOrderedWhen { get; }
        public BilledWhenEnum ActualBilledWhen { get; }
        public string Error_SaleRentType { get; }
        public string Error_BillItemOn { get; }
        public string Error_OrderedWhen { get; }
        public string Error_BilledWhen { get; }
        public static void UpdateErrorIfNeeded(ref string field, string value)
        {
            if (string.IsNullOrEmpty(field) && !string.IsNullOrEmpty(value))
            {
                field = value;
            }
        }

        public static string YouMustSelectSomething() => 
            "You must select something";

        public static string YouMustSelect(params string[] Options)
        {
            if (Options == null)
            {
                throw new ArgumentNullException("Options");
            }
            if (Options.Length == 0)
            {
                throw new ArgumentException("cannot be empty", "Options");
            }
            return ((Options.Length != 1) ? ("You must select either " + string.Join(" or ", Options)) : ("You must select " + Options[0]));
        }

        public static string YouMustSelectAnythingExcept(string Option) => 
            "You must select anything, except " + Option;

        public static string YouMustSelect(IEnumerable<string> Options)
        {
            if (Options == null)
            {
                throw new ArgumentNullException("Options");
            }
            int num1 = Options.Count<string>();
            if (num1 == 0)
            {
                throw new ArgumentException("must be non empty", "Options");
            }
            return ((num1 != 1) ? ("You must select either " + string.Join(" or ", Options)) : ("You must select " + Options.First<string>()));
        }

        public static bool IsOrderedWhenValid(OrderedWhenEnum ow, BilledWhenEnum bw, ref string message)
        {
            bool flag;
            OrderedWhenEnum[] enumArray;
            _Closure$__42-0 e$__- = new _Closure$__42-0 {
                $VB$Local_ow = ow
            };
            if (bw == BilledWhenEnum.OneTime)
            {
                enumArray = new OrderedWhenEnum[] { OrderedWhenEnum.OneTime };
            }
            else if (bw == BilledWhenEnum.Daily)
            {
                enumArray = new OrderedWhenEnum[] { OrderedWhenEnum.OneTime, OrderedWhenEnum.Daily };
            }
            else if (bw == BilledWhenEnum.Weekly)
            {
                enumArray = new OrderedWhenEnum[] { OrderedWhenEnum.OneTime, OrderedWhenEnum.Daily, OrderedWhenEnum.Weekly };
            }
            else if (bw == BilledWhenEnum.Monthly)
            {
                enumArray = new OrderedWhenEnum[] { OrderedWhenEnum.OneTime, OrderedWhenEnum.Daily, OrderedWhenEnum.Weekly, OrderedWhenEnum.Monthly };
            }
            else if (bw == BilledWhenEnum.CalendarMonthly)
            {
                enumArray = new OrderedWhenEnum[] { OrderedWhenEnum.OneTime, OrderedWhenEnum.Daily, OrderedWhenEnum.Weekly, OrderedWhenEnum.Monthly };
            }
            else if (bw == BilledWhenEnum.Quarterly)
            {
                enumArray = new OrderedWhenEnum[] { OrderedWhenEnum.OneTime, OrderedWhenEnum.Daily, OrderedWhenEnum.Weekly, OrderedWhenEnum.Monthly, OrderedWhenEnum.Quarterly };
            }
            else if (bw == BilledWhenEnum.SemiAnnually)
            {
                enumArray = new OrderedWhenEnum[] { OrderedWhenEnum.OneTime, OrderedWhenEnum.Daily, OrderedWhenEnum.Weekly, OrderedWhenEnum.Monthly, OrderedWhenEnum.Quarterly, OrderedWhenEnum.SemiAnnually };
            }
            else if (bw == BilledWhenEnum.Annually)
            {
                enumArray = new OrderedWhenEnum[] { OrderedWhenEnum.OneTime, OrderedWhenEnum.Daily, OrderedWhenEnum.Weekly, OrderedWhenEnum.Monthly, OrderedWhenEnum.Quarterly, OrderedWhenEnum.SemiAnnually, OrderedWhenEnum.Annually };
            }
            else if (bw != BilledWhenEnum.Custom)
            {
                enumArray = null;
            }
            else
            {
                enumArray = new OrderedWhenEnum[] { OrderedWhenEnum.OneTime, OrderedWhenEnum.Daily };
            }
            if ((enumArray != null) && !enumArray.Any<OrderedWhenEnum>(new Func<OrderedWhenEnum, bool>(e$__-._Lambda$__0)))
            {
                message = YouMustSelect(enumArray.Select<OrderedWhenEnum, string>((_Closure$__.$I42-1 == null) ? (_Closure$__.$I42-1 = new Func<OrderedWhenEnum, string>(_Closure$__.$I._Lambda$__42-1)) : _Closure$__.$I42-1));
                flag = false;
            }
            else
            {
                message = string.Empty;
                flag = true;
            }
            return flag;
        }

        public Actualizer(string SaleRentType, string BillItemOn, string OrderedWhen, string BilledWhen)
        {
            string str2;
            this = new Actualizer();
            this._ArgSaleRentType = SaleRentType;
            this._ArgBillItemOn = BillItemOn;
            this._ArgOrderedWhen = OrderedWhen;
            this._ArgBilledWhen = BilledWhen;
            StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
            SaleRentTypeEnum enum2 = SaleRentTypeHelper.ToEnum(SaleRentType);
            BillItemOnEnum enum3 = BillItemOnHelper.ToEnum(BillItemOn);
            OrderedWhenEnum ow = OrderedWhenHelper.ToEnum(OrderedWhen);
            BilledWhenEnum bw = BilledWhenHelper.ToEnum(BilledWhen);
            string message = string.Empty;
            this._ActualSaleRentType = enum2;
            this._ActualBillItemOn = enum3;
            this._ActualBilledWhen = bw;
            this._ActualOrderedWhen = ow;
            if (enum2 == SaleRentTypeEnum.OneTimeSale)
            {
                if (!IsOrderedWhenValid(ow, bw, ref message))
                {
                    this._ActualOrderedWhen = (OrderedWhenEnum) 0;
                    str2 = this.Error_OrderedWhen;
                    UpdateErrorIfNeeded(ref str2, message);
                    this._Error_OrderedWhen = str2;
                }
            }
            else if (enum2 == SaleRentTypeEnum.ReoccurringSale)
            {
                if (bw == BilledWhenEnum.OneTime)
                {
                    this._ActualBilledWhen = (BilledWhenEnum) 0;
                    str2 = this.Error_BilledWhen;
                    UpdateErrorIfNeeded(ref str2, YouMustSelectAnythingExcept("One time"));
                    this._Error_BilledWhen = str2;
                }
                if (!IsOrderedWhenValid(ow, bw, ref message))
                {
                    this._ActualOrderedWhen = (OrderedWhenEnum) 0;
                    str2 = this.Error_OrderedWhen;
                    UpdateErrorIfNeeded(ref str2, message);
                    this._Error_OrderedWhen = str2;
                }
            }
            else if (enum2 == SaleRentTypeEnum.OneTimeRental)
            {
                if (enum3 != BillItemOnEnum.LastDayOfThePeriod)
                {
                    this._ActualBillItemOn = (BillItemOnEnum) 0;
                    str2 = this.Error_BillItemOn;
                    string[] options = new string[] { "Last day of the Period" };
                    UpdateErrorIfNeeded(ref str2, YouMustSelect(options));
                    this._Error_BillItemOn = str2;
                }
                if (bw != BilledWhenEnum.OneTime)
                {
                    this._ActualBilledWhen = (BilledWhenEnum) 0;
                    str2 = this.Error_BilledWhen;
                    string[] options = new string[] { "One time" };
                    UpdateErrorIfNeeded(ref str2, YouMustSelect(options));
                    this._Error_BilledWhen = str2;
                }
            }
            else if (enum2 == SaleRentTypeEnum.MonthlyRental)
            {
                if (bw == BilledWhenEnum.OneTime)
                {
                    this._ActualBilledWhen = (BilledWhenEnum) 0;
                    str2 = this.Error_BilledWhen;
                    UpdateErrorIfNeeded(ref str2, YouMustSelectAnythingExcept("One time"));
                    this._Error_BilledWhen = str2;
                }
                if (!IsOrderedWhenValid(ow, bw, ref message))
                {
                    this._ActualOrderedWhen = (OrderedWhenEnum) 0;
                    str2 = this.Error_OrderedWhen;
                    UpdateErrorIfNeeded(ref str2, message);
                    this._Error_OrderedWhen = str2;
                }
            }
            else if ((enum2 == SaleRentTypeEnum.RentToPurchase) || ((enum2 == SaleRentTypeEnum.CappedRental) || ((enum2 == SaleRentTypeEnum.ParentalCappedRental) || (enum2 == SaleRentTypeEnum.MedicareOxygenRental))))
            {
                if ((ow != OrderedWhenEnum.OneTime) & (ow != OrderedWhenEnum.Monthly))
                {
                    this._ActualOrderedWhen = (OrderedWhenEnum) 0;
                    str2 = this.Error_OrderedWhen;
                    string[] options = new string[] { "One time", "Monthly" };
                    UpdateErrorIfNeeded(ref str2, YouMustSelect(options));
                    this._Error_OrderedWhen = str2;
                }
                if (bw != BilledWhenEnum.Monthly)
                {
                    this._ActualBilledWhen = (BilledWhenEnum) 0;
                    str2 = this.Error_BilledWhen;
                    string[] options = new string[] { "Monthly" };
                    UpdateErrorIfNeeded(ref str2, YouMustSelect(options));
                    this._Error_BilledWhen = str2;
                }
            }
            str2 = this.Error_SaleRentType;
            UpdateErrorIfNeeded(ref str2, (enum2 == ((SaleRentTypeEnum) 0)) ? YouMustSelectSomething() : "");
            this._Error_SaleRentType = str2;
            str2 = this.Error_BillItemOn;
            UpdateErrorIfNeeded(ref str2, (enum3 == ((BillItemOnEnum) 0)) ? YouMustSelectSomething() : "");
            this._Error_BillItemOn = str2;
            str2 = this.Error_OrderedWhen;
            UpdateErrorIfNeeded(ref str2, (ow == ((OrderedWhenEnum) 0)) ? YouMustSelectSomething() : "");
            this._Error_OrderedWhen = str2;
            str2 = this.Error_BilledWhen;
            UpdateErrorIfNeeded(ref str2, (bw == ((BilledWhenEnum) 0)) ? YouMustSelectSomething() : "");
            this._Error_BilledWhen = str2;
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
                    minValue = FromDate.AddDays(1.0).AddDays(-1.0);
                    break;

                case BilledWhenEnum.Weekly:
                    minValue = FromDate.AddDays(7.0).AddDays(-1.0);
                    break;

                case BilledWhenEnum.Monthly:
                    minValue = FromDate.AddMonths(1).AddDays(-1.0);
                    break;

                case BilledWhenEnum.CalendarMonthly:
                    FromDate = FromDate.AddMonths(1);
                    minValue = FromDate.AddDays((double) (0 - FromDate.Day));
                    break;

                case BilledWhenEnum.Quarterly:
                    minValue = FromDate.AddMonths(3).AddDays(-1.0);
                    break;

                case BilledWhenEnum.SemiAnnually:
                    minValue = FromDate.AddMonths(6).AddDays(-1.0);
                    break;

                case BilledWhenEnum.Annually:
                    minValue = FromDate.AddMonths(12).AddDays(-1.0);
                    break;

                case BilledWhenEnum.Custom:
                    minValue = ToDate;
                    break;

                default:
                    minValue = DateTime.MinValue;
                    break;
            }
            return minValue;
        }

        public DateTime ActualDosTo(DateTime DosFrom, DateTime DosTo) => 
            (this.ActualSaleRentType != SaleRentTypeEnum.CappedRental) ? ((this.ActualSaleRentType != SaleRentTypeEnum.MedicareOxygenRental) ? ((this.ActualSaleRentType != SaleRentTypeEnum.ParentalCappedRental) ? ((this.ActualSaleRentType != SaleRentTypeEnum.RentToPurchase) ? ((this.ActualSaleRentType != SaleRentTypeEnum.OneTimeSale) ? ((this.ActualSaleRentType != SaleRentTypeEnum.ReoccurringSale) ? ((this.ActualSaleRentType != SaleRentTypeEnum.MonthlyRental) ? ((this.ActualSaleRentType != SaleRentTypeEnum.OneTimeRental) ? DosFrom : DosTo) : GetPeriodEnd(DosFrom, DosTo, this.ActualBilledWhen)) : GetPeriodEnd(DosFrom, DosTo, this.ActualBilledWhen)) : GetPeriodEnd(DosFrom, DosTo, this.ActualBilledWhen)) : GetPeriodEnd(DosFrom, DosTo, BilledWhenEnum.Monthly)) : GetPeriodEnd(DosFrom, DosTo, BilledWhenEnum.Monthly)) : GetPeriodEnd(DosFrom, DosTo, BilledWhenEnum.Monthly)) : GetPeriodEnd(DosFrom, DosTo, BilledWhenEnum.Monthly);

        public static IEnumerable<string> EnumSaleRentType()
        {
            string[] source = new string[9];
            source[0] = string.Empty;
            source[1] = "One Time Sale";
            source[2] = "Re-occurring Sale";
            source[3] = "Medicare Oxygen Rental";
            source[4] = "Monthly Rental";
            source[5] = "One Time Rental";
            source[6] = "Rent to Purchase";
            source[7] = "Capped Rental";
            source[8] = "Parental Capped Rental";
            return source.OrderBy<string, string>(((_Closure$__.$I47-0 == null) ? (_Closure$__.$I47-0 = new Func<string, string>(_Closure$__.$I._Lambda$__47-0)) : _Closure$__.$I47-0), StringComparer.OrdinalIgnoreCase);
        }

        public static IEnumerable<string> EnumBillItemOn()
        {
            string[] source = new string[] { string.Empty, "Day of Delivery", "Day of Pick-up", "Last day of the Month", "Last day of the Period" };
            return source.OrderBy<string, string>(((_Closure$__.$I48-0 == null) ? (_Closure$__.$I48-0 = new Func<string, string>(_Closure$__.$I._Lambda$__48-0)) : _Closure$__.$I48-0), StringComparer.OrdinalIgnoreCase);
        }

        public static IEnumerable<string> EnumOrderedWhen()
        {
            string[] source = new string[] { string.Empty, "One time", "Daily", "Weekly", "Monthly", "Quarterly", "Semi-Annually", "Annually" };
            return source.OrderBy<string, string>(((_Closure$__.$I49-0 == null) ? (_Closure$__.$I49-0 = new Func<string, string>(_Closure$__.$I._Lambda$__49-0)) : _Closure$__.$I49-0), StringComparer.OrdinalIgnoreCase);
        }

        public static IEnumerable<string> EnumBilledWhen()
        {
            string[] source = new string[10];
            source[0] = string.Empty;
            source[1] = "One time";
            source[2] = "Daily";
            source[3] = "Weekly";
            source[4] = "Monthly";
            source[5] = "Calendar Monthly";
            source[6] = "Quarterly";
            source[7] = "Semi-Annually";
            source[8] = "Annually";
            source[9] = "Custom";
            return source.OrderBy<string, string>(((_Closure$__.$I50-0 == null) ? (_Closure$__.$I50-0 = new Func<string, string>(_Closure$__.$I._Lambda$__50-0)) : _Closure$__.$I50-0), StringComparer.OrdinalIgnoreCase);
        }

        public static void Debug0()
        {
            IEnumerator<string> enumerator;
            StringBuilder builder1 = new StringBuilder();
            builder1.Append("\"SaleRentType\",");
            builder1.Append("\"BillItemOn\",");
            builder1.Append("\"OrderedWhen\",");
            builder1.Append("\"BilledWhen\",");
            builder1.Append("\"Error_SaleRentType\",");
            builder1.Append("\"Error_BillItemOn\",");
            builder1.Append("\"Error_OrderedWhen\",");
            builder1.Append("\"Error_BilledWhen\",");
            IEnumerable<string> enumerable = EnumSaleRentType();
            IEnumerable<string> enumerable2 = EnumBillItemOn();
            IEnumerable<string> enumerable3 = EnumOrderedWhen();
            IEnumerable<string> enumerable4 = EnumBilledWhen();
            try
            {
                enumerator = enumerable.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    IEnumerator<string> enumerator2;
                    string current = enumerator.Current;
                    try
                    {
                        enumerator2 = enumerable2.GetEnumerator();
                        while (enumerator2.MoveNext())
                        {
                            IEnumerator<string> enumerator3;
                            string str2 = enumerator2.Current;
                            try
                            {
                                enumerator3 = enumerable3.GetEnumerator();
                                while (enumerator3.MoveNext())
                                {
                                    IEnumerator<string> enumerator4;
                                    string str3 = enumerator3.Current;
                                    try
                                    {
                                        enumerator4 = enumerable4.GetEnumerator();
                                        while (enumerator4.MoveNext())
                                        {
                                            string str4 = enumerator4.Current;
                                            StringBuilder builder2 = new StringBuilder();
                                            builder2.Append("\"").Append(current).Append("\",");
                                            builder2.Append("\"").Append(str2).Append("\",");
                                            builder2.Append("\"").Append(str3).Append("\",");
                                            builder2.Append("\"").Append(str4).Append("\",");
                                            Actualizer actualizer = new Actualizer(current, str2, str3, str4);
                                            builder2.Append("\"").Append(!string.IsNullOrWhiteSpace(actualizer.Error_SaleRentType) ? "SaleRentType" : "").Append("\",");
                                            StringBuilder local1 = builder2;
                                            local1.Append("\"").Append(!string.IsNullOrWhiteSpace(actualizer.Error_BillItemOn) ? "BillItemOn" : "").Append("\",");
                                            StringBuilder local2 = local1;
                                            local2.Append("\"").Append(!string.IsNullOrWhiteSpace(actualizer.Error_OrderedWhen) ? "OrderedWhen" : "").Append("\",");
                                            local2.Append("\"").Append(!string.IsNullOrWhiteSpace(actualizer.Error_BilledWhen) ? "BilledWhen" : "").Append("\",");
                                        }
                                    }
                                    finally
                                    {
                                        if (enumerator4 != null)
                                        {
                                            enumerator4.Dispose();
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                if (enumerator3 != null)
                                {
                                    enumerator3.Dispose();
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (enumerator2 != null)
                        {
                            enumerator2.Dispose();
                        }
                    }
                }
            }
            finally
            {
                if (enumerator != null)
                {
                    enumerator.Dispose();
                }
            }
        }

        public static void Debug1()
        {
            IEnumerator<string> enumerator;
            StringBuilder builder1 = new StringBuilder();
            builder1.Append("\"SaleRentType\",");
            builder1.Append("\"BillItemOn\",");
            builder1.Append("\"OrderedWhen\",");
            builder1.Append("\"BilledWhen\",");
            builder1.Append("\"Error_SaleRentType\",");
            builder1.Append("\"Error_BillItemOn\",");
            builder1.Append("\"Error_OrderedWhen\",");
            builder1.Append("\"Error_BilledWhen\",");
            IEnumerable<string> enumerable = EnumSaleRentType();
            IEnumerable<string> enumerable2 = EnumBillItemOn();
            IEnumerable<string> enumerable3 = EnumOrderedWhen();
            IEnumerable<string> enumerable4 = EnumBilledWhen();
            try
            {
                enumerator = enumerable.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    IEnumerator<string> enumerator2;
                    string current = enumerator.Current;
                    try
                    {
                        enumerator2 = enumerable2.GetEnumerator();
                        while (enumerator2.MoveNext())
                        {
                            IEnumerator<string> enumerator3;
                            string str2 = enumerator2.Current;
                            try
                            {
                                enumerator3 = enumerable3.GetEnumerator();
                                while (enumerator3.MoveNext())
                                {
                                    IEnumerator<string> enumerator4;
                                    string str3 = enumerator3.Current;
                                    try
                                    {
                                        enumerator4 = enumerable4.GetEnumerator();
                                        while (enumerator4.MoveNext())
                                        {
                                            string str4 = enumerator4.Current;
                                            StringBuilder builder2 = new StringBuilder();
                                            builder2.Append("\"").Append(current).Append("\",");
                                            builder2.Append("\"").Append(str2).Append("\",");
                                            builder2.Append("\"").Append(str3).Append("\",");
                                            builder2.Append("\"").Append(str4).Append("\",");
                                            Actualizer actualizer = new Actualizer(current, str2, str3, str4);
                                            builder2.Append("\"").Append(actualizer.Error_SaleRentType).Append("\",");
                                            builder2.Append("\"").Append(actualizer.Error_BillItemOn).Append("\",");
                                            builder2.Append("\"").Append(actualizer.Error_OrderedWhen).Append("\",");
                                            builder2.Append("\"").Append(actualizer.Error_BilledWhen).Append("\",");
                                        }
                                    }
                                    finally
                                    {
                                        if (enumerator4 != null)
                                        {
                                            enumerator4.Dispose();
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                if (enumerator3 != null)
                                {
                                    enumerator3.Dispose();
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (enumerator2 != null)
                        {
                            enumerator2.Dispose();
                        }
                    }
                }
            }
            finally
            {
                if (enumerator != null)
                {
                    enumerator.Dispose();
                }
            }
        }

        public static void Debug2()
        {
            IEnumerator<string> enumerator;
            StringBuilder builder1 = new StringBuilder();
            builder1.Append("\"SaleRentType\",");
            builder1.Append("\"BillItemOn\",");
            builder1.Append("\"OrderedWhen\",");
            builder1.Append("\"BilledWhen\",");
            builder1.Append("\"ActualSaleRentType\",");
            builder1.Append("\"ActualBillItemOn\",");
            builder1.Append("\"ActualOrderedWhen\",");
            builder1.Append("\"ActualBilledWhen\",");
            builder1.Append("\"Error_SaleRentType\",");
            builder1.Append("\"Error_BillItemOn\",");
            builder1.Append("\"Error_OrderedWhen\",");
            builder1.Append("\"Error_BilledWhen\",");
            IEnumerable<string> enumerable = EnumSaleRentType();
            IEnumerable<string> enumerable2 = EnumBillItemOn();
            IEnumerable<string> enumerable3 = EnumOrderedWhen();
            IEnumerable<string> enumerable4 = EnumBilledWhen();
            try
            {
                enumerator = enumerable.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    IEnumerator<string> enumerator2;
                    string current = enumerator.Current;
                    try
                    {
                        enumerator2 = enumerable2.GetEnumerator();
                        while (enumerator2.MoveNext())
                        {
                            IEnumerator<string> enumerator3;
                            string str2 = enumerator2.Current;
                            try
                            {
                                enumerator3 = enumerable3.GetEnumerator();
                                while (enumerator3.MoveNext())
                                {
                                    IEnumerator<string> enumerator4;
                                    string str3 = enumerator3.Current;
                                    try
                                    {
                                        enumerator4 = enumerable4.GetEnumerator();
                                        while (enumerator4.MoveNext())
                                        {
                                            string str4 = enumerator4.Current;
                                            StringBuilder builder2 = new StringBuilder();
                                            builder2.Append("\"").Append(current).Append("\",");
                                            builder2.Append("\"").Append(str2).Append("\",");
                                            builder2.Append("\"").Append(str3).Append("\",");
                                            builder2.Append("\"").Append(str4).Append("\",");
                                            Actualizer actualizer = new Actualizer(current, str2, str3, str4);
                                            builder2.Append("\"").Append(SaleRentTypeHelper.ToString(actualizer.ActualSaleRentType)).Append("\",");
                                            builder2.Append("\"").Append(BillItemOnHelper.ToString(actualizer.ActualBillItemOn)).Append("\",");
                                            builder2.Append("\"").Append(OrderedWhenHelper.ToString(actualizer.ActualOrderedWhen)).Append("\",");
                                            builder2.Append("\"").Append(BilledWhenHelper.ToString(actualizer.ActualBilledWhen)).Append("\",");
                                            builder2.Append("\"").Append(actualizer.Error_SaleRentType).Append("\",");
                                            builder2.Append("\"").Append(actualizer.Error_BillItemOn).Append("\",");
                                            builder2.Append("\"").Append(actualizer.Error_OrderedWhen).Append("\",");
                                            builder2.Append("\"").Append(actualizer.Error_BilledWhen).Append("\",");
                                        }
                                    }
                                    finally
                                    {
                                        if (enumerator4 != null)
                                        {
                                            enumerator4.Dispose();
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                if (enumerator3 != null)
                                {
                                    enumerator3.Dispose();
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (enumerator2 != null)
                        {
                            enumerator2.Dispose();
                        }
                    }
                }
            }
            finally
            {
                if (enumerator != null)
                {
                    enumerator.Dispose();
                }
            }
        }

        public static void Debug3()
        {
            IEnumerator<string> enumerator;
            StringBuilder builder1 = new StringBuilder();
            builder1.Append("\"SaleRentType\",");
            builder1.Append("\"BillItemOn\",");
            builder1.Append("\"OrderedWhen\",");
            builder1.Append("\"BilledWhen\",");
            builder1.Append("\"ActualSaleRentType\",");
            builder1.Append("\"ActualBillItemOn\",");
            builder1.Append("\"ActualOrderedWhen\",");
            builder1.Append("\"ActualBilledWhen\",");
            IEnumerable<string> enumerable = EnumSaleRentType();
            IEnumerable<string> enumerable2 = EnumBillItemOn();
            IEnumerable<string> enumerable3 = EnumOrderedWhen();
            IEnumerable<string> enumerable4 = EnumBilledWhen();
            try
            {
                enumerator = enumerable.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    IEnumerator<string> enumerator2;
                    string current = enumerator.Current;
                    try
                    {
                        enumerator2 = enumerable2.GetEnumerator();
                        while (enumerator2.MoveNext())
                        {
                            IEnumerator<string> enumerator3;
                            string str2 = enumerator2.Current;
                            try
                            {
                                enumerator3 = enumerable3.GetEnumerator();
                                while (enumerator3.MoveNext())
                                {
                                    IEnumerator<string> enumerator4;
                                    string str3 = enumerator3.Current;
                                    try
                                    {
                                        enumerator4 = enumerable4.GetEnumerator();
                                        while (enumerator4.MoveNext())
                                        {
                                            string str4 = enumerator4.Current;
                                            StringBuilder builder2 = new StringBuilder();
                                            builder2.Append("\"").Append(current).Append("\",");
                                            builder2.Append("\"").Append(str2).Append("\",");
                                            builder2.Append("\"").Append(str3).Append("\",");
                                            builder2.Append("\"").Append(str4).Append("\",");
                                            Actualizer actualizer = new Actualizer(current, str2, str3, str4);
                                            builder2.Append("\"").Append(SaleRentTypeHelper.ToString(actualizer.ActualSaleRentType)).Append("\",");
                                            builder2.Append("\"").Append(BillItemOnHelper.ToString(actualizer.ActualBillItemOn)).Append("\",");
                                            builder2.Append("\"").Append(OrderedWhenHelper.ToString(actualizer.ActualOrderedWhen)).Append("\",");
                                            builder2.Append("\"").Append(BilledWhenHelper.ToString(actualizer.ActualBilledWhen)).Append("\",");
                                        }
                                    }
                                    finally
                                    {
                                        if (enumerator4 != null)
                                        {
                                            enumerator4.Dispose();
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                if (enumerator3 != null)
                                {
                                    enumerator3.Dispose();
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (enumerator2 != null)
                        {
                            enumerator2.Dispose();
                        }
                    }
                }
            }
            finally
            {
                if (enumerator != null)
                {
                    enumerator.Dispose();
                }
            }
        }
        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly Actualizer._Closure$__ $I = new Actualizer._Closure$__();
            public static Func<OrderedWhenEnum, string> $I42-1;
            public static Func<string, string> $I47-0;
            public static Func<string, string> $I48-0;
            public static Func<string, string> $I49-0;
            public static Func<string, string> $I50-0;

            internal string _Lambda$__42-1(OrderedWhenEnum item) => 
                OrderedWhenHelper.ToString(item);

            internal string _Lambda$__47-0(string e) => 
                e;

            internal string _Lambda$__48-0(string e) => 
                e;

            internal string _Lambda$__49-0(string e) => 
                e;

            internal string _Lambda$__50-0(string e) => 
                e;
        }

        [CompilerGenerated]
        internal sealed class _Closure$__42-0
        {
            public OrderedWhenEnum $VB$Local_ow;

            internal bool _Lambda$__0(OrderedWhenEnum item) => 
                item == this.$VB$Local_ow;
        }
    }
}

