namespace DevExpress.Office.History
{
    using System;

    public class NotificationIdGenerator
    {
        public const int EmptyId = 0;
        private int lastId = -2147483648;

        public virtual int GenerateId()
        {
            this.lastId++;
            this.lastId++;
            return this.lastId;
        }
    }
}

