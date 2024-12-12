namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;

    public class EmptyHistory : DocumentHistory
    {
        private int transactionItemCount;

        public EmptyHistory(IDocumentModel documentModel) : base(documentModel)
        {
        }

        public override void Add(HistoryItem item)
        {
            this.transactionItemCount++;
        }

        public override HistoryItem BeginTransaction()
        {
            if (base.TransactionLevel == 0)
            {
                this.transactionItemCount = 0;
            }
            return base.BeginTransaction();
        }

        protected override NotificationIdGenerator CreateIdGenerator() => 
            new EmptyNotificationIdGenerator();

        public override bool HasChangesInCurrentTransaction() => 
            (base.TransactionLevel > 0) ? (this.transactionItemCount != 0) : base.HasChangesInCurrentTransaction();

        protected int TransactionItemCount =>
            this.transactionItemCount;
    }
}

