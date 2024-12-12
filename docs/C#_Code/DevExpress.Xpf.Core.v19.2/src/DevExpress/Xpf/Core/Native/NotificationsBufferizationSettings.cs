namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class NotificationsBufferizationSettings
    {
        private readonly TimeSpan interval;

        public NotificationsBufferizationSettings(TimeSpan interval);

        public TimeSpan Interval { get; }
    }
}

