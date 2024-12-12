namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ExcelDateTimeColumnFilterValues
    {
        public ExcelDateTimeColumnFilterValues()
        {
            this.Dates = new List<DateTime>();
            this.Years = new List<DateTime>();
            this.Months = new List<DateTime>();
        }

        public List<Tuple<int, int, int>> GetMonthRanges()
        {
            List<Tuple<int, int, int>> list = new List<Tuple<int, int, int>>();
            List<Tuple<int, List<int>>> list2 = new List<Tuple<int, List<int>>>();
            Tuple<int, List<int>> item = null;
            foreach (DateTime time in this.Months)
            {
                if (item != null)
                {
                    if (item.Item1 == time.Year)
                    {
                        item.Item2.Add(time.Month);
                        continue;
                    }
                    item = null;
                }
                List<int> list1 = new List<int>();
                list1.Add(time.Month);
                item = new Tuple<int, List<int>>(time.Year, list1);
                list2.Add(item);
            }
            using (List<Tuple<int, List<int>>>.Enumerator enumerator2 = list2.GetEnumerator())
            {
                Tuple<int, List<int>> current;
                int num;
                int num2;
                int num3;
                int num4;
                goto TR_0012;
            TR_0003:
                num4++;
            TR_000F:
                while (true)
                {
                    if (num4 >= current.Item2.Count)
                    {
                        break;
                    }
                    int num5 = current.Item2[num4];
                    if (num != -2147483648)
                    {
                        if (num5 != num3)
                        {
                            list.Add(new Tuple<int, int, int>(current.Item1, num, num2));
                            num = -2147483648;
                        }
                        else
                        {
                            num2 = num5;
                            num3++;
                            if (num4 == (current.Item2.Count - 1))
                            {
                                list.Add(new Tuple<int, int, int>(current.Item1, num, num2));
                            }
                            goto TR_0003;
                        }
                    }
                    num = num5;
                    num2 = num5;
                    num3 = num + 1;
                    if (num4 == (current.Item2.Count - 1))
                    {
                        list.Add(new Tuple<int, int, int>(current.Item1, num, num2));
                    }
                    goto TR_0003;
                }
            TR_0012:
                while (true)
                {
                    if (enumerator2.MoveNext())
                    {
                        current = enumerator2.Current;
                        num = -2147483648;
                        num2 = -2147483648;
                        num3 = -2147483648;
                        num4 = 0;
                    }
                    else
                    {
                        return list;
                    }
                    break;
                }
                goto TR_000F;
            }
        }

        public List<Tuple<int, int>> GetYearRanges()
        {
            List<Tuple<int, int>> list = new List<Tuple<int, int>>();
            int num = -2147483648;
            int num2 = -2147483648;
            int num3 = -2147483648;
            int num4 = 0;
            while (true)
            {
                while (true)
                {
                    if (num4 >= this.Years.Count)
                    {
                        return list;
                    }
                    DateTime time = this.Years[num4];
                    int year = time.Year;
                    if (num != -2147483648)
                    {
                        if (year == num3)
                        {
                            num2 = year;
                            num3++;
                            if (num4 == (this.Years.Count - 1))
                            {
                                list.Add(new Tuple<int, int>(num, num2));
                            }
                            break;
                        }
                        list.Add(new Tuple<int, int>(num, num2));
                        num = -2147483648;
                    }
                    num = year;
                    num2 = year;
                    num3 = num + 1;
                    if (num4 == (this.Years.Count - 1))
                    {
                        list.Add(new Tuple<int, int>(num, num2));
                    }
                    break;
                }
                num4++;
            }
        }

        public List<DateTime> Years { get; private set; }

        public List<DateTime> Months { get; private set; }

        public List<DateTime> Dates { get; private set; }

        public bool IncludeBlanks { get; set; }
    }
}

