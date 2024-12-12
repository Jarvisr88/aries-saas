namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class FilterDateRange
    {
        public FilterDateRange(DateTime begin, DateTime? end)
        {
            this.<Begin>k__BackingField = begin;
            this.End = end;
        }

        internal unsafe bool Enlarge(FilterDateRange other)
        {
            int? nullable3;
            int? nullable1;
            DateTime?* nullablePtr1 = &this.End;
            if (nullablePtr1 != null)
            {
                nullable1 = new int?(nullablePtr1.GetValueOrDefault().Year);
            }
            else
            {
                DateTime?* local1 = nullablePtr1;
                nullable3 = null;
                nullable1 = nullable3;
            }
            int? nullable = nullable1;
            int year = other.Begin.Year;
            if ((nullable.GetValueOrDefault() == year) ? (nullable != null) : false)
            {
                int? nullable4;
                DateTime?* nullablePtr2 = &this.End;
                if (nullablePtr2 != null)
                {
                    nullable4 = new int?(nullablePtr2.GetValueOrDefault().Month);
                }
                else
                {
                    DateTime?* local2 = nullablePtr2;
                    nullable3 = null;
                    nullable4 = nullable3;
                }
                nullable = nullable4;
                year = other.Begin.Month;
                if ((nullable.GetValueOrDefault() == year) ? (nullable != null) : false)
                {
                    int? nullable5;
                    DateTime?* nullablePtr3 = &this.End;
                    if (nullablePtr3 != null)
                    {
                        nullable5 = new int?(nullablePtr3.GetValueOrDefault().Day);
                    }
                    else
                    {
                        DateTime?* local3 = nullablePtr3;
                        nullable3 = null;
                        nullable5 = nullable3;
                    }
                    nullable = nullable5;
                    year = other.Begin.Day;
                    if ((nullable.GetValueOrDefault() == year) ? (nullable != null) : false)
                    {
                        this.End = other.End;
                        return true;
                    }
                }
            }
            return false;
        }

        public DateTime Begin { get; }

        public DateTime? End { get; private set; }
    }
}

