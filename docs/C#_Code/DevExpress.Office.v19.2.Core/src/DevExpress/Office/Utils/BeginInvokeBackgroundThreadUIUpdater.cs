namespace DevExpress.Office.Utils
{
    using DevExpress.Office.Services.Implementation;
    using DevExpress.Utils;
    using System;

    public class BeginInvokeBackgroundThreadUIUpdater : BackgroundThreadUIUpdater
    {
        private readonly IThreadSyncService service;

        public BeginInvokeBackgroundThreadUIUpdater(IThreadSyncService service)
        {
            Guard.ArgumentNotNull(service, "service");
            this.service = service;
        }

        public override void UpdateUI(Action action)
        {
            this.service.EnqueueInvokeInUIThread(action);
        }

        public IThreadSyncService Service =>
            this.service;
    }
}

