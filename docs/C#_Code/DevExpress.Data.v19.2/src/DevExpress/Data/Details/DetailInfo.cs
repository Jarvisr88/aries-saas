namespace DevExpress.Data.Details
{
    using System;
    using System.Collections;

    public class DetailInfo : IDisposable
    {
        private MasterRowInfo masterRow;
        private IDisposable detailOwner;
        private IList detailList;
        private int relationIndex;

        public DetailInfo(MasterRowInfo masterRow, IList detailList, int relationIndex);
        public virtual void Dispose();

        public MasterRowInfo MasterRow { get; }

        public IDisposable DetailOwner { get; set; }

        public IList DetailList { get; }

        public int RelationIndex { get; }
    }
}

