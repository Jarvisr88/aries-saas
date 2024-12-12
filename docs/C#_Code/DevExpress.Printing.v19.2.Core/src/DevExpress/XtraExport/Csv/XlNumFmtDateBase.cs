namespace DevExpress.XtraExport.Csv
{
    using System;

    internal abstract class XlNumFmtDateBase : XlNumFmtElementBase
    {
        private int count;

        protected XlNumFmtDateBase(int count)
        {
            this.count = count;
        }

        public int Count =>
            this.count;

        protected bool IsDefaultNetDateTimeFormat =>
            this.count == 1;
    }
}

