namespace DevExpress.Emf
{
    using System;
    using System.Collections.Generic;

    public class EmfPlusClippingStack
    {
        private readonly List<EmfPlusSetClipRecord> records = new List<EmfPlusSetClipRecord>();

        public void Apply(IEmfMetafileBuilder builder)
        {
            foreach (EmfPlusSetClipRecord record in this.records)
            {
                builder.AppendRecord(record);
            }
        }

        public bool Pop()
        {
            int index = this.records.Count - 1;
            bool flag = this.records[index] is EmfPlusSetClipPathRecord;
            this.records.RemoveAt(index);
            return flag;
        }

        public void Push(EmfPlusSetClipRecord record)
        {
            this.records.Insert(this.records.Count, record);
        }
    }
}

