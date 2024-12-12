namespace DevExpress.Data.Async
{
    using System;
    using System.Collections;

    public class NotificationExceptionThrown : Command
    {
        private Exception exception;

        public NotificationExceptionThrown(Exception exceptionMessage, params DictionaryEntry[] tags);
        public override void Accept(IAsyncCommandVisitor visitor);

        public Exception Notification { get; }
    }
}

