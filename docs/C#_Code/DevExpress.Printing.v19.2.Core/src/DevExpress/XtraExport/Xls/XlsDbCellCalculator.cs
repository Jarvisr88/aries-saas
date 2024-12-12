namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.Collections.Generic;

    public class XlsDbCellCalculator
    {
        private List<long> rowPositions = new List<long>();
        private List<long> firstCellPositions = new List<long>();
        private long dbCellPosition;

        public long CalculateFirstRowOffset() => 
            (this.rowPositions.Count != 0) ? (this.dbCellPosition - this.rowPositions[0]) : 0L;

        public List<int> CalculateStreamOffsets()
        {
            List<int> list = new List<int>();
            if (this.rowPositions.Count > 0)
            {
                if (this.rowPositions.Count == 1)
                {
                    list.Add(0);
                }
                else
                {
                    list.Add((int) (this.firstCellPositions[0] - this.rowPositions[1]));
                    int count = this.firstCellPositions.Count;
                    for (int i = 1; i < count; i++)
                    {
                        int item = (int) (this.firstCellPositions[i] - this.firstCellPositions[i - 1]);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public void RegisterDbCellPosition(long position)
        {
            this.dbCellPosition = position;
        }

        public void RegisterFirstCellPosition(long position)
        {
            this.firstCellPositions.Add(position);
        }

        public void RegisterRowPosition(long position)
        {
            this.rowPositions.Add(position);
        }

        public void Reset()
        {
            this.rowPositions.Clear();
            this.firstCellPositions.Clear();
        }
    }
}

