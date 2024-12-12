namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Generic;

    public class FixedSummariesHelper
    {
        private IList<SummaryItemBase> fixedSummariesLeftCore = new List<SummaryItemBase>();
        private IList<SummaryItemBase> fixedSummariesRightCore = new List<SummaryItemBase>();

        public IList<SummaryItemBase> FixedSummariesLeftCore =>
            this.fixedSummariesLeftCore;

        public IList<SummaryItemBase> FixedSummariesRightCore =>
            this.fixedSummariesRightCore;
    }
}

