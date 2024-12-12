namespace DevExpress.Xpf.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public sealed class FetchRowsResult
    {
        public FetchRowsResult(object[] rows, bool hasMoreRows = true, object nextSkipToken = null)
        {
            object[] objArray1 = rows;
            if (rows == null)
            {
                object[] local1 = rows;
                objArray1 = new object[0];
            }
            this.Rows = objArray1;
            this.HasMoreRows = hasMoreRows;
            this.NextSkipToken = nextSkipToken;
        }

        public static implicit operator FetchRowsResult(object[] rows) => 
            new FetchRowsResult(rows, true, null);

        public object[] Rows { get; private set; }

        public bool HasMoreRows { get; private set; }

        public object NextSkipToken { get; private set; }
    }
}

