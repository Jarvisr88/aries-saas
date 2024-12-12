namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public class XfProperties : IEnumerable<XfPropBase>, IEnumerable
    {
        private readonly List<XfPropBase> items = new List<XfPropBase>();

        public void Add(XfPropBase item)
        {
            Guard.ArgumentNotNull(item, "item");
            this.items.Add(item);
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public IEnumerator<XfPropBase> GetEnumerator() => 
            this.items.GetEnumerator();

        public int GetSize()
        {
            int num = 4;
            int count = this.items.Count;
            for (int i = 0; i < count; i++)
            {
                num += this.items[i].GetSize();
            }
            return num;
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.items.GetEnumerator();

        public void Write(BinaryWriter writer)
        {
            Guard.ArgumentNotNull(writer, "writer");
            int count = this.items.Count;
            writer.Write((ushort) 0);
            writer.Write((ushort) count);
            for (int i = 0; i < count; i++)
            {
                this.items[i].Write(writer);
            }
        }

        public int Count =>
            this.items.Count;

        public XfPropBase this[int index] =>
            this.items[index];
    }
}

