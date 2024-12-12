namespace DevExpress.Office.Utils
{
    using DevExpress.Services;
    using DevExpress.Utils;
    using System;

    public class ProgressIndication : IProgressIndicationService
    {
        private static readonly TimeSpan progressShowDelay = TimeSpan.FromMilliseconds(500.0);
        private static readonly TimeSpan minIndicationInterval = TimeSpan.FromMilliseconds(50.0);
        private const int progressLimit = 30;
        private readonly IServiceProvider provider;
        private DateTime indicationTime;
        private string displayName;
        private int minProgress;
        private int progressRange = 1;
        private int normalizedProgress;
        private ProgressIndicationState indicationState;

        public ProgressIndication(IServiceProvider provider)
        {
            Guard.ArgumentNotNull(provider, "provider");
            this.provider = provider;
        }

        public virtual void Begin(string displayName, int minProgress, int maxProgress, int currentProgress)
        {
            this.displayName = displayName;
            this.minProgress = minProgress;
            this.progressRange = Math.Max(1, maxProgress - minProgress);
            this.normalizedProgress = this.CalculateProgress(currentProgress);
            this.indicationTime = DateTime.Now;
            this.indicationState = ProgressIndicationState.Unknown;
        }

        protected internal virtual void BeginIndicationCore()
        {
            IProgressIndicationService service = this.GetService();
            if (service != null)
            {
                service.Begin(this.displayName, 0, 100, this.normalizedProgress);
            }
        }

        protected internal virtual int CalculateProgress(int value) => 
            (100 * (value - this.minProgress)) / this.progressRange;

        public virtual void End()
        {
            IProgressIndicationService service = this.GetService();
            if (service != null)
            {
                service.End();
            }
        }

        protected internal virtual IProgressIndicationService GetService() => 
            (IProgressIndicationService) this.provider.GetService(typeof(IProgressIndicationService));

        protected internal virtual void IndicateProgressCore()
        {
            IProgressIndicationService service = this.GetService();
            if (service != null)
            {
                service.SetProgress(this.normalizedProgress);
            }
        }

        public virtual void SetProgress(int currentProgress)
        {
            int num = this.CalculateProgress(currentProgress);
            if (this.indicationState == ProgressIndicationState.Unknown)
            {
                DateTime now = DateTime.Now;
                if ((now - this.indicationTime) >= progressShowDelay)
                {
                    if (num > 30)
                    {
                        this.indicationState = ProgressIndicationState.Forbidden;
                    }
                    else
                    {
                        this.indicationState = ProgressIndicationState.Allowed;
                        this.normalizedProgress = num;
                        this.BeginIndicationCore();
                        this.IndicateProgressCore();
                        this.indicationTime = now;
                    }
                }
            }
            if (num != this.normalizedProgress)
            {
                this.normalizedProgress = num;
                if (this.indicationState == ProgressIndicationState.Allowed)
                {
                    DateTime now = DateTime.Now;
                    if (((now - this.indicationTime) >= minIndicationInterval) || (this.normalizedProgress == 100))
                    {
                        this.IndicateProgressCore();
                        this.indicationTime = now;
                    }
                }
            }
        }

        protected IServiceProvider Provider =>
            this.provider;
    }
}

