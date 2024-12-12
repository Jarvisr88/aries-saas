namespace DevExpress.XtraExport.Csv
{
    using System;

    internal abstract class XlNumFmtTimeBase : XlNumFmtDateBase
    {
        private bool elapsed;

        public XlNumFmtTimeBase(int count, bool elapsed) : base(count)
        {
            this.elapsed = elapsed;
        }

        protected bool Elapsed =>
            this.elapsed;
    }
}

