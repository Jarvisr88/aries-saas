namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Security;

    [Serializable]
    public sealed class RecordDragDropData : ISerializable
    {
        private ISerializable[] serializableItems;
        private object[] records;

        public RecordDragDropData(object[] records)
        {
            this.serializableItems = new ISerializable[0];
            this.records = new object[0];
            this.records = records;
            this.serializableItems = this.GetSerializableItems(records);
        }

        public RecordDragDropData(SerializationInfo info, StreamingContext context)
        {
            this.serializableItems = new ISerializable[0];
            this.records = new object[0];
            this.serializableItems = (ISerializable[]) info.GetValue("items", typeof(ISerializable[]));
            this.records = this.serializableItems;
        }

        private ISerializable[] GetSerializableItems(object[] items)
        {
            List<ISerializable> list = new List<ISerializable>();
            foreach (object obj2 in items)
            {
                if (obj2 is ISerializable)
                {
                    list.Add((ISerializable) obj2);
                }
            }
            return list.ToArray();
        }

        [SecurityCritical]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("items", this.serializableItems, typeof(ISerializable[]));
        }

        public object[] Records =>
            this.records;
    }
}

