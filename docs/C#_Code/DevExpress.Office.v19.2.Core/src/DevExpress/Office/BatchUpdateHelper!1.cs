namespace DevExpress.Office
{
    using DevExpress.Utils;
    using System;

    public class BatchUpdateHelper<T> : BatchUpdateHelper
    {
        private T deferredNotifications;
        private bool fakeAssignDetected;
        private int suppressDirectNotificationsCount;
        private int suppressIndexRecalculationOnEndInitCount;

        public BatchUpdateHelper(IBatchUpdateHandler handler) : base(handler)
        {
        }

        public void ResumeDirectNotifications()
        {
            this.suppressDirectNotificationsCount--;
        }

        public void ResumeIndexRecalculationOnEndInit()
        {
            this.suppressIndexRecalculationOnEndInitCount--;
        }

        public void SuppressDirectNotifications()
        {
            this.suppressDirectNotificationsCount++;
        }

        public void SuppressIndexRecalculationOnEndInit()
        {
            this.suppressIndexRecalculationOnEndInitCount++;
        }

        public T DeferredNotifications
        {
            get => 
                this.deferredNotifications;
            set => 
                this.deferredNotifications = value;
        }

        public bool FakeAssignDetected
        {
            get => 
                this.fakeAssignDetected;
            set => 
                this.fakeAssignDetected = value;
        }

        public bool IsDirectNotificationsEnabled =>
            this.suppressDirectNotificationsCount == 0;

        public bool IsIndexRecalculationOnEndInitEnabled =>
            this.suppressIndexRecalculationOnEndInitCount == 0;
    }
}

