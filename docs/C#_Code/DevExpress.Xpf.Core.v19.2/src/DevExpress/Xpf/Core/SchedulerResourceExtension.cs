namespace DevExpress.Xpf.Core
{
    using System;

    public class SchedulerResourceExtension : ResourceExtensionBase
    {
        public SchedulerResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.Scheduler";
    }
}

