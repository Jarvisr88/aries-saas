namespace DevExpress.Office
{
    using DevExpress.Utils;
    using System;

    public class BatchUpdateHelper<TInfo, TOptions> : BatchUpdateHelper
    {
        private TInfo deferredInfoNotifications;
        private TOptions deferredOptionsNotifications;

        public BatchUpdateHelper(IBatchUpdateHandler handler) : base(handler)
        {
        }

        public TInfo DeferredInfoNotifications
        {
            get => 
                this.deferredInfoNotifications;
            set => 
                this.deferredInfoNotifications = value;
        }

        public TOptions DeferredOptionsNotifications
        {
            get => 
                this.deferredOptionsNotifications;
            set => 
                this.deferredOptionsNotifications = value;
        }
    }
}

