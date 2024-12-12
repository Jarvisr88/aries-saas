namespace DevExpress.Office.Utils
{
    using DevExpress.XtraSpreadsheet.Model;
    using System;
    using System.Collections.Generic;

    public class CellReferencePart
    {
        private readonly Dictionary<char, int> table;
        private PositionType type;
        private int value;
        private int order;

        public CellReferencePart(Dictionary<char, int> table, int order)
        {
            this.table = table;
            this.order = order;
        }

        public int Parse(string reference, int from)
        {
            int length = reference.Length;
            for (int i = from; i < length; i++)
            {
                int num3;
                if (!this.table.TryGetValue(reference[i], out num3))
                {
                    return ((this.value == 0) ? from : i);
                }
                if (num3 != -1)
                {
                    this.value *= this.order;
                    this.value += num3;
                }
                else
                {
                    if (i != from)
                    {
                        return i;
                    }
                    this.type = PositionType.Absolute;
                }
            }
            return length;
        }

        public PositionType Type =>
            this.type;

        public int Value
        {
            get => 
                this.value;
            set => 
                this.value = value;
        }
    }
}

