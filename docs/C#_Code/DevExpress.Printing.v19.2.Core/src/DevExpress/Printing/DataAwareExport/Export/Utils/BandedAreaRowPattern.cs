namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using System;
    using System.Collections.Generic;

    public class BandedAreaRowPattern : List<BandedRowInfo>
    {
        public BandedAreaRowPattern()
        {
        }

        public BandedAreaRowPattern(int count)
        {
            for (int i = 0; i < count; i++)
            {
                base.Add(new BandedRowInfo());
            }
        }

        public int FindColumnRowIndexInTemplate(string fieldName)
        {
            int num = 0;
            while (num < base.Count)
            {
                int num2 = 0;
                while (true)
                {
                    if (num2 >= base[num].Count)
                    {
                        num++;
                        break;
                    }
                    if (string.Equals(base[num][num2].Column.FieldName, fieldName))
                    {
                        return num;
                    }
                    num2++;
                }
            }
            return -1;
        }
    }
}

