namespace DevExpress.Office.History
{
    using System;

    public class EmptyNotificationIdGenerator : NotificationIdGenerator
    {
        public override int GenerateId() => 
            0;
    }
}

