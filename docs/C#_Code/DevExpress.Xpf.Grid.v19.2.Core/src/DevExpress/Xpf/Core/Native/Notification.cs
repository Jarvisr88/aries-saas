namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;

    public class Notification
    {
        public static readonly IEnumerable<NotificationType> EmptyNotificationsList = new NotificationType[0];
        public static readonly IEnumerable<NotificationType> AllNotificationsList;

        static Notification()
        {
            List<NotificationType> list = new List<NotificationType>();
            foreach (NotificationType type in typeof(NotificationType).GetValues())
            {
                list.Add(type);
            }
            AllNotificationsList = list.ToArray();
        }
    }
}

