namespace DevExpress.Emf
{
    using System;
    using System.IO;

    public class EmfPlusContinuedObject
    {
        private byte[] content;
        private readonly int totalSize;
        private readonly EmfPlusObjectType objectType;
        private int currentIndex;

        public EmfPlusContinuedObject(EmfPlusObjectType objectType, int totalSize, byte[] content)
        {
            this.content = content;
            this.totalSize = totalSize;
            this.objectType = objectType;
            this.currentIndex = content.Length;
            content.CopyTo(this.content, 0);
        }

        public object Append(EmfPlusContinuedObject record)
        {
            if (this.content.Length < this.totalSize)
            {
                byte[] array = new byte[this.totalSize];
                this.content.CopyTo(array, 0);
                this.content = array;
            }
            Array.Copy(record.content, 0, this.content, this.currentIndex, record.currentIndex);
            this.currentIndex += record.currentIndex;
            if (this.currentIndex <= (this.totalSize - 1))
            {
                return this;
            }
            using (EmfPlusReader reader = new EmfPlusReader(new MemoryStream(this.content)))
            {
                return EmfPlusObjectRecord.CreateObject(this.objectType, reader);
            }
        }
    }
}

