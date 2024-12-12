namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;

    public class DisabledHistory : EmptyHistory
    {
        private int transactionCount;

        public DisabledHistory(IDocumentModel documentModel) : base(documentModel)
        {
        }

        public override void Add(HistoryItem item)
        {
            if (base.TransactionLevel > 0)
            {
                this.transactionCount++;
            }
            this.Modified = true;
        }

        public override HistoryItem BeginTransaction()
        {
            if (base.TransactionLevel == 0)
            {
                this.transactionCount = 0;
            }
            return base.BeginTransaction();
        }

        public override HistoryItem EndTransaction()
        {
            if (base.TransactionLevel == 1)
            {
                this.transactionCount = 0;
            }
            return base.EndTransaction();
        }

        public override bool HasChangesInCurrentTransaction() => 
            (base.TransactionLevel > 0) ? (this.transactionCount > 0) : false;
    }
}

