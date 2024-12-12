namespace DevExpress.Xpf.Grid.EditForm
{
    using System;

    internal class Row
    {
        private int columnCount;
        private int rowIndex;
        private const int IncorrectSlotIndex = -1;
        private bool[] slots;

        public Row(int columnCount, int index)
        {
            this.columnCount = columnCount;
            this.rowIndex = index;
            this.slots = new bool[columnCount];
        }

        public void FillSlotRange(IEditFormLayoutItem item)
        {
            this.FillSlotRange(item.Column, item.ColumnSpan);
        }

        public void FillSlotRange(int startSlotIndex, int rangeSize)
        {
            for (int i = startSlotIndex; i < (startSlotIndex + rangeSize); i++)
            {
                this.slots[i] = true;
            }
        }

        private int FindIndexOfFreeSlotRange(int rangeSize)
        {
            int num = -1;
            int num2 = 0;
            for (int i = 0; i < this.slots.Length; i++)
            {
                if (this.slots[i])
                {
                    num2 = 0;
                }
                else
                {
                    if (num2 == 0)
                    {
                        num = i;
                    }
                    num2++;
                }
                if (num2 == rangeSize)
                {
                    return num;
                }
            }
            return -1;
        }

        public bool TryAddItem(IEditFormLayoutItem item)
        {
            int num = this.FindIndexOfFreeSlotRange(item.ColumnSpan);
            if ((num == -1) || (item.StartNewRow && (num != 0)))
            {
                this.FillSlotRange(0, this.columnCount);
                return false;
            }
            item.Column = num;
            item.Row = this.rowIndex;
            this.FillSlotRange(0, item.Column + item.ColumnSpan);
            return true;
        }

        public int Index =>
            this.rowIndex;
    }
}

