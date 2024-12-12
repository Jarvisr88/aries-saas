namespace DevExpress.Office
{
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;

    public class MultiIndexBatchUpdateHelper : BatchUpdateHelper
    {
        private HistoryTransaction transaction;
        private int suppressIndexRecalculationOnEndInitCount;
        private bool fakeAssignDetected;

        public MultiIndexBatchUpdateHelper(IBatchUpdateHandler handler) : base(handler)
        {
        }

        public virtual void BeginUpdateDeferredChanges()
        {
        }

        public virtual void CancelUpdateDeferredChanges()
        {
        }

        public virtual void EndUpdateDeferredChanges()
        {
        }

        public void ResumeIndexRecalculationOnEndInit()
        {
            this.suppressIndexRecalculationOnEndInitCount--;
        }

        public void SuppressIndexRecalculationOnEndInit()
        {
            this.suppressIndexRecalculationOnEndInitCount++;
        }

        internal HistoryTransaction Transaction
        {
            get => 
                this.transaction;
            set => 
                this.transaction = value;
        }

        public bool FakeAssignDetected
        {
            get => 
                this.fakeAssignDetected;
            set => 
                this.fakeAssignDetected = value;
        }

        public bool IsIndexRecalculationOnEndInitEnabled =>
            this.suppressIndexRecalculationOnEndInitCount == 0;
    }
}

