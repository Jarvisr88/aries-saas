namespace DevExpress.Data.Async
{
    using System;
    using System.Collections;

    public class NotificationInconsistencyDetected : Command
    {
        public bool Handled;
        private Exception notification;

        public NotificationInconsistencyDetected(Exception notificationMessage, params DictionaryEntry[] tags);
        public override void Accept(IAsyncCommandVisitor visitor);

        public Exception Notification { get; }
    }
}

