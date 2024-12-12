namespace DevExpress.Xpo.DB
{
    using System;
    using System.Collections;

    [Serializable]
    public class SelectStatementResult
    {
        public SelectStatementResultRow[] Rows;

        public SelectStatementResult() : this(new SelectStatementResultRow[0])
        {
        }

        public SelectStatementResult(SelectStatementResultRow[] rows)
        {
            this.Rows = rows;
        }

        public SelectStatementResult(ICollection rows)
        {
            SelectStatementResultRow[] rowArray = new SelectStatementResultRow[rows.Count];
            int index = 0;
            foreach (object obj2 in rows)
            {
                SelectStatementResultRow row = obj2 as SelectStatementResultRow;
                rowArray[index] = new SelectStatementResultRow((object[]) obj2);
                index++;
            }
            this.Rows = rowArray;
        }

        public SelectStatementResult(params object[] testData) : this((ICollection) objArray2)
        {
            object[] objArray2;
            if (testData[0] is object[])
            {
                objArray2 = testData;
            }
            else
            {
                objArray2 = new object[] { testData };
            }
        }

        public SelectStatementResult Clone()
        {
            SelectStatementResultRow[] rows = new SelectStatementResultRow[this.Rows.Length];
            for (int i = 0; i < this.Rows.Length; i++)
            {
                rows[i] = this.Rows[i].Clone();
            }
            return new SelectStatementResult(rows);
        }
    }
}

